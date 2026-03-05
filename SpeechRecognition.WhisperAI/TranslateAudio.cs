using System.Transactions;
using SpeechRecognition.WhisperAI.Contracts;
using SpeechRecognition.WhisperAI.DTO;
using SpeechRecognition.WhisperAI.Interfaces;
using Whisper.net;

namespace SpeechRecognition.WhisperAI
{
    public class TranslateAudio 
    {

        public async Task<byte[]> TranslateAudioLocal(TranslationContract.Request.GeneralTranslation dto)
        {
            try
            {
                ISpeechRecognitionAbstractFactory factor = new Service.SpeechRecognitionAbstractFactory();
                var factoryDTO = new SpeechRecognitionFactoryDTO(dto);
                ITranslateAudioFacade facade = await factor.Create(factoryDTO);
                var result = await facade.TranslateAudio();
                return result.GetJsonBytea();
            }
            catch(Exception ex)
            {
                var result = new TranslationContract.Response.GeneralTranslation(ex.Message);
                return result.GetJsonBytea();
            }
            
        }
    }
}
