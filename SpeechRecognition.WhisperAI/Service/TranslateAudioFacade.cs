using System;
using System.Collections.Generic;
using System.Text;
using SpeechRecognition.WhisperAI.Contracts;
using SpeechRecognition.WhisperAI.DTO;
using SpeechRecognition.WhisperAI.Interfaces;
using SpeechRecognition.WhisperAI.Templates;

namespace SpeechRecognition.WhisperAI.Service
{
    internal class TranslateAudioFacade : ITranslateAudioFacade
    {
        private ISpeechRecognitionStrategy _strategy;
        private TranslationTemplateModel _template;
        private SpeechRecognitionFactoryDTO _dto;

        internal TranslateAudioFacade(ISpeechRecognitionStrategy model, TranslationTemplateModel template, SpeechRecognitionFactoryDTO dto)
        {
            this._strategy= model;
            this._template = template;
            this._dto = dto;
        }

        public async Task<ITranslationResponseAdapter> TranslateAudio()
        {
            var translation = await this._strategy.Start(_dto.AudioStream);
            ITranslationResponseAdapter result = new TranslationContract.Response.GeneralTranslation((int)_dto.Model,translation.GetResult());
            return result;
        }
    }
}
