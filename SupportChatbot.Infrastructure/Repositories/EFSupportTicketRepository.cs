  namespace SupportChatbot.Infrastructure.Repositories
  {
    
  
  public class EFSupportTicketRepository : ISupportTicketRepository
    {
        private readonly ChatDbContext _context;

        public EFSupportTicketRepository(ChatDbContext context)
        {
            _context = context;
        }

        public async Task<SupportTicket> CreateTicketAsync(SupportTicket ticket)
        {
            _context.SupportTickets.Add(ticket);
            await _context.SaveChangesAsync();
            return ticket;
        }

        public async Task<SupportTicket> GetTicketByIdAsync(Guid ticketId)
        {
            return await _context.SupportTickets
                .Include(t => t.Messages)
                .FirstOrDefaultAsync(t => t.Id == ticketId);
        }

        public async Task<List<SupportTicket>> GetTicketsByUserIdAsync(string userId)
        {
            return await _context.SupportTickets
                .Where(t => t.UserId == userId)
                .Include(t => t.Messages)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task UpdateTicketStatusAsync(Guid ticketId, TicketStatus status)
        {
            var ticket = await _context.SupportTickets.FindAsync(ticketId);
            if (ticket != null)
            {
                ticket.Status = status;
                if (status == TicketStatus.Closed)
                {
                    ticket.ClosedAt = DateTime.UtcNow;
                }
                await _context.SaveChangesAsync();
            }
        }
    }

  }