using SupportChatbot.Core.Models;

namespace SupportChatbot.Infrastructure.Repositories.Contracts
{
   

    public interface ISupportUserRepository
    {
        Task<SupportUser> GetByIdAsync(string userId);
    }

}