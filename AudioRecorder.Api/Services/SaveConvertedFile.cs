namespace AudioRecorder.Api.Services
{
    public class SaveConvertedFile : FileStorageService
    {
        public SaveConvertedFile(IConfiguration config )
        {
            string rawPath = config.GetRequiredSection("FileStorage:RawAudioPath").Value ?? "";

            string fileName = Guid.NewGuid().ToString();
            base.InitializeParameters(rawPath, fileName);
        }
        public SaveConvertedFile(string path )
        {
            string rawPath = path;

            string fileName = Guid.NewGuid().ToString();
            base.InitializeParameters(rawPath, fileName);
        }
    }
}
