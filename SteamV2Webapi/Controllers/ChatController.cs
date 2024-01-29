using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PTHUWEBAPI.Database;
using SteamV2Webapi.DTO;

namespace SteamV2Webapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public ChatController(AppDbContext appDbcontext)
        {
            _appDbContext = appDbcontext;
        }

        //limit messages 1 page = 20 message
        [HttpGet]
        [Route("getUserMessages/{id}/{page}")]
        public async Task<IActionResult> getMessages(int id, int page)
        {

            return Ok();
        }
        [HttpPut]
        [Route("sendMessage")]
        public async Task<IActionResult> sendMessage(MessageSendDTO msdto)
        {

            return Ok();
        }
        [HttpPost]
        [Route("editMessage")]
        public async Task<IActionResult> EditMessage(EditMessageDTO editMessage)
        {
            return Ok();
        }
    }
}
