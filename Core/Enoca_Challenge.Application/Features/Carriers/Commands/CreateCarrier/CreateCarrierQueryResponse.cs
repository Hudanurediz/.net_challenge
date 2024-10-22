using Enoca_Challenge.Application.Dtos;
using Enoca_Challenge.Domain.Entities;

namespace Enoca_Challenge.Application.Features.Carriers.Commands.CreateCarrier
{
    public class CreateCarrierQueryResponse:BaseResponseDto
    {
        public Carrier Carrier { get; set; }
    }
}
