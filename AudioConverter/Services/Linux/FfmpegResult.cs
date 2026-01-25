namespace AudioConverter.Services.Linux
{
    public sealed record FfmpegResult(
        int ExitCode,
        string Output,
        string Error)
    {
        public bool Success => ExitCode == 0;
    }
}
