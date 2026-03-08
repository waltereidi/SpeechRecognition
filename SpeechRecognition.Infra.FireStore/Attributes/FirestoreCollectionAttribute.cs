using System;

namespace SpeechRecognition.Infra.Firestore.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class FirestoreCollectionAttribute : Attribute
    {
        public string CollectionName { get; }
        public FirestoreCollectionAttribute(string collectionName) => CollectionName = collectionName;
    }
}