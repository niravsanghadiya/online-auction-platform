using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineAuctionPlatform.Application.Repositories;
using OnlineAuctionPlatform.Domain.Entities;
using System.Security.Claims;

namespace OnlineAuctionPlatform.Presentation.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BidController : ControllerBase
    {
        private readonly IRepository<Bid> _bidRepository;
        private readonly IRepository<AuctionItem> _auctionItemRepository;

        public BidController(IRepository<Bid> bidRepository, IRepository<AuctionItem> auctionItemRepository)
        {
            _bidRepository = bidRepository;
            _auctionItemRepository = auctionItemRepository;            
        }

        [HttpPost("post-bid")]
        public async Task<IActionResult> PostBid([FromBody] Bid bid)
        {
            var auctionItem = await _auctionItemRepository.GetByIdAsync(bid.AuctionItemId);
            if(auctionItem == null) return NotFound($"Auction item not found");

            var currentHighestBid = _bidRepository.GetAllAsync().Result
                                   .Where(x => x.AuctionItem == bid.AuctionItem)
                                   .OrderByDescending(x => x.Amount)
                                   .FirstOrDefault();

            bid.BidTime = DateTime.UtcNow;
            if(currentHighestBid != null && currentHighestBid.Amount <= bid.Amount)
            {
                return BadRequest($"Bid amount should be higher than current amount");
            }
         
            await _bidRepository.AddAsync(bid);

            return CreatedAtAction(nameof(PostBid), new { id = bid.Id }, bid);
        }

        [HttpGet("auctionitem/{auctionItemId}")]
        public async Task<IActionResult> GetBidsForAuctionItem(Guid auctionItemId)
        {
            var bids = _bidRepository.GetAllAsync().Result
                .Where(b => b.AuctionItemId == auctionItemId)
                .OrderByDescending(b => b.Amount)
                .ToList();

            return Ok(bids);
        }

        [HttpGet("highest/{auctionItemId}")]
        public async Task<ActionResult<Bid>> GetHighestBid(Guid auctionItemId)
        {
            var highestBid = _bidRepository.GetAllAsync().Result
                .Where(b => b.AuctionItemId == auctionItemId)
                .OrderByDescending(b => b.Amount)
                .FirstOrDefault();

            if (highestBid == null)
            {
                return NotFound($"No bids placed for this auction item.");
            }

            return highestBid;
        }
    }
}
