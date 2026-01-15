using Microsoft.AspNetCore.Mvc;
using MediatR;
using IncidentManagement.Application.Commands;
using IncidentManagement.Application.Queries;
using IncidentManagement.Domain.DTOs;

namespace IncidentManagement.Api;

[ApiController]
[Route("api/incidents")]
public class IncidentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public IncidentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>Get all incidents</summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _mediator.Send(new GetIncidentsQuery()));

    /// <summary>Create a new incident</summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateIncidentDto dto)
    {
        var command = new CreateIncidentCommand(
            dto.Title,
            dto.Description,
            dto.Severity);

        var id = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetAll), new { id }, new { id });
    }

    /// <summary>Update incident status</summary>
    [HttpPut("{id:int}/status")]
    public async Task<IActionResult> UpdateStatus([FromBody] UpdateIncidentStatusDto dto)
    {
        await _mediator.Send(new UpdateIncidentStatusCommand(dto.Id, dto.Title, dto.Description, dto.Severity, dto.Status));
        return NoContent();
    }

    /// <summary>Upload attachment for incident</summary>
    [HttpPost("{id:int}/attachments")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadAttachment(int id, IFormFile file)
    {
        await _mediator.Send(new UploadAttachmentCommand(id, file));
        return NoContent();
    }

}