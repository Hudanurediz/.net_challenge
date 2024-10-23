using Enoca_Challenge.Domain.Entities.Common;

namespace Enoca_Challenge.Domain.Entities
{
    public class Carrier:BaseEntity
    {
        public string CarrierName { get; set; }

        public bool CarrierIsActive { get; set; }

        public int CarrierPlusDesiCost { get; set; }

        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; }

        [JsonIgnore]
        public virtual ICollection<CarrierConfiguration> CarrierConfigurations { get; set; }

        public Carrier(string carrierName, bool carrierIsActive, int carrierPlusDesiCost) : base()
        {
            CarrierName = carrierName;
            CarrierIsActive = carrierIsActive;
            CarrierPlusDesiCost = carrierPlusDesiCost;
            CarrierConfigurations = new HashSet<CarrierConfiguration>();
            Orders = new HashSet<Order>();
        }
    }
}
