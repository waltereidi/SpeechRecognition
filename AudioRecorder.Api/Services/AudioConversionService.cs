using AudioRecord.Api.DTO;
using BuildingBlocks.Messaging.Abstractions;
using Shared.Events.AudioConverter;
using Shared.Events.AudioRecorderApi;
using SpeechRecognition.Dominio.Entidades;
using SpeechRecognition.Infra.Context;

namespace AudioRecorder.Api.Services
{
    public class AudioConversionService
    {
        private readonly AppDbContext _context;
        private readonly IEventBus _eventBus;
        private readonly IConfiguration _config;
        private readonly ConfigurationDTO.FileStorageConfig _fsConfig;
        public AudioConversionService(AppDbContext context, IEventBus eventBus ,IConfiguration config  )
        {
            _context = context;
            _eventBus = eventBus;
            _config = config;
            _fsConfig = ConfigurationDTO.GetFileStorageConfig(config);
        }

        public async Task RequestAudioConversion(IFormFileCollection formFile )
        {
            //var service = new SaveRawFile(_context, _config);
            //formFile.ToList().ForEach(async file =>
            //{
            //    var fs = new FileStorage()
            //    {
            //        FileName = file.FileName,
            //        ContentType = file.ContentType,
            //        Size = file.Length,
            //        CreatedAt = DateTime.UtcNow
            //    };
            //    _context.FileStorages.Add(fs);
            //    await _context.SaveChangesAsync();
            //    await PublishAudioConversionEvent(fs);
            //});

            //var audioConvert = new AudioConversionToWav16kLocalEvent()
            //{
            //    DirectoryPath = _fsConfig.ConvertedAudioDir.FullName,
            //    FilePath = fs.FileInfo.FullName,
            //    FileStorageId = fs.Id.ToString()
            //};

            //await _eventBus.PublishAsync(audioConvert);
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
