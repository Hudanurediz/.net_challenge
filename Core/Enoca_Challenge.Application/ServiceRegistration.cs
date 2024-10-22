using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Enoca_Challenge.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationService(this IServiceCollection collection)
        {
            collection.AddMediatR(typeof(ServiceRegistration));
        }
    }
}
