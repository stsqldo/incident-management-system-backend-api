using MediatR;
using IncidentManagement.Domain.Entities;

namespace IncidentManagement.Application.Queries;

public record GetIncidentsQuery() : IRequest<List<Incident>>;