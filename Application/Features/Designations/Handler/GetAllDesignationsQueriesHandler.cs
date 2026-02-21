using Application.Features.Designations.Queries;
using Application.Interfaces;
using Application.ModelDto.Responce;
using Domain.Common;
using MediatR;

namespace Application.Features.Designations.Handler
{
    public class GetAllDesignationsQueriesHandler : IRequestHandler<GetAllDesignationsQueries, ApiResponse<PagedResponse<DesignationsDto>>>
    {
        private readonly IDesignations _designationsRepository;

        public GetAllDesignationsQueriesHandler(IDesignations designationsRepository)
        {
            _designationsRepository = designationsRepository;
        }

        public async Task<ApiResponse<PagedResponse<DesignationsDto>>> Handle(GetAllDesignationsQueries request, CancellationToken cancellationToken)
        {
            var result = await _designationsRepository.GetAllDesignations(request.PaginationParams);
            return result;
        }
    }

}
