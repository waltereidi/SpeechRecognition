// See https://aka.ms/new-console-template for more information
using System.Text;
using Whisper.net;

    var modelPath = "C:\\Users\\walte\\source\\repos\\SpeechRecognition\\WhisperSpeechRecognition\\Models\\ggml-medium.bin";
    var audioPath = "C:\\Users\\walte\\source\\repos\\SpeechRecognition\\WhisperSpeechRecognition\\output2.wav"; // WAV mono, 16kHz recomendado

    if (!File.Exists(modelPath))
    {
        Console.WriteLine("Modelo não encontrado.");
        return;
    }

    if (!File.Exists(audioPath))
    {
        Console.WriteLine("Áudio não encontrado.");
        return;
    }

    Console.OutputEncoding = Encoding.UTF8;

    // Carrega o modelo
    using var factory = WhisperFactory.FromPath(modelPath);

    // Cria o processador
    using var processor = factory.CreateBuilder()
        .WithLanguage("pt")          // pt-br
        .Build();

    Console.WriteLine("Transcrevendo...\n");

    // Processa o áudio
    using (FileStream ms = new FileStream(audioPath, FileMode.Open))
    {
        await foreach (var segment in processor.ProcessAsync(ms))
        {
            Console.WriteLine(
                $"[{segment.Start:hh\\:mm\\:ss} -> {segment.End:hh\\:mm\\:ss}] {segment.Text}"
            );
        }
    }
    Console.WriteLine("\nConcluído.");

