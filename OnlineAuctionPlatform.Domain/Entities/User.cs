using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace OnlineAuctionPlatform.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
       
        public List<AuctionItem> AuctionItems { get; set; }
        public List<Bid> Bids { get; set; }

        public User()
        {
            AuctionItems = new List<AuctionItem>();
            Bids = new List<Bid>();
        }
    }
}
