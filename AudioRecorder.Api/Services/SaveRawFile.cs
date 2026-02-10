using AudioRecord.Api.DTO;
using AudioRecorder.Api.Contracts;
using AudioRecorder.Api.Interfaces;
using SpeechRecognition.Dominio.Entidades;
using SpeechRecognition.Dominio.ValueObjects;
using SpeechRecognition.Infra.Context;

namespace AudioRecorder.Api.Services
{
    public class SaveRawFile : FileStorageService
    {
        private readonly ConfigurationDTO.FileStorageConfig _config;
        private readonly IFormFile _ff; 
        public SaveRawFile(ConfigurationDTO.FileStorageConfig config, IFormFile ff )
        {
            _config = config;
            _ff = ff;
        }
        public override async Task<FileInfo> SaveFile() 
        {
            string fileName = new UniqueFileNameVO(_ff.FileName).Value;

            base.InitializeParameters(_config.RawAudioPathDir.FullName, fileName);

            var result = base.SaveFile(_ff.OpenReadStream());
            
            return result;
        }
    }
}
