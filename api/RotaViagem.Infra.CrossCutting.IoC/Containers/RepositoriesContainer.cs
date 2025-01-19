using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RotaViagem.Data.Repositories;
using RotaViagem.Application.Infra.Repositories;

namespace RotaViagem.Infra.CrossCutting.IoC.Containers
{
    public class RepositoriesContainer
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IRotaRepository, RotaRepository>();
        }
    }
}
