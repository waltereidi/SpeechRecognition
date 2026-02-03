using Shared.Events.AudioRecorderApi;
using SpeechRecognition.Dominio.Entidades;
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

        public async Task<FileStorageConversion> SaveAudioConversion(SaveAudioConversionSuccessEvent @event)
        {
            var entity = new FileStorageConversion()
            {
                FileStorageId= Guid.Parse(@event.FileStorageId)
            };
            _context.FileStorageConversions.Add(entity);
            await _context.SaveChangesAsync();

            var result = _context.FileStorageConversions
                .Where(x => x.Id == entity.Id)
                .First(); 
            
            return result;
        }
    }
}
