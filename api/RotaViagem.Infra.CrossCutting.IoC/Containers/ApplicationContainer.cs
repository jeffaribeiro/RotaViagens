using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RotaViagem.Application.Infra.Notification;

namespace RotaViagem.Infra.CrossCutting.IoC.Containers
{
    public class ApplicationContainer
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<INotificador, Notificador>();
        }
    }
}
