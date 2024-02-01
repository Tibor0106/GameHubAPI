using Microsoft.AspNetCore.Mvc;

using PTHUWEBAPI.Database;
using SteamV2Webapi.Objects;
using Microsoft.EntityFrameworkCore;
using SteamV2Webapi.DTO.Library;

namespace SteamV2Webapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public LibraryController(AppDbContext appDbcontext)
        {
            _appDbContext = appDbcontext;
        }

        [HttpPut]
        [Route("AddLibraryItem")]
        public async Task<IActionResult> addLibraryItem(AddLibraryItemDTO aliDTO)
        {
            _appDbContext.library.Add(new Library(0, aliDTO.userId, aliDTO.gameId, DateTime.Now));
            await _appDbContext.SaveChangesAsync();
            return Ok(true);
        }
        [HttpGet]
        [Route("GetLibraryItems")]
        public async Task<IActionResult> getLibraryItems(int userId)
        {
            var libraryItems = _appDbContext.library.Where(i => i.userId == userId).ToList();
            return Ok(libraryItems);
        }
        [HttpPost]
        [Route("StartLibraryItem")]//Start game, etc
        public async Task<IActionResult> startLibaryItem(StartLibraryItemDTO sliDTO)
        {
            var gameAvailable = _appDbContext.library.Where(l => l.userId == sliDTO.userId && l.gameId == sliDTO.gameId).ToList();
            if (gameAvailable.Count != 0)
                return Ok(true);
            return Ok(false);
        }
        [HttpDelete]
        [Route("RemoveLibraryItem")]
        public async Task<IActionResult> removeLibraryItem(DeleteLibraryItemDTO dliDTO)
        {
            var game = _appDbContext.library.FirstOrDefault(i => i.userId == dliDTO.userId && i.gameId == dliDTO.gameId);
            if (game == null)
                return BadRequest();
            _appDbContext.library.Remove(game);
            await _appDbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
