using Microsoft.AspNetCore.Mvc;
using SteamV2Webapi.DTO;

using PTHUWEBAPI.Database;
using SteamV2Webapi.Objects;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> addLibraryItem(LibraryItemDTO aliDTO)
        {
            _appDbContext.library.Add(new Library(0, aliDTO.userId, aliDTO.gameId, aliDTO.purchased_since));
            await _appDbContext.SaveChangesAsync();
            return Ok(true);
        }
        [HttpPost]
        [Route("StartLibraryItem")]//Start game, etc
        public async Task<IActionResult> startLibaryItem(LibraryItemDTO aliDTO)
        {
            var gameAvailable = await _appDbContext.library.Where(l => l.userId == aliDTO.userId && l.gameId == aliDTO.gameId).ToListAsync() != new List<Library>();
            if (gameAvailable)
                return Ok(true);
            return Ok(false);
        }
        [HttpPost]
        [Route("RemoveLibraryItem")]
        public async Task<IActionResult> removeLibraryItem(LibraryItemDTO aliDTO)
        {
            var game = _appDbContext.library.FirstOrDefault(i => i.userId == aliDTO.userId && i.gameId == aliDTO.gameId);
            if (game == null)
                return BadRequest();
            _appDbContext.library.Remove(game);
            await _appDbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
