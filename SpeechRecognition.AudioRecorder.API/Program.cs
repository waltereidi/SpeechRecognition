using AudioRecord.Api.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using SpeechRecognition.Application.Services;
using SpeechRecognition.AudioRecorder.Api.ExtensionMethod;
using SpeechRecognition.CrossCutting.BuildingBlocks.Messaging;
using SpeechRecognition.CrossCutting.Framework.Interfaces;
using SpeechRecognition.FileStorageDomain.Interfaces;
using SpeechRecognition.Infra.Context;
using SpeechRecognition.Infra.Repositories.Aggregates;
using SpeechRecognition.Infra.UoW;


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
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddScoped<IUnitOfWork, PostgresqlUnitOfWork>();
builder.Services.AddScoped<IFileStorageAggregateRepository, FileStorageAggregateRepository>();
builder.Services.AddScoped<FileStorageAggregateApplicationService>();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo());
});


var app = builder.Build();
app.EnsureDatabase();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "SpeechRecognition API v1");
        options.RoutePrefix = string.Empty; // abre na raiz: https://localhost:xxxx/
    });
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllers();

app.MapRazorPages()
   .WithStaticAssets();
app.UseCors();
app.Run();

