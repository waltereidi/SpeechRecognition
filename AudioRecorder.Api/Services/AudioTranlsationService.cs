using Shared.Events.AudioRecorderApi;
using SpeechRecognition.Dominio.Entidades;
using SpeechRecognition.Infra.Context;

namespace AudioRecorder.Api.Services
{
    public class AudioTranlsationService
    {
        private readonly AppDbContext _context;
        public AudioTranlsationService(AppDbContext context )
        {
            _context = context;
        }
        internal async Task SaveAudioTranslation(SaveAudioTranslationSuccessEvent @event)
        {
            var fileStorageConversion = _context.FileStorageConversions
                .First(x => x.Id == Guid.Parse(@event.FileStorageConversionId));

            var entity = new AudioTranslation
            {
                FileStorageId  = fileStorageConversion.FileStorage.Id , 
                Translation = @event.Translation,
                WhisperModel = @event.ModelId, 
                TranslationTemplate = @event.TemplateId , 
                IsSuccess = true, 
                IsApproved = null
            };
            _context.AudioTranslations.Add(entity); 
            await _context.SaveChangesAsync();
        }
    }
}
