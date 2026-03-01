using SpeechRecognition.AudioConverter.Api.Interfaces;

namespace SpeechRecognition.AudioConverter.Api.Services.Linux
{
    public sealed class ConvertToWavMono16kLinux : IAudioConversionStrategy
    {
        private readonly FfmpegExecutor _executor;
        private readonly FileInfo _input;
        private readonly DirectoryInfo _output;
        public readonly FileInfo _outputFile;
        public string GetOutputFullName => _outputFile?.FullName ?? $"{Guid.NewGuid()}.wav";
        public ConvertToWavMono16kLinux(FileInfo input , FileInfo outputFileInfo )
        {
            _input = input;
            _output = outputFileInfo.Directory ?? throw new Exception();
            _executor = new FfmpegExecutor();
        }
        private void EnsureParametersAreValid()
        { 
            if(!_input.Exists)
                throw new FileNotFoundException("Input file not found", _input.FullName);
            if(!_output.Exists)
            {
                _output.Create();
            }
        }
        
        public async Task Start( CancellationToken cancellationToken = default )
        {
            EnsureParametersAreValid();

            var cmd = new FfmpegCommand()
            .Overwrite()
            .Input(_input.FullName)
            .NoVideo()
            .Mono()
            .AudioSampleRate(16000)
            .Output(GetOutputFullName);

            var result = await _executor.RunAsync(cmd.ToString(), cancellationToken);

            if (!result.Success)
                throw new InvalidOperationException(result.Error);
        }

    }
}
