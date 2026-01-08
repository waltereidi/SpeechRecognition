using System;
using System.Collections.Generic;
using System.Text;
using Xabe.FFmpeg;

namespace AudioConverter.Services
{
    public class AudioConverterService
    {
        private readonly DirectoryInfo _outputDir;
        private readonly FileInfo _file;
        private string _outPutName;
        public AudioConverterService(DirectoryInfo outputDir , FileInfo file ) 
        {
            FFmpeg.SetExecutablesPath("C:\\ProgramData\\chocolatey\\bin");
            _outputDir = outputDir;
            _file = file;
            _outPutName = Path.Combine(_outputDir.FullName, $"{Guid.NewGuid()}.wav");
        }
        public async Task<FileInfo> ConvertToWavMono16kHz()
        {
            var conversion = FFmpeg.Conversions.New()
            .AddParameter($"-i \"{_file.FullName}\"", ParameterPosition.PreInput)
            .AddParameter("-ac 1")        // mono
            .AddParameter("-ar 16000")    // 16kHz
            .AddParameter("-vn")          // remove vídeo se existir
            .SetOutput(_outPutName);

            await conversion.Start();

            return new FileInfo(_outPutName);
        }

            

    }
}
