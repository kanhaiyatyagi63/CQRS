using CQRS.API.Context;
using CQRS.API.Models.Dtos.ProjectDto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRS.API.Features.ProjectFeatures.Queries;

public record class GetAllProjectByQuery : IRequest<IEnumerable<ProjectViewModel>>;

public class GetAllProjectByQueryHandler : IRequestHandler<GetAllProjectByQuery, IEnumerable<ProjectViewModel>>
{
    private readonly IApplicationContext _applicationContext;

    public GetAllProjectByQueryHandler(IApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    public async Task<IEnumerable<ProjectViewModel>> Handle(GetAllProjectByQuery request, CancellationToken cancellationToken)
    {
        return await _applicationContext.Projects.Select(x => new ProjectViewModel()
        {
            Author= x.Author,
            Name= x.Name,
            Description= x.Description,
            Id= x.Id,
            Published =x.Published
        }).ToListAsync();
    }
}
