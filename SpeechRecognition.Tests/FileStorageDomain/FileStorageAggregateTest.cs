using SpeechRecognition.FileStorageDomain;
using SpeechRecognition.FileStorageDomain.DomainEvents;
using SpeechRecognition.FileStorageDomain.Entidades;
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
            _fileStorageAggregate = new FileStorageAggregate(new(Guid.NewGuid()));
        }
        [Fact]
        public void CanAddNewFile()
        {
            _fileStorageAggregate.AddFileStorageLocal(new(Guid.NewGuid()),new FileInfo("path/to/file"), "originalFileName.mp3");
            Assert.Single(_fileStorageAggregate.FileStorages);
        }
        [Fact]
        public void CanAddNewFileConversion()
        {
            _fileStorageAggregate.AddFileStorageLocal(new(Guid.NewGuid()),new FileInfo("path/to/file"), "originalFileName.mp3");
            _fileStorageAggregate.AddFileStorageConversionLocal(new FileInfo("path/to/converted/file"), new(Guid.NewGuid()));
            Assert.Single(_fileStorageAggregate.FileStorageConversions);
        }
        [Fact]
        public void CanAddNewTranslation()
        {
            _fileStorageAggregate.AddFileStorageLocal(new(Guid.NewGuid()), new FileInfo("path/to/file"), "originalFileName.mp3");
            FileStorageId createId = new(Guid.NewGuid());
            _fileStorageAggregate.AddFileStorageConversionLocal(new FileInfo("path/to/converted/file"), createId);
            _fileStorageAggregate.AddTranslation(new(createId, "translation", 1, 1, true));
            Assert.Single(_fileStorageAggregate.AudioTranslations);
        }
    }
}
