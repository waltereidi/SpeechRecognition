using System.ComponentModel.DataAnnotations;

namespace AudioRecorder.Api.Contracts
{
    public class UploadContracts
    {
        public class Request
        {
            public record class AudioUpload(IFormFileCollection fileCollection)
            {
                public IFormFileCollection fileCollection { get; init; } 
                    = fileCollection.Count() != 1 
                        ? fileCollection 
                        : throw new ArgumentNullException("File was not found");
                public IFormFile GetFormFile() => fileCollection[0];
            }
            
        }
        public class Response
        {
            public record AudioUpload(string fileName , string fileId );
        }
    }
}
