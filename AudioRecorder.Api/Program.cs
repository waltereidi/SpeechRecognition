using AudioRecord.Api.DTO;
using AudioRecorder.Api.Interfaces;
using AudioRecorder.Api.Services;
using BuildingBlocks.Messaging;
using Microsoft.EntityFrameworkCore;
using SpeechRecognition.Infra.Context;


var builder = WebApplication.CreateBuilder(args);
var configuration = new ConfigurationDTO();


// Configuração do MassTransit com RabbitMQ
builder.Services.AddMessaging(cfg =>
{
    cfg.Host = configuration.RabbitMqConfig.HostName;

    cfg.Username = configuration.RabbitMqConfig.UserName;

    cfg.Password = configuration.RabbitMqConfig.Password;

    cfg.Port = configuration.RabbitMqConfig.Port;

    cfg.EnableLogging = true;
});


// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<AudioConversionService>();
builder.Services.AddScoped<RabbitMqLogService>();

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
