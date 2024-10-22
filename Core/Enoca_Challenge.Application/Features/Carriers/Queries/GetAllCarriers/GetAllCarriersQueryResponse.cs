using Enoca_Challenge.Application.Dtos;
using Enoca_Challenge.Domain.Entities;

namespace Enoca_Challenge.Application.Features.Carriers.Queries.GetAllCarriers
{
    public class GetAllCarriersQueryResponse:BaseResponseDto
    {
        public List<Carrier> Carriers { get; set; }
    }
}
