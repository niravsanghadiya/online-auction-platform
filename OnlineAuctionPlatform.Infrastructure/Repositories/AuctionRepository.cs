using Microsoft.EntityFrameworkCore;
using OnlineAuctionPlatform.Application.Repositories;
using OnlineAuctionPlatform.Domain.Entities;
using OnlineAuctionPlatform.Infrastructure.Data;


namespace OnlineAuctionPlatform.Infrastructure.Repositories
{
    public class AuctionRepository : IAuctionRepository
    {
        private readonly AuctionContext _context;

        public AuctionRepository(AuctionContext context)
        {
            _context = context;
        }
        public async Task AddAsync(AuctionItem auctionItem)
        {
            await _context.AuctionItems.AddAsync(auctionItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var currentAuction = _context.AuctionItems.FirstOrDefaultAsync(a => a.Id == id).Result;
            if (currentAuction != null)
            {
                _context.AuctionItems.Remove(currentAuction);
                await _context.SaveChangesAsync();
            }            
        }

        public async Task<IEnumerable<AuctionItem>> GetAllAsync()
        {
            return await _context.AuctionItems.ToListAsync();
        }

        public async Task<AuctionItem> GetByIdAsync(Guid id)
        {
            return await _context.AuctionItems.Include(x => x.Bids).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(AuctionItem auction)
        {
            var auctionObj = _context.AuctionItems.FirstOrDefaultAsync(x => x.Id == auction.Id).Result;
            auctionObj.Description = auction.Description;
            auctionObj.EndDate = auction.EndDate;
            auctionObj.Bids = auction.Bids;

            await _context.SaveChangesAsync();
        }
    }
}
