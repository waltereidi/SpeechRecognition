using SpeechRecognition.CrossCutting.Framework;
using SpeechRecognition.FileStorageDomain.DomainEvents;
using SpeechRecognition.FileStorageDomain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace SpeechRecognition.FileStorageDomain
{
    public class FileStorageAggregate : AggregateRoot<FileStorageAggregateId>
    {
         
        public FileStorageAggregate(FileStorageAggregateId id )
        {
            Apply( new Events.FileStorageAggregateCreated(id ));
        }
        public List<FileStorage> FileStorages { get; private set; }
        public List<FileStorageConversion> FileStorageConversions { get; private set; }
        public List<AudioTranslation> AudioTranslations { get; private set; }
        public List<RabbitMqLog> Logs { get; private set; }

        protected override void EnsureValidState()
        {
            
        }
        public void AddFileStorageLocal(FileInfo fi , string originalFileName )
            => Apply(new Events.FileStorageAdded(Guid.NewGuid(), fi, originalFileName));

        public void AddFileStorageConversionLocal(FileInfo fi, string fileName )
            => Apply(new Events.FileStorageAdded(Guid.NewGuid(), fi, fileName ));
        public void AddTranslation(Events.TranslationAdded e)
            => Apply(e);
        protected override void When(object @event)
        {
            switch (@event)
            {
                case Events.FileStorageAggregateCreated e:
                    Id = new FileStorageAggregateId( e.aggId);
                    break;

                case Events.FileStorageAdded e:
                    {
                        EnsureCanAddRootUpload();
                        var fs = new FileStorage(Apply);
                        ApplyToEntity(fs, e);
                        FileStorages.Add(fs);
                        break;
                    }
                case Events.FileStorageConversionAdded e:
                    {
                        var fsId = EnsureCanAddNewConversion();
                        var fs = new FileStorage(Apply);
                        ApplyToEntity(fs, e);

                        var fsc = new FileStorageConversion(Apply);
                        ApplyToEntity(fsc, e );

                        FileStorageConversions.Add(fsc);
                        break;
                    }
                case Events.TranslationAdded e:
                    {
                        EnsureTranslationCanBeAdded(e);
                        var at = new AudioTranslation(Apply);
                        ApplyToEntity(at, e);
                        AudioTranslations.Add(at);
                        break;
                    }
            }
        }
        protected void EnsureTranslationCanBeAdded(Events.TranslationAdded e)
        {
            if (!FileStorages.Any(x => x.Id == e.fileStorageid))
                throw new ArgumentOutOfRangeException($"fileStorage not found id:{nameof(e.fileStorageid)}");

            if(AudioTranslations.Any(x=> x.WhisperModel == e.modelId && x.IsApproved == true && x.TranslationTemplate == e.templateId))
                throw new InvalidOperationException($"Domain aggregate already contains a successful translation with the same model and template");
        }
        protected void EnsureCanAddRootUpload()
        {
            if(FileStorages != null && FileStorages.Count > 0 )
                throw new InvalidOperationException("Domain aggregate can only have one file upload at once");

            FileStorages = FileStorages ?? new();

        }
        protected FileStorageId EnsureCanAddNewConversion()
        {
            FileStorageConversions = FileStorageConversions ?? new();

            if (!FileStorages.Any(x => FileStorageConversions.Any(y => x.Id == y.FileStorageId))
                && FileStorages.Count() > 0 )
                return FileStorages.First(x => FileStorageConversions.Any(y => x.Id == y.FileStorageId)).Id;
            else 
                throw new InvalidOperationException("Domain aggregate does not contain a valid file to relate to aggregate");
        }


    }
}
