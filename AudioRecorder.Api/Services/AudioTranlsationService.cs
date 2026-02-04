using Shared.Events.AudioRecorderApi;
using SpeechRecognition.Infra.Context;

namespace AudioRecorder.Api.Services
{
    public class AudioTranlsationService
    {
        private readonly AppDbContext _appDbContext;
        public AudioTranlsationService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        internal async Task SaveAudioTranslation(SaveAudioTranslationSuccessEvent @event)
        {
            var entity = new 
        }
    }
}
