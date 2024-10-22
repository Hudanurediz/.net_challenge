using MediatR;

namespace Enoca_Challenge.Application.Features.CarrierConfigurations.Commands.DeleteCarrierConfiguration
{
    public class DeleteCarrierConfigurationQueryRequest:IRequest<DeleteCarrierConfigurationQueryResponse>
    {
        public int Id { get; set; }
    }
}
