using AudioRecord.Api.DTO;
using SpeechRecognition.AudioRecorder.Api.Contracts;
using SpeechRecognition.AudioRecorder.Api.Interfaces;
using SpeechRecognition.FileStorageDomain.Entidades;
using SpeechRecognition.FileStorageDomain.ValueObjects;
using SpeechRecognition.Infra.Context;

namespace SpeechRecognition.AudioRecorder.Api.Services
{
    public class SaveRawFile : FileService
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
