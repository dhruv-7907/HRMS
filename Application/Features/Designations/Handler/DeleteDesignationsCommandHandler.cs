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
    public class DeleteDesignationsCommandHandler : IRequestHandler <DeleteDesignationsCommand, int>
    {
        private readonly IDesignations _designationsRepository;

        public DeleteDesignationsCommandHandler(IDesignations designationsRepository)
        {
            _designationsRepository = designationsRepository;
        }

        public async Task<int> Handle(DeleteDesignationsCommand request , CancellationToken cancellationToken)
        {
            var result =  await _designationsRepository.DeleteDesignations(request.Id);
            return  result;
        }
    }
}
