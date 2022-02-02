using Api.Services.SampleSoap;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly ISampleSoapRestService _sampleSoapRestService;

        public TestController(ILogger<TestController> logger
            , ISampleSoapRestService sampleSoapRestService)
        {
            _logger = logger;
            _sampleSoapRestService = sampleSoapRestService;
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
    }
}