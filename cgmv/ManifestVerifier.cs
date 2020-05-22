using cgmv.Validators;
using Microsoft.VisualStudio.Services.Governance.ComponentDetection;
using Newtonsoft.Json;
using System.IO;

namespace cgmv
{
    public class ManifestVerifier
    {
        public static int AssertManifestFilesInPath(string workingPath)
        {
            var filesPath = Directory.GetFiles(workingPath, searchPattern:"cgmanifest.json");

            int counter = 0;
            foreach (var filePath in filesPath)
            {
                using var reader = new StreamReader(filePath);
                var fileContent = reader.ReadToEnd();
                AssertCgManifestContent(fileContent);
                
                counter++;
            }

            return counter;
        }

        private static void AssertCgManifestContent(string cgManifestContent)
        {
            var componentManifest = JsonConvert.DeserializeObject<ComponentManifest>(cgManifestContent);

            foreach (var registration in componentManifest.Registrations)
            {
                ComponentValidator.IsComponentValid(registration.Component);
            }
        }
    }
}
