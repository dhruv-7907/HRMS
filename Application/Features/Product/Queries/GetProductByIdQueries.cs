using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Product.Queries
{
    public class GetProductByIdQueries : IRequest<Domain.Entities.Product>
    {
        public int Id { get; set; }
        internal class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQueries,Domain.Entities.Product>
        {
            private readonly IApplicationDbContext _context;

            public GetProductByIdQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<Domain.Entities.Product> Handle(GetProductByIdQueries request, CancellationToken cancellationToken)
            {
             var result =  await _context.Products.Where(x=> x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
                return result;
            }
        }
    }

}
