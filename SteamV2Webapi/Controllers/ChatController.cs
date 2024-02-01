﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PTHUWEBAPI.Database;
using SteamV2Webapi.DTO;
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
        [HttpPut]
        [Route("sendMessage")]
        public async Task<IActionResult> sendMessage(MessageSendDTO msdto)
        {
            _appDbContext.messages.Add(new Message(0, msdto.senderId, msdto.receiverId, msdto.Message, DateTime.Now, false));
            await _appDbContext.SaveChangesAsync();
            return Ok(true);
        }
        [HttpPost]
        [Route("editMessage")]
        public async Task<IActionResult> EditMessage(EditMessageDTO editMessage)
        {
            var message = _appDbContext.messages.FirstOrDefault(i => i.id == editMessage.messageId);
            if(message == null) return BadRequest();
            message.messageBody = editMessage.newmessage;
            message.edited = true;
            await _appDbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete]
        [Route("deletemessage/{id}")]
        public async Task<IActionResult> deletemessage(int id)
        {
            var message = _appDbContext.messages.FirstOrDefault(i => i.id == id);
            if (message == null) return BadRequest();

            _appDbContext.Remove(message);
            await _appDbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
