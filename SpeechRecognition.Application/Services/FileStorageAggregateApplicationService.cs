using SpeechRecognition.Application.Interfaces;
using SpeechRecognition.Application.Services.Services;
using SpeechRecognition.CrossCutting.BuildingBlocks.Messaging.Abstractions;
using SpeechRecognition.CrossCutting.Framework.Interfaces;
using SpeechRecognition.CrossCutting.Shared.Events.AudioConverter;
using SpeechRecognition.CrossCutting.Shared.Events.WhisperSpeechRecognition;
using SpeechRecognition.FileStorageDomain;
using static SpeechRecognition.Application.Contracts.FileStorageAggregateContract;

namespace SpeechRecognition.Application.Services
{
    public class FileStorageAggregateApplicationService : IApplicationService
    {
        public readonly IFileStorageAggregateRepository _repository;
        public readonly IUnitOfWork _unitOfWork;
        public readonly IEventBus _eventBus; 

        public FileStorageAggregateApplicationService(
            IFileStorageAggregateRepository repository,
            IUnitOfWork unitOfWork, 
            IEventBus eventBus )
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _eventBus = eventBus;
        }

        public Task Handle(object command) => command switch
        {
            V1.Create cmd => HandleCreate(cmd),
            V1.UpdateFileStorage cmd => HandleSaveFile(cmd),
            V1.UpdateConvertedFile cmd => HandleSaveFileConversion(cmd),
        };

        private async Task HandleSaveFileConversion(V1.UpdateConvertedFile cmd)
        {
            var fileStorageAgg = await _repository
                .Load(cmd.id.ToString());

            var service = new SaveRawFile(cmd.convertedAudioDir , cmd.fs, cmd.fileName );
            var fi = await service.SaveFile();

            fileStorageAgg.AddFileStorageConversionLocal(fi, cmd.fileName);
            await HandleFileUpdate(fileStorageAgg);

            var re = new AudioTranslationLocalEvent()
            {
                FilePath = fi.FullName,
                FileStorageConversionId = fileStorageAgg.FileStorageConversions.Last().Id.ToString(),
                FileStorageAggregateId = fileStorageAgg.Id.ToString()
            };
            await _eventBus.PublishAsync(re);
        }

        private async Task HandleSaveFile(V1.UpdateFileStorage cmd)
        {
            var fileStorageAgg = await _repository
                .Load(cmd.id.ToString());
            
            var service = new SaveRawFile( cmd.rawAudioDir , cmd.fs , cmd.originalFileName);
            var fi = await service.SaveFile();
            
            fileStorageAgg.AddFileStorageLocal(fi, cmd.originalFileName);
            
            await HandleFileUpdate( fileStorageAgg );

            var re = new AudioConversionToWav16kLocalEvent
            {
                DirectoryPath = cmd.convertedAudioDir.FullName,
                FilePath = fi.FullName,
                FileStorageId = cmd.id.ToString()
            };
            await _eventBus.PublishAsync(re);
        }

        private async Task HandleFileUpdate(FileStorageAggregate fsa)
        {
            await _repository.AddAsync( fsa );
            await _unitOfWork.CommitAsync();
        }
         
        private async Task HandleCreate(V1.Create cmd)
        {
            if (await _repository.Exists(cmd.id.ToString()))
                throw new InvalidOperationException( 
                    $"Entity with id {cmd.id} already exists" 
                );

            var fileStorageAgg = new FileStorageAggregate(new FileStorageAggregateId(cmd.id));
            
            await _repository.AddAsync(fileStorageAgg);
            await _unitOfWork.CommitAsync();
        }
    }

}
