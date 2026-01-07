using System;
using System.Collections.Generic;
using System.Text;
using WhisperSpeechRecognition.Interfaces;
namespace SpeechRecognition.Tests.Whisper
{
    public class WhisperModelTest : Configuration
    {
        private readonly ISpeechRecognition _serviceProvider;
        public WhisperModelTest()
        { 
            _serviceProvider = new WhisperSpeechRecognition.Service.WhisperModel();
        }
        [Fact]
        public async void TestTranslation()
        {
            var file = base.GetTestFile("output.wav");
            using var stream = file.OpenRead();
            var result = await _serviceProvider.GetTranslation(stream);
            Assert.NotEmpty(result);
        }

    }
}
