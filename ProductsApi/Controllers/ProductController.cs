using Application.UseCases.Products.AddProduct;
using Application.UseCases.Products.GetProductById;
using Application.UseCases.Products.GetProducts;
using Application.UseCases.Products.GetProductsByColour;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProductsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductsController() : ControllerBase
    {
        [HttpPost]
        public async Task<Unit> CreateProduct([FromBody] AddProductCommand command, 
                                              [FromServices] ISender sender)
        {            
            return await sender.Send(command);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromServices] ISender sender)
        {            
            var products = await sender.Send(new GetProductsQuery());
            return Ok(products);
        }

        [HttpGet("colour/{colour}")]
        public async Task<IActionResult> GetProductsByColor(string colour,
                                                            [FromServices] ISender sender)
        {            
            var query = new GetProductsByColourQuery { Colour = colour };
            var products = await sender.Send(query);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id,
                                                       [FromServices] ISender sender)
        {            
            var query = new GetProductByIdQuery { Id = id };
            var product = await sender.Send(query);            
            return Ok(product);
        }
    }
}
