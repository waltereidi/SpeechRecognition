using Google.Cloud.Firestore;

namespace SpeechRecognition.Infra.FireStore.Context
{
    public class FirestoreDbContext
    {
        private readonly FirestoreDb _firestore;

        public FirestoreDbContext(string projectId, string credentialPath)
        {
            var basePath = AppContext.BaseDirectory;

            var credentialFullPath = Path.Combine(
                basePath,
                "Properties",
                credentialPath
            );

            Environment.SetEnvironmentVariable(
                "GOOGLE_APPLICATION_CREDENTIALS",
                credentialFullPath
            );

            _firestore = FirestoreDb.Create(projectId);
        }

        public FirestoreDb Db => _firestore;

    }
}
