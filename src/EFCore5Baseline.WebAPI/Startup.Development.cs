using EFCore5Baseline.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace EFCore5Baseline.WebAPI
{
    public partial class Startup
    {
        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            ConfigureServices(services);
            services.AddSQLiteDatabaseContext(Configuration);
        }

        public void ConfigureDevelopment(IApplicationBuilder app)
        {
            app.UseSqlDatabaseMigration();

            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EFCore5Baseline.WebAPI v1"));

            Configure(app);
        }
    }
}
