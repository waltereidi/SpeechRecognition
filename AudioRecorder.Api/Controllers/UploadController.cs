using AudioRecorder.Api.Contracts;
using AudioRecorder.Api.Controllers;
using AudioRecorder.Api.Services;
using Microsoft.AspNetCore.Mvc;


[Route("Upload")]
public class UploadController : BaseController
{
    private readonly IConfiguration _config;
    private readonly AudioConversionService _service;
        
    public UploadController(ILogger<UploadController> logger, 
        AudioConversionService service , 
        IConfiguration config ) 
        : base(logger)
    {
        _service = service;
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UploadAudio( UploadContracts.Request.AudioUpload request )
        => await HandleRequest( request, _service.Handle);


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
