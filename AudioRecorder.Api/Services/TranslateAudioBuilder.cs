using AudioRecorder.Api.Interfaces;
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

        public void ConvertRawFile()
        {
            throw new NotImplementedException();
        }

        public void SaveAudioTranslation()
        {
            throw new NotImplementedException();
        }

        public void SaveConvertedFile()
        {
            throw new NotImplementedException();
        }

        public FileInfo SaveRawFile(Stream stream)
        {
            var path = _config.GetSection("FileStorage:RawAudioPath").Value ?? "";
            IFileStorageService fs = new SaveRawFile(path);
            return fs.SaveFile(stream);
        }

        public void TranslateAudio()
        {
            throw new NotImplementedException();
        }
        public abstract Task GetResult();
    }
}
