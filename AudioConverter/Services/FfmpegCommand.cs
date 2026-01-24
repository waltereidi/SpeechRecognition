namespace AudioConverter.Services
{
    using System.Text;

    public sealed class FfmpegCommand
    {
        private readonly StringBuilder _args = new();

        public FfmpegCommand Overwrite()
        {
            _args.Append("-y ");
            return this;
        }

        public FfmpegCommand Input(string file)
        {
            _args.Append($"-i \"{file}\" ");
            return this;
        }

        public FfmpegCommand NoVideo()
        {
            _args.Append("-vn ");
            return this;
        }

        public FfmpegCommand AudioCodec(string codec)
        {
            _args.Append($"-acodec {codec} ");
            return this;
        }

        public FfmpegCommand AudioBitrate(int kbps)
        {
            _args.Append($"-ab {kbps}k ");
            return this;
        }

        public FfmpegCommand AudioSampleRate(int hz)
        {
            _args.Append($"-ar {hz} ");
            return this;
        }

        public FfmpegCommand Mono()
        {
            _args.Append("-ac 1 ");
            return this;
        }

        public FfmpegCommand Output(string file)
        {
            _args.Append($"\"{file}\"");
            return this;
        }

        public override string ToString() => _args.ToString();
    }
}
