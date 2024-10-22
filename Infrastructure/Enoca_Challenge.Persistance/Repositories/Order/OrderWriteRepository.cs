using Enoca_Challenge.Application.Abstractions;
using Enoca_Challenge.Application.Dtos;
using Enoca_Challenge.Domain.Entities;
using Enoca_Challenge.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace Enoca_Challenge.Persistance.Repositories
{
    public class OrderWriteRepository : WriteRepository<Order>, IOrderWriteRepository
    {
        readonly private ICarrierReadRepository _carrierReadRepository;

        public OrderWriteRepository(ApplicationDbContext context, ICarrierReadRepository carrierReadRepository) : base(context)
        {
            _carrierReadRepository = carrierReadRepository;
        }

        public async Task<CalculateCostDto> CalculateCarrierCost(decimal orderDesi)
        {
            var carriers = await _context.Carriers
                .Include(c => c.CarrierConfigurations)
                .Where(c => c.DeletedDate == null)
                .ToListAsync();

            if (!carriers.Any())
            {
                throw new Exception("İstenilen türde kargo firması bulunamadı");
            }

            var applicableCarriers = carriers
                .Select(c => new
                {
                    Carrier = c,
                    Configurations = c.CarrierConfigurations
                        .Where(cc => orderDesi >= cc.CarrierMinDesi && orderDesi <= cc.CarrierMaxDesi)
                        .ToList()
                })
                .Where(c => c.Configurations.Any())
                .ToList();

            var lowestCostCarrier = applicableCarriers
               .SelectMany(c => c.Configurations, (c, cc) => new
               {
                   Carrier = c.Carrier,
                   Cost = cc.CarrierCost
               })
               .OrderBy(cost => cost.Cost)
               .FirstOrDefault();

            if (lowestCostCarrier != null)
            {
                return new CalculateCostDto
                {
                    CarrierId = lowestCostCarrier.Carrier.Id,
                    OrderCarrierCost = lowestCostCarrier.Cost
                };
            }

            if (!applicableCarriers.Any())
            {
                var nearestCarrier = carriers
               .Select(c => new
               {
                   Carrier = c,
                   Configurations = c.CarrierConfigurations
                       .Where(x => x.CarrierMinDesi > orderDesi || x.CarrierMaxDesi < orderDesi)
                       .ToList()
               })
               .Where(x => x.Configurations.Any())
               .SelectMany(x => x.Configurations, (x, cc) => new { x.Carrier, Configuration = cc })
               .OrderBy(x => Math.Min(
                   Math.Abs(x.Configuration.CarrierMinDesi - orderDesi),
                   Math.Abs(x.Configuration.CarrierMaxDesi - orderDesi)))
               .FirstOrDefault();

                if (nearestCarrier == null)
                {

                    throw new Exception($"Sipariş desi: {orderDesi} için geçerli taşıyıcı bulunamadı.");
                }

                var baseCost = nearestCarrier.Configuration.CarrierCost;

                if (orderDesi > nearestCarrier.Configuration.CarrierMaxDesi)
                {
                    decimal extraDesi = orderDesi - nearestCarrier.Configuration.CarrierMaxDesi;
                    baseCost += extraDesi * nearestCarrier.Carrier.CarrierPlusDesiCost;
                }

                return new CalculateCostDto
                {
                    CarrierId = nearestCarrier.Carrier.Id,
                    OrderCarrierCost = baseCost
                };

            }

            throw new Exception("Bir hata oluştu");


        }

    }
}
