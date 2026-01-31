using AudioConverter.Interfaces;

namespace AudioConverter.Contracts
{
    public class AudioContractResponse : IAudioConversionResponseAdapter
    {

        public FileInfo GetResultFileInfo()
        {
            throw new NotImplementedException();
        }

        public string GetResultFileName()
        {
            throw new NotImplementedException();
        }
    }
}
