using AudioRecorder.Api.Interfaces;
using SpeechRecognition.Infra.Context;
using System;

namespace AudioRecorder.Api.Services
{
    public class TranslateAudioBuilder: ITranslateAudioBuilder
    {
        private readonly AppDbContext _dbContext;
        private readonly Stream _file; 
        private readonly string _originalFileName;
        

        public TranslateAudioBuilder(AppDbContext dbContext , Stream file , string originalFileName  )
        {
            _dbContext = dbContext;
            _file = file;
            _originalFileName = originalFileName;
        }

        public async Task Start()
        {
            await _dbContext.SaveChangesAsync();

        }
    }
}
