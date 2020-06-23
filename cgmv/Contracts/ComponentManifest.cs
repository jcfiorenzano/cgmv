using System.Collections.Generic;
using System.Linq;

namespace Microsoft.VisualStudio.Services.Governance.ComponentDetection
{
    /// <summary>Results object for a component scan.</summary>
    public class ComponentManifest
    {
        /// <summary>A collection of Registration Requests</summary>
        public IEnumerable<Contracts.RegistrationRequest> Registrations { get; set; } = Enumerable.Empty<Contracts.RegistrationRequest>();

        // <summary> The version of the CGManifest.json file </summary>
        public int? Version { get; set; }

        public IEnumerable<Contracts.ContainerDetails> ContainerDetails { get; set; } = Enumerable.Empty<Contracts.ContainerDetails>();
    }
}
