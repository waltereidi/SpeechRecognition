using Microsoft.AspNetCore.Mvc;
using SpeechRecognition.Application.Services;
using SpeechRecognition.AudioRecorder.Api.Controllers;
using SpeechRecognition.CrossCutting.Framework.Interfaces;
using static SpeechRecognition.Application.Contracts.FileStorageAggregateContract;
using SpeechRecognition.FileStorageDomain;
using AudioRecord.Api.DTO;

[Route("Upload")]
public class UploadController : BaseController
{
    private readonly IConfiguration _config;
    private readonly IApplicationService _service;
        
    public UploadController(ILogger<UploadController> logger,
        FileStorageAggregateApplicationService service , 
        IConfiguration config ) 
        : base(logger)
    {
        _service = service;
    }
    [HttpPost("Create")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody]string guid)
        => await HandleRequest(new V1.Create(new FileStorageAggregateId(Guid.Parse(guid))),
            _service.Handle);

    [HttpPost("UploadAudioFile")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UploadAudioFile([FromBody] IFormFileCollection file , string aggId)
    => await HandleRequest(new V1.UpdateFileStorage(
        new FileStorageAggregateId(Guid.Parse(aggId)),
        file.ElementAt(0).OpenReadStream(),
        file.ElementAt(0).FileName,
        new ConfigurationDTO.FileStorageConfig().RawAudioPathDir
        ),
        _service.Handle);


    //[HttpPost]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //public async Task<IActionResult> UploadFile(Guid guid, IFormFile file )
    //    => await HandleRequest(new V1.UpdateFileStorage(new FileStorageAggregateId(guid), file, originalFileInfo),
    //        _service.Handle);   

}
