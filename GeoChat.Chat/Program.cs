using GeoChat.Chat.Api.Hubs;
using GeoChat.Chat.Api.Services;
using GeoChat.Chat.Core.EventBus;
using GeoChat.Chat.Core.EventBus.EventHandlers;
using GeoChat.Chat.Core.EventBus.Events;
using GeoChat.Chat.Core.EventBus.Extensions;
using GeoChat.Chat.Core.Interfaces;
using GeoChat.Chat.Core.Services;
using GeoChat.Chat.Infra.DbAccess;
using GeoChat.Chat.Infra.EventBus.Extensions;
using GeoChat.Identity.Api.AuthExtensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.RegisterAuthServices(builder.Configuration);
builder.Services.RegisterSwaggerWithAuthInformation();
builder.Services.RegisterDbAndRepos(builder.Configuration);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSignalR(o => o.MaximumReceiveMessageSize = 102400000);

builder.Services.AddTransient<SyncCaller>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IHubNotifier, HubNotifier>();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSingleton<IEventBus, MockEventBus>();
}
else
{
    builder.Services.RegisterEventBus();
    builder.Services.RegisterEventHandlers();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{ 
    // SUBSCRIBE TO ALL THE QUEUES
    var bus = app.Services.GetService<IEventBus>();
    if (bus == null) throw new Exception("Bus is null");
    bus.Subscribe<MessageSentEvent, MessageSentEventHandler>();
    bus.Subscribe<NewAccountCreatedEvent, NewAccountCreatedEventHandler>();
    bus.Subscribe<SyncResponseEvent, SyncResponseEventHandler>();
    bus.Subscribe<NewChatCreatedEvent, NewChatCreatedEventHandler>();   

    // RESYNC THE SERVER ON STARTUP
    var syncCaller = app.Services.GetService<SyncCaller>();
    if (syncCaller == null) throw new Exception("Sync caller is null");
    syncCaller.SyncAsync().GetAwaiter().GetResult();

    // CREATE LOCATION CHAT
    var chatService = app.Services.GetService<IChatService>();
    if (chatService == null) throw new Exception("Chat service is null");
    chatService.CreateLocationChatIfNotAvailable();

    // UPDATE DATABASE
    var dbContext = app.Services.GetService<AppDbContext>();
    if (dbContext == null) throw new Exception("DbContext is null");
    dbContext.Database.Migrate();
}
app.UseCors(builder => builder
         //.AllowAnyOrigin()
         //.WithOrigins("null")
         .SetIsOriginAllowed(_ => true)
         .AllowAnyHeader()
         .AllowAnyMethod()
         .AllowCredentials()
     );


app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("/chatHub"); // TODO: change magic string?
app.Run();
