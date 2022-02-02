using SampleSoapService;
using System.ServiceModel;

namespace Api.Services.SampleSoap
{
    public class SampleSoapRestService : ISampleSoapRestService
    {
        private readonly EndpointAddress endpointAddress;
        private readonly BasicHttpBinding basicHttpBinding;
        private readonly string serviceUrl = "http://www.gcomputer.net/webservices/dilbert.asmx";

        public SampleSoapRestService()
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

        public async Task<TodaysDilbertResponse> GetTodaysDilbertResponse()
        {
            var client = await GetInstanceAsync();

            var response = await client.TodaysDilbertAsync();

            return response;
        }

        public async Task<DailyDilbertResponse> GetDailyDilbertAsync(DateTime dateTime)
        {
            var client = await GetInstanceAsync();

            var response = await client.DailyDilbertAsync(dateTime);

            return response;
        }

        private async Task<DilbertSoapClient> GetInstanceAsync()
        {
            return await Task.Run(() => new DilbertSoapClient(basicHttpBinding, endpointAddress));
        }
    }
}
