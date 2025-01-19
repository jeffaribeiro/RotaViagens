using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RotaViagem.Infra.CrossCutting.IoC.Containers;

namespace RotaViagem.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            DatabaseContainer.RegisterServices(services, configuration);
            RepositoriesContainer.RegisterServices(services, configuration);
            MediatRContainer.RegisterServices(services, configuration);
            ApplicationContainer.RegisterServices(services, configuration);
        }
    }
}
