using Microsoft.Extensions.Logging;
using SupportChatbot.Core.Enums;
using SupportChatbot.Core.Models;
using SupportChatbot.Infrastructure.Repositories.Contracts;

namespace SupportChatbot.Core.Services
{
    public class SupportChatService
    {
        private readonly IChatMessageRepository _messageRepository;
        private readonly ISupportTicketRepository _ticketRepository;
        private readonly ISupportUserRepository _userRepository;
        private readonly ILogger<SupportChatService> _logger;

        public SupportChatService(
            IChatMessageRepository messageRepository,
            ISupportTicketRepository ticketRepository,
            ISupportUserRepository userRepository,
            ILogger<SupportChatService> logger)
        {
            _messageRepository = messageRepository;
            _ticketRepository = ticketRepository;
            _userRepository = userRepository;
            _logger = logger;
        }

        /// <summary>
        /// Create a new support ticket
        /// </summary>
        public async Task<SupportTicket> CreateSupportTicketAsync(
            string userId, 
            string subject, 
            string initialMessage)
        {
            // Validate user exists
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                _logger.LogWarning("Attempt to create ticket for non-existent user {UserId}", userId);
                throw new InvalidOperationException($"User with ID {userId} not found");
            }

            // Check user's active ticket count
            var activeTicketsCount = await _ticketRepository.CountActiveTicketsForUserAsync(userId);
            if (activeTicketsCount >= 5) // Example limit
            {
                _logger.LogWarning("User {UserId} exceeded maximum active tickets limit", userId);
                throw new InvalidOperationException("Maximum number of active tickets exceeded");
            }

            // Create ticket
            var ticket = new SupportTicket
            {
                Id = Guid.NewGuid(),
                Subject = subject,
                UserId = userId,
                Priority = TicketPriority.Medium,
                Status = TicketStatus.New,
                CreatedAt = DateTime.UtcNow,
                Messages = new List<ChatMessage>()
            };

            // Create initial message
            var message = new ChatMessage
            {
                Id = Guid.NewGuid(),
                TicketId = ticket.Id,
                SenderId = userId,
                Content = initialMessage,
                Timestamp = DateTime.UtcNow,
                Type = MessageType.UserMessage,
                Status = MessageStatus.Sent
            };

            ticket.Messages.Add(message);

            // Save ticket and message
            await _ticketRepository.CreateTicketAsync(ticket);
            await _messageRepository.AddAsync(message);

            _logger.LogInformation(
                "Support ticket created. TicketId: {TicketId}, UserId: {UserId}, Subject: {Subject}", 
                ticket.Id, 
                userId, 
                subject
            );

            return ticket;
        }

        /// <summary>
        /// Send a message to an existing ticket
        /// </summary>
        public async Task<ChatMessage> SendMessageAsync(
            Guid ticketId, 
            string senderId, 
            string content, 
            MessageType messageType)
        {
            // Validate ticket exists
            var ticket = await _ticketRepository.GetTicketByIdAsync(ticketId);
            if (ticket == null)
            {
                _logger.LogWarning("Attempt to send message to non-existent ticket {TicketId}", ticketId);
                throw new InvalidOperationException($"Ticket with ID {ticketId} not found");
            }

            // Validate sender exists
            var sender = await _userRepository.GetByIdAsync(senderId);
            if (sender == null)
            {
                _logger.LogWarning("Attempt to send message by non-existent user {SenderId}", senderId);
                throw new InvalidOperationException($"User with ID {senderId} not found");
            }

            // Create message
            var message = new ChatMessage
            {
                Id = Guid.NewGuid(),
                TicketId = ticketId,
                SenderId = senderId,
                Content = content,
                Timestamp = DateTime.UtcNow,
                Type = messageType,
                Status = MessageStatus.Sent
            };

            // Update ticket last updated time
            ticket.LastUpdatedAt = DateTime.UtcNow;

            // Determine appropriate ticket status based on sender type
            if (messageType == MessageType.SupportAgentMessage)
            {
                // If agent responds, change status to Open or InProgress
                ticket.Status = ticket.Status == TicketStatus.New 
                    ? TicketStatus.Open 
                    : TicketStatus.Pending;
            }

            // Save message
            await _messageRepository.AddAsync(message);
            await _ticketRepository.CreateTicketAsync(ticket);

            _logger.LogInformation(
                "Message sent. TicketId: {TicketId}, SenderId: {SenderId}, MessageType: {MessageType}", 
                ticketId, 
                senderId, 
                messageType
            );

            return message;
        }

        /// <summary>
        /// Retrieve messages for a specific ticket
        /// </summary>
        public async Task<List<ChatMessage>> GetTicketMessagesAsync(Guid ticketId)
        {
            // Validate ticket exists
            var ticket = await _ticketRepository.GetTicketByIdAsync(ticketId);
            if (ticket == null)
            {
                _logger.LogWarning("Attempt to retrieve messages for non-existent ticket {TicketId}", ticketId);
                throw new InvalidOperationException($"Ticket with ID {ticketId} not found");
            }

            // Retrieve messages
            return await _messageRepository.GetMessagesByTicketIdAsync(ticketId);
        }

        /// <summary>
        /// Close a support ticket
        /// </summary>
        public async Task CloseTicketAsync(Guid ticketId, string closedByUserId, string resolution)
        {
            // Validate ticket exists
            var ticket = await _ticketRepository.GetTicketByIdAsync(ticketId);
            if (ticket == null)
            {
                _logger.LogWarning("Attempt to close non-existent ticket {TicketId}", ticketId);
                throw new InvalidOperationException($"Ticket with ID {ticketId} not found");
            }

            // Update ticket status
            ticket.Status = TicketStatus.Closed;
            ticket.ClosedAt = DateTime.UtcNow;
            ticket.Resolution = resolution;

            await _ticketRepository.UpdateTicket(ticket);

            _logger.LogInformation(
                "Ticket closed. TicketId: {TicketId}, ClosedBy: {ClosedByUserId}", 
                ticketId, 
                closedByUserId
            );
        }
    }
}