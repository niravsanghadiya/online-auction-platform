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
        public string UserName { get; set; }
        public string Email { get; set; }     
        
        public string PasswordHash { get; set; }
        
    }
}
