using MediatR;

namespace e_commerce_Api.Queries.Products.GetProducts
{
    public class GetProductsQuery : IRequest<List<ProductResponse>>
    {
    }

    public class ProductResponse
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}