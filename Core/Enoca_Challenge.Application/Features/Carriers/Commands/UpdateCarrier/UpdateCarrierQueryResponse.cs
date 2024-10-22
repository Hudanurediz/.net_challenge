using Enoca_Challenge.Application.Dtos;
using Enoca_Challenge.Domain.Entities;

namespace Enoca_Challenge.Application.Features.Carriers.Commands.UpdateCarrier
{
    public class UpdateCarrierQueryResponse:BaseResponseDto
    {
        public Carrier Carrier { get; set; }
    }
}
