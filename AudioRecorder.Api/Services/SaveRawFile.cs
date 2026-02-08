using AudioRecord.Api.DTO;
using SpeechRecognition.Dominio.Entidades;
using SpeechRecognition.Infra.Context;

namespace AudioRecorder.Api.Services
{
    public class SaveRawFile : FileStorageService
    {
        private readonly AppDbContext _context;
        private readonly ConfigurationDTO.FileStorageConfig _config;
        public SaveRawFile(AppDbContext context, IConfiguration config )
        {
            _context = context;
            _config = ConfigurationDTO.GetFileStorageConfig(config);
        }
        public SaveRawFile(Stream file)
        {

            string fileName = $"{Guid.NewGuid().ToString()}.wav";
            
            base.InitializeParameters(_config.RawAudioPathDir.FullName, fileName);
            
            var result = base.SaveFile(file);
            

        }
    }
}
