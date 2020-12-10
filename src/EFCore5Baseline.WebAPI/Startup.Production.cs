using EFCore5Baseline.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace EFCore5Baseline.WebAPI
{
    public partial class Startup
    {
        public void ConfigureProductionServices(IServiceCollection services)
        {
            ConfigureServices(services);
            services.AddMySqlDatabaseContext(Configuration);
        }

        public void ConfigureProduction(IApplicationBuilder app)
        {
            app.UseSqlDatabaseMigration();
            Configure(app);
        }
    }
}