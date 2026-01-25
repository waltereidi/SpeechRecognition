namespace AudioConverter.Services.Linux
{
    public sealed class AudioService
    {
        private readonly FfmpegExecutor _executor;

        public AudioService(FfmpegExecutor executor)
        {
            _executor = executor;
        }

        public async Task ConvertToMp3Async(
            string input,
            string output,
            int bitrateKbps = 192,
            CancellationToken cancellationToken = default)
        {
            var cmd = new FfmpegCommand()
                .Overwrite()
                .Input(input)
                .NoVideo()
                .AudioCodec("libmp3lame")
                .AudioBitrate(bitrateKbps)
                .Output(output);

            var result = await _executor.RunAsync(cmd.ToString(), cancellationToken);

            if (!result.Success)
                throw new InvalidOperationException(result.Error);
        }
        public async Task ExtractWavAsync(
string videoFile,
string wavFile,
CancellationToken cancellationToken = default)
        {
            var cmd = new FfmpegCommand()
            .Overwrite()
            .Input(videoFile)
            .NoVideo()
            .Mono()
            .AudioSampleRate(16000)
            .Output(wavFile);


            var result = await _executor.RunAsync(cmd.ToString(), cancellationToken);


            if (!result.Success)
                throw new InvalidOperationException(result.Error);
        }
    }
}
