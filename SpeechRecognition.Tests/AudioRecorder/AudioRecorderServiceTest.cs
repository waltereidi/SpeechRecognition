using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using AudioRecorder.Interfaces;
using AudioRecorder.Services;

namespace SpeechRecognition.Tests.AudioRecorder
{
    public class AudioRecorderServiceTest : Configuration
    {
        private readonly IAudioRecorderService _serviceProvider;
        public AudioRecorderServiceTest()
        {
            _serviceProvider = new AudioRecorderService();
        }
        [Fact]
        public void TestAudioRecorder()
        {
            _serviceProvider.StartRecording(Path.Combine(base._fileOutputDir.FullName, "AudioRecorderServiceTest.TestAudioRecorder.wav"));
            _serviceProvider.StopRecording();
        }
        [Fact]
        public void TestRecordToStream()
        {
            var stream = new MemoryStream();

            _serviceProvider.StartRecording(stream);
            Thread.Sleep(5000);
            _serviceProvider.StopRecording();

        }
    }
}
