using AudioRecorder.Api.Services;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SpeechRecognition.Infra.Context;

var builder = WebApplication.CreateBuilder(args);

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



//using var connection = await factory.CreateConnectionAsync();
var channel = await RabbitMqConnectionSingleton.CreateChannelAsync();

await channel.ExchangeDeclareAsync(exchange: "WhisperSpeechRecognition", type: ExchangeType.Fanout);
await channel.QueueDeclareAsync("WhisperSpeechRecognition", false, false, false, null);

var consumer = new AsyncEventingBasicConsumer(channel);


consumer.ReceivedAsync += async (ch, ea) =>
{
    var body = ea.Body.ToArray();
    
    Thread.Sleep(5000);

    await channel.BasicAckAsync(ea.DeliveryTag, false);
};
// this consumer tag identifies the subscription
// when it has to be cancelled
string consumerTag = await channel.BasicConsumeAsync("WhisperSpeechRecognition", false, consumer);

app.Run();
