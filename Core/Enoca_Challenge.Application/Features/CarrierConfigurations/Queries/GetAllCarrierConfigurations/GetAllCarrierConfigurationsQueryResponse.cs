using Enoca_Challenge.Application.Dtos;
using Enoca_Challenge.Domain.Entities;

namespace Enoca_Challenge.Application.Features.CarrierConfigurations.Queries.GetAllCarrierConfigurations
{
    public class GetAllCarrierConfigurationsQueryResponse:BaseResponseDto
    {
        public List<CarrierConfiguration> CarrierConfigurations { get; set; }
    }
}
