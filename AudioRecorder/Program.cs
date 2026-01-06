using System;
using System.Windows.Forms;
using AudioRecorder.Forms;
using AudioRecorder.Interfaces;
using AudioRecorder.Services;

namespace AudioRecorder
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new AudioRecorderForm());
        }
    }
}
