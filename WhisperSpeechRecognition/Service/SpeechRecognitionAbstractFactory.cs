using WhisperSpeechRecognition.DTO;
using WhisperSpeechRecognition.Enum;
using WhisperSpeechRecognition.Interfaces;
using WhisperSpeechRecognition.Templates;

namespace WhisperSpeechRecognition.Service
{
    public class SpeechRecognitionAbstractFactory : ISpeechRecognitionAbstractFactory
    {

        //public async Task<ISpeechRecognitionStrategy> Create(AudioTranslation entity)
        //{
        //    var template = GenerateTemplate(entity.TranslationTemplate.TemplateName );
        //    var model = CreateWhisperModel(entity.WhisperModel, template);

        //    return model;
        //}

        public async Task<ITranslateAudioFacade> Create(SpeechRecognitionFactoryDTO dto)
        {
            var template = GenerateTemplate(dto.Template);
            var model = CreateStrategy(dto.Model , template);
            ITranslateAudioFacade facade = new TranslateAudioFacade(model, template , dto );
            return model;
        }

        private ISpeechRecognitionStrategy CreateStrategy(WhisperModels i, TranslationTemplateModel template) => i switch
        {
            WhisperModels.None => new WhisperMedium(template),
            _ => throw new NotImplementedException($"The model {i} is not implemented.")
        };
        private TranslationTemplateModel GenerateTemplate(TranslationTemplates template) => template switch
        {
            TranslationTemplates.General => new GeneralTranslation(),
            _ => new GeneralTranslation(),
        };

    }
}
