using CQRS.API.Context;
using CQRS.API.Models;
using MediatR;

namespace CQRS.API.Features.CountryFeatures.Command;

public class CreateCountryCommand : IRequest<int>
{
    public string Name { get; set; }

    public class CreateCountryCommandHandler : IRequestHandler<CreateCountryCommand, int>
    {
        private readonly IApplicationContext _applicationContext;

        public CreateCountryCommandHandler(IApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        public async Task<int> Handle(CreateCountryCommand command, CancellationToken cancellationToken)
        {
            var country = new Country()
            {
                Name= command.Name,
            };

            await _applicationContext.Countries.AddAsync(country);
            await _applicationContext.SaveChangesAsync();
            return country.Id;
        }
    }
}
