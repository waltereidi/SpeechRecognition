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
    private readonly IConfiguration _config;
    public AudioController(  IConfiguration config )
    {
        _config = config;
    }
    [HttpGet("Test")]
    public async Task<string> Test()
    {
        var fsConfig = ConfigurationDTO.GetFileStorageConfig(_config);

        var pedidoCriadoEvent = new PedidoCriadoEvent();

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
