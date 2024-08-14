namespace Api.Services.SampleSoap;

public interface IInstitutionIntegrationService
{
    Task<BaseResponseOfDebtQueryResponse> GetDebtsAsync();
}
