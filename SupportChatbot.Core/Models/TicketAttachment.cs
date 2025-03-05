using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupportChatbot.Core.Models
{
     /// <summary>
    /// Represents an attachment to a support ticket
    /// </summary>
    public class TicketAttachment
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid TicketId { get; set; }

        [Required]
        [MaxLength(200)]
        public string FileName { get; set; }

        [Required]
        [MaxLength(100)]
        public string FileType { get; set; }

        [Required]
        public long FileSize { get; set; }

        [Required]
        [MaxLength(500)]
        public string FilePath { get; set; }

        [Required]
        public DateTime UploadedAt { get; set; }

        [Required]
        [MaxLength(50)]
        public string UploadedById { get; set; }

        // Navigation properties
        [ForeignKey("TicketId")]
        public virtual SupportTicket Ticket { get; set; }

        [ForeignKey("UploadedById")]
        public virtual SupportUser UploadedBy { get; set; }
    }
    
}