using SpeechRecognition.FileStorageDomain.Entidades;

namespace SpeechRecognition.AudioRecorder.Api.Interfaces
{
    public interface ITranslateAudioBuilder
    {
        FileInfo SaveRawFile(Stream stream);
        FileInfo ConvertRawFile(Stream stream);
        FileStorageConversion SaveConvertedFile();
        void TranslateAudio();
        AudioTranslation SaveAudioTranslation();
        Task GetResult();
    }
}
