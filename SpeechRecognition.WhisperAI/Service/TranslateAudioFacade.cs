using System;
using System.Collections.Generic;
using System.Text;
using SpeechRecognition.WhisperAI.Contracts;
using SpeechRecognition.WhisperAI.DTO;
using SpeechRecognition.WhisperAI.Enum;
using SpeechRecognition.WhisperAI.Interfaces;
using SpeechRecognition.WhisperAI.Templates;

namespace SpeechRecognition.WhisperAI.Service
{
    internal class TranslateAudioFacade : ITranslateAudioFacade
    {
        private ISpeechRecognitionStrategy _strategy;
        private TranslationTemplates _templateId;
        private SpeechRecognitionFactoryDTO _dto;
        private WhisperModels _nextModel;

        internal TranslateAudioFacade(ISpeechRecognitionStrategy model
            , TranslationTemplates template
            , SpeechRecognitionFactoryDTO dto
            , WhisperModels nextModel 
            )
        {
            this._strategy= model;
            this._templateId = template;
            this._dto = dto;
            this._nextModel = nextModel;
        }

        public async Task<ITranslationResponseAdapter> TranslateAudio()
        {
            var translation = await this._strategy.Start(_dto.AudioStream);
            
            ITranslationResponseAdapter result = new TranslationContract.Response.GeneralTranslation(
                (int)_nextModel,
                translation.GetResult(),
                (int)_templateId 
                );

            return result;
        }
    }
}
