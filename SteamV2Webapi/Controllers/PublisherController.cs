using Microsoft.AspNetCore.Mvc;

using PTHUWEBAPI.Database;
using GameHubAPI.Objects;
using GameHubAPI.DTO.Publisher;

namespace GameHubAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public PublisherController(AppDbContext appDbcontext)
        {
            _appDbContext = appDbcontext;
        }

        [HttpPut]
        [Route("AddPublisher")]
        public async Task<IActionResult> addPublisher(PublisherDTO pDTO)
        {
            _appDbContext.publisher.Add(new Publisher(0, pDTO.publisherId, pDTO.publisherName, pDTO.publisherIcon));
            await _appDbContext.SaveChangesAsync();
            return Ok(true);
        }
        [HttpGet]
        [Route("getPublisher")]
        public async Task<IActionResult> getPublisher(int publisherId)
        {
            var publisher = _appDbContext.publisher.Where(i => i.publisherId == publisherId).ToList();
            if (publisher.Count == 0)
                return BadRequest();
            return Ok(publisher);
        }
        [HttpGet]
        [Route("GetAllPublishers")]
        public async Task<IActionResult> getAllPublishers()
        {
            var publisher = _appDbContext.publisher.ToList();
            if (publisher.Count == 0)
                return BadRequest();
            return Ok(publisher);
        }
        [HttpPost]
        [Route("EditPublisher")]
        public async Task<IActionResult> editPublisher(EditPublisherDTO epDTO)
        {
            if (epDTO.newPublisherName == "" && epDTO.newPublisherIcon == "")
                return BadRequest(); // Haszontalan request; Ne csináljunk semmit, teljesítményt ront.

            var publisher = _appDbContext.publisher.FirstOrDefault(i => i.publisherId == epDTO.publisherId);
            if (publisher == null)
                return BadRequest();
            if (epDTO.newPublisherName != "")
                publisher.publisherName = epDTO.newPublisherName;
            if (epDTO.newPublisherIcon != "")
                publisher.publisherIcon = epDTO.newPublisherIcon;
            await _appDbContext.SaveChangesAsync();
            return Ok(true);
        }
        [HttpDelete]
        [Route("RemovePublisher")]
        public async Task<IActionResult> removePublisher(DeletePublisherDTO dpDTO)
        {
            var publisher = _appDbContext.publisher.FirstOrDefault(i => i.publisherId == dpDTO.publisherId);
            if (publisher == null)
                return BadRequest();
            _appDbContext.publisher.Remove(publisher);
            await _appDbContext.SaveChangesAsync();
            return Ok(true);
        }
    }
}
