using Application.Interfaces;
using MediatR;
using WebApi.ModelDto.Request;



namespace Application.Features.Product.Commands
{
    public class CreateProductCommand : IRequest<int>
    {
        //✅ Properties you want to pass in the request
        public ProductDto Product { get; set; }


        internal class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public CreateProductCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                //var request = new ProductDto();

                var product = new Domain.Entities.Product();
                product.Name = request.Product.Name;
                product.Description = request.Product.Description;
                product.Rate = request.Product.Rate;

                await  _context.Products.AddAsync(product);
              await  _context.SaveChangesAsync();
                return product.Id;
            }
        }
    }
}
