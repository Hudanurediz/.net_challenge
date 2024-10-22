using Enoca_Challenge.Application.Dtos;
using Enoca_Challenge.Domain.Entities;

namespace Enoca_Challenge.Application.Features.CarrierConfigurations.Commands.UpdateCarrierConfiguration
{
    public class UpdateCarrierConfigurationQueryResponse:BaseResponseDto
    {
        public CarrierConfiguration CarrierConfiguration { get; set; }

    }
}
