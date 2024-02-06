using Microsoft.AspNetCore.Mvc;

using PTHUWEBAPI.Database;
using GameHubAPI.Objects;
using GameHubAPI.DTO.Shop;

namespace GameHubAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public ShopController(AppDbContext appDbcontext)
        {
            _appDbContext = appDbcontext;
        }

        [HttpPut]
        [Route("AddGame")]
        public async Task<IActionResult> addGame(ShopDTO sDTO)
        {
            _appDbContext.shop.Add(new Shop(0, sDTO.gameId, sDTO.publisherId, sDTO.price, sDTO.discount, sDTO.popularity, sDTO.featured));
            await _appDbContext.SaveChangesAsync();
            return Ok(true);
        }
        [HttpDelete]
        [Route("RemoveGame")]
        public async Task<IActionResult> removeGame(int shopId)
        {
            var game = _appDbContext.shop.FirstOrDefault(i => i.Id == shopId);
            if (game == null)
                return BadRequest();
            _appDbContext.shop.Remove(game);
            await _appDbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
