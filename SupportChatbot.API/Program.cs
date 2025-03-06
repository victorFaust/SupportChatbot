using Microsoft.EntityFrameworkCore;
using SupportChatbot.Core;
using SupportChatbot.Infrastructure.Repositories;
using Serilog;
using SupportChatbot.Infrastructure.Logging;
using SupportChatbot.Core.Services;
using SupportChatbot.Infrastructure.Repositories.Contracts;

Log.Logger = LoggerConfig.CreateLoggerConfiguration().CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ChatDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories
builder.Services.AddScoped<IChatMessageRepository, EFChatMessageRepository>();
builder.Services.AddScoped<ISupportTicketRepository, EFSupportTicketRepository>();
builder.Services.AddScoped<ISupportUserRepository, EFSupportUserRepository>();

builder.Logging.ClearProviders();
builder.Host.UseSerilog();

// Register main service
builder.Services.AddScoped<SupportChatService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSerilogRequestLogging(options =>
    {
        // Customize the request logging
        options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";
        options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
        {
            diagnosticContext.Set("User", httpContext.User.Identity?.Name ?? "anonymous");
            diagnosticContext.Set("Host", httpContext.Request.Host.Value);
            diagnosticContext.Set("UserAgent", httpContext.Request.Headers["User-Agent"].FirstOrDefault());
        };
    });
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
