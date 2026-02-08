using AudioRecorder.Api.Interfaces;

namespace AudioRecorder.Api.Services
{
    public abstract class FileStorageService
    {
        protected DirectoryInfo SavePath { get; private set; }
        protected string FileName { get; private set; }
        public virtual string GetFullFileName() => Path.Combine(SavePath.FullName, FileName);
        public FileStorageService()
        {
        }
        protected virtual void InitializeParameters(string savePath , string fileName )
        {
            SavePath = new DirectoryInfo(savePath);
            FileName = fileName;
        }
        protected virtual void EnsureParametersAreValid()
        { 
            if(!SavePath.Exists)
                SavePath.Create();

            if(String.IsNullOrEmpty(FileName))
                throw new ArgumentException("FileName must be provided");
        }
        protected virtual void EnsureValidState()
        {
            var file = new FileInfo(GetFullFileName());
            if(!file.Exists)
                throw new FileNotFoundException("File was not saved correctly", GetFullFileName());
        }

        public virtual FileInfo SaveFile(Stream stream)
        {
            EnsureParametersAreValid();
            using (FileStream fs = new FileStream(GetFullFileName(),FileMode.Create ))
            {
                stream.CopyTo(fs);
            }
            EnsureValidState();

            return new FileInfo( GetFullFileName() );
        }
    }
}
