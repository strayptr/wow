using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DSP
{
    public interface ISignal
    {
        int SamplesPerSec { get; }
        long NumSamples { get; }
        long Position { get; }
        void Seek(long sampleIdx);

        long ReadSamples(Complex[] dst, long dstOffset, long count);
    }
}
