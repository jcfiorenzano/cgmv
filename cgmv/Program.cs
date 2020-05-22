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
                int totalOfFilesVerified = ManifestVerifier.AssertManifestFilesInPath(Environment.CurrentDirectory);
                Console.WriteLine($"{totalOfFilesVerified} file(s) inspected");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Succeed");
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch (Exception) 
            {
            }
        }
    }
}
