using SpeechRecognition.AudioRecorder.Api.Interfaces;
using Microsoft.EntityFrameworkCore;
using SpeechRecognition.Infra.Context;
using System;

namespace SpeechRecognition.AudioRecorder.Api.Services
{
    public class TranslateAudioBuilderFactory : ITranslateAudioBuilderFactory
    {
        private readonly AppDbContext _dbContext;

        public TranslateAudioBuilderFactory(AppDbContext dbContext )
        {
            _dbContext = dbContext;

        }

        public ITranslateAudioBuilder CreateFromStream(string originalFileName, Stream stream)
        {
            throw new NotImplementedException();
        }
        //public ITranslateAudioBuilder CreateFromStream(string originalFileName , Stream stream)
        //{
        //    ITranslateAudioBuilder builder = new TranslateAudioBuilder(_dbContext, originalFileName, stream);
        //    return builder;
        //}

    }
}
