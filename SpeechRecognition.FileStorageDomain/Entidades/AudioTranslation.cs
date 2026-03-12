using SpeechRecognition.CrossCutting.Framework;
using SpeechRecognition.FileStorageDomain.DomainEvents;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static SpeechRecognition.FileStorageDomain.Entidades.FileStorage;

namespace SpeechRecognition.FileStorageDomain.Entidades;

public class AudioTranslation : Entity<AudioTranslationId>
{
    public AudioTranslation() 
    {

    }
    public AudioTranslation(Action<object> applier) : base(applier)
    {
    }
    public override void SetId(string id)
    {
        this.Id = new AudioTranslationId( Guid.Parse(id) );
    }
    public string Translation { get;  set; }
    [Required]
    public FileStorageId FileStorageId { get;  set; }

    /// <summary>
    /// Preenchido por whisper, para indicar um erro no resultado
    /// </summary>
    public bool IsSuccess { get;  set; }
    /// <summary>
    /// Preenchido pelo usuário, para indicar se a tradução foi aprovada
    /// </summary>
    public bool? IsApproved { get;  set; }
    public int? TranslationTemplate { get;  set; }
    public int? WhisperModel { get;  set; }
    protected override void When(object @event)
    {
        switch (@event)
        {
            case Events.TranslationAdded e:
                Id = new AudioTranslationId(Guid.NewGuid());
                FileStorageId = e.fileStorageid;
                Translation = e.translation;
                IsSuccess = e.isSuccess;
                TranslationTemplate = e.templateId;
                WhisperModel = e.modelId;
                break;
            
        }
    }

}

public class AudioTranslationId : Value<AudioTranslationId>
{
    protected AudioTranslationId() { }
    private Guid Value { get; set; }

    public AudioTranslationId(Guid value)
    {
        if (value == default)
            throw new ArgumentNullException(nameof(value), "User id cannot be empty");

        Value = value;
    }


    public static implicit operator Guid(AudioTranslationId self) => self.Value;

    public static implicit operator AudioTranslationId(string value)
        => new AudioTranslationId(Guid.Parse(value));

    public override string ToString() => Value.ToString();
}
