using Application.Features.Product.Commands;
using Application.Features.Product.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductsController(IMediator mediator)
        {
                _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
        {
           var result = await _mediator.Send(new GetAllProductQueries(), cancellationToken);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductsById(int id,CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetProductByIdQueries { Id = id });
            return Ok(result);
        }
        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProducts(CreateProductCommand createProduct, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(createProduct, cancellationToken);
            return Ok(result);
        }
        [HttpPost("UpdateProduct")]
        public async Task<IActionResult> UpdateProducts(UpdateProductCommand updateProduct, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(updateProduct, cancellationToken);
            return Ok(result);
        }
        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProducts(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteProductCommand { Id = id}, cancellationToken);
            return Ok(result);
        }
    }
}
