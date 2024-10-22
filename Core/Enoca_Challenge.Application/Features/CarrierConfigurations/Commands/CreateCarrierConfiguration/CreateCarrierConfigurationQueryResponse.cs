using Enoca_Challenge.Application.Dtos;
using Enoca_Challenge.Domain.Entities;

namespace Enoca_Challenge.Application.Features.CarrierConfigurations.Commands
{
    public class CreateCarrierConfigurationQueryResponse:BaseResponseDto
    {
        public CarrierConfiguration CarrierConfiguration { get; set; }

    }
}
