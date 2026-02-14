using SpeechRecognition.AudioConverter.Api.Interfaces;
using SpeechRecognition.CrossCutting.Shared.Events.AudioRecorderApi;

namespace SpeechRecognition.AudioConverter.Api.Contracts
{
    public class AudioContractResponse : IAudioConversionResponseAdapter
    {
        private readonly FileInfo _output;
        public AudioContractResponse(FileInfo output)
        {
            _output = output;
        }

        public FileInfo GetResultFileInfo()
            => new(_output.FullName);

        public string GetResultFileName()
            => _output.Name;
     
    }
}
