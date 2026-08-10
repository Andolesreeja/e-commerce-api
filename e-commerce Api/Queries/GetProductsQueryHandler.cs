using e_commerce_Api.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_Api.Queries.Products.GetProducts
{
    public class GetProductsQueryHandler
        : IRequestHandler<GetProductsQuery, List<ProductResponse>>
    {
        private readonly AppDbContext _context;

        public GetProductsQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductResponse>> Handle(
            GetProductsQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Products
                .Select(product => new ProductResponse
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = product.Quantity
                })
                .ToListAsync(cancellationToken);
        }
    }
}