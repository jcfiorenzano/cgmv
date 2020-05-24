using System;

namespace cgmv
{
    class Program
    {
        /// <summary>
        /// Validate that the structure of cgmanifest.json files are correct
        /// </summary>
        /// <param name="Path">Path to the cgmanifest file</param>
        public static void Main(string Path)
        {
            try
            {
                var scannerResult = ManifestScanner.ScanManifestFile(Path);
                ReportPrinter.Print(scannerResult);
            }
            catch (Exception) 
            {
            }
        }
    }
}
