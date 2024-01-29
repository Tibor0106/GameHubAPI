using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PTHUWEBAPI.Database;
using SteamV2Webapi.DTO;
using System.Reflection.Metadata;

namespace SteamV2Webapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public UserController(AppDbContext appDbcontext)
        {
            _appDbContext = appDbcontext;
        }
        [HttpPost]
        [Route("register")]
         public async Task<IActionResult> register(RegisterDTO  reg)
         {
            

            return Ok();
         }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> register(LoginDTO loginData)
        {
            return Ok();
        }
        [HttpGet]
        [Route("getUserByName")]
        public async Task<IActionResult> getByUsername(string username)
        {
            return Ok();
        }
        [HttpGet]
        [Route("getFriends/{userid}")]
        public async Task<IActionResult> getFriends(int userid)
        {
            return Ok();
        }
    }
}
