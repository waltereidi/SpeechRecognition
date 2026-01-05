using System;
using System.IO;
using Vosk;
using Newtonsoft.Json.Linq;

class Program
{
    static void Main()
    {
        // Inicializa o Vosk
        Vosk.Vosk.SetLogLevel(0);

        string modelPath = "C:\\Users\\walte\\source\\repos\\SpeechRecognition\\SpeechRecognition\\models\\vosk-model-small-pt-0.3";
        string audioPath = "C:\\Users\\walte\\source\\repos\\SpeechRecognition\\SpeechRecognition\\Files\\output2.wav";

        if (!Directory.Exists(modelPath))
        {
            Console.WriteLine("Modelo não encontrado.");
            return;
        }

        if (!File.Exists(audioPath))
        {
            Console.WriteLine("Arquivo de áudio não encontrado.");
            return;
        }

        Vosk.Vosk.SetLogLevel(0);
        using var model = new Model(modelPath);
        using var recognizer = new VoskRecognizer(model, 16000.0f);

        using var fs = File.OpenRead(audioPath);

        byte[] buffer = new byte[4096];
        int bytesRead;

        while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) > 0)
        {
            recognizer.AcceptWaveform(buffer, bytesRead);
        }

        string resultJson = recognizer.FinalResult();

        // Extrai apenas o texto
        var json = JObject.Parse(resultJson);
        string texto = json["text"]?.ToString();

        Console.WriteLine("Texto reconhecido:");
        Console.WriteLine(texto);
    }

}
