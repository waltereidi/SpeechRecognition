using AudioRecord.Api.DTO;
using AudioRecorder.Api.Interfaces;
using AudioRecorder.Api.Services;
using BuildingBlocks.Messaging;
using Microsoft.EntityFrameworkCore;
using SpeechRecognition.Infra.Context;


var builder = WebApplication.CreateBuilder(args);

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
                ?? "Production";

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile(
        $"appsettings.{environment}.json",
        optional: true,
        reloadOnChange: true)
    .AddJsonFile(
        "Properties/launchSettings.json",
        optional: true,
        reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();
builder.Configuration.AddConfiguration(configuration);


// Configuração do MassTransit com RabbitMQ
builder.Services.AddMessaging(cfg =>
{
    var rabbitMqConfig = ConfigurationDTO.GetRabbitMqConfig(configuration);
    cfg.Host = rabbitMqConfig.HostName;

    cfg.Username = rabbitMqConfig.UserName;

    cfg.Password = rabbitMqConfig.Password;

    cfg.Port = rabbitMqConfig.Port;

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
