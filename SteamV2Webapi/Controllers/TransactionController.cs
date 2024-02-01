using Microsoft.AspNetCore.Mvc;
using SteamV2Webapi.DTO;

using PTHUWEBAPI.Database;
using SteamV2Webapi.Objects;
using Microsoft.EntityFrameworkCore;
using Steam2WebApi.Objects;

namespace SteamV2Webapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public TransactionController(AppDbContext appDbcontext)
        {
            _appDbContext = appDbcontext;
        }

        [HttpPut]
        [Route("AddTransaction")]
        public async Task<IActionResult> addTransaction(TransactionDTO tDTO)
        {
            _appDbContext.transactions.Add(new Transaction(tDTO.transactionData, tDTO.price));
            await _appDbContext.SaveChangesAsync();
            return Ok(true);
        }
    }
}
