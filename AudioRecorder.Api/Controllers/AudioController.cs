using AudioRecorder.Api.Services;
using BuildingBlocks.Messaging.Abstractions;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using Shared.Events;
using SpeechRecognition.Infra.Context;


[Route("Audio")]
public class AudioController : Controller
{
    private readonly AppDbContext _dbContext;
    private readonly IEventBus _eventBus;
    public AudioController(AppDbContext builder, IEventBus eventBus)
    {
        _dbContext = builder;
        _eventBus = eventBus;
    }
    [HttpGet("Test")]
    public async Task<string> Test()
    {

        var pedidoCriadoEvent = new PedidoCriadoEvent();
        var audioConvert = new AudioConversionToWav16kLocalEvent();
        await _eventBus.PublishAsync(audioConvert);
        return "ok";
    }

    //[HttpPost("Upload")]
    //public async Task<IActionResult> Upload(IFormFile audio)
    //{
        

    //    if (audio == null || audio.Length == 0)
    //        return BadRequest("Áudio não recebido.");

    //    var pasta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "audios");

    //    if (!Directory.Exists(pasta))
    //        Directory.CreateDirectory(pasta);

    //    var nomeArquivo = $"audio_{DateTime.Now:yyyyMMdd_HHmmss}.webm";
    //    var caminho = Path.Combine(pasta, nomeArquivo);

    //    using (var stream = new FileStream(caminho, FileMode.Create))
    //    {
    //        await audio.CopyToAsync(stream);
    //        var service =  new AudioConverterService(new DirectoryInfo("C:\\Users\\walte\\source\\repos\\SpeechRecognition"), new FileInfo(stream.Name));
    //        var file = await service.ConvertToWavMono16kHz();
    //        var texto = await _model.GetTranslation(file.OpenRead());
    //        return Ok(new { texto = texto });
    //    }
    //}
    //[HttpGet("Test")]
    //public async Task<IActionResult> Test()
    //{
    //    _channel.BasicPublishAsync(
    //           exchange: "",
    //           routingKey: "WhisperSpeechRecognition",
    //           body: System.Text.Encoding.UTF8.GetBytes("Hellow World"));

    //    _channel.BasicPublishAsync(
    //   exchange: "",
    //   routingKey: "WhisperSpeechRecognition",
    //   body: System.Text.Encoding.UTF8.GetBytes("Hellow World2"));
    //    return Ok();
    //}
}
