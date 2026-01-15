using IncidentManagement.Domain.Enum;

namespace IncidentManagement.Domain.DTOs;

public class CreateIncidentDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Severity Severity { get; set; }
}

