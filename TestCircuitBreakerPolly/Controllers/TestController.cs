using Microsoft.AspNetCore.Mvc;

namespace TestCircuitBreakerPolly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TestController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        //[HttpGet("test-comments")]
        //public async Task<IActionResult> TestComments()
        //{
        //    var client = _httpClientFactory.CreateClient("ApiClient");
        //    var response = await client.GetAsync("api/comments");
        //    response.EnsureSuccessStatusCode();

        //    var content = await response.Content.ReadAsStringAsync();
        //    return Ok(content);
        //}


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var client = _httpClientFactory.CreateClient("ApiClient");
            var response = await client.GetAsync("https://localhost:7286/api/comments");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            return StatusCode((int)response.StatusCode, response.ReasonPhrase);
        }
    }
}
