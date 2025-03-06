using SupportChatbot.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupportChatbot.Core.Models
{
    /// <summary>
    /// Represents a user in the support system
    /// </summary>
    public class SupportUser
    {
        [Key]
        [MaxLength(50)] // Ensure the length matches SupportTicket.UserId
        public string Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Username { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(50)]
        public string PhoneNumber { get; set; }

        public UserRole Role { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }
    }

}
   

 
   

   

    

