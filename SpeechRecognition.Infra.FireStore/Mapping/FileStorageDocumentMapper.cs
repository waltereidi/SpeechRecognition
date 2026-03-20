using SpeechRecognition.FileStorageDomain.Entidades;
using SpeechRecognition.Infra.Firestore.Documents;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.Infra.FireStore.Mapping
{
    public static class FileStorageDocumentMapper
    {
        public static FileStorageDocument ToDocument(FileStorage entity)
        {
            return new FileStorageDocument
            {
                Id = entity.Id.ToString(),
                OriginalFileName = entity.OriginalFileName,
                FileInfo = entity.FileInfo == null ? null : new FileInfoDocument
                {
                    FullName = entity.FileInfo.FullName,
                    Name = entity.FileInfo.Name,
                    Length = entity.FileInfo.Length,
                    Extension = entity.FileInfo.Extension,
                    CreationTimeUtc = entity.FileInfo.CreationTimeUtc
                }
            };
        }
        public static FileStorage ToDomain(FileStorageDocument doc)
        {
            if (doc == null)
                return null;

            var entity = new FileStorage();

            // Id
            if (!string.IsNullOrEmpty(doc.Id))
                entity.SetId(doc.Id);

            // FileInfo (precisa reconstruir manualmente)
            if (doc.FileInfo != null)
            {
                entity.FileInfo = new FileInfo(doc.FileInfo.FullName);
            }

            // Nome original
            entity.OriginalFileName = doc.OriginalFileName;

            return entity;
        }

    }
}
