using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SpeechRecognition.Infra.FireStore.Repositories
{
    public static class FirestoreIdResolver
    {
        public static string GetId<TEntity>(TEntity entity)
        {
            var prop = typeof(TEntity).GetProperty("Id",
                BindingFlags.Public | BindingFlags.Instance);

            if (prop == null)
                throw new Exception("Entity must have Id property");

            return prop.GetValue(entity)?.ToString()
                ?? throw new Exception("Id cannot be null");
        }
    }
}
