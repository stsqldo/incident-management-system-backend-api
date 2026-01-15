using MediatR;
using Microsoft.AspNetCore.Http;

namespace IncidentManagement.Application.Commands;

public record UploadAttachmentCommand( int IncidentId, IFormFile File) : IRequest;