using AudioRecorder.Interfaces;
using AudioRecorder.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WhisperSpeechRecognition.Interfaces;
using WhisperSpeechRecognition.Service;

namespace AudioRecorderForms
{
    public partial class AudioRecorderForm : Form
    {
        private readonly IAudioRecorderService _recorder;
        private readonly ISpeechRecognition _model;
        private MemoryStream _audioStream { get; set; }
        public AudioRecorderForm()
        {
            InitializeComponent();
            _recorder = new AudioRecorderService();
            _model = new WhisperModel();
            stopButton.Enabled = false;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            _audioStream = new MemoryStream();
            //_recorder.StartRecording(_audioStream);
            _recorder.StartRecording("forms.wav");
            startButton.Enabled = false;
            stopButton.Enabled = true;
        }

        private void stopButton_Click(object sender, EventArgs e)
        {

            _recorder.StopRecording();
           
            startButton.Enabled = true;
            stopButton.Enabled = false;
        }

        private void translateButton_Click(object sender, EventArgs e)
        {
            var audio = _audioStream;
            new FileStream("forms.wav", FileMode.Open).CopyTo(audio);
            string result = _model.GetTranslation(audio).Result;
            richTextBox1.Text = result;
        }
    }
}
