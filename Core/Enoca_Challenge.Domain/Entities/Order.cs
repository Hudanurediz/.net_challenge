using Enoca_Challenge.Domain.Entities.Common;
using System.Text.Json.Serialization;

namespace Enoca_Challenge.Domain.Entities
{
    public class Order:BaseEntity
    {
        public int OrderDesi {  get; set; }

        public DateTime OrderDate { get; set; }

        public decimal OrderCarrierCost { get; set; }

        public int CarrierId { get; set; }

        [JsonIgnore]
        public virtual Carrier? Carrier { get; set; }

        public Order(int orderDesi, DateTime orderDate, decimal orderCarrierCost, int carrierId) : base()
        {
            OrderDesi = orderDesi;
            OrderDate = orderDate;
            OrderCarrierCost = orderCarrierCost;
            CarrierId = carrierId;
        }
    }
}
