using MediatR;

namespace Enoca_Challenge.Application.Features.Carriers.Commands.DeleteCarrier
{
    public class DeleteCarrierQueryRequest:IRequest<DeleteCarrierQueryResponse>
    {
        public int Id { get; set; }
    }
}
