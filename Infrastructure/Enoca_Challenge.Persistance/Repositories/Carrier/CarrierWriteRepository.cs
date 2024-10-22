using Enoca_Challenge.Application.Abstractions;
using Enoca_Challenge.Application.Features.Carriers.Commands.CreateCarrier;
using Enoca_Challenge.Domain.Entities;
using Enoca_Challenge.Persistance.Context;

namespace Enoca_Challenge.Persistance.Repositories
{
    public class CarrierWriteRepository : WriteRepository<Carrier>, ICarrierWriteRepository
    {
        private readonly IOrderWriteRepository _orderWriteRepository;
        public CarrierWriteRepository(ApplicationDbContext context, IOrderWriteRepository orderWriteRepository) : base(context)
        {
            _orderWriteRepository = orderWriteRepository;
        }

        public async Task<bool> AddCarrier(CreateCarrierQueryRequest request)
        {
            try
            {
                var carrier = new Carrier(request.CarrierName, request.CarrierIsActive, request.CarrierPlusDesiCost);

                await _context.Carriers.AddAsync(carrier);
                await _context.SaveChangesAsync();

                if (request.CarrierConfigurations != null)
                {
                    foreach (var carrierConfiguration in request.CarrierConfigurations)
                    {
                        var configuration = new CarrierConfiguration(
                            carrierConfiguration.CarrierMaxDesi,
                            carrierConfiguration.CarrierMinDesi,
                            carrierConfiguration.CarrierCost,
                            carrier.Id);

                        carrier.CarrierConfigurations.Add(configuration);
                    }
                }

                if (request.Orders != null)
                {
                    foreach (var order in request.Orders)
                    {
                        var carrierCost = await _orderWriteRepository.CalculateCarrierCost(order.OrderDesi);

                        var newOrder = new Order(
                            order.OrderDesi,
                            DateTime.UtcNow,
                            carrierCost.OrderCarrierCost,
                            carrierCost.CarrierId);

                        carrier.Orders.Add(newOrder);
                    }
                }

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Hata: " + ex.Message, ex);
            }
        }
    }
}
