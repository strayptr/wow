using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DSP
{
    public struct Complex
    {
        public float Re;
        public float Im;

        public Complex(float re, float im)
        {
            Re = re;
            Im = im;
        }

        public static void FromUInt8Complex(
            Complex[] dst, long dstOffset, 
            byte[] src, long srcOffset, long srcNumBytes)
        {
            long at = dstOffset;
            for (long i = srcOffset; i < srcOffset + srcNumBytes; i += 2, at++)
            {
                dst[at].Re = (src[i]   / 256.0f) * 2.0f - 1.0f;
                dst[at].Im = (src[i+1] / 256.0f) * 2.0f - 1.0f;
            }
        }

        public static unsafe void FromInt8Complex(
            Complex[] dst, long dstOffset, 
            byte[] src, long srcOffset, long srcNumBytes)
        {
            fixed (byte* p = src)
            {
                sbyte* psrc = (sbyte*)p;
                long at = dstOffset;
                for (long i = srcOffset; i < srcOffset + srcNumBytes; i += 2, at++)
                {
                    dst[at].Re = ((psrc[i] + 128) / 256.0f) * 2.0f - 1.0f;
                    dst[at].Im = ((psrc[i + 1] + 128) / 256.0f) * 2.0f - 1.0f;
                }
            }
        }
    }
}
