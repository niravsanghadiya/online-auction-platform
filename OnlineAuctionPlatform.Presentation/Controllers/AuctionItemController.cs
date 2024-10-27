using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineAuctionPlatform.Application.Repositories;
using OnlineAuctionPlatform.Domain.Entities;
using OnlineAuctionPlatform.Infrastructure.Data;
using OnlineAuctionPlatform.Infrastructure.Repositories;

namespace OnlineAuctionPlatform.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionItemController : ControllerBase
    {       
        private readonly IAuctionRepository _auctionRepository;

        public AuctionItemController(IAuctionRepository auctionRepository)
        {
            _auctionRepository = auctionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuctionItems()
        {
            var auctionItems = await _auctionRepository.GetAllAsync();
            return Ok(auctionItems);
        }

        [HttpPost]
        public async Task<IActionResult>  CreateAuctionItem(AuctionItem auctionItem)
        {         
            await _auctionRepository.AddAsync(auctionItem);  
            return CreatedAtAction(nameof(CreateAuctionItem),new { id=auctionItem.Id} , auctionItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuctionItem(Guid id,AuctionItem auctionItem)
        {
            var auctionItemObj = await _auctionRepository.GetByIdAsync(id);
            if(auctionItemObj != null)
            {
                await _auctionRepository.UpdateAsync(auctionItem);
                return NoContent();
            }
            else
            {
                return NotFound($"Auction Item having Id:{id} does not exists");
            }
            
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAuctionItem(Guid id)
        {
            var auctionItemObj = await _auctionRepository.GetByIdAsync(id);
            if (auctionItemObj != null)
            {
                await _auctionRepository.DeleteAsync(id);
                return NoContent();
            }
            else
            {
                return NotFound($"Auction Item having Id:{id} does not exists");
            }
        }
    }
}
