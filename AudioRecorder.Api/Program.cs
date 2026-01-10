using AudioRecorder.Api.DTO;
using AudioRecorder.Api.Services;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

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

await channel.ExchangeDeclareAsync(exchange: "LivrosExpo", type: ExchangeType.Fanout);
await channel.ExchangeDeclareAsync(exchange: "FileStorage", type: ExchangeType.Fanout);

app.Run();
