using ChattingSystem.Data;
using ChattingSystem.DataService;
using ChattingSystem.Hubs;
using ChattingSystem.Repositories.Implements;
using ChattingSystem.Repositories.Interfaces;
using ChattingSystem.Services.Implements;
using ChattingSystem.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(
    e =>
    {
        e.CustomSchemaIds(type => type.FullName.Replace("+", "."));
    }
    );

builder.Services.AddSignalR();
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("reactApp", builder =>
    {
        builder
        //.AllowAnyOrigin()
        .WithOrigins("http://localhost:3000")
        .AllowCredentials()
        .AllowAnyHeader()
        .AllowAnyMethod();
        //.AllowCredentials();
    });
});
builder.Services.AddSingleton<TempDb>();
builder.Services.AddTransient<DapperDbContext>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IParticipantRepository, ParticipantRepository>();
builder.Services.AddScoped<IConversationRepository, ConversationRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IConversationGroupRepository, ConversationGroupRepository>();
builder.Services.AddScoped<IGroupUserRepository, GroupUserRepository>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IDirectMessageRepository, DirectMessageRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IConversationService, ConversationService>();
builder.Services.AddScoped<IParticipantService, ParticipantService>();
builder.Services.AddScoped<IMessageService, MessageService>();
//builder.Services.AddScoped<IChattingService, ChattingService>();
builder.Services.AddScoped<IConversationGroupService, ConversationGroupService>();
builder.Services.AddScoped<IGroupUserService,  GroupUserService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IDirectMessageService, DirectMessageService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors("reactApp");
app.MapHub<ChatHub>("/chat");
app.Run();
