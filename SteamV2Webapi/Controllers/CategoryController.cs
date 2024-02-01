using Microsoft.AspNetCore.Mvc;
using SteamV2Webapi.DTO;

using PTHUWEBAPI.Database;
using SteamV2Webapi.Objects;
using Microsoft.EntityFrameworkCore;
using SteamV2Webapi.DTO.Category;

namespace SteamV2Webapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public CategoryController(AppDbContext appDbcontext)
        {
            _appDbContext = appDbcontext;
        }

        [HttpPut]
        [Route("AddCategory")]
        public async Task<IActionResult> addCategory(AddCategoryDTO cDTO)
        {
            _appDbContext.category.Add(new Category(0, cDTO.categoryName, 0));
            await _appDbContext.SaveChangesAsync();
            return Ok(true);
        }
        [HttpDelete]
        [Route("RemoveCategoryItem")]
        public async Task<IActionResult> removeCategoryItem(DeleteCategoryDTO cDTO)
        {
            var categoryItem = _appDbContext.category.FirstOrDefault(i => i.categoryName == cDTO.categoryName);
            if (categoryItem == null)
                return BadRequest();
            _appDbContext.category.Remove(categoryItem);
            await _appDbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        [Route("GetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = _appDbContext.category.ToList();
            if (categories.Count == 0)
                return BadRequest();
            return Ok(categories);
        }
        [HttpGet]
        [Route("GetPopularCategories")]
        public async Task<IActionResult> getXPopularCategories(int x)
        {
            List<Category> categories = new List<Category>();
            if (x == 0)
                categories = _appDbContext.category.OrderByDescending(i => i.popularity).ToList();
            else
                categories = _appDbContext.category.OrderByDescending(i => i.popularity).Take(x).ToList();
            if (categories.Count == 0)
                return BadRequest();
            return Ok(categories);
        }
        [HttpGet]
        [Route("GetPopularGamesFromCategory")]
        public async Task <IActionResult> getXPopularGamesFromYCategory(int x, int y)
        {
            var games = _appDbContext.games.Where(i => i.categoryId == y).OrderByDescending(i => i.popularity).Take(x).ToList();
            if (games == null)
                return BadRequest();
            return Ok(games);
        }
    }
}
