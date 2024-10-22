using MediatR;

namespace Enoca_Challenge.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderQueryRequest:IRequest<CreateOrderQueryResponse>
    {
        public int OrderDesi { get; set; }

        //yorum satirinda bulunan veriler girilen order desi bilgisine göre atanır

        //public DateTime OrderDate { get; set; }

        //public decimal? OrderCarrierCost { get; set; }

        //public int? CarrierId { get; set; }
    }
}
