using MicroOrm.Dapper.Repositories;
using SupportChatbot.Core.Enums;
using SupportChatbot.Core.Models;

namespace SupportChatbot.Infrastructure.Repositories.Contracts
{
    public interface ISupportTicketRepository
    {
        Task<SupportTicket> CreateTicketAsync(SupportTicket ticket);
        Task<SupportTicket> GetTicketByIdAsync(Guid ticketId);
        Task<List<SupportTicket>> GetTicketsByUserIdAsync(string userId);
        Task UpdateTicketStatusAsync(Guid ticketId, TicketStatus status);
        Task<int> CountActiveTicketsForUserAsync(string userId);
        Task<SupportTicket> UpdateTicket(SupportTicket ticket);
    }
}