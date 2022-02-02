using Api.Services.SampleSoap;

namespace Api.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<ISampleSoapRestService, SampleSoapRestService>();

            return services;
        }
    }
}
