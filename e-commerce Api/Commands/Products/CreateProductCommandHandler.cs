using e_commerce_Api.Data;
using MediatR;

namespace e_commerce_Api.Commands.Products.CreateProduct
{
    public class CreateProductCommandHandler
        : IRequestHandler<CreateProductCommand, ProductResponse>
    {
        private readonly AppDbContext _context;

        public CreateProductCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ProductResponse> Handle(
            CreateProductCommand request,
            CancellationToken cancellationToken)
        {
            var product = new Models.Product
            {
                Name = request.Name,
                Price = request.Price,
                Quantity = request.Quantity
            };

            _context.Products.Add(product);

            await _context.SaveChangesAsync(cancellationToken);

            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity
            };
        }
    }
}