using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Cloo;

namespace OpenCLTest1
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        static void Panic(string msg)
        {
            Console.Write(msg);
            Environment.Exit(1);
        }

        static void PrintOpenCLInfo()
        {
            PrintOpenCLPlatformInfo();
            foreach (var platform in ComputePlatform.Platforms)
                PrintOpenCLDeviceInfo(platform);
        }

        static void PrintOpenCLPlatformInfo()
        {
            if (ComputePlatform.Platforms.Count <= 0)
                Console.WriteLine("No OpenCL platform found");
            else
                Console.WriteLine("Found {0} platform(s).", ComputePlatform.Platforms.Count);

            for (int i = 0; i < ComputePlatform.Platforms.Count; i++)
                Console.WriteLine("\t Platform ({0}):  {1}", i, ComputePlatform.Platforms[i].Name);
        }

        static void PrintOpenCLDeviceInfo(ComputePlatform platform)
        {
            if (platform.Devices.Count <= 0)
                Console.WriteLine("No OpenCL devices found for platform {0}", platform.Name);
            else
                Console.WriteLine("Found {0} device(s) for platform {1}:", platform.Devices.Count, platform.Name);

            for (int i = 0; i < platform.Devices.Count; i++)
                Console.WriteLine("\t Device ({0}):  {1}", i, platform.Devices[i].Name);
        }
    }
}
