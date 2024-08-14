namespace Api.Services;

public static class DI
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<InstitutionIntegrationOption>(configuration.GetSection(nameof(InstitutionIntegrationOption)));

        services.AddScoped<ISampleSoapRestService, SampleSoapRestService>();
        services.AddScoped<IEnerjisaSoapRestService, EnerjisaSoapRestService>();
        services.AddScoped<IInstitutionIntegrationService, InstitutionIntegrationService>();

        return services;
    }
}
