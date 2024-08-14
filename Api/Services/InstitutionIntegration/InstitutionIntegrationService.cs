namespace Api.Services.SampleSoap;

public class InstitutionIntegrationService : IInstitutionIntegrationService
{
    private readonly EndpointAddress endpointAddress;
    private readonly BasicHttpBinding basicHttpBinding;
    private readonly string serviceUrl = "http://localhost:5102/DebtService.asmx";

    public InstitutionIntegrationService()
    {
        endpointAddress = new EndpointAddress(serviceUrl);

        basicHttpBinding = new BasicHttpBinding(endpointAddress.Uri.Scheme.ToLower() == "http"
            ? BasicHttpSecurityMode.None
            : BasicHttpSecurityMode.Transport)
        {
            //Please set the time accordingly, this is only for demo
            OpenTimeout = TimeSpan.MaxValue,
            CloseTimeout = TimeSpan.MaxValue,
            ReceiveTimeout = TimeSpan.MaxValue,
            SendTimeout = TimeSpan.MaxValue
        };
    }

    public async Task<DebtQueryResponse> GetDebtsAsync()
    {
        var client = await GetInstanceAsync();

        var debtRequest = new DebtQueryRequest();

        var response = await client.GetDebtsAsync(debtRequest);

        return response;
    }

    private async Task<DebtServiceClient> GetInstanceAsync()
    {
        return await Task.Run(() => new DebtServiceClient(basicHttpBinding, endpointAddress));
    }
}
