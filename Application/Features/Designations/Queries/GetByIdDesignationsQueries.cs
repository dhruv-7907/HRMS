using Application.ModelDto.Responce;
using MediatR;


namespace Application.Features.Designations.Queries
{
    public record  GetByIdDesignationsQueries(int Id):IRequest<DesignationsDto>;
}
