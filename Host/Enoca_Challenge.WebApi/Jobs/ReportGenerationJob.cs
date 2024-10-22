using Enoca_Challenge.Domain.Entities;
using Enoca_Challenge.Persistance.Context;

namespace Enoca_Challenge.WebApi.Jobs
{
    public class ReportGenerationJob
    {
        protected readonly ApplicationDbContext _context;

        public ReportGenerationJob(ApplicationDbContext context)
        {
            _context = context;
        }
        public void ReportGenerationHourlyJob()
        {
            var carrierReports = _context.Orders
                .GroupBy(o => new
                {
                    o.CarrierId,
                    OrderDateTime = new DateTime(o.OrderDate.Year, o.OrderDate.Month, o.OrderDate.Day, o.OrderDate.Hour, 0, 0)
                }).Select(g => new
                {
                    CarrierId = g.Key.CarrierId,
                    OrderDate = g.Key.OrderDateTime,
                    TotalCarrierCost = g.Sum(o => o.OrderCarrierCost)
                }).ToList();

            foreach (var report in carrierReports)
            {
                var carrierReport = new CarrierReport(
                    carrierId: report.CarrierId,
                    carrierCost: report.TotalCarrierCost,
                    carrierReportDate: report.OrderDate
                );

                if (carrierReport.CarrierId == 0)
                {
                    throw new Exception("Taşıyıcıya it veri bulunmuyor.");
                }

                _context.CarrierReports.Add(carrierReport);
            }

            _context.SaveChanges();

        }

    }
}
