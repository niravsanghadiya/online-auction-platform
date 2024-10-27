using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuctionPlatform.Domain.Entities
{
    public class AuctionItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal StartingBid { get; set; }
        public DateTime EndDate { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<Bid> Bids { get; set; }

        public AuctionItem()
        {
            Bids = new List<Bid>();
        }
    }
}
