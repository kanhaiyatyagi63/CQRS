using CQRS.API.Context;
using CQRS.API.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRS.API.Features.CountryFeatures.Queries
{
    public class GetAllCountryQuery : IRequest<IEnumerable<Country>>
    {
        public class GetAllCountryQueryHandler : IRequestHandler<GetAllCountryQuery, IEnumerable<Country>>
        {
            private readonly IApplicationContext _applicationContext;

            public GetAllCountryQueryHandler(IApplicationContext applicationContext)
            {
                _applicationContext = applicationContext;
            }
            public async Task<IEnumerable<Country>> Handle(GetAllCountryQuery request, CancellationToken cancellationToken)
            {
                return await _applicationContext.Countries.ToListAsync();
            }
        }
    }
}
