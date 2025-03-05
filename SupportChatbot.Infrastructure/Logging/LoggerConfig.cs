using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using System;
using System.IO;

namespace SupportChatbot.Infrastructure.Logging
{
    /// <summary>
    /// Centralized logging configuration for the application
    /// </summary>
    public static class LoggerConfig
    {
        /// <summary>
        /// Configures and creates a Serilog logger with multiple sinks and enrichers
        /// </summary>
        /// <param name="applicationName">Name of the application for log contexting</param>
        /// <returns>Configured Serilog Logger configuration</returns>
        public static LoggerConfiguration CreateLoggerConfiguration(string applicationName = "SupportChatbot")
        {
            // Ensure logs directory exists
            var logDirectory = Path.Combine(AppContext.BaseDirectory, "logs");
            Directory.CreateDirectory(logDirectory);

            return new LoggerConfiguration()
                // Set the minimum log level
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)

                // Add enrichers for additional context
                .Enrich.WithExceptionDetails()
                .Enrich.FromLogContext()
                .Enrich.WithEnvironmentName()
                .Enrich.WithMachineName()
                .Enrich.WithProcessId()
                .Enrich.WithThreadId()
                .Enrich.WithProperty("Application", applicationName)

                // Console sink for development
                .WriteTo.Console(
                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")

                // File sink for persistent logging
                .WriteTo.File(
                    path: Path.Combine(logDirectory, "log-.txt"),
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 7,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")

                // Optional: Conditional log filtering
                .Filter.ByExcluding(logEvent => 
                    logEvent.Properties.TryGetValue("SourceContext", out var value) && 
                    value.ToString().Contains("Microsoft.AspNetCore.Hosting.Diagnostics"));
        }
    }

    /// <summary>
    /// Extension methods for logging
    /// </summary>
    public static class LoggingExtensions
    {
        /// <summary>
        /// Logs an audit event with structured data
        /// </summary>
        public static void LogAuditEvent(
            this ILogger logger, 
            string eventName, 
            string userId, 
            string description, 
            object additionalInfo = null)
        {
            logger.Information(
                "Audit Event: {EventName} - User: {UserId} - Description: {Description} {AdditionalInfo}", 
                eventName, 
                userId, 
                description, 
                additionalInfo);
        }

        /// <summary>
        /// Logs a support ticket event with detailed context
        /// </summary>
        public static void LogTicketEvent(
            this ILogger logger, 
            string ticketId, 
            string userId, 
            string eventType, 
            string description)
        {
            logger.Information(
                "Ticket Event: {TicketId} - User: {UserId} - Type: {EventType} - {Description}", 
                ticketId, 
                userId, 
                eventType, 
                description);
        }
    }
}