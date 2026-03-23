using Google.Cloud.Firestore;
using SpeechRecognition.FileStorageDomain;
using SpeechRecognition.FileStorageDomain.Entidades;
using SpeechRecognition.Infra.FireStore.Documents.Base;

namespace SpeechRecognition.Infra.FireStore.Mapping
{
    public class FireStoreMapperCommand<TEntity>
    {
        public FireStoreBaseDocument MapToDocument(TEntity command) => command switch
        {
            FileStorageAggregate cmd => FileStorageAggregateMapper.ToDocument(cmd),
            _ => throw new InvalidOperationException($"No mapping defined for type {typeof(TEntity).Name}")
        };
        public TEntity MapToDomain<TEntity>(FireStoreBaseDocument doc)
        {
            if (typeof(TEntity) == typeof(FileStorageAggregate))
            {
                var id = new FileStorageAggregateId(Guid.Parse(doc.Id));

                var aggregate = new FileStorageAggregate(id);

                // hydrate manual
                // aggregate.SetNome(doc.Nome);

                return (TEntity)(object)aggregate;
            }

            throw new InvalidOperationException(
                $"No mapping defined for type {typeof(TEntity).Name}"
            );
        }

    }
}
