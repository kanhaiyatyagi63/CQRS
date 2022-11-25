using CQRS.API.Context;
using CQRS.API.Models;
using CQRS.API.Models.Dtos.ProjectDto;
using MediatR;

namespace CQRS.API.Features.ProjectFeatures.Command;

public record class CreateProjectCommand(AddOrEditProjectDto addProjectDto) : IRequest<int>;

public class AddProjectCommandHandler : IRequestHandler<CreateProjectCommand, int>
{
    private readonly IApplicationContext _applicationContext;

    public AddProjectCommandHandler(IApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }
    public async Task<int> Handle(CreateProjectCommand command, CancellationToken cancellationToken)
    {
        var project = new Project()
        {
            Name = command.addProjectDto.Name,
            Description = command.addProjectDto.Description,
            Published = command.addProjectDto.Published,
            Author = command.addProjectDto.Author
        };

        _applicationContext.Projects.Add(project);
        await _applicationContext.SaveChangesAsync();
        return project.Id;
    }
}