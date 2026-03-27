using AudioRecord.Api.DTO;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using SpeechRecognition.Application.Interfaces;
using SpeechRecognition.Application.Models;
using SpeechRecognition.Application.Services;
using SpeechRecognition.AudioRecorder.Api.ExtensionMethod;
using SpeechRecognition.AudioRecorder.Api.Handler;
using SpeechRecognition.AudioRecorder.Api.Interfaces;
using SpeechRecognition.CrossCutting.BuildingBlocks.Messaging;
using SpeechRecognition.CrossCutting.BuildingBlocks.Messaging.Abstractions;
using SpeechRecognition.CrossCutting.BuildingBlocks.Messaging.MassTransit;
using SpeechRecognition.CrossCutting.Framework.Interfaces;
using SpeechRecognition.CrossCutting.IoC;
using SpeechRecognition.CrossCutting.IoC.DataBase;
using SpeechRecognition.CrossCutting.Shared.Events.AudioRecorderApi;
using SpeechRecognition.CrossCutting.Shared.Events.Generic;
using SpeechRecognition.Infra.Context;
using SpeechRecognition.Infra.Repositories.Aggregates;
using SpeechRecognition.Infra.UoW;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);


#region Environment Config
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
#endregion

#region RabbitMQ
// Registra o handler de eventos
builder.Services.AddIntegrationEventHandler<SaveAudioConversionSuccessEvent, SaveAudioConversionSuccessHandler>();
builder.Services.AddIntegrationEventHandler<SaveAudioTranslationSuccessEvent, SaveAudioTranslationSuccessHandler>();
builder.Services.AddIntegrationEventHandler<ErrorLogEvent , RabbitMqLogHandler>();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<GenericConsumer<SaveAudioConversionSuccessEvent>>();
    x.AddConsumer<GenericConsumer<SaveAudioTranslationSuccessEvent>>();
    x.AddConsumer<GenericConsumer<ErrorLogEvent>>();


    x.UsingRabbitMq((context, cfg) =>
    {
        var rabbitMqConfig = ConfigurationDTO.GetRabbitMqConfig(configuration);
        cfg.Host(rabbitMqConfig.HostName, rabbitMqConfig.Port, "/", hostCfg =>
        {
            hostCfg.Username(rabbitMqConfig.UserName);
            hostCfg.Password(rabbitMqConfig.Password);
        });
        // Fila única para consumo
        cfg.ReceiveEndpoint("audio-recorder-queue", endpointCfg =>
        {
            endpointCfg.ConfigureConsumer<GenericConsumer<SaveAudioConversionSuccessEvent>>(context);
            endpointCfg.ConfigureConsumer<GenericConsumer<SaveAudioTranslationSuccessEvent>>(context);
            endpointCfg.ConfigureConsumer<GenericConsumer<ErrorLogEvent>>(context);
        });
    });
});

// Registra o adaptador que expõe IEventBus usando a infra do MassTransit
builder.Services.AddScoped<IEventBus, MassTransitEventBus>();
#endregion
#region Web
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();



builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // 🔥 Resolve loop de referência
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;

        // 🔥 Aumenta profundidade máxima (se necessário)
        options.JsonSerializerOptions.MaxDepth = 32;

        // (opcional) melhora saída
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Speech Recognition API",
        Version = "File Version: v1.0.0"
    });
});
#endregion

#region DataBase
    PostgreSQLIoC.AddIoC(builder.Services, configuration);
//FireStoreIoC.AddIoC(builder.Services, configuration);
#endregion
#region Application 
    RegisterIoC.AddIoC(builder.Services, configuration);
#endregion


var app = builder.Build();
app.EnsureDatabase(); //PostgreSQL run migrations

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllers();

app.MapRazorPages()
   .WithStaticAssets();

app.UseCors();
app.Run();

