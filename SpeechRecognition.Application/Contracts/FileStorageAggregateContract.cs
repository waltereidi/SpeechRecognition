using SpeechRecognition.FileStorageDomain;
using SpeechRecognition.FileStorageDomain.Entidades;

namespace SpeechRecognition.Application.Contracts
{
    public class FileStorageAggregateContract
    {
        public class V1()
        {
            public record UpdateConvertedFile(
                FileStorageAggregateId id,
                Stream fs ,
                DirectoryInfo convertedAudioDir,
                string fileName
                );
            public record UpdateFileStorage(
                FileStorageAggregateId id
                , Stream fs 
                , string originalFileName
                , DirectoryInfo rawAudioDir 
                , DirectoryInfo convertedAudioDir
                );

            public record Create(FileStorageAggregateId id);

        }
    }
}
