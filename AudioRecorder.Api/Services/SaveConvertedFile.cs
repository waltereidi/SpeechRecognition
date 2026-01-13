namespace AudioRecorder.Api.Services
{
    public class SaveConvertedFile : FileStorageService
    {

        public SaveConvertedFile(string path )
        {
            string rawPath = path;

            string fileName = Guid.NewGuid().ToString();
            base.InitializeParameters(rawPath, fileName);
        }
    }
}
