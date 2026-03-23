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
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(QueryModels.GetFileStorageAggregate request)
                =>await HandleQuery(() => _queries.GetAggregate(request.id));
    }
}
