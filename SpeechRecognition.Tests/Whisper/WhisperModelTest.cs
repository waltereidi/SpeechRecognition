using WhisperSpeechRecognition.Interfaces;
using WhisperSpeechRecognition.Templates;
namespace SpeechRecognition.Tests.Whisper
{
    public class WhisperModelTest : Configuration
    {
        private readonly ISpeechRecognitionStrategy _serviceProvider;
        private readonly TranslationTemplateModel _template;
        public WhisperModelTest()
        { 
            _template = new GeneralTranslation();
            _serviceProvider = new WhisperSpeechRecognition.Service.WhisperMedium(_template);
        }
        [Fact]
        public async void TestTranslation()
        {
            var file = base.GetTestFile("output.wav");
            using var stream = file.OpenRead();
            var result = await _serviceProvider.Start(stream);

            Assert.NotEmpty(result.GetResult() );
        }

    }
}
