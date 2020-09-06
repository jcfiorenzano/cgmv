using cgmv.Contracts;
using Microsoft.VisualStudio.Services.Governance.Contracts;
using System;
using System.Collections.Generic;

namespace cgmv.Validators
{
    /// <summary>
    /// Validator for DockerImages components.
    /// </summary>
    public class DockerImageComponentValidator : ITypedComponentValidator
    {
        /// <summary>
        /// Validate that the typed component is a docker image component with correct properties defined
        /// </summary>
        /// <param name="typedComponent">Component to validate properties</param>
        /// <returns>Validation results.</returns>
        public ValidationResult IsValid(TypedComponent typedComponent)
        {
            return new ValidationResult()
            {
                IsValid = false,
                Messages = new List<string> { string.Format(Resources.ComponentTypeNotSupportedMessage, ComponentType.DockerImage) }
            };
        }
    }
}
