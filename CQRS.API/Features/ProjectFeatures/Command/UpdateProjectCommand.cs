using CQRS.API.Context;
using CQRS.API.Models.Dtos.ProjectDto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRS.API.Features.ProjectFeatures.Command
{
    public record class UpdateProjectCommand(AddOrEditProjectDto editProjectDto) : IRequest<int>;

    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, int>
    {
        private readonly IApplicationContext _applicationContext;

        public UpdateProjectCommandHandler(IApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<int> Handle(UpdateProjectCommand command, CancellationToken cancellationToken)
        {
            var project = await _applicationContext.Projects.FirstOrDefaultAsync(x => x.Id == command.editProjectDto.Id);
            if (project == null)
            {
                return await Task.FromResult(0);
            }

            project.Published = command.editProjectDto.Published;
            project.Author = command.editProjectDto.Author;
            project.Name = command.editProjectDto.Name;
            project.Description = command.editProjectDto.Description;

            await _applicationContext.SaveChangesAsync();
            return project.Id;
        }
    }
}
