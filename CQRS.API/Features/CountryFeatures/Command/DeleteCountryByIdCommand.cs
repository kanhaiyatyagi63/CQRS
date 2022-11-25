using CQRS.API.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace CQRS.API.Features.CountryFeatures.Command;

public class DeleteCountryByIdCommand : IRequest<int>
{
    public int Id { get; set;}

    public class DeleteCountryCommandHandler : IRequestHandler<DeleteCountryByIdCommand, int>
    {
        private readonly IApplicationContext _applicationContext;

        public DeleteCountryCommandHandler(IApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<int> Handle(DeleteCountryByIdCommand command, CancellationToken cancellationToken)
        {
            var country = await _applicationContext.Countries.FirstOrDefaultAsync(x => x.Id == command.Id);
            if (country == null) { return default(int); }

            _applicationContext.Countries.Remove(country);
            await _applicationContext.SaveChangesAsync();
            return country.Id;
        }
    }
}
