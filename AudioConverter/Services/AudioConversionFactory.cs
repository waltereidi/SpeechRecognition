using AudioConverter.Contracts;
using AudioConverter.Interfaces;
using AudioConverter.Services.Linux;
using AudioConverter.Services.Windows;
using BuildingBlocks.Messaging.Abstractions;
using MassTransit;
using System.Runtime.InteropServices;

namespace AudioConverter.Services
{
    public class AudioConversionFactory : IAudioConversionFactory
    {
        private readonly DirectoryInfo _di;
        private readonly FileInfo _input;
        private readonly FileInfo _output;
        public AudioConversionFactory(string directoryPath , string inputFile)
        {
            _di = new DirectoryInfo(directoryPath);
            _input = new FileInfo(inputFile);
            _output =new( Path.Combine(directoryPath,$"{Guid.NewGuid()}.wav"));
            EnsureParametersAreValid();
        }
        private void EnsureParametersAreValid()
        {
            if (!_input.Exists)
                throw new FileNotFoundException("Input file not found", _input.FullName);
            if (!_di.Exists)
            {
                _di.Create();
            }
        }   
        public IAudioConversionResponseAdapter GetAdapter()
            => new AudioContractResponse(_output);

        public IAudioConversionStrategy GetStrategy()
            => IdentifyOs();

        private IAudioConversionStrategy IdentifyOs()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return new ConvertToWavMono16kWindows(_output,_input);
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return new ConvertToWavMono16kLinux( _input , _output );
            else
                throw new Exception("This implementation does not contains resolution for the current running operational system");
        }
    }
}
