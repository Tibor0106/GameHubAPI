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

            var cartItems = (from c in _appDbContext.cart
                             join s in _appDbContext.shop on c.gameId equals s.gameId
                             join g in _appDbContext.games on c.gameId equals g.Id
                             select new
                             {
                                 cid = c.Id,
                                 uid = c.userId,
                                 name = g.name,
                                 shortdesc = g.shortdescr,
                                 price = s.price,
                                 banner = g.banner
                             }).Where(i => i.uid == userid).ToList();
        

            return Ok(cartItems);
        }
        [HttpGet]
        [Route("getUserCartTotal/{userid}")]
        public async Task<IActionResult> getUserCartTotal(int userid) {
            var cartTotal = (from c in _appDbContext.cart join s in _appDbContext.shop on c.gameId equals s.gameId select new {price = s.price, uid = c.userId}).Where(i => i.uid == userid).ToList();
            var data = new List<int>();
            data.Add(cartTotal.Count());
            int total = 0;
            for(int k = 0; k < cartTotal.Count(); k++) {
                total += cartTotal[k].price;
            }
            data.Add(total);
            return Ok(data);
        }
        [HttpGet]
        [Route("RemoveCartItem/{cartId}")]
        public async Task<IActionResult> removeCartItem(int cartId)
        {
            var cartItem = _appDbContext.cart.FirstOrDefault(i => i.Id == cartId);
            if (cartItem == null)
                return BadRequest();
            _appDbContext.cart.Remove(cartItem);
            await _appDbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
