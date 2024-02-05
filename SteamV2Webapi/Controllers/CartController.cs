using Microsoft.AspNetCore.Mvc;

using PTHUWEBAPI.Database;
using SteamV2Webapi.Objects;
using Microsoft.EntityFrameworkCore;
using SteamV2Webapi.DTO.Cart;

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
        [HttpGet]
        [Route("getUserCart/{userid}")]
        public async Task<IActionResult> getCartItem(int userid)    
        {
           
          var cartItems = await _appDbContext.cart.Join(_appDbContext.games, cart => cart.gameId,game => game.Id,(cart, game) => new{ Cart = cart, Game = game}).ToListAsync();
        

            return Ok(cartItems);
        }
        [HttpGet]
        [Route("getUserCartTotal/{userid}")]
        public async Task<IActionResult> getUserCartTotal(int userid) {
            var cartTotal = await _appDbContext.cart.Where(i => i.userId == userid).ToList();
            var data = new List<int>();
            data.Add(cartTotal.Count);
            int total = 0;
            for(int k = 0; k < cartTotal.Count; k++) {
                total += cartTotal[k].price;
            }
            data.Add(total);
            return Ok(data);
        }
        [HttpDelete]
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
