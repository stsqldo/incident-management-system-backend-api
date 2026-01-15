using MediatR;
using IncidentManagement.Domain.Entities;
using IncidentManagement.Application.Queries;
using IncidentManagement.Infrastructure;

namespace IncidentManagement.Application.Handlers;

public class GetIncidentsHandler : IRequestHandler<GetIncidentsQuery, List<Incident>>
{
    private readonly IIncidentRepository _repo;

    public GetIncidentsHandler(IIncidentRepository repo)
    {
        _repo = repo;
    }

    public Task<List<Incident>> Handle(GetIncidentsQuery request, CancellationToken ct)
        => _repo.GetAllAsync();
}