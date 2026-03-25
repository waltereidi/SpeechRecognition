using MassTransit;
using Microsoft.AspNetCore.Mvc;
using SpeechRecognition.Application.Interfaces;
using SpeechRecognition.Application.Models;

namespace SpeechRecognition.AudioRecorder.Api.Controllers
{
    [Route("api/AggregateQuery/[action]")]
    [ApiController]
    public class AggregateQueryController : BaseController
    {
        private readonly Queries _queries;
        public AggregateQueryController(ILogger<BaseController> logger ,IFileStorageAggregateRepository repository ) : base(logger)
        {
            _queries = new Queries(repository);
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(string id)
            =>await HandleQuery(() => _queries.GetAggregate(new(new(id))));

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll(int page , int size)
            => await HandleQuery(() => _queries.GetAllAggregates(new(page,size)));


    }
}
