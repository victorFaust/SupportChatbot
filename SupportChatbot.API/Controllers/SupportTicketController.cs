using Microsoft.AspNetCore.Mvc;
using SupportChatbot.Core;
using SupportChatbot.Core.DTOs;
using SupportChatbot.Core.Enums;
using SupportChatbot.Core.Models;
using SupportChatbot.Core.Services;
using System;
using System.Threading.Tasks;

namespace SupportChatbot.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupportTicketController : ControllerBase
    {
        private readonly SupportChatService _chatService;

        public SupportTicketController(SupportChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<SupportTicket>> CreateTicket(
            [FromBody] CreateTicketRequest request)
        {
            var ticket = await _chatService.CreateSupportTicketAsync(
                request.UserId,
                request.Subject,
                request.InitialMessage);

            return CreatedAtAction(
                nameof(GetTicketMessages),
                new { ticketId = ticket.Id },
                ticket);
        }

        [HttpPost("{ticketId}/message")]
        public async Task<ActionResult<ChatMessage>> SendMessage(
            Guid ticketId,
            [FromBody] SendMessageRequest request)
        {
            var message = await _chatService.SendMessageAsync(
                ticketId,
                request.SenderId,
                request.Content,
                request.MessageType);

            return Ok(message);
        }

        [HttpGet("{ticketId}/messages")]
        public async Task<ActionResult<List<ChatMessage>>> GetTicketMessages(Guid ticketId)
        {
            var messages = await _chatService.GetTicketMessagesAsync(ticketId);
            return Ok(messages);
        }
    }

}