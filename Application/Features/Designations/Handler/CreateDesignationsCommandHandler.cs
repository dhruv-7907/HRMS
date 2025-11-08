
using Application.Features.Designations.Commands;
using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Designations.Handler
{
    public class CreateDesignationsCommandHandler : IRequestHandler<CreateDesignationsCommand, int>
    {
        private readonly IDesignations _designationsRepository;

        public CreateDesignationsCommandHandler(IDesignations designationsRepository)
        {
            _designationsRepository = designationsRepository;
        }

        public async Task<int> Handle(CreateDesignationsCommand request, CancellationToken cancellationToken)
        {
            // Pass DTO to repository for data saving
            var result = await _designationsRepository.CreateDesignations(request.Designation);
            return result;
        }
    }
}
