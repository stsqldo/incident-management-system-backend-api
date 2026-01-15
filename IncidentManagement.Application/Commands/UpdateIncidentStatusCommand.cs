using MediatR;
using IncidentManagement.Domain.Enum;

namespace IncidentManagement.Application.Commands;

public record UpdateIncidentStatusCommand(int Id, string Title, string Description, Severity Severity, IncidentStatus Status) : IRequest;