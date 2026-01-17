using SpeechRecognition.Dominio.Entidades;
using WhisperSpeechRecognition.Interfaces;
using WhisperSpeechRecognition.Service;
using WhisperSpeechRecognition.Templates;


namespace WhisperSpeechRecognition
{
    public class SpeechRecognitionAbstractFactory : ISpeechRecognitionAbstractFactory
    {
        private readonly AudioTranslation _entity;

        public async Task<WhisperModel> Create(AudioTranslation fsc)
        {
            var template = GenerateTemplate(fsc.TranslationTemplate.TemplateName );
            throw new NotImplementedException();
        }
        private TranslationTemplateModel GenerateTemplate(string templateName) => templateName switch
        {
            "" => new GeneralTranslation(),
            _ => new GeneralTranslation(),
        };
        
    }
}
