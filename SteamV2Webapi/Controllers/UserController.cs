﻿using Microsoft.AspNetCore.Mvc;
using PTHUWEBAPI.Database;
using GameHubAPI.Objects;
using GameHubAPI.DTO.Login;

namespace GameHubAPI.Controllers
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
        public async Task<IActionResult> register(RegisterDTO reg)
        {
            var cUser = _appDbContext.users.FirstOrDefault(i => i.email == reg.email);
            if (cUser != null) return BadRequest("this email is already used !");
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
            if (User == null)
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
        [Route("getUserById/{userid}")]
        public async Task<IActionResult> getById(int userid)
        {
            var User = _appDbContext.users.FirstOrDefault(i => i.Id == userid);
            if (User == null) BadRequest();


            return Ok(User);
        }
        [HttpGet]
        [Route("getFriends/{userid}")]
        public async Task<IActionResult> getFriends(int userid)
        {
            var friendRequests = _appDbContext.friends.Where(i => i.userId == userid).OrderByDescending(i => i.friend_since).ToList();
            return Ok(friendRequests);
        }
        [HttpGet]
        [Route("getFriendsData/{userid}")]
        public async Task<IActionResult> getFriendsData(int userid)
        {
            var friendRequests = (from f in _appDbContext.friends join u in _appDbContext.users on f.friendId equals u.Id select new { username = u.username, name = u.name, online = u.online, friend_since = f.friend_since, uid = u.Id, id = f.userId, lhb = u.last_heartbeat }).Where(i => i.id == userid).ToList();
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
        [HttpGet]
        [Route("updateHeartBeat/{userid}")]
        public async Task<IActionResult> updateHB(int userid)
        {
            var user = _appDbContext.users.FirstOrDefault(i => i.Id == userid);
            if (user == null) return BadRequest();
            user.last_heartbeat = DateTime.Now;
            await _appDbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        [Route("getLastHeartBeat/{userid}")]
        public async Task<IActionResult> getlhb(int userid)
        {
            var user = _appDbContext.users.FirstOrDefault(i => i.Id == userid);
            if (user == null) return BadRequest();

            return Ok(user.last_heartbeat);
        }

        [HttpGet]
        [Route("updateHeartBeats")]
        public async Task<IActionResult> updateHeartBeats()
        {
            Console.WriteLine("hi");
            _appDbContext.users.ToList().ForEach(i =>
            {
                Console.WriteLine(i.last_heartbeat.AddMinutes(2) < DateTime.Now);
                if (i.last_heartbeat.AddSeconds(10) < DateTime.Now)
                    i.online = false;
                else
                    i.online = true;
                _appDbContext.SaveChanges();
            });
            return Ok();
        }
    }
}
