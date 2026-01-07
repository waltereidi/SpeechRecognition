using AudioRecorder.Interfaces;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioRecorder.Services
{

    public class AudioRecorderService : IAudioRecorderService
    {
        private WaveInEvent? _waveIn;
        private WaveFileWriter? _writer;

        public void StartRecording(string outputPath)
        {
            if (_waveIn != null)
                throw new InvalidOperationException("Gravação já em andamento.");

            _waveIn = new WaveInEvent
            {
                DeviceNumber = 0, // microfone padrão
                WaveFormat = new WaveFormat(
                    rate: 16000,     // 16 kHz (ideal para Whisper, Vosk, etc.)
                    bits: 16,
                    channels: 1      // mono
                )
            };

            _writer = new WaveFileWriter(outputPath, _waveIn.WaveFormat);

            _waveIn.DataAvailable += (s, e) =>
            {
                _writer.Write(e.Buffer, 0, e.BytesRecorded);
                _writer.Flush();
            };

            _waveIn.RecordingStopped += (s, e) =>
            {
                _writer?.Dispose();
                _waveIn?.Dispose();

                _writer = null;
                _waveIn = null;
            };

            _waveIn.StartRecording();
        }

        public void StartRecording(Stream outputStream)
        {
            if (_waveIn != null)
                throw new InvalidOperationException("Gravação já em andamento.");

            _waveIn = new WaveInEvent
            {
                DeviceNumber = 0, // microfone padrão
                WaveFormat = new WaveFormat(
                    rate: 16000,     // 16 kHz (ideal para Whisper, Vosk, etc.)
                    bits: 16,
                    channels: 1      // mono
                )
            };

            _writer = new WaveFileWriter(outputStream, _waveIn.WaveFormat);
            _waveIn.DataAvailable += (s, e) =>
            {
                _writer.Write(e.Buffer, 0, e.BytesRecorded);
                _writer.Flush();
            };

            _waveIn.RecordingStopped += (s, e) =>
            {
                _writer?.Dispose();
                _waveIn?.Dispose();

                _writer = null;
                _waveIn = null;
            };

            _waveIn.StartRecording();
        }

        public void StopRecording()
        {
            _waveIn?.StopRecording();
        }
    }
    }
