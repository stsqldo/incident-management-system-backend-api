using IncidentManagement.Domain.Enum;

namespace IncidentManagement.Domain.DTOs;

public class UpdateIncidentStatusDto
{
    public int Id { get; set; }
    public IncidentStatus Status { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Severity Severity { get; set; }
}

