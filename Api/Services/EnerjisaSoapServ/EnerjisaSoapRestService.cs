using EnerjisaSoapService;
using System.ServiceModel;

namespace Api.Services.EnerjisaSoapServ
{
    public class EnerjisaSoapRestService : IEnerjisaSoapRestService
    {
        private readonly EndpointAddress endpointAddress;
        private readonly BasicHttpBinding basicHttpBinding;
        private readonly string serviceUrl = "http://10.1.3.17:9090/INST_Enerjisa_D/dir/wsdl?p=sa/c2db865f7ccc3139a70e419729dd8f7d&opt_name=SI_ECD_OUT_SYNC&opt_namespace=http%3A%2F%2Fenerjisa.com%2Fbank%2Fecd&opt_swcv_guid=a21b0d10b68911edcb0eda610aa7000a&oah_name=SI_ECD_OUT_SYNC&oah_interfaceNamespace=http%3A%2F%2Fenerjisa.com%2Fbank%2Fecd";

        public EnerjisaSoapRestService()
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

        public async Task<BorcSorguResponse> GetBorcSorgu(BorcSorguRequest request)
        {
            var client = await GetInstanceAsync();

            try
            {
                var response = await client.BorcSorguAsync(request);
                
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private async Task<SI_ECD_OUT_SYNCClient> GetInstanceAsync()
        {
            return await Task.Run(() => new SI_ECD_OUT_SYNCClient(basicHttpBinding, endpointAddress));
        }
    }
}
