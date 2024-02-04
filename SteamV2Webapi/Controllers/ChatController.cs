using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PTHUWEBAPI.Database;
using SteamV2Webapi.DTO.Message;
using SteamV2Webapi.Objects;

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
            //rohadt nagy errort dobott


            // itt még kell "bütykölni"

            int start = page;
            page *= 20;
            var messages = await _appDbContext.messages
            .Where(m => (m.senderId == id || m.receiverId == id)) //tudtommal ha jól értelmezem, ez a chateket hozná le, magyarul egy receiverid is kell és fordítva is. Ezért kell nekünk a datetime object, hogy sorba tudjuk rakni a messageket.
            .OrderBy(m => m.messageSent)
            .Skip((start - 1) * page / start)
            .Take(page)
            .ToListAsync();
            if (messages.Count == 0) return BadRequest();

            // sql pl: SELECT * FROM xy WHERE senderId like id LIMIT page OFFSET (start - 1) * page / start <= a konnyeb megertes edekeben
            // Console.WriteLine(page +" "+(start - 1) * page / start);

            return Ok(messages);
        }
        [HttpGet]
        [Route("getMessagesWithUser/{id}/{friendId}")]
        public async Task<IActionResult> getMessagesWithUser(int id, int friendId)
        {
            var messages = _appDbContext.messages.Where(m => (m.senderId == id && m.receiverId == friendId) || (m.senderId == friendId && m.receiverId == id)).OrderBy(i => i.messageSent);
            return Ok(messages);
        }
        [HttpGet]
        [Route("sendMessage/{senderId}/{receiverId}/{message}")]
        public async Task<IActionResult> sendMessage(int senderId, int receiverId, string message)
        {
            _appDbContext.messages.Add(new Message(senderId, receiverId, message, DateTime.Now, false));
            await _appDbContext.SaveChangesAsync();
            return Ok(true);
        }
        [HttpPost]
        [Route("editMessage")]
        public async Task<IActionResult> editMessage(EditMessageDTO editMessage)
        {
            var message = _appDbContext.messages.FirstOrDefault(i => i.Id == editMessage.messageId);
            if(message == null) return BadRequest();
            message.messageBody = editMessage.newmessage;
            message.edited = true;
            await _appDbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete]
        [Route("DeleteMessage/{id}")]
        public async Task<IActionResult> deleteMessage(int id)
        {
            var message = _appDbContext.messages.FirstOrDefault(i => i.Id == id);
            if (message == null) return BadRequest();

            _appDbContext.Remove(message);
            await _appDbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
