﻿namespace Api.Services.SampleSoap;

public class InstitutionIntegrationService : IInstitutionIntegrationService
{
    private readonly EndpointAddress endpointAddress;
    private readonly BasicHttpBinding basicHttpBinding;
#if DEBUG
    private readonly string serviceUrl = "http://localhost:5102/DebtService.asmx";
#else
    private readonly string serviceUrl = "http://10.1.3.58/InstitutionIntegration/DebtService.asmx";    
#endif

    public InstitutionIntegrationService()
    {
        endpointAddress = new EndpointAddress(serviceUrl);

        basicHttpBinding = new BasicHttpBinding(endpointAddress.Uri.Scheme.Equals("http", StringComparison.OrdinalIgnoreCase)
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

    public async Task<BaseResponseOfDebtQueryResponse> GetDebtsAsync()
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
