using SpeechRecognition.FileStorageDomain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SpeechRecognition.Tests.Domain.ValueObjects
{
    public class UniqueFileNameVOTest
    {
        public UniqueFileNameVOTest()
        {

        }
        [Fact]
        public void Should_Keep_File_Extension()
        {
            // Arrange
            var originalFileName = "documento.pdf";

            // Act
            var vo = new UniqueFileNameVO(originalFileName);

            // Assert
            Console.WriteLine("==============================RESULT============================");
            Console.WriteLine(vo.Value);
            Assert.EndsWith(".pdf", vo.Value);
        }

    }
}
