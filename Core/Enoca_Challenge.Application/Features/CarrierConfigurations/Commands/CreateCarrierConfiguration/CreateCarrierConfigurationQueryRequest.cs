using Enoca_Challenge.Domain.Entities;
using MediatR;

namespace Enoca_Challenge.Application.Features.CarrierConfigurations.Commands.CreateCarrierConfiguration
{
    public class CreateCarrierConfigurationQueryRequest:IRequest<CreateCarrierConfigurationQueryResponse>
    {
        public int CarrierMaxDesi { get; set; }

        public int CarrierMinDesi { get; set; }

        public decimal CarrierCost { get; set; }

        public int CarrierId { get; set; }
    }
}
