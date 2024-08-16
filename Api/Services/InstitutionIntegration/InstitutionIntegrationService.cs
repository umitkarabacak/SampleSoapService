namespace Api.Services.SampleSoap;

public class InstitutionIntegrationService : IInstitutionIntegrationService
{
    private readonly EndpointAddress endpointAddress;
    private readonly BasicHttpBinding basicHttpBinding;
    private readonly InstitutionIntegrationOption institutionIntegrationOption;
    private readonly string serviceUrl;

    public InstitutionIntegrationService(IOptions<InstitutionIntegrationOption> options)
    {
        institutionIntegrationOption = options.Value;
        serviceUrl = institutionIntegrationOption.BaseUrl;

        endpointAddress = new EndpointAddress(serviceUrl);

        basicHttpBinding = new BasicHttpBinding(
            endpointAddress.Uri.Scheme.Equals("http", StringComparison.OrdinalIgnoreCase)
            ? BasicHttpSecurityMode.None
            : BasicHttpSecurityMode.Transport)
        {
            OpenTimeout = TimeSpan.MaxValue,
            CloseTimeout = TimeSpan.MaxValue,
            ReceiveTimeout = TimeSpan.MaxValue,
            SendTimeout = TimeSpan.MaxValue,
        };
    }

    public async Task<BaseResponseOfDebtQueryResponse> GetDebtsAsync()
    {
        var client = await GetInstanceAsync();

        var debtRequest = new DebtQueryRequest();

        var response = await client.GetDebtsAsync(debtRequest);

        return response;
    }

    public async Task GetTestWithLocal()
    {
        var client = await GetLocalInstanceAsync();

        var request = new DebtServiceLocal.DebtPaymentRequest();

        var response = await client.DebtPaymentAsync(request, false);

    }


    private async Task<DebtServiceClient> GetInstanceAsync()
    {
        var client = new DebtServiceClient(basicHttpBinding, endpointAddress);

        client.ClientCredentials.UserName.UserName = institutionIntegrationOption.UserName;
        client.ClientCredentials.UserName.Password = institutionIntegrationOption.Password;

        await Console.Out.WriteLineAsync(System.Text.Json.JsonSerializer.Serialize(client.ClientCredentials));

        return client;
    }

    private async Task<DebtServiceLocal.DebtServiceClient> GetLocalInstanceAsync()
    {
        var client = new DebtServiceLocal.DebtServiceClient(basicHttpBinding, endpointAddress);

        client.ClientCredentials.UserName.UserName = institutionIntegrationOption.UserName;
        client.ClientCredentials.UserName.Password = institutionIntegrationOption.Password;

        await Console.Out.WriteLineAsync(System.Text.Json.JsonSerializer.Serialize(client.ClientCredentials));

        return client;
    }
}
