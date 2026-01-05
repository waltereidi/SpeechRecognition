using AudioRecorder.Interfaces;
using AudioRecorder.Services;

namespace AudioRecorder
{
    class Program
    {
        static void Main()
        {
            IAudioRecorderService recorder = new AudioRecorderService();

            Console.WriteLine("Pressione ENTER para iniciar a gravação...");
            Console.ReadLine();

            recorder.StartRecording("audio.wav");
            Console.WriteLine("Gravando... Pressione ENTER para parar.");

            Console.ReadLine();

            recorder.StopRecording();
            Console.WriteLine("Áudio salvo em audio.wav");
        }
    }
}
