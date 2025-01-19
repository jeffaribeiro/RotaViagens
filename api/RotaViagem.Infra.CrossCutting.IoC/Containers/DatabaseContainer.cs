using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Common;
using System.Data.SqlClient;
using RotaViagem.Data.DatabaseSession;
using RotaViagem.Application.Infra.DatabaseSession;

namespace RotaViagem.Infra.CrossCutting.IoC.Containers
{
    public class DatabaseContainer
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<DbConnection>(provider =>
            {
                return new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IDBSession, DBSession>();
            services.AddScoped<IUoW, UoW>();
        }
    }
}
