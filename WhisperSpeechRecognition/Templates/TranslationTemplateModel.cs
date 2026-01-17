using System;
using System.Collections.Generic;
using System.Text;
using Whisper.net;

namespace WhisperSpeechRecognition.Templates
{
    public abstract class TranslationTemplateModel
    {
        protected List<SegmentData> _segments = new List<SegmentData>();
        public TranslationTemplateModel()
        {
        }
        public virtual void AddSegment(SegmentData segment)
            => _segments.Add(segment);
        public abstract string GetResult();
    }

}
