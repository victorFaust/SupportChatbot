namespace SupportChatbot.Infrastructure.Repositories.Contracts
{
        public interface ISupportTicketRepository
        {
            Task<SupportTicket> CreateTicketAsync(SupportTicket ticket);

            Task<SupportTicket> GetTicketByIdAsync(Guid ticketId);

            Task<List<SupportTicket>> GetTicketsByUserIdAsync(string userId);

            Task UpdateTicketStatusAsync(Guid ticketId, TicketStatus status);
        }
}