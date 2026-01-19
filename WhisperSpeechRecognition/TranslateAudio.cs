using SpeechRecognition.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using WhisperSpeechRecognition.Interfaces;

namespace WhisperSpeechRecognition
{
    public class TranslateAudio : ITranslateAudioFacade
    {
        public async Task<string> TranslateAudioLocal(AudioTranslation entity)
        {
            try
            {
                ISpeechRecognitionAbstractFactory factory = new Service.SpeechRecognitionAbstractFactory();
                var strategy = await factory.Create(entity);
                var result = await strategy.Start(entity.FileStorage.FileInfo.Open(FileMode.Open));

                return result ?? throw new Exception();

            }catch(Exception ex)
            {

            }
            
        }
    }
}
