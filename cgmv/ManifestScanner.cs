using cgmv.Contracts;
using cgmv.Validators;
using Microsoft.VisualStudio.Services.Governance.ComponentDetection;
using Microsoft.VisualStudio.Services.Governance.Contracts;
using Newtonsoft.Json;
using System;
using System.IO;

namespace cgmv
{
    public static class ManifestScanner
    {
        public static ScanningReport ScanManifestFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                new FileNotFoundException(filePath);
            }

            var scanningReport = new ScanningReport();

            int counter = 0;

            using var reader = new StreamReader(filePath);
            var fileReport = new FileReport();
            try
            {
                var fileContent = reader.ReadToEnd();
                fileReport.FilePath = filePath;
                AssertCgManifestContent(fileContent, fileReport);
            }
            catch (Exception ex)
            {
                fileReport.UnexpectedExceptionMessage = ex.Message;
            }

            scanningReport.FilesReport.Add(fileReport);
            counter++;

            scanningReport.NumberFilesScanned = 1;
            return scanningReport;
        }

        private static void AssertCgManifestContent(string cgManifestContent, FileReport fileReport)
        {
            var componentManifest = JsonConvert.DeserializeObject<ComponentManifest>(cgManifestContent);

            foreach (var registration in componentManifest.Registrations)
            {
                var validationResult = ComponentValidator.IsComponentValid(registration.Component);
                var componentReport = CreateComponentReportForRegistration(registration.Component, validationResult);
                fileReport.ComponentReport.Add(componentReport);
            }
        }

        private static ComponentReport CreateComponentReportForRegistration(TypedComponent component, ValidationResult validationResult)
        {
            var componentReport = new ComponentReport { ValidationPassed = validationResult.IsValid };

            if (!componentReport.ValidationPassed)
            {
                componentReport.ComponentJsonRepresentation = JsonConvert.SerializeObject(component);
                componentReport.Messages = validationResult.Messages; 
            }

            return componentReport;
        }
    }
}
