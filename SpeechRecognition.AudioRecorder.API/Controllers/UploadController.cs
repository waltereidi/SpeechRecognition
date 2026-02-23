using Microsoft.AspNetCore.Mvc;
using SpeechRecognition.Application.Services;
using SpeechRecognition.AudioRecorder.Api.Controllers;
using SpeechRecognition.CrossCutting.Framework.Interfaces;
using static SpeechRecognition.Application.Contracts.FileStorageAggregateContract;
using SpeechRecognition.FileStorageDomain;
using AudioRecord.Api.DTO;

[Route("api/Upload/[action]")]
[ApiController]
public class UploadController : BaseController
{
    private readonly IConfiguration _config;
    private readonly IApplicationService _service;
        
    public UploadController(ILogger<UploadController> logger,
        FileStorageAggregateApplicationService service , 
        IConfiguration config ) 
        : base(logger)
    {
        _config = config;
        _service = service;
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromForm]string guid)
        => await HandleRequest(new V1.Create(new FileStorageAggregateId(Guid.Parse(guid))),
            _service.Handle);

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadAudioFile(
    [FromForm] IFormFileCollection file,
    [FromForm] string aggId)
    => await HandleRequest(new V1.UpdateFileStorage(
        new FileStorageAggregateId(Guid.Parse(aggId)),
        file.ElementAt(0).OpenReadStream(),
        file.ElementAt(0).FileName,
        ConfigurationDTO.GetFileStorageConfig(_config).RawAudioPathDir
        ),
        _service.Handle);

    //[HttpPost]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //public async Task<IActionResult> UploadFile(Guid guid, IFormFile file )
    //    => await HandleRequest(new V1.UpdateFileStorage(new FileStorageAggregateId(guid), file, originalFileInfo),
    //        _service.Handle);   

}
