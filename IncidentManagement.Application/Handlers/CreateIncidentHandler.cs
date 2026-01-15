using MediatR;
using IncidentManagement.Infrastructure;
using IncidentManagement.Domain.Entities;
using IncidentManagement.Domain.Enum;

namespace IncidentManagement.Application.Commands;

public class CreateIncidentHandler : IRequestHandler<CreateIncidentCommand, int>
{
    private readonly IIncidentRepository _incidentRepository;

    public CreateIncidentHandler(IIncidentRepository incidentRepository)
    {
        _incidentRepository = incidentRepository;
    }

    public async Task<int> Handle(CreateIncidentCommand request, CancellationToken cancellationToken)
    {
        // Basic validation. We can use FluentValidation to make this more extensible and readble
        if (string.IsNullOrWhiteSpace(request.Title))
            throw new ArgumentException("Incident title is required.");

        if (string.IsNullOrWhiteSpace(request.Description))
            throw new ArgumentException("Incident description is required.");

        // Create domain entity
        var incident = new Incident
        {
            Title = request.Title.Trim(),
            Description = request.Description.Trim(),
            Severity = request.Severity,
            Status = IncidentStatus.Open,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        // Persist
        await _incidentRepository.AddAsync(incident);
        await _incidentRepository.SaveAsync();

        // Return new incident Id
        return incident.Id;
    }
}
