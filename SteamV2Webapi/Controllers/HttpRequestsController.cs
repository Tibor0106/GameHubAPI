using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SteamV2Webapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HttpRequestsController : ControllerBase
    {
        [HttpGet]
        [Route("getRequests")]
        public async Task<IActionResult> getRequests()
        {
            return Ok(RequestManager.Requests);
        }
    }
}
