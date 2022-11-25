using CQRS.API.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRS.API.Features.ProductFeatures.Command;

public class DeleteProductByIdCommand : IRequest<int>
{

    public int Id { get; set; }

    public class DeleteProductByIdCommandHandler : IRequestHandler<DeleteProductByIdCommand, int>
    {
        private readonly IApplicationContext _applicationContext;

        public DeleteProductByIdCommandHandler(IApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        public async Task<int> Handle(DeleteProductByIdCommand command, CancellationToken cancellationToken)
        {
            var product = await _applicationContext.Products.FirstOrDefaultAsync(x => x.Id == command.Id);
            if (product == null)
            {
                return default;
            }
            _applicationContext.Products.Remove(product);
            await _applicationContext.SaveChangesAsync();
            return product.Id;
        }
    }
}
