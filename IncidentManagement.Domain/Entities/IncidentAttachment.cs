namespace IncidentManagement.Domain.Entities;

public class IncidentAttachment
{
    public int Id { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string BlobUrl { get; set; } = string.Empty;
}