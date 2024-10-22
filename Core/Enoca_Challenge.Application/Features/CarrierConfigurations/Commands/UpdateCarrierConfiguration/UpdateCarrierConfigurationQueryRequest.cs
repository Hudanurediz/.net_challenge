using MediatR;

namespace Enoca_Challenge.Application.Features.CarrierConfigurations.Commands.UpdateCarrierConfiguration
{
    public class UpdateCarrierConfigurationQueryRequest:IRequest<UpdateCarrierConfigurationQueryResponse>
    {
        public int Id { get; set; }

        public int? CarrierMaxDesi { get; set; }

        public int? CarrierMinDesi { get; set; }

        public decimal? CarrierCost { get; set; }

        public int? CarrierId { get; set; }
    }
}
