using Microsoft.Extensions.DependencyInjection;
using UserDataService.Grpc;


namespace testbot.sdk
{
    public static class ServiceCollectionExtension
    {
        public static void AddGrpcSdk(this IServiceCollection services)
        {
            services.AddGrpcClient<Greeter.GreeterClient>(client =>
            {
                client.Address = new Uri("https://localhost:7272");
            });

            services.AddScoped<IGreeterGrpcService, GreeterGrpcService>();
        }
    }
}
