namespace Api.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<ISampleSoapRestService, SampleSoapRestService>();
        services.AddScoped<IEnerjisaSoapRestService, EnerjisaSoapRestService>();

        return services;
    }
}
