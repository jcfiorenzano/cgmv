using cgmv.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cgmv
{
    public static class ReportPrinter
    {
        public static void Print(ScanningReport report)
        {
            Console.WriteLine($"Number of files scanned: {report.NumberFilesScanned}");
            var failedFilesReport = FailedFilesReport(report);
            Console.WriteLine($"Number of files succeeded: {report.NumberFilesScanned - failedFilesReport.Count}");
            Console.WriteLine($"Number of files failed: {failedFilesReport.Count}");
            Console.WriteLine();

            if (failedFilesReport.Any())
            {
                PrintFailedFiles(failedFilesReport);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Succeed!!!");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        private static void PrintFailedFiles(IList<FileReport> filesReport)
        {
            foreach (var fileReport in filesReport)
            {
                PrintDivider();
                Console.WriteLine($"Path: {fileReport.FilePath}");
                Console.WriteLine();
                if (!string.IsNullOrWhiteSpace(fileReport.UnexpectedExceptionMessage))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Exception Parsing: {fileReport.UnexpectedExceptionMessage}");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                var failedComponents = fileReport.ComponentReport.Where(c => !c.ValidationPassed).ToList();

                PrintComponentReport(failedComponents);

                Console.WriteLine();
            }
        }

        private static void PrintComponentReport(IList<ComponentReport> componentsReport)
        {
            if (componentsReport.Any())
            {
                Console.WriteLine("Failed Component(s):");
                Console.WriteLine();
            }
            foreach (var componentReport in componentsReport)
            {
                Console.WriteLine($"    {componentReport.ComponentJsonRepresentation}");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("     Message(s):");
                Console.ForegroundColor = ConsoleColor.White;
                foreach (var message in componentReport.Messages)
                {
                    Console.WriteLine($"     - {message}");
                }
                Console.WriteLine();
                Console.WriteLine();
            }
        }

        private static void PrintDivider()
        {
            for (int i = 0; i < Console.BufferWidth; i++)
            {
                Console.Write("#");
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        private static IList<FileReport> FailedFilesReport(ScanningReport report)
        {
            var filesFailed = new List<FileReport>();
            foreach(var fileReport in report.FilesReport)
            {
                if (!string.IsNullOrWhiteSpace(fileReport.UnexpectedExceptionMessage) || fileReport.ComponentReport.Any(cr => !cr.ValidationPassed))
                {
                    filesFailed.Add(fileReport);
                }
            }

            return filesFailed;
        }
    }
}
