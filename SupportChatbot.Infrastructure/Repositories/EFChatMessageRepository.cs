using Microsoft.EntityFrameworkCore;
using SupportChatbot.Core.Enums;
using SupportChatbot.Core.Models;
using SupportChatbot.Infrastructure.Repositories.Contracts;

namespace SupportChatbot.Infrastructure.Repositories
{
    


 public class EFChatMessageRepository : IChatMessageRepository
    {
        private readonly ChatDbContext _context;

        public EFChatMessageRepository(ChatDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ChatMessage message)
        {
            _context.ChatMessages.Add(message);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ChatMessage>> GetMessagesByTicketIdAsync(Guid ticketId)
        {
            return await _context.ChatMessages
                .Where(m => m.Id == ticketId)
                .OrderBy(m => m.Timestamp)
                .ToListAsync();
        }

        public async Task UpdateMessageStatusAsync(Guid messageId, MessageStatus status)
        {
            var message = await _context.ChatMessages.FindAsync(messageId);
            if (message != null)
            {
                message.Status = status;
                await _context.SaveChangesAsync();
            }
        }
    }

}