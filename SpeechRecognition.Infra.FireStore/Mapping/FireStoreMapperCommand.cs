using Google.Cloud.Firestore;
using SpeechRecognition.FileStorageDomain;
using SpeechRecognition.FileStorageDomain.Entidades;
using SpeechRecognition.Infra.FireStore.Documents;
using SpeechRecognition.Infra.FireStore.Documents.Base;
using SpeechRecognition.Infra.FireStore.DomainMapper;

namespace SpeechRecognition.Infra.FireStore.Mapping
{
    public class FireStoreMapperCommand<TEntity>
    {
        public FireStoreBaseDocument MapToDocument(TEntity command) => command switch
        {
            FileStorageAggregate cmd => FileStorageAggregateMapper.ToDocument(cmd),
            _ => throw new InvalidOperationException($"No mapping defined for type {typeof(TEntity).Name}")
        };
        public TEntity MapToDomain<TEntity>(DocumentSnapshot doc)
        {
            if (typeof(TEntity) == typeof(FileStorageAggregate))
            {
                var document = doc.ConvertTo<FileStorageAggregateDocument>();

                return (TEntity)(object)FileStorageAggregateDomainMapper.ToDomain(document);
            }

            throw new InvalidOperationException(
                $"No mapping defined for type {typeof(TEntity).Name}"
            );
        }

    }
}
