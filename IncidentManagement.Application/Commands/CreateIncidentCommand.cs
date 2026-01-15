using MediatR;
using Microsoft.AspNetCore.Http;
using IncidentManagement.Domain.Enum;

namespace IncidentManagement.Application.Commands;

public record CreateIncidentCommand(string Title, string Description, Severity Severity) : IRequest<int>;