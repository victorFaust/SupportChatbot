namespace SupportChatbot.Infrastructure.Repositories.Contracts
{

        public interface IChatMessageRepository
        {
            Task AddMessageAsync(ChatMessage message);
            
            Task<List<ChatMessage>> GetMessagesByTicketIdAsync(Guid ticketId);

            Task UpdateMessageStatusAsync(Guid messageId, MessageStatus status);


        }
}
