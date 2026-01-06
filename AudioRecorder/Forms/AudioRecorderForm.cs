using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AudioRecorder.Interfaces;
using AudioRecorder.Services;

namespace AudioRecorder.Forms
{
    public partial class AudioRecorderForm : Form
    {
        private IAudioRecorderService _recorder;
        public AudioRecorderForm()
        {
            InitializeComponent();
            _recorder = new AudioRecorderService();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            _recorder.StartRecording("audio.wav");
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            _recorder.StopRecording();
        }
    }
}
