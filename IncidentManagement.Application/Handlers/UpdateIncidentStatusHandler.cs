using IncidentManagement.Application.Commands;
using IncidentManagement.Domain.Enum;
using IncidentManagement.Infrastructure;
using MediatR;

namespace IncidentManagement.Application.Handlers;

public class UpdateIncidentStatusHandler : IRequestHandler<UpdateIncidentStatusCommand>
{
    private readonly IIncidentRepository _repo;

    public UpdateIncidentStatusHandler(IIncidentRepository repo)
    {
        _repo = repo;
    }

    public async Task Handle(UpdateIncidentStatusCommand request, CancellationToken ct)
    {
        var incident = await _repo.GetByIdAsync(request.Id)
            ?? throw new Exception("Incident not found");

        incident.Id = request.Id;
        incident.Title = request.Title;
        incident.Description = request.Description;
        incident.Severity = request.Severity;
        incident.Status = request.Status;
        incident.UpdatedAt = DateTime.UtcNow;

        await _repo.SaveAsync();
    }
}