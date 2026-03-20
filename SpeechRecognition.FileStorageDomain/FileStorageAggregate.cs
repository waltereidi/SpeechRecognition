using SpeechRecognition.CrossCutting.Framework;
using SpeechRecognition.FileStorageDomain.DomainEvents;
using SpeechRecognition.FileStorageDomain.Entidades;
using static SpeechRecognition.FileStorageDomain.Entidades.FileStorage;
using static System.Reflection.Metadata.BlobBuilder;

namespace SpeechRecognition.FileStorageDomain
{
    public class FileStorageAggregate : AggregateRoot<FileStorageAggregateId>
    {
        public FileStorageAggregate(FileStorageAggregateId id)
        {
            Apply(new Events.FileStorageAggregateCreated(id));
        }

        public List<FileStorage> FileStorages
        {
            get => _fileStorages;
            private set => SetFileStorages(value);
        }

        public List<FileStorageConversion> FileStorageConversions
        {
            get => _fileStorageConversions;
            private set => SetFileStorageConversions(value);
        }

        public List<AudioTranslation> AudioTranslations
        {
            get => _audioTranslations;
            private set => SetAudioTranslations(value);
        }

        public List<RabbitMqLog> Logs
        {
            get => _logs;
            private set => SetLogs(value);
        }
        private List<FileStorage> _fileStorages;
        private List<FileStorageConversion> _fileStorageConversions;
        private List<AudioTranslation> _audioTranslations;
        private List<RabbitMqLog> _logs;

        public void SetFileStorages(List<FileStorage> value)
        {
            _fileStorages = value ?? new();
        }

        public void SetFileStorageConversions(List<FileStorageConversion> value)
        {
            _fileStorageConversions = value ?? new();
        }

        public void SetAudioTranslations(List<AudioTranslation> value)
        {
            _audioTranslations = value ?? new();
        }

        public void SetLogs(List<RabbitMqLog> value)
        {
            _logs = value ?? new();
        }
        protected override void EnsureValidState()
        {
            
        }

        public void AddFileStorageLocal(FileStorageId fsId , FileInfo fi , string originalFileName )
            => Apply(new Events.FileStorageAdded(fsId, fi, originalFileName));

        public void AddFileStorageConversionLocal(FileInfo fi, FileStorageId createId )
            => Apply(new Events.FileStorageConversionAdded( fi , createId ));
        public void AddTranslation(Events.TranslationAdded e)
            => Apply(e);
        public void AddErrorLog(Events.ErrorLog errorLog)
            =>  Apply(errorLog);

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
                        ApplyToEntity(fsc, new Events.CreateFileStorageConversion(fsId,e.fi) );

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
                case Events.ErrorLog e:
                    {
                        var log = new RabbitMqLog(Apply);
                        ApplyToEntity(log, e);
                        Logs = Logs ?? new();
                        Logs.Add(log);
                        break;
                    }
            }
        }
        protected void EnsureTranslationCanBeAdded(Events.TranslationAdded e)
        {
            AudioTranslations = AudioTranslations ?? new();

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
                return FileStorages.First(x => !FileStorageConversions.Any(y => x.Id == y.FileStorageId)).Id;
            else 
                throw new InvalidOperationException("Domain aggregate does not contain a valid file to relate to aggregate");
        }


    }
}
