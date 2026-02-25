using SpeechRecognition.FileStorageDomain.ValueObjects;

namespace SpeechRecognition.Application.Services.Services
{
    public class SaveRawFile : FileService
    {
        private readonly DirectoryInfo _dir;
        private readonly Stream _st; 
        private readonly string _fileName;
        public SaveRawFile(DirectoryInfo dir, Stream st , string fileName )
        {
            _dir = dir;
            _st = st;
            _fileName = fileName;
        }
        public override async Task<FileInfo> SaveFile() 
        {
            string fileName = new UniqueFileNameVO(_fileName).Value;

            base.InitializeParameters(_dir.FullName, fileName);

            var result = base.SaveFile(_st);
            
            return result;
        }
    }
}
