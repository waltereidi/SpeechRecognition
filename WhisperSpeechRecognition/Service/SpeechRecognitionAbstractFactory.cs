using SpeechRecognition.Dominio.Entidades;
using SpeechRecognition.Dominio.Enum;
using WhisperSpeechRecognition.Interfaces;
using WhisperSpeechRecognition.Templates;

namespace WhisperSpeechRecognition.Service
{
    public class SpeechRecognitionAbstractFactory : ISpeechRecognitionAbstractFactory
    {
        public async Task<ISpeechRecognitionStrategy> Create(AudioTranslation fsc)
        {
            var template = GenerateTemplate(fsc.TranslationTemplate.TemplateName );
            var model = CreateWhisperModel(fsc.WhisperModel, template);

            return model;
        }
        private ISpeechRecognitionStrategy CreateWhisperModel(WhisperModels i, TranslationTemplateModel template) => i switch
        {
            WhisperModels.None => new WhisperMedium(template),
            _ => throw new NotImplementedException($"The model {i} is not implemented.")
        };
        private TranslationTemplateModel GenerateTemplate(string templateName) => templateName switch
        {
            "GeneralTranslation" => new GeneralTranslation(),
            _ => new GeneralTranslation(),
        };

    }
}
