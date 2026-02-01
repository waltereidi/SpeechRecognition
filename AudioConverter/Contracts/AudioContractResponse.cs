using AudioConverter.Interfaces;
using Shared.Events.AudioRecorderApi;

namespace AudioConverter.Contracts
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
