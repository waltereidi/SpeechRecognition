using SpeechRecognition.Infra.Context;

namespace AudioRecorder.Api.Services
{
    public class AudioConversionService
    {
        private readonly AppDbContext _context;
        public AudioConversionService(AppDbContext context )
        {
            _context = context;
        }

    }
}
