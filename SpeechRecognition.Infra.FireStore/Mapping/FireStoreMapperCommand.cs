using SpeechRecognition.FileStorageDomain;
using SpeechRecognition.FileStorageDomain.Entidades;
using SpeechRecognition.Infra.FireStore.Documents.Base;

namespace SpeechRecognition.Infra.FireStore.Mapping
{
    public class FireStoreMapperCommand<TEntity>
    {
        public FireStoreBaseDocument MapToDocument(TEntity command) => command switch
        {
            FileStorageAggregate cmd => FileStorageAggregateMapper.ToDocument(cmd ),
            _=> throw new InvalidOperationException($"No mapping defined for type {typeof(TEntity).Name}")
        };

    }
}
