using System;
using EFCore5Baseline.Common;
using EFCore5Baseline.Core.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace EFCore5Baseline.Core
{
    public static class EFCore5BaselineModule
    {
        /// <summary>
        /// This method will register all the supported functions for the repositories.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/>service-collection to extend</param>
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            Guard.IsNotNull(services);
            services.AddTransient<IGenericRepository, GenericRepository>();
            return services;
        }


        /// <summary>
        /// This will configure the MySQL database connection.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" />object for registration</param>
        /// <param name="configuration">The <see cref="IConfiguration" />object for registration</param>
        public static IServiceCollection AddMySqlDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            Console.WriteLine("- Database: MySql (remote)");
            var connectionString = BuildConnectionString(configuration);
            services.AddDbContext<EFCore5BaselineContext>(options =>
            {
                options.UseMySQL(connectionString, mysqlOptions =>
                {
                    mysqlOptions.CommandTimeout(5);
                });
            }); // Default dependency injection is scope, which can cause errors with threading if async calls were not awaited !

            return services;
        }

        /// <summary>
        ///     This will configure the SQLite database connection.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" />object for registration</param>
        /// <param name="configuration">The <see cref="IConfiguration" />object for registration</param>
        public static IServiceCollection AddSQLiteDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            Console.WriteLine("- Database: SQLite (local)");
            var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();
            connection.EnableExtensions(true);
            services.AddDbContext<EFCore5BaselineContext>(options => options.UseSqlite(connection));
            return services;
        }

        /// <summary>
        /// This will migrate the SQL database to the latest version.
        /// Therefore a migration must be created manually by running
        /// - initially: Add-Migration InitialCreate
        /// - after every change: Add-Migration &lt;yyyy-mm-dd_migration-name&gt;
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder" /> object for registration.</param>
        public static void UseSqlDatabaseMigration(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<EFCore5BaselineContext>();
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                var logger = app.ApplicationServices.GetRequiredService<ILogger<DbContext>>();
                logger.LogError(ex, "An error occurred and the DB migration failed");
                throw ex;
            }
        }

        /// <summary>
        /// This will delete the SQL database and create a new one.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder" /> object for registration.</param>
        public static void UseSqlDatabaseRecreation(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<EFCore5BaselineContext>();
                if (!context.Database.EnsureCreated())
                {
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();
                }
            }
            catch (Exception ex)
            {
                var logger = app.ApplicationServices.GetRequiredService<ILogger<DbContext>>();
                logger.LogError(ex, "An error occured and the DB migration failed");
                throw ex;
            }
        }

        private static string BuildConnectionString(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MySQLConnection");
            Console.WriteLine("- MySQL Connection string: " + connectionString);
            var dbUser = configuration.GetValue<string>("Database:User");
            var dbPassword = configuration.GetValue<string>("Database:Password");

            return connectionString
                .Replace("{DB_USER}", dbUser)
                .Replace("{DB_PASSWORD}", dbPassword);
        }
    }


}