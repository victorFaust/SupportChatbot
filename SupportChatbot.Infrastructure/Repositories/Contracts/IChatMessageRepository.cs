using SupportChatbot.Core.Enums;
using SupportChatbot.Core.Models;

namespace SupportChatbot.Infrastructure.Repositories.Contracts
{

        public interface IChatMessageRepository
        {
            Task AddAsync(ChatMessage message);
            
            Task<List<ChatMessage>> GetMessagesByTicketIdAsync(Guid ticketId);

            Task UpdateMessageStatusAsync(Guid messageId, MessageStatus status);


        }
}
