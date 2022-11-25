using CQRS.API.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRS.API.Features.ProjectFeatures.Command;

public record class DeleteProjectByIdCommand(int id) : IRequest<int>;

public class DeleteProjectByIdCommandHandler : IRequestHandler<DeleteProjectByIdCommand, int>
{
    private readonly IApplicationContext _applicationContext;

    public DeleteProjectByIdCommandHandler(IApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    public async Task<int> Handle(DeleteProjectByIdCommand request, CancellationToken cancellationToken)
    {
        var project = await _applicationContext.Projects.FirstOrDefaultAsync(x => x.Id == request.id);

        if (project == null)
        {
            return default(int);
        }
        _applicationContext.Projects.Remove(project);
        await _applicationContext.SaveChangesAsync();
        return project.Id;
    }
}
