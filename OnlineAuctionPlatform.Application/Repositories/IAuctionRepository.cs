using OnlineAuctionPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuctionPlatform.Application.Repositories
{
    public interface IAuctionRepository
    {
        Task<IEnumerable<AuctionItem>> GetAllAsync();
        Task<AuctionItem> GetByIdAsync(Guid id);
        Task AddAsync(AuctionItem auctionItem);
        Task UpdateAsync(AuctionItem auctionItem);
        Task DeleteAsync(Guid id);
    }
}
