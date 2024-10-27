using Microsoft.EntityFrameworkCore;
using OnlineAuctionPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAuctionPlatform.Infrastructure.Data
{
    public class AuctionContext : DbContext
    {
        public AuctionContext(DbContextOptions<AuctionContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<AuctionItem> AuctionItems { get; set; }
        public DbSet<Bid> Bids { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure entity relationships and constraints

            modelBuilder.Entity<User>()
                .HasMany(u => u.AuctionItems)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);

            //modelBuilder.Entity<User>()
            //    .HasMany(u => u.Bids)
            //    .WithOne(b => b.User)
            //    .HasForeignKey(b => b.UserId);

            modelBuilder.Entity<AuctionItem>()
                .HasMany(a => a.Bids)
                .WithOne(b => b.AuctionItem)
                .HasForeignKey(b => b.AuctionItemId);
        }
    }
    
}
