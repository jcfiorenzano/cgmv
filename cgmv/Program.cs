using System;
using System.IO;

namespace cgmv
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var scannerResult = ManifestScanner.ScanManifestFiles(Environment.CurrentDirectory);
            }
            catch (Exception) 
            {
            }
        }
    }
}
