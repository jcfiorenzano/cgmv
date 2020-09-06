using System;

namespace cgmv
{
    class Program
    {
        /// <summary>
        /// Validate that the structure of cgmanifest.json files are correct
        /// </summary>
        /// <param name="path">Path to the cgmanifest file</param>
        public static void Main(string path)
        {
            try
            {
                var scannerResult = ManifestScanner.ScanManifestFile(path);
                ReportPrinter.Print(scannerResult);
                Console.WriteLine();
                Console.WriteLine("You can find all the documenation associated with the cgmanifest file in: https://docs.opensource.microsoft.com/tools/cg/cgmanifest.html");
                Console.WriteLine("An example of a cgmanifest can be found in : https://mseng.visualstudio.com/AzureDevOps/_git/Governance.Specs?path=%2Fcgmanifest.json&version=GBmaster&_a=contents");
                Console.WriteLine();
            }
            catch (Exception) 
            {
            }
        }
    }
}
