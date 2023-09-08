using EmailSend.Service;
using EmailSend.settings;
using Entity.Entity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var conectionString = builder.Configuration.GetConnectionString("Emaill");
builder.Services.AddDbContextPool<EntityDbcontext>(option => option.UseSqlServer(conectionString));
builder.Configuration.AddJsonFile("appsettings.json");
builder.Services.Configure<EmaillSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddTransient<IEmailService, EmailService>();

builder.Services.AddControllers();
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

app.Run();
