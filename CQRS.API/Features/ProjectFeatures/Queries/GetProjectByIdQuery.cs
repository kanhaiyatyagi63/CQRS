using CQRS.API.Context;
using CQRS.API.Models.Dtos.ProjectDto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRS.API.Features.ProjectFeatures.Queries;

public record class GetProjectByIdQuery(int id) : IRequest<ProjectViewModel>;

public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectViewModel>
{
    private readonly IApplicationContext _applicationContext;

    public GetProjectByIdQueryHandler(IApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    public async Task<ProjectViewModel?> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var project = await _applicationContext.Projects.Select(x => new ProjectViewModel()
        {
            Author = x.Author,
            Name = x.Name,
            Description = x.Description,
            Id = x.Id,
            Published = x.Published
        }).FirstOrDefaultAsync();
        if (project == null)
        {
            return default;
        }
        return project;
    }
}

