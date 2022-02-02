using SampleSoapService;

namespace Api.Services.SampleSoap
{
    public interface ISampleSoapRestService
    {
        Task<TodaysDilbertResponse> GetTodaysDilbertResponse();

        Task<DailyDilbertResponse> GetDailyDilbertAsync(DateTime dateTime);
    }
}
