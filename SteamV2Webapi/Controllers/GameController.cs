using Microsoft.AspNetCore.Mvc;

using PTHUWEBAPI.Database;
using SteamV2Webapi.Objects;
using SteamV2Webapi.DTO.Game;

namespace SteamV2Webapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public GameController(AppDbContext appDbcontext)
        {
            _appDbContext = appDbcontext;
        }

        [HttpPut]
        [Route("AddGame")]
        public async Task<IActionResult> addGame(GameDTO gDTO)
        {
            _appDbContext.games.Add(new Game(0, gDTO.categoryId, gDTO.name, gDTO.linkId, gDTO.shortdescr, gDTO.longdescr, gDTO.icon, gDTO.banner));
            await _appDbContext.SaveChangesAsync();
            return Ok(true);
        }
        [HttpGet]
        [Route("GetGame/{gameId}")]
        public async Task<IActionResult> getGame(int gameId)
        {
            if (gameId == 0)
                return _appDbContext.games.ToList().Count > 0 ? Ok(_appDbContext.games.ToList()) : BadRequest();

            var game = _appDbContext.games.Where(i => i.Id == gameId).FirstOrDefault();

            if (game == null)
                return BadRequest();

            return Ok(game);
        }
        [HttpGet]
        [Route("GetGameByLinkId/{gamelinkId}")]
        public async Task<IActionResult> getGameByLinkId(string gamelinkId)
        {
            var item = (from g in _appDbContext.games
                        join s in _appDbContext.shop on g.Id equals s.gameId
                        where g.linkId == gamelinkId
                        select new { game = g, price = s.price, shop = s });

          
            return Ok(item);
        }
        [HttpPost]
        [Route("EditGameStatistics/{gameId}")]
        public async Task<IActionResult> editGameStatistics(EditGameDTO egDTO, int gameId)
        {
            var game = _appDbContext.games.Where(i => i.Id == gameId).ToList()[0];
            if (game == null)
                return BadRequest();

            if (egDTO.addpopularity > 0)
                game.popularity += egDTO.addpopularity;
            if (egDTO.banner != game.banner && egDTO.banner != "string")
                game.banner = egDTO.banner;
            if (egDTO.icon != game.icon && egDTO.icon != "string")
                game.icon = egDTO.icon;
            if (egDTO.name != game.name && egDTO.name != "string")
                game.name = egDTO.name;
            if (egDTO.linkId != game.linkId && egDTO.linkId != "string")
                game.linkId = egDTO.linkId;
            if (game.categoryId != egDTO.categoryId && egDTO.categoryId != 0)
                game.categoryId = egDTO.categoryId;
            if (game.shortdescr != egDTO.shortdescr && egDTO.shortdescr != "string")
                game.shortdescr = egDTO.shortdescr;
            if (game.longdescr != egDTO.longdescr && egDTO.longdescr != "string")
                game.longdescr = egDTO.longdescr;

            await _appDbContext.SaveChangesAsync();
            return Ok(true);
        }
        [HttpDelete]
        [Route("RemoveGame")]
        public async Task<IActionResult> removeGame(int gameId)
        {
            var game = _appDbContext.games.FirstOrDefault(i => i.Id == gameId);
            if (game == null)
                return BadRequest();
            _appDbContext.games.Remove(game);
            await _appDbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
