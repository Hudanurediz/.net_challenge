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
            var carriers = await GetActiveCarriersAsync();

            if (!carriers.Any())
            {
                throw new Exception("İstenilen türde kargo firması bulunamadı");
            }

            var applicableCarriers = GetApplicableCarriers(carriers, orderDesi);
            var lowestCostCarrier = GetLowestCostCarrier(applicableCarriers);

            if (lowestCostCarrier != null)
            {
                return CreateCostDto(lowestCostCarrier);
            }

            var nearestCarrier = GetNearestCarrier(carriers, orderDesi);
            if (nearestCarrier == null)
            {
                throw new Exception($"Sipariş desi: {orderDesi} için geçerli taşıyıcı bulunamadı.");
            }

            var totalCost = CalculateTotalCost(nearestCarrier, orderDesi);
            return new CalculateCostDto
            {
                CarrierId = nearestCarrier.Carrier.Id,
                OrderCarrierCost = totalCost
            };
        }

        private async Task<List<Carrier>> GetActiveCarriersAsync()
        {
            return await _context.Carriers
                .Include(c => c.CarrierConfigurations)
                .Where(c => c.DeletedDate == null)
                .ToListAsync();
        }

        private List<ApplicableCarrier> GetApplicableCarriers(List<Carrier> carriers, decimal orderDesi)
        {
            return carriers
                .SelectMany(c => c.CarrierConfigurations
                    .Where(cc => orderDesi >= cc.CarrierMinDesi && orderDesi <= cc.CarrierMaxDesi)
                    .Select(cc => new ApplicableCarrier
                    {
                        Carrier = c,
                        Configuration = cc
                    }))
                .ToList();
        }

        private LowestCostCarrier GetLowestCostCarrier(List<ApplicableCarrier> applicableCarriers)
        {
            return applicableCarriers
                .Select(c => new LowestCostCarrier
                {
                    Carrier = c.Carrier,
                    Cost = c.Configuration.CarrierCost
                })
                .OrderBy(cost => cost.Cost)
                .FirstOrDefault();
        }

        private NearestCarrier GetNearestCarrier(List<Carrier> carriers, decimal orderDesi)
        {
            return carriers
                .Select(c => new
                {
                    Carrier = c,
                    Configurations = c.CarrierConfigurations
                        .Where(x => x.CarrierMinDesi > orderDesi || x.CarrierMaxDesi < orderDesi)
                        .ToList()
                })
                .Where(x => x.Configurations.Any())
                .SelectMany(x => x.Configurations, (x, cc) => new NearestCarrier { Carrier = x.Carrier, Configuration = cc })
                .OrderBy(x => Math.Min(
                    Math.Abs(x.Configuration.CarrierMinDesi - orderDesi),
                    Math.Abs(x.Configuration.CarrierMaxDesi - orderDesi)))
                .FirstOrDefault();
        }

        private decimal CalculateTotalCost(NearestCarrier nearestCarrier, decimal orderDesi)
        {
            decimal baseCost = nearestCarrier.Configuration.CarrierCost;

            if (orderDesi > nearestCarrier.Configuration.CarrierMaxDesi)
            {
                decimal extraDesi = orderDesi - nearestCarrier.Configuration.CarrierMaxDesi;
                baseCost += extraDesi * nearestCarrier.Carrier.CarrierPlusDesiCost;
            }

            return baseCost;
        }

        private CalculateCostDto CreateCostDto(LowestCostCarrier lowestCostCarrier)
        {
            return new CalculateCostDto
            {
                CarrierId = lowestCostCarrier.Carrier.Id,
                OrderCarrierCost = lowestCostCarrier.Cost
            };
        }

        private class ApplicableCarrier
        {
            public Carrier Carrier { get; set; }
            public CarrierConfiguration Configuration { get; set; }
        }

        private class LowestCostCarrier
        {
            public Carrier Carrier { get; set; }
            public decimal Cost { get; set; }
        }

        private class NearestCarrier
        {
            public Carrier Carrier { get; set; }
            public CarrierConfiguration Configuration { get; set; }
        }

    }
}
