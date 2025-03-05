using Microsoft.EntityFrameworkCore;
using SupportChatbot.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupportChatbot.Infrastructure.Repositories
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options) { }

        public DbSet<SupportTicket> SupportTickets { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SupportTicket>()
                .HasMany(t => t.Messages)
                .WithOne()
                .HasForeignKey("TicketId");

            modelBuilder.Entity<ChatMessage>()
                .HasIndex(m => m.Timestamp);
        }
    }

   

  
}