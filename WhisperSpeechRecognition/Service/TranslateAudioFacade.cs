using System;
using System.Collections.Generic;
using System.Text;
using WhisperSpeechRecognition.Contracts;
using WhisperSpeechRecognition.DTO;
using WhisperSpeechRecognition.Interfaces;
using WhisperSpeechRecognition.Templates;

namespace WhisperSpeechRecognition.Service
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
