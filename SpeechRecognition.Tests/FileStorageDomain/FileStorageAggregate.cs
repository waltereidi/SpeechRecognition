using SpeechRecognition.FileStorageDomain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace SpeechRecognition.Tests.FileStorageDomain
{
    public class FileStorageAggregateTest
    {
        private readonly FileStorageAggregate _fileStorageAggregate; 
        public FileStorageAggregateTest() 
        { 
            _fileStorageAggregate = new FileStorageAggregate( new(Guid.NewGuid()) );
        
        
        }
        [Fact]
        public void CanAddNewFile()
        {
            _fileStorageAggregate.AddFileStorageLocal(new FileInfo("path/to/file"), "originalFileName.mp3");
             Assert.Single(_fileStorageAggregate.FileStorages);
        }
        
    }
}
