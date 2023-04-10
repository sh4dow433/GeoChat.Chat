using GeoChat.Chat.Api.Hubs;
using GeoChat.Chat.Core.EventBus;
using GeoChat.Chat.Infra.DbAccess;
using GeoChat.Chat.Infra.EventBus.Extensions;
using GeoChat.Identity.Api.AuthExtensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.RegisterAuthServices(builder.Configuration);
builder.Services.RegisterSwaggerWithAuthInformation();
builder.Services.RegisterDbAndRepos(builder.Configuration);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSignalR();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSingleton<IEventBus, MockEventBus>();
}
else
{
    builder.Services.RegisterEventBus();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("/chats"); // TODO: change magic string?
app.Run();
