using SpeechRecognition.AudioRecorder.Api.Interfaces;
using SpeechRecognition.AudioRecorder.Api.Services;
using System;
using System.IO;
using System.Text;
using Xunit;

namespace SpeechRecognition.Tests.Api.Services
{
    public class SaveConvertedFileTest
    {
        private IFileStorageService _service;
        public SaveConvertedFileTest()
        {
            
            _service = new SaveConvertedFile(Path.Combine(AppContext.BaseDirectory , "Files" , "SaveFile"));
        }
        [Fact]
        public void Test_SaveFile_SavesFileCorrectly()
        {
            // Arrange
            // (Setup necessary objects and dependencies)
            //var texto = "abc";
            //var bytes = Encoding.UTF8.GetBytes(texto);

            //Stream stream = new MemoryStream(bytes);

            //var result = _service.SaveFile( stream );

            //Assert.True(result.Exists);
            // Act
            // (Call the method to be tested)
            // Assert
            // (Verify the expected outcome)
        }
    }
}
