using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.Infra.FireStore.Documents.Base
{
    [FirestoreData]
    public class FireStoreBaseDocument
    {
        [FirestoreDocumentId]
        public string Id { get; set; } = default!;

    }
}
