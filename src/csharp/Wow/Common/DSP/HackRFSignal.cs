using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Common.DSP
{
    public class HackRFSignal : ISignal
    {
        public struct Settings
        {
            public int Frequency;
            public int SamplesPerSec;
        }

        static int BytesPerSample = 2;

        Settings _settings;
        string _filepath;
        FileStream _stream;
        byte[] _samplesCache;

        public HackRFSignal(HackRFSignal.Settings settings, string filepath)
        {
            _settings = settings;
            _filepath = filepath;
            _stream = File.OpenRead(filepath);
            _samplesCache = new byte[BytesPerSample * 1024];
        }

        public int SamplesPerSec
        {
            get { return _settings.SamplesPerSec; }
        }

        public long NumSamples
        {
            get { return (_stream.Length / BytesPerSample); }
        }

        public long Position
        {
            get { return (_stream.Position / BytesPerSample); }
        }

        public void Seek(long sampleIdx)
        {
            _stream.Seek(BytesPerSample * sampleIdx, SeekOrigin.Begin);
        }

        public unsafe long ReadSamples(Complex[] dstSamples, long dstOffset, long numSamples)
        {
            long remainingSamples = (this.NumSamples - this.Position);
            long count = Math.Min(numSamples, remainingSamples);
            long stepBy = (_samplesCache.Length / BytesPerSample);

            for (; count > 0; count -= stepBy)
            {
                long n = BytesPerSample * Math.Min(count, stepBy);
                if (n <= 0)
                    break;

                int read = _stream.Read(_samplesCache, 0, (int)n);
                read -= (read % 2);

                Complex.FromInt8Complex(dstSamples, dstOffset, _samplesCache, 0, read);
            }

            return 0;
        }
    }
}
