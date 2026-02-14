using AudioRecord.Api.DTO;
using SpeechRecognition.AudioRecorder.Api.Contracts;
using SpeechRecognition.AudioRecorder.Api.Interfaces;
using SpeechRecognition.CrossCutting.BuildingBlocks.Messaging.Abstractions;
using SpeechRecognition.CrossCutting.Shared.Events.AudioConverter;
using SpeechRecognition.CrossCutting.Shared.Events.AudioRecorderApi;
using SpeechRecognition.FileStorageDomain.Entidades;
using SpeechRecognition.Infra.Context;

namespace SpeechRecognition.AudioRecorder.Api.Services
{
    public class AudioConversionService
    {
        private readonly AppDbContext _context;
        private readonly IEventBus _eventBus;
        private readonly ConfigurationDTO.FileStorageConfig _fsConfig;
        
        public AudioConversionService(AppDbContext context, 
            IEventBus eventBus, 
            IConfiguration config,
            SaveRawFile saveRawFileService )
        {
            _context = context;
            _eventBus = eventBus;
            _fsConfig = ConfigurationDTO.GetFileStorageConfig(config);
        }
        public async Task<object?> Handle(object command) => command switch
        {
            UploadContracts.Request.AudioUpload cmd => await AudioUploaded(cmd),
            SaveAudioConversionSuccessEvent cmd => await SaveAudioConversion(cmd),
            _ => throw new InvalidOperationException()
        };

        private async Task<UploadContracts.Response.AudioUpload> AudioUploaded(UploadContracts.Request.AudioUpload cmd )
        {
            IFileStorageService service = new SaveRawFile( _fsConfig , cmd.GetFormFile());
            var result = await service.SaveFile();

            var fileStorage = AddToFileStorage(result, cmd.GetFormFile().FileName);
            
            await _context.SaveChangesAsync();

            var audioConvert = new AudioConversionToWav16kLocalEvent()
            {
                DirectoryPath = _fsConfig.ConvertedAudioDir.FullName,
                FilePath = fileStorage.FileInfo.FullName,
                FileStorageId = fileStorage.Id.ToString()
            };

            await _eventBus.PublishAsync(audioConvert);

            return new(fileStorage.OriginalFileName , fileStorage.Id.ToString());
        }
        private FileStorage AddToFileStorage(FileInfo fi, string originalFileName)
        {
            var entity = new FileStorage
            {
                FileInfo = fi,
                OriginalFileName = originalFileName,
            };

            _context.FileStorages.Add(entity);

            return entity;
        }

        private async Task<FileStorageConversion> SaveAudioConversion(SaveAudioConversionSuccessEvent @event)
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
