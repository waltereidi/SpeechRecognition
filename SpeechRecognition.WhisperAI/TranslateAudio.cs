using System.Transactions;
using SpeechRecognition.WhisperAI.Contracts;
using SpeechRecognition.WhisperAI.DTO;
using SpeechRecognition.WhisperAI.Interfaces;
using Whisper.net;

namespace SpeechRecognition.WhisperAI
{
    public class TranslateAudio 
    {
        public WhisperFactory _factory;
        public TranslateAudio(WhisperFactory factory)
        {
            _factory = factory;
        }
        public async Task<byte[]> TranslateAudioLocal(TranslationContract.Request.GeneralTranslation dto , WhisperFactory factory)
        {
            try
            {
                ISpeechRecognitionAbstractFactory factor = new Service.SpeechRecognitionAbstractFactory( _factory );
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
