namespace Api.Services.EnerjisaSoapServ;

public interface IEnerjisaSoapRestService
{
    Task<BorcSorguResponse> GetBorcSorgu(BorcSorguRequest request);
}
