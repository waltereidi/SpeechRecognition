using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioRecorder.Interfaces
{
    public interface IAudioRecorderService
    {
        /// <summary>
        /// Record to a file
        /// </summary>
        /// <param name="outputPath"></param>
        void StartRecording(string outputPath);
        void StartRecording(Stream outStream);
        void StopRecording();
    }
}
