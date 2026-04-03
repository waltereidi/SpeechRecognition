using SpeechRecognition.WhisperAI.DTO;
using SpeechRecognition.WhisperAI.Enum;
using SpeechRecognition.WhisperAI.Interfaces;
using SpeechRecognition.WhisperAI.Templates;
using Whisper.net;

namespace SpeechRecognition.WhisperAI.Service
{
    internal class SpeechRecognitionAbstractFactory : ISpeechRecognitionAbstractFactory
    {

        public async Task<ITranslateAudioFacade> Create(SpeechRecognitionFactoryDTO dto)
        {
            var templateEnum = GetTemplateEnum(dto.Template);

            var template = GenerateTemplate(templateEnum);

            var nextModel = NextModel(dto.Model);

            var strategy = CreateStrategy(dto.Model , template);
            ITranslateAudioFacade facade = new TranslateAudioFacade(strategy, templateEnum , dto , nextModel);
            return facade;
        }

        private WhisperModels NextModel(WhisperModels i) => i switch
        {
            //WhisperModels.None => WhisperModels.Tiny,
            WhisperModels.None => WhisperModels.Medium,
            WhisperModels.Tiny => WhisperModels.Small,
            WhisperModels.Small => WhisperModels.Medium,
            _ => throw new NotImplementedException($"The model {i} configuration is not implemented.")
        };

        private ISpeechRecognitionStrategy CreateStrategy(WhisperModels i, TranslationTemplateModel template) => i switch
        {
            //WhisperModels.None => new WhisperTiny(template),
            WhisperModels.None => new WhisperSmall(template),
            WhisperModels.Tiny => new WhisperSmall(template),
            WhisperModels.Small => new WhisperMedium(template),
            _ => throw new NotImplementedException($"The model {i} configuration is not implemented.")
        };
        private TranslationTemplates GetTemplateEnum(TranslationTemplates template) => template switch
        {
            TranslationTemplates.None =>  TranslationTemplates.General,
            TranslationTemplates.General => TranslationTemplates.General,
            _ => throw new ArgumentOutOfRangeException($"Invalid template number {nameof(template)}"),
        };

        private TranslationTemplateModel GenerateTemplate(TranslationTemplates template) => template switch
        {
            TranslationTemplates.General => new GeneralTranslation(),
            _ => throw new ArgumentOutOfRangeException($"Invalid template number {nameof(template)}"),
        };

    }
}
