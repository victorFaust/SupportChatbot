using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupportChatbot.Core.Models
{
     /// <summary>
    /// Represents a message within a support ticket
    /// </summary>
    public class ChatMessage
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid TicketId { get; set; }

        [Required]
        [MaxLength(50)]
        public string SenderId { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Content { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        [Required]
        public MessageType Type { get; set; }

        [Required]
        public MessageStatus Status { get; set; }

        // Navigation properties
        [ForeignKey("TicketId")]
        public virtual SupportTicket Ticket { get; set; }

        [ForeignKey("SenderId")]
        public virtual SupportUser Sender { get; set; }
    }

}