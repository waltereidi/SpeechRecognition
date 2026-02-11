using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace SpeechRecognition.Dominio.Entidades.Base
{
    public abstract class EntityBase<TId>
    {
        public TId Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}