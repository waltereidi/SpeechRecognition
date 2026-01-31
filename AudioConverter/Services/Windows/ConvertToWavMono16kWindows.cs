using AudioConverter.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xabe.FFmpeg;

namespace AudioConverter.Services.Windows
{
    public class ConvertToWavMono16kWindows : IAudioConversionStrategy
    {
        private readonly DirectoryInfo _outputDir;
        private readonly FileInfo _file;
        private readonly FileInfo _outputFile;
        public string _outPutName => _outputFile.FullName;
        public ConvertToWavMono16kWindows(FileInfo outputFile , FileInfo file ) 
        {
            FFmpeg.SetExecutablesPath("C:\\ProgramData\\chocolatey\\bin");
            _outputDir = outputFile.Directory;
            _file = file;
            _outputFile = outputFile;
        }
        private void EnsureParametersAreValid()
        {
            if (!_file.Exists)
                throw new FileNotFoundException("Input file not found", _file.FullName);
            if (!_outputDir.Exists)
            {
                _outputDir.Create();
            }
        }

        public async Task Start(CancellationToken cancellationToken = default )
        {
            EnsureParametersAreValid();

            var conversion = FFmpeg.Conversions.New()
            .AddParameter($"-i \"{_file.FullName}\"", ParameterPosition.PreInput)
            .AddParameter("-ac 1")        // mono
            .AddParameter("-ar 16000")    // 16kHz
            .AddParameter("-vn")          // remove vídeo se existir
            .SetOutput(_outPutName);

            await conversion.Start();
            var result = new FileInfo(_outPutName);
            if (!result.Exists)
                throw new InvalidOperationException($"Conversion failed, output file not found. {result.FullName}");
        }

    }
}
