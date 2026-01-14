using SpeechRecognition.Infra.Context;

namespace AudioRecorder.Api.Services
{
    public class TranslateAudioBuilderFromRaw : TranslateAudioBuilder
    {
        private string _originalFileName;
        private Stream _stream;
        public TranslateAudioBuilderFromRaw(AppDbContext dbContext, string originalFileName, Stream stream) : base(dbContext)
        {
            _originalFileName = originalFileName;
            _stream = stream;
        }
        public override Task GetResult()
        {
            throw new NotImplementedException();
        }

    }
}
