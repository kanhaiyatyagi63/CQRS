using CQRS.API.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRS.API.Features.ProductFeatures.Command;

public class UpdateProductCommand : IRequest<int>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Barcode { get; set; }
    public string? Description { get; set; }
    public decimal BuyingPrice { get; set; }
    public decimal Rate { get; set; }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, int>
    {
        private readonly IApplicationContext _applicationContext;

        public UpdateProductCommandHandler(IApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<int> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await _applicationContext.Products.FirstOrDefaultAsync(x => x.Id == command.Id);
            if (product == null)
            {
                return default;
            }
            product.Name = command.Name;
            product.Barcode = command.Barcode;
            product.Description = command.Description;
            product.Rate = command.Rate;
            product.BuyingPrice = command.BuyingPrice;

            await _applicationContext.SaveChangesAsync();
            return product.Id;
        }
    }
}
