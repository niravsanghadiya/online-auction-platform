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
        private readonly IRepository<AuctionItem> _auctionItemRepository;

        public AuctionItemController(IAuctionRepository auctionRepository, IRepository<AuctionItem> auctionItemRepository)
        {
            _auctionRepository = auctionRepository;
            _auctionItemRepository = auctionItemRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuctionItems()
        {

            var auctionItems = await _auctionItemRepository.GetAllAsync();
            return Ok(auctionItems);
        }

        [HttpPost]
        public async Task<IActionResult>  CreateAuctionItem(AuctionItem auctionItem)
        {         
            await _auctionItemRepository.AddAsync(auctionItem);  
            return CreatedAtAction(nameof(CreateAuctionItem),new { id=auctionItem.Id} , auctionItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuctionItem(Guid id,AuctionItem auctionItem)
        {
            var auctionItemObj = await _auctionItemRepository.GetByIdAsync(id);
            if(auctionItemObj != null)
            {
                await _auctionItemRepository.UpdateAsync(auctionItem);
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
            var auctionItemObj = await _auctionItemRepository.GetByIdAsync(id);
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
