﻿using SupportChatbot.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportChatbot.Core.DTOs
{
    public class SendMessageRequest
    {
        public string SenderId { get; set; }
        public string Content { get; set; }
        public MessageType MessageType { get; set; }
    }
}
