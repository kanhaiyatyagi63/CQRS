namespace CQRS.API.Models.Dtos.ProjectDto;

public class AddOrEditProjectDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? Author { get; set; }
    public DateTime Published { get; set; }
}
