using Api.Services.SampleSoap;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly ILogger<TestController> _logger;
    private readonly ISampleSoapRestService _sampleSoapRestService;
    private readonly IEnerjisaSoapRestService _enerjisaSoapRestService;

    public TestController(ILogger<TestController> logger
        , ISampleSoapRestService sampleSoapRestService
        , IEnerjisaSoapRestService enerjisaSoapRestService)
    {
        _logger = logger;
        _sampleSoapRestService = sampleSoapRestService;
        _enerjisaSoapRestService = enerjisaSoapRestService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        _logger.LogInformation("information log ");

        var todaysDilbertResponse = await _sampleSoapRestService.GetTodaysDilbertResponse();
        var dailyDilbertResponse = await _sampleSoapRestService.GetDailyDilbertAsync(DateTime.Now);

        var responseObject = new
        {
            todaysDilbertResponse,
            dailyDilbertResponse,
        };

        return Ok(responseObject);
    }

    [HttpGet]
    [Route("enerji-sa")]
    public async Task<IActionResult> GetEnerjiSa()
    {
        var request = new BorcSorguRequest
        {
            CONTRACT_ACCOUNT_NUMBER = "005000247412",
            LEGACY_CONTRACT_ACCOUNT_NUMBER = string.Empty,
            TRIDNUMBER = string.Empty,
            DOCUMENT_NUMBER = string.Empty,
        };

        var response = await _enerjisaSoapRestService.GetBorcSorgu(request);

        return Ok(response);
    }
}