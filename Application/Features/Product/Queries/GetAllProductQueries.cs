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
    public class GetAllProductQueries : IRequest<IEnumerable<Domain.Entities.Product>>
    {
        internal class GetAllproductQueryHandler : IRequestHandler<GetAllProductQueries, IEnumerable<Domain.Entities.Product>>
        {
            private readonly IApplicationDbContext _context;

            public GetAllproductQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<Domain.Entities.Product>> Handle(GetAllProductQueries request, CancellationToken cancellationToken)
            {
             var result =  await _context.Products.ToListAsync(cancellationToken);
                return result;
            }
        }
    }

}
