using Microsoft.EntityFrameworkCore;
using SpeechRecognition.Infra.Context;
using BuildingBlocks.Messaging;


var builder = WebApplication.CreateBuilder(args);

// Configuração do MassTransit com RabbitMQ
builder.Services.AddMessaging(config =>
{
    config.Host = "localhost";
    config.Username = "admin";
    config.Password = "admin";
    config.Port = 5672;
    config.EnableLogging = true;
});


// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllers();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
