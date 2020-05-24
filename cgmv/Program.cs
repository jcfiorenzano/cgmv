using System;

namespace cgmv
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var scannerResult = ManifestScanner.ScanManifestFiles(Environment.CurrentDirectory);
                ReportPrinter.Print(scannerResult);
            }
            catch (Exception) 
            {
            }
        }
    }
}
