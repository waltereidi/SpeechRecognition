using SpeechRecognition.WhisperAI.DTO;
using SpeechRecognition.WhisperAI.Enum;
using SpeechRecognition.WhisperAI.Interfaces;
using SpeechRecognition.WhisperAI.Templates;

namespace SpeechRecognition.WhisperAI.Service
{
    internal class SpeechRecognitionAbstractFactory : ISpeechRecognitionAbstractFactory
    {

        public async Task<ITranslateAudioFacade> Create(SpeechRecognitionFactoryDTO dto)
        {
            var template = GenerateTemplate(dto.Template);
            var model = CreateStrategy(dto.Model , template);
            ITranslateAudioFacade facade = new TranslateAudioFacade(model, template , dto );
            return facade;
        }

        private ISpeechRecognitionStrategy CreateStrategy(WhisperModels i, TranslationTemplateModel template) => i switch
        {
            WhisperModels.None => new WhisperTiny(template),
            WhisperModels.Tiny => new WhisperSmall(template),
            WhisperModels.Small => new WhisperMedium(template),
            _ => throw new NotImplementedException($"The model {i} configuration is not implemented.")
        };
        private TranslationTemplateModel GenerateTemplate(TranslationTemplates template) => template switch
        {
            TranslationTemplates.General => new GeneralTranslation(),
            _ => new GeneralTranslation(),
        };

    }
}
