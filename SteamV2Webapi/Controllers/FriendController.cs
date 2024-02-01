using Microsoft.AspNetCore.Mvc;
using SteamV2Webapi.DTO;

using PTHUWEBAPI.Database;
using SteamV2Webapi.Objects;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

namespace SteamV2Webapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FriendController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public FriendController(AppDbContext appDbcontext)
        {
            _appDbContext = appDbcontext;
        }

        [HttpPut]
        [Route("SendFriendRequest")]
        public async Task<IActionResult> sendFriendRequest(FriendRequestDTO fDTO)
        {
            _appDbContext.friend_requests.Add(new FriendRequest(0, fDTO.userId, fDTO.friendId));
            await _appDbContext.SaveChangesAsync();
            return Ok(true);
        }
        [HttpGet]
        [Route("GetSentFriendRequest")]
        public async Task<IActionResult> getSentFriendRequest(int senderId)
        {
            var friendRequests = _appDbContext.friend_requests.Where(i => i.userId == senderId).ToList();
            if (friendRequests.Count == 0)
                return BadRequest();
            await _appDbContext.SaveChangesAsync();
            return Ok(friendRequests);
        }

        [HttpPost]
        [Route("AcceptFriendRequest")]
        public async Task<IActionResult> acceptFriendRequest(FriendRequestDTO frDTO)
        {
            var friendRequest = _appDbContext.friend_requests.Where(i => i.userId == frDTO.userId).ToList()[0];
            _appDbContext.friends.Add(new Friend(0, frDTO.userId, frDTO.friendId, DateTime.Now));
            _appDbContext.friend_requests.Remove(friendRequest);
            await _appDbContext.SaveChangesAsync();
            return Ok(true);
        }
        [HttpPost]
        [Route("DeclineFriendRequest")]
        public async Task<IActionResult> declineFriendRequest(FriendRequestDTO frDTO)
        {
            var friendRequest = _appDbContext.friend_requests.Where(i => i.userId == frDTO.userId).ToList()[0];
            _appDbContext.friend_requests.Remove(friendRequest);
            await _appDbContext.SaveChangesAsync();
            return Ok(true);
        }

        [HttpDelete]
        [Route("RemoveFriend")]
        public async Task<IActionResult> removeFriend(FriendDTO fDTO)
        {
            var friend = _appDbContext.friends.FirstOrDefault(i => i.userId == fDTO.userId && i.friendId == fDTO.friendId);
            if (friend == null)
                return BadRequest();
            _appDbContext.friends.Remove(friend);
            await _appDbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
