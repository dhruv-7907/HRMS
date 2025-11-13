using Application.Features.Designations.Queries;
using Application.Interfaces;
using Application.ModelDto.Responce;
using MediatR;
using System.Reflection.Metadata;



namespace Application.Features.Designations.Handler
{
    public class GetByIdDesignationsCommandHandler : IRequestHandler<GetByIdDesignationsQueries, DesignationsDto>
    {
        private readonly IDesignations _designationsRepository;

        public GetByIdDesignationsCommandHandler(IDesignations designationsRepository)
        {
            _designationsRepository = designationsRepository;
        }

        public async Task<DesignationsDto> Handle(GetByIdDesignationsQueries request, CancellationToken cancellationToken)
        {
            var result = await _designationsRepository.GetDesignationsById(request.Id);
            return result;
        }


    }
}
