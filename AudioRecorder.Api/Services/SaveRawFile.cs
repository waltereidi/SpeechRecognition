namespace AudioRecorder.Api.Services
{
    public class SaveRawFile : FileStorageService
    {
        public SaveRawFile(string path)
        {
            string rawPath = path;

            string fileName = Guid.NewGuid().ToString();
            base.InitializeParameters(rawPath, fileName);
        }
    }
}
