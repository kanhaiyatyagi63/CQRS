using CQRS.API.Context;
using CQRS.API.Models;
using MediatR;

namespace CQRS.API.Features.ProductFeatures.Command;

public class CreateProductCommand : IRequest<int>
{
    public string? Name { get; set; }
    public string? Barcode { get; set; }
    public string? Description { get; set; }
    public decimal Rate { get; set; }
    public decimal BuyingPrice { get; set; }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IApplicationContext _applicationContext;

        public CreateProductCommandHandler(IApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<int> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = new Product()
            {
                Barcode = command.Barcode,
                Rate = command.Rate,
                BuyingPrice = command.BuyingPrice,
                Name = command.Name,
                Description = command.Description,
            };
            _applicationContext.Products.Add(product);
            await _applicationContext.SaveChangesAsync();
            return product.Id;
        }
    }
}
