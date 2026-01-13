namespace AudioRecorder.Api.Services
{
    public class SaveRawFile : FileStorageService
    {
        public SaveRawFile(IConfiguration config)
        {
            string rawPath = config.GetRequiredSection("FileStorage:RawAudioPath").Value ?? "";

            string fileName = Guid.NewGuid().ToString();
            base.InitializeParameters(rawPath, fileName);
        }
    }
}
