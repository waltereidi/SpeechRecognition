using System.Transactions;
using WhisperSpeechRecognition.Contracts;
using WhisperSpeechRecognition.DTO;
using WhisperSpeechRecognition.Interfaces;

namespace WhisperSpeechRecognition
{
    public class TranslateAudio 
    {
        public async Task<ITranslationResponseAdapter> TranslateAudioLocal(TranslationContract.Request.GeneralTranslation dto)
        {
            try
            {
                ISpeechRecognitionAbstractFactory factory = new Service.SpeechRecognitionAbstractFactory();
                var factoryDTO = new SpeechRecognitionFactoryDTO(dto);
                ITranslateAudioFacade facade = await factory.Create(factoryDTO);
                var result = await facade.TranslateAudio();

                return result;
            }
            catch(Exception ex)
            {
                ITranslationResponseAdapter result = new TranslationContract.Response.GeneralTranslation((int)_dto.Model, translation.GetResult());
            }
            
        }
    }
}
