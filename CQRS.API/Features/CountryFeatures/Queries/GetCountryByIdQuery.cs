using CQRS.API.Context;
using CQRS.API.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRS.API.Features.CountryFeatures.Queries;

public class GetCountryByIdQuery : IRequest<Country>
{
    public int Id { get; set; }
    public class GetCountryByIdQueryHandler : IRequestHandler<GetCountryByIdQuery, Country>
    {
        private readonly IApplicationContext _applicationContext;

        public GetCountryByIdQueryHandler(IApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<Country> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
        {
            return await _applicationContext.Countries.FirstOrDefaultAsync(x => x.Id == request.Id);
        }
    }
}
