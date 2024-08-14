namespace Api.Services.SampleSoap;

public interface IInstitutionIntegrationService
{
    Task<DebtQueryResponse> GetDebtsAsync();
}
