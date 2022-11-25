using CQRS.API.Context;
using CQRS.API.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRS.API.Features.ProductFeatures.Queries;

public class GetProductByIdQuery : IRequest<Product>
{
    public int Id { get; set; }

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
    {
        private readonly IApplicationContext _applicationContext;

        public GetProductByIdQueryHandler(IApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = _applicationContext.Products.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (product == null)
                return null;
            return product;
        }
    }
}
