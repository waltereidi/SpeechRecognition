using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Unicode;
using WhisperSpeechRecognition;
using WhisperSpeechRecognition.Contracts;

namespace SpeechRecognition.Tests.Whisper
{
    public class TranslateAudioTest : Configuration
    {
        private readonly TranslateAudio _service;
        public TranslateAudioTest()
        {
            _service = new TranslateAudio();
        }

        [Fact]
        public async void TranslateAudioLocalTest()
        {
            var output = base.GetTestFile("output.wav");
            using (Stream fileStream = output.OpenRead() )
            {
                var dto = new TranslationContract.Request.GeneralTranslation(fileStream , 0 );
                var response = await _service.TranslateAudioLocal(dto);
                var decodedResponse  = Encoding.UTF8.GetString(response);
                Assert.True( decodedResponse.Length > 0 );
            }
            
        }

    }
}
