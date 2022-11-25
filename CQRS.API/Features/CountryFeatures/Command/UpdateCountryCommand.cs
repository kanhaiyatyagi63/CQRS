using CQRS.API.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRS.API.Features.CountryFeatures.Command;

public class UpdateCountryCommand : IRequest<int>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public class UpdateCountryCommandHandler : IRequestHandler<UpdateCountryCommand, int>
    {
        private readonly IApplicationContext _applicationContext;

        public UpdateCountryCommandHandler(IApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        public async Task<int> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
        {
            var country = await _applicationContext.Countries.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (country == null) return default;

            country.Name = request.Name;

            await _applicationContext.SaveChangesAsync();
            return country.Id;
        }
    }
}
