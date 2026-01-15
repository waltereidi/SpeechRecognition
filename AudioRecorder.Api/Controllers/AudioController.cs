using AudioConverter.Services;
using AudioRecorder.Api.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using SpeechRecognition.Infra.Context;
using WhisperSpeechRecognition.Interfaces;
using WhisperSpeechRecognition.Service;

[Route("Audio")]
public class AudioController : Controller
{
    private readonly IChannel _channel;
    public AudioController(AppDbContext builder)
    {
        _channel = RabbitMqConnectionSingleton.CreateChannelAsync().Result;
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
