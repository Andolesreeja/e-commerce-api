using MediatR;

namespace e_commerce_Api.Commands.Products.CreateProduct
{
    public class CreateProductCommand : IRequest<ProductResponse>
    {
        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }

    public class ProductResponse
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}