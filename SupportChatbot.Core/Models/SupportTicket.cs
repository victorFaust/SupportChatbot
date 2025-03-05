using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupportChatbot.Core.Models
{
       /// <summary>
    /// Represents a support ticket in the system
    /// </summary>
    public class SupportTicket
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Subject { get; set; }

        [Required]
        [MaxLength(50)]
        public string UserId { get; set; }

        [MaxLength(50)]
        public string AssignedAgentId { get; set; }

        [Required]
        public TicketPriority Priority { get; set; }

        [Required]
        public TicketStatus Status { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? LastUpdatedAt { get; set; }
        public DateTime? ClosedAt { get; set; }

        [MaxLength(500)]
        public string Resolution { get; set; }

        // Navigation properties
        [ForeignKey("UserId")]
        public virtual SupportUser User { get; set; }

        [ForeignKey("AssignedAgentId")]
        public virtual SupportUser AssignedAgent { get; set; }

        public virtual ICollection<TicketAttachment> Attachments { get; set; }
        public virtual ICollection<ChatMessage> Messages { get; set; }
    }

}