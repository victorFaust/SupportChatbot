namespace SupportChatbot.Core.Enums
{
    /// <summary>
        /// Defines the priority levels for support tickets
        /// </summary>
    public enum TicketPriority
    {
        Low,
        Medium,
        High,
        Critical
    }
    /// <summary>
    /// Represents the current status of a support ticket
    /// </summary>
    public enum TicketStatus
    {
        New,           // Newly created ticket
        Open,          // Ticket is actively being worked on
        Pending,       // Waiting for additional information
        Escalated,     // Moved to higher support level
        Resolved,      // Solution provided, awaiting user confirmation
        Closed         // Ticket is completed and closed
    }

    /// <summary>
    /// Defines the type of message in the chat
    /// </summary>
    public enum MessageType
    {
        UserMessage,           // Message from the user
        SupportAgentMessage,   // Message from support agent
        SystemNotification,    // Automated system message
        Internal               // Internal communication between agents
    }

    /// <summary>
    /// Represents the current status of a message
    /// </summary>
    public enum MessageStatus
    {
        Sent,        // Message has been sent
        Delivered,   // Message has been delivered to recipient
        Read,        // Message has been read
        Failed       // Message delivery failed
    }

      /// <summary>
    /// Defines user roles in the support system
    /// </summary>
    public enum UserRole
    {
        User,           // Regular user submitting tickets
        SupportAgent,   // First-line support
        SupportManager, // Escalation and management
        Administrator   // System administrator
    }

}