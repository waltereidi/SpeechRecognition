using SpeechRecognition.Dominio.Entidades;

namespace AudioRecorder.Api.Interfaces
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
