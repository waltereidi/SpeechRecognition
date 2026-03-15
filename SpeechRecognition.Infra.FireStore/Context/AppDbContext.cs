using Google.Cloud.Firestore;

namespace SpeechRecognition.Infra.FireStore.Context
{
    public class FirestoreDbContext
    {
        private readonly FirestoreDb _firestore;

        public FirestoreDbContext(string projectId, string credentialPath)
        {
            Environment.SetEnvironmentVariable(
                "windy-ellipse-399512S",
                credentialPath
            );

            _firestore = FirestoreDb.Create(projectId);
        }

        public FirestoreDb Db => _firestore;

    }
}
