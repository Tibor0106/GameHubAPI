using Microsoft.AspNetCore.Mvc;
using SteamV2Webapi.DTO;

using PTHUWEBAPI.Database;
using SteamV2Webapi.Objects;

namespace SteamV2Webapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GameStatController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public GameStatController(AppDbContext appDbcontext)
        {
            _appDbContext = appDbcontext;
        }

        [HttpPut]
        [Route("AddGameStat")]//IF doesnt exist.
        public async Task<IActionResult> addGameStat(NewGameStatDTO ngsDTO)
        {
            var gS = _appDbContext.game_stats.FirstOrDefault(i => i.gameId == ngsDTO.gameId && i.userId == ngsDTO.userId);
            if (gS != null)
                return BadRequest();
            _appDbContext.game_stats.Add(new GameStats(0, ngsDTO.userId, ngsDTO.gameId, 0, 0, 0));
            await _appDbContext.SaveChangesAsync();
            return Ok(true);
        }
        [HttpDelete]
        [Route("RemoveGameStat")]//IF exists.
        public async Task<IActionResult> removeGameStat(NewGameStatDTO ngsDTO)
        {
            var gS = _appDbContext.game_stats.FirstOrDefault(i => i.gameId == ngsDTO.gameId && i.userId == ngsDTO.userId);
            if (gS == null)
                return BadRequest();
            _appDbContext.game_stats.Remove(gS);
            await _appDbContext.SaveChangesAsync();
            return Ok(true);
        }

        [HttpPost]
        [Route("AddGameHours")]
        public async Task<IActionResult> addGameHours(AddGameHoursDTO aghDTO)
        {
            var gS = _appDbContext.game_stats.FirstOrDefault(i => i.gameId == aghDTO.gameId && i.userId == aghDTO.userId);
            if (gS == null)
                return BadRequest();
            gS.playerHours += aghDTO.toAddHours;
            await _appDbContext.SaveChangesAsync();
            return Ok(true);
        }
        [HttpPost]
        [Route("AddGameLevels")]
        public async Task<IActionResult> addGameLevels(AddGameLevelsDTO aglDTO)
        {
            var gS = _appDbContext.game_stats.FirstOrDefault(i => i.gameId == aglDTO.gameId && i.userId == aglDTO.userId);
            if (gS == null)
                return BadRequest();
            gS.level += aglDTO.toAddLevels;
            await _appDbContext.SaveChangesAsync();
            return Ok(true);
        }
        [HttpPost]
        [Route("AddGameAchievements")]
        public async Task<IActionResult> addGameAchievements(AddGameAchievementsDTO agaDTO)
        {
            var gS = _appDbContext.game_stats.FirstOrDefault(i => i.gameId == agaDTO.gameId && i.userId == agaDTO.userId);
            if (gS == null)
                return BadRequest();
            gS.achievements += agaDTO.toAddAchievements;
            await _appDbContext.SaveChangesAsync();
            return Ok(true);
        }
    }
}
