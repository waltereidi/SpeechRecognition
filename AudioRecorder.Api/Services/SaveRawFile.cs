using AudioRecord.Api.DTO;
using AudioRecorder.Api.Interfaces;
using SpeechRecognition.Dominio.Entidades;
using SpeechRecognition.Infra.Context;

namespace AudioRecorder.Api.Services
{
    public class SaveRawFile : FileStorageService
    {
        private readonly AppDbContext _context;
        private readonly ConfigurationDTO.FileStorageConfig _config;
        private readonly IFormFile _fc;
        public SaveRawFile(AppDbContext context, IConfiguration config , IFormFile file)
        {
            _context = context;
            _config = ConfigurationDTO.GetFileStorageConfig(config);
            _fc = file;
        }
        public override async Task<FileStorage> SaveFile()
        {
            string fileName = $"{Guid.NewGuid().ToString()}";

            base.InitializeParameters(_config.RawAudioPathDir.FullName, fileName);

            var result = base.SaveFile(_fc.OpenReadStream());

            var fileStorage = new FileStorage
            {
                FileInfo = result,
                OriginalFileName = _fc.FileName
            };

            _context.FileStorages.Add(fileStorage);
            return fileStorage;

        }
    }
}
