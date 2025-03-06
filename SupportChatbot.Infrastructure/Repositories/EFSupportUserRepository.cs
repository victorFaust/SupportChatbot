using Microsoft.EntityFrameworkCore;
using SupportChatbot.Core.Models;
using SupportChatbot.Infrastructure.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportChatbot.Infrastructure.Repositories
{
    public class EFSupportUserRepository : ISupportUserRepository
    {
        private readonly ChatDbContext _context;

        public EFSupportUserRepository(ChatDbContext context)
        {
            _context = context;
        }
        public async Task<SupportUser> GetByIdAsync(string userId)
        {
        
            return await _context.SupportUsers
                .FirstOrDefaultAsync(u => u.Id == userId);
        }
    }
    
}
