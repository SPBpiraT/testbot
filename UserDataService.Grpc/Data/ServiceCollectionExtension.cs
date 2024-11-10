using Microsoft.EntityFrameworkCore;

namespace UserDataService.Grpc.Data
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            var dbName = Environment.GetEnvironmentVariable("DB_NAME");
            var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");

            var connectionString = $"Data Source={dbHost};Initial Catalog={dbName};User ID=sa;Password={dbPassword}";

            services.AddDbContext<UserDataContext>(options =>
            {
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Migrations"));
            });

            services.AddScoped(provider =>
                provider.GetService<UserDataContext>());

            return services;
        }
    }
}
