using AudioRecord.Api.DTO;
using AudioRecorder.Api.Services;
using BuildingBlocks.Messaging.Abstractions;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using Shared.Events;
using Shared.Events.AudioConverter;
using SpeechRecognition.Infra.Context;


[Route("Audio")]
public class AudioController : Controller
{
    private readonly AppDbContext _dbContext;
    private readonly IEventBus _eventBus;
    private readonly IConfiguration _config;
    public AudioController(AppDbContext builder, IEventBus eventBus , IConfiguration config )
    {
        _dbContext = builder;
        _eventBus = eventBus;
        _config = config;
    }
    [HttpGet("Test")]
    public async Task<string> Test()
    {
        var fsConfig = ConfigurationDTO.GetFileStorageConfig(_config);

        var pedidoCriadoEvent = new PedidoCriadoEvent();
        var audioConvert = new AudioConversionToWav16kLocalEvent()
        {
            DirectoryPath = fsConfig.RawAudioPath,
            FilePath = Path.Combine(fsConfig.RawAudioPath, "audio_20240617_123456.webm"),
            FileStorageId = Guid.NewGuid().ToString()   
        };
        await _eventBus.PublishAsync(audioConvert);
        return "ok";
    }

    //[HttpPost("Upload")]
    //public async Task<IActionResult> Upload(IFormFile audio)
    //{
    //    if (audio == null || audio.Length == 0)
    //        return BadRequest("Áudio não recebido.");


    //    var nomeArquivo = $"audio_{DateTime.Now:yyyyMMdd_HHmmss}.webm";
    //    var caminho = Path.Combine(pasta, nomeArquivo);

    //}
    //[HttpGet("Test")]
    ////public async Task<IActionResult> Test()
    ////{
    ////    _channel.BasicPublishAsync(
    ////           exchange: "",
    ////           routingKey: "WhisperSpeechRecognition",
    ////           body: System.Text.Encoding.UTF8.GetBytes("Hellow World"));

    ////    _channel.BasicPublishAsync(
    ////   exchange: "",
    ////   routingKey: "WhisperSpeechRecognition",
    ////   body: System.Text.Encoding.UTF8.GetBytes("Hellow World2"));
    ////    return Ok();
    ////}
}
