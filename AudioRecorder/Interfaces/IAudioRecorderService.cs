using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioRecorder.Interfaces
{
    public interface IAudioRecorderService
    {
        void StartRecording(string outputPath);
        void StopRecording();
    }
}
