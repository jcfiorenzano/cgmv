using cgmv.Contracts;
using cgmv.Exceptions;
using Microsoft.VisualStudio.Services.Governance.Contracts;
using System;

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

            if (typedComponent is null)
            {
                throw new ArgumentNullException(nameof(typedComponent));
            }

            if (typedComponent.DockerImage is null)
            {
                throw new MissingValidComponentException();
            }

            var validationResult = new ValidationResult { IsValid = true };

            if (string.IsNullOrWhiteSpace(typedComponent.DockerImage.Digest))
            {
                validationResult.IsValid = false;
                validationResult.Messages.Add(string.Format(Resources.MissingRequiredProperty, nameof(typedComponent.DockerImage.Digest)));
            }

            return validationResult;
        }
    }
}
