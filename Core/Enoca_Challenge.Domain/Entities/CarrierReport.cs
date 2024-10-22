using Enoca_Challenge.Domain.Entities.Common;

namespace Enoca_Challenge.Domain.Entities
{
    public class CarrierReport : BaseEntity
    {
        public int CarrierId { get; set; }             

        public virtual Carrier Carrier { get; set; }   

        public decimal CarrierCost { get; set; }        

        public DateTime CarrierReportDate { get; set; }

        public CarrierReport(int carrierId, decimal carrierCost, DateTime carrierReportDate)
        {
            CarrierId = carrierId;
            CarrierCost = carrierCost;
            CarrierReportDate = carrierReportDate;
        }

    }
}
