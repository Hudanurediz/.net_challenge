using Enoca_Challenge.Domain.Entities.Common;

namespace Enoca_Challenge.Domain.Entities
{
    public class CarrierConfiguration:BaseEntity
    {
        public int CarrierMaxDesi { get; set; }

        public int CarrierMinDesi { get; set; }

        public decimal CarrierCost { get; set; }

        public int CarrierId { get; set; }

        [JsonIgnore]
        public virtual Carrier? Carrier { get; set; }

        public CarrierConfiguration(int carrierMaxDesi, int carrierMinDesi, decimal carrierCost, int carrierId) : base()
        {
            CarrierMaxDesi = carrierMaxDesi;
            CarrierMinDesi = carrierMinDesi;
            CarrierCost = carrierCost;
            CarrierId = carrierId;
        }
    }
}
