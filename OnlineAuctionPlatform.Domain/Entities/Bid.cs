using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuctionPlatform.Domain.Entities
{
    public class Bid
    {
        public Guid Id { get; set; }
        public Guid AuctionItemId { get; set; }
        public decimal Amount { get; set; }
        public DateTime BidTime { get; set; }
        public Guid UserId { get; set; }
        public AuctionItem AuctionItem { get; set; }

        public virtual User User { get; set; }
        
    }
}
