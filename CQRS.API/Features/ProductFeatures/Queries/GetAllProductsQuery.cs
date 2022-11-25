using CQRS.API.Context;
using CQRS.API.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRS.API.Features.ProductFeatures.Queries;

#nullable disable
public class GetAllProductsQuery : IRequest<IEnumerable<Product>>
{

    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<Product>>
    {
        private readonly IApplicationContext _applicationContext;

        public GetAllProductsQueryHandler(IApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<IEnumerable<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var productList = await _applicationContext.Products.ToListAsync();
            if (productList == null)
            {
                return null;
            }
            return productList.AsReadOnly();
        }
    }

}
