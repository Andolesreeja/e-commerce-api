using e_commerce_Api.Commands.Products.CreateProduct;
using e_commerce_Api.Queries.Products.GetProducts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var query = new GetProductsQuery();

            var products = await _mediator.Send(query);

            return Ok(products);
        }

        // POST: api/Products
        [HttpPost]
        public async Task<IActionResult> CreateProduct(
            CreateProductCommand command)
        {
            var product = await _mediator.Send(command);

            return Ok(product);
        }
    }
}