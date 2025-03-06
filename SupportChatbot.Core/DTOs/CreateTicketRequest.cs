using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportChatbot.Core.DTOs
{
    // DTOs for API requests
    public class CreateTicketRequest
    {
        public string UserId { get; set; }
        public string Subject { get; set; }
        public string InitialMessage { get; set; }
    }
}
