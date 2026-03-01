using SpeechRecognition.Application.Interfaces;
using SpeechRecognition.Application.Services.Services;
using SpeechRecognition.CrossCutting.BuildingBlocks.Messaging.Abstractions;
using SpeechRecognition.CrossCutting.Framework.Interfaces;
using SpeechRecognition.CrossCutting.Shared.Events.AudioConverter;
using SpeechRecognition.CrossCutting.Shared.Events.Generic;
using SpeechRecognition.CrossCutting.Shared.Events.WhisperSpeechRecognition;
using SpeechRecognition.FileStorageDomain;
using SpeechRecognition.FileStorageDomain.DomainEvents;
using SpeechRecognition.FileStorageDomain.Entidades;
using static SpeechRecognition.Application.Contracts.FileStorageAggregateContract;
using static SpeechRecognition.FileStorageDomain.DomainEvents.Events;

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
            V1.SaveAudioTranslationLocal cmd => HandleAudioTranslation(cmd),
            V1.ErrorLog cmd => HandleErrorLog(cmd),
        };

        private async Task HandleErrorLog(V1.ErrorLog cmd)
        {
            var fileStorageAgg = await LoadAggregate(cmd.aggegateId);
            fileStorageAgg.AddErrorLog(
                new Events.ErrorLog(cmd.source, cmd.errorMessage, cmd.severity, cmd.aggegateId)
                );
            await HandleFileUpdate(fileStorageAgg);
        }

        private async Task HandleAudioTranslation(V1.SaveAudioTranslationLocal cmd)
        {
            var fileStorageAgg = await LoadAggregate(cmd.id);

            fileStorageAgg.AddTranslation(new TranslationAdded(cmd.fileStorageId, cmd.translation, cmd.temlateId, cmd.modelId, cmd.isSuccess));

            await HandleFileUpdate(fileStorageAgg);
        }
        private async Task<FileStorageAggregate> LoadAggregate(FileStorageAggregateId id)
        {
            var fileStorageAgg = await _repository
                .Load(id);
            if (fileStorageAgg == null)
                throw new InvalidOperationException($"Entity with id {id} not found");
            
            return fileStorageAgg;
        }

        private async Task HandleSaveFileConversion(V1.UpdateConvertedFile cmd)
        {
            var fileStorageAgg = await LoadAggregate(cmd.id);

            var service = new SaveRawFile(cmd.convertedAudioDir , cmd.fs, cmd.fileName );
            var fi = await service.SaveFile();

            var createId = new FileStorageId(Guid.NewGuid());
            fileStorageAgg.AddFileStorageConversionLocal(fi , createId);
            await HandleFileUpdate(fileStorageAgg);

            var re = new AudioTranslationLocalEvent()
            {
                ModelId = null,
                TemplateId = null,
                FilePath = fi.FullName,
                FileStorageId = createId.ToString() ,
                FileStorageAggregateId = fileStorageAgg.Id.ToString()
            };

            await _eventBus.PublishAsync(re);
        }
        
        private async Task HandleSaveFile(V1.UpdateFileStorage cmd)
        {
            var fileStorageAgg = await LoadAggregate(cmd.id);

            var service = new SaveRawFile( cmd.rawAudioDir , cmd.fs , cmd.originalFileName);
            var fi = await service.SaveFile();

            FileStorageId createId = new(cmd.id);  
            fileStorageAgg.AddFileStorageLocal(createId,fi, cmd.originalFileName);
            
            await HandleFileUpdate( fileStorageAgg );

            var re = new AudioConversionToWav16kLocalEvent
            {
                DirectoryPath = cmd.convertedAudioDir.FullName,
                FilePath = fi.FullName,
                FileStorageId = createId.ToString()
            };
            await _eventBus.PublishAsync(re);
        }

        private async Task HandleFileUpdate(FileStorageAggregate fsa)
        {
            await _repository.Update( fsa );
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
