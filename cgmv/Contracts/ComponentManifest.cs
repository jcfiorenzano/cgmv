using System.Collections.Generic;

namespace Microsoft.VisualStudio.Services.Governance.ComponentDetection
{
    /// <summary>Results object for a component scan.</summary>
    public class ComponentManifest
    {
        /// <summary>A collection of Registration Requests</summary>
        public IEnumerable<Contracts.RegistrationRequest> Registrations { get; set; }

        // <summary> The version of the CGManifest.json file </summary>
        public int? Version { get; set; }

        public IEnumerable<Contracts.ContainerDetails> ContainerDetails { get; set; }
    }
}
