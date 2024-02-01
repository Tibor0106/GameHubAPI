using Microsoft.AspNetCore.Mvc;

using PTHUWEBAPI.Database;
using SteamV2Webapi.Objects;
using SteamV2Webapi.DTO.Login;

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
            User user =
                new User(0, reg.name, reg.username, 
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
        [Route("getUserByName/{username}")]
        public async Task<IActionResult> getByUsername(string username)
        {
            var User = _appDbContext.users.FirstOrDefault(i => i.username == username);
            if (User == null) BadRequest();
            
            
             return Ok(User);
        }
        [HttpGet]
        [Route("getFriends/{userid}")]
        public async Task<IActionResult> getFriends(int userid)
        {
            var friendRequests = _appDbContext.friends.Where(i => i.userId == userid).ToList();
            return Ok(friendRequests);
        }
        [HttpGet]
        [Route("getStats/{userid}")]
        public async Task<IActionResult> getStats(int userid)
        {
            var stat = _appDbContext.game_stats.Where(i => i.userId == userid).ToList();
            if (stat.Count == 0)
                return BadRequest();
            return Ok(stat);
        }
        [HttpGet]
        [Route("getStat/{userid}/{gameId}")]
        public async Task<IActionResult> getStats(int userid, int gameId)
        {
            var stat = _appDbContext.game_stats.Where(i => i.userId == userid && i.gameId == gameId).ToList();
            if (stat.Count == 0)
                return BadRequest();
            return Ok(stat[0]);
        }

    }
}
