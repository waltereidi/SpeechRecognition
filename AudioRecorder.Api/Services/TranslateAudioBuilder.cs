using AudioRecorder.Api.Interfaces;
using SpeechRecognition.Dominio.Entidades;
using SpeechRecognition.Infra.Context;

namespace AudioRecorder.Api.Services
{
    public abstract class TranslateAudioBuilder : ITranslateAudioBuilder
    {
        private readonly AppDbContext _dbContext;
        private readonly IConfiguration _config;
        public TranslateAudioBuilder(AppDbContext dbContext , IConfiguration config )
        {
            _dbContext = dbContext;
            _config = config;
        }

        public FileInfo ConvertRawFile(Stream stream)
        {
            throw new NotImplementedException();
        }

        public Task GetResult()
        {
            throw new NotImplementedException();
        }

        public AudioTranslation SaveAudioTranslation()
        {
            throw new NotImplementedException();
        }

        public FileStorageConversion SaveConvertedFile()
        {
            throw new NotImplementedException();
        }

        public FileInfo SaveRawFile(Stream stream)
        {
            throw new NotImplementedException();
        }

        public void TranslateAudio()
        {
            throw new NotImplementedException();
        }
    }
}
