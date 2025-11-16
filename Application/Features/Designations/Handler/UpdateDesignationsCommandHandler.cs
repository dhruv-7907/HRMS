using Application.Features.Designations.Commands;
using Application.Interfaces;
using MediatR;


namespace Application.Features.Designations.Handler
{
    public class UpdateDesignationsCommandHandler: IRequestHandler<UpdateDesignationsCommand, int>
    {
            private readonly IDesignations _designationsRepository;

            public UpdateDesignationsCommandHandler(IDesignations designationsRepository)
            {
                _designationsRepository = designationsRepository;
            }

            public async Task<int> Handle(UpdateDesignationsCommand request, CancellationToken cancellationToken)
            {
                // Pass DTO to repository for data saving
                var result = await _designationsRepository.UpdateDesignations(request.Designation);
                return result;
            }
        
    }
}
