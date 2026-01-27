using AudioConverter.Interfaces;

namespace AudioConverter.Services.Linux
{
    public sealed class ConvertToWavMono16kLinux : IAudioConversionStrategy
    {
        private readonly FfmpegExecutor _executor;
        private readonly FileInfo _input;
        private readonly DirectoryInfo _output;
        public readonly string fileName;
        public string GetOutputFullName => Path.Combine(_output.FullName, fileName);
        public ConvertToWavMono16kLinux(FileInfo input , DirectoryInfo di  )
        {
            _input = input;
            _output = di;
            fileName = Guid.NewGuid().ToString() + "wav";
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
