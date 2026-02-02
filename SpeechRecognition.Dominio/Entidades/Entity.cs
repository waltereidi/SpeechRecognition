using System.ComponentModel.DataAnnotations;

namespace SpeechRecognition.Dominio.Entidades
{
    public class Entity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}