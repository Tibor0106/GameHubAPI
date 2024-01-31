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
            var cUser = _appDbContext.users.FirstOrDefault(i => i.email == reg.email);
            if (cUser != null) return BadRequest("this email already used !");
            UserDTO user =
                new UserDTO(0, reg.name, reg.username, 
                reg.email, BCrypt.Net.BCrypt.HashPassword(reg.password), "", 
                DateTime.Now, DateTime.Now, false);

            _appDbContext.users.Add(user); //bármilyen módosítás után, csak mentéssel kerül be az adatbázisba
            await _appDbContext.SaveChangesAsync();
            return Ok();        
         }
        [HttpGet]
        [Route("verify/{verifykey}")]
        public async Task<IActionResult> getverify(string verifykey)
        {
            return Ok();
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> login(LoginDTO loginData)
        {
            var User = _appDbContext.users.FirstOrDefault(i => i.email == loginData.email);
            if(User == null)
            {
                User = _appDbContext.users.FirstOrDefault(i => i.username == loginData.username);
            }
            if (User == null) return BadRequest("this user does not exists.");

            if (BCrypt.Net.BCrypt.Verify(loginData.password, User.password))
            {
                return Ok(User);
            }
            return BadRequest();
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
        [HttpGet]
        [Route("getStats/{userid}")]
        public async Task<IActionResult> getStats(int userid)
        {
            return Ok();
        }
        [HttpGet]
        [Route("getStat/{userid}/{gameId}")]
        public async Task<IActionResult> getStats(int userid, int gameId)
        {
            return Ok();
        }

    }
}
