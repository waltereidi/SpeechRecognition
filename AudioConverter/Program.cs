using Xabe.FFmpeg;

namespace AudioConverter
{
    class Program
    {
        static async Task Main()
        {
            FFmpeg.SetExecutablesPath(@"C:\ProgramData\chocolatey\bin");

            string inputFile = "C:\\Users\\walte\\source\\repos\\SpeechRecognition\\AudioConverter\\input.wav";
            string outputFile = "C:\\Users\\walte\\source\\repos\\SpeechRecognition\\AudioConverter\\output.wav";

            var conversion = FFmpeg.Conversions.New()
                .AddParameter($"-i \"{inputFile}\"", ParameterPosition.PreInput)
                .AddParameter("-ac 1")        // mono
                .AddParameter("-ar 16000")    // 16kHz
                .AddParameter("-vn")          // remove vídeo se existir
                .SetOutput(outputFile);

            await conversion.Start();

            Console.WriteLine("Conversão concluída!");
        }
    }
}
