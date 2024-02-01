using Microsoft.AspNetCore.Mvc;
using SteamV2Webapi.DTO;

using PTHUWEBAPI.Database;
using SteamV2Webapi.Objects;

namespace SteamV2Webapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public CartController(AppDbContext appDbcontext)
        {
            _appDbContext = appDbcontext;
        }

        [HttpPut]
        [Route("AddCartItem")]
        public async Task<IActionResult> addCartItem(CartItemDTO ciDTO)
        {
            _appDbContext.cart.Add(new Cart(0, ciDTO.userId, ciDTO.gameId));
            await _appDbContext.SaveChangesAsync();
            return Ok(true);
        }
        [HttpPost]
        [Route("RemoveCartItem")]
        public async Task<IActionResult> removeLibraryItem(CartItemDTO ciDTO)
        {
            var cartItem = _appDbContext.cart.FirstOrDefault(i => i.userId == ciDTO.userId && i.gameId == ciDTO.gameId);
            if (cartItem == null)
                return BadRequest();
            _appDbContext.cart.Remove(cartItem);
            await _appDbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
