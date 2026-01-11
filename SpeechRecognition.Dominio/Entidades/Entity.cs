using System.ComponentModel.DataAnnotations;

namespace SpeechRecognition.Dominio.Entidades
{
    public class Entity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}