using IncidentManagement.Application.Commands;
using IncidentManagement.Infrastructure;
using IncidentManagement.Domain.Entities;
using MediatR;

namespace IncidentManagement.Application.Handlers;

public class UploadAttachmentHandler : IRequestHandler<UploadAttachmentCommand>
{
    private readonly IIncidentRepository _incidentRepository;
    private readonly IBlobStorageService _blobStorageService;

    public UploadAttachmentHandler(IIncidentRepository incidentRepository,IBlobStorageService blobStorageService)
    {
        _incidentRepository = incidentRepository;
        _blobStorageService = blobStorageService;
    }

    public async Task Handle(UploadAttachmentCommand request, CancellationToken cancellationToken)
    {
        // Validate file
        if (request.File == null || request.File.Length == 0)
            throw new ArgumentException("Attachment file is required.");

        // Load incident
        var incident = await _incidentRepository.GetByIdAsync(request.IncidentId);
        if (incident == null)
            throw new KeyNotFoundException($"Incident {request.IncidentId} not found.");

        // Upload to Blob Storage
        var blobUrl = await _blobStorageService.UploadAsync(
            request.File.OpenReadStream(),
            request.File.FileName);

        // Save metadata in domain
        incident.Attachments.Add(new IncidentAttachment
        {
            FileName = request.File.FileName,
            BlobUrl = blobUrl
        });

        incident.UpdatedAt = DateTime.UtcNow;

        // Persist changes
        await _incidentRepository.SaveAsync();
    }
}
