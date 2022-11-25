namespace CQRS.API.Models;

public class Project
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? Author { get; set; }
    public DateTime Published { get; set; }
}
