using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace SpeechRecognition.AudioRecorder.Api.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly ILogger<BaseController> _logger;
        public BaseController(ILogger<BaseController> logger)
        {
            _logger = logger;

        }
        protected async Task<IActionResult> HandleRequest<T>(T request, Func<T, Task<object?>> handler) where T : class
        {
            try
            {
                var result = await handler(request);
                return Ok(result);
            }
            //Throw this exception when is required to logoff from application
            catch (Exception e) when (e.GetType().Name.Contains("Unauthorized") || e.GetType().Name.Contains("Authentication"))
            {
                return Unauthorized(e.Message);
            }
            //Throw this exception when an unexpected empty respoonse is required
            catch (Exception e) when (e.GetType().Name.Contains("NotFound"))
            {
                _logger.LogError(e.Message);
                return NotFound(e.Message);
            }
            //Throw this exception when authentication attempt fails
            catch (Exception e) when (e.GetType().Name.Contains("Invalid"))
            {
                return BadRequest(e.Message);
            }
            //Unhandled exceptions
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
