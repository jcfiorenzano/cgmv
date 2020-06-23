using cgmv.Contracts;
using cgmv.Exceptions;
using Microsoft.VisualStudio.Services.Governance.Contracts;
using System;

namespace cgmv.Validators
{
    /// <summary>
    /// Validator for Linux components.
    /// </summary>
    public class LinuxComponentValidator : ITypedComponentValidator
    {
        /// <summary>
        /// Validate that the typed component is a linux component with correct properties defined
        /// </summary>
        /// <param name="typedComponent">Component to validate properties</param>
        /// <returns>Validation results.</returns>
        public ValidationResult IsValid(TypedComponent typedComponent)
        {
            if (typedComponent is null)
            {
                throw new ArgumentNullException(nameof(typedComponent));
            }

            if (typedComponent.Linux is null)
            {
                throw new MissingValidComponentException();
            }

            var validationResult = new ValidationResult { IsValid = true };

            if (string.IsNullOrWhiteSpace(typedComponent.Linux.Name))
            {
                validationResult.IsValid = false;
                validationResult.Messages.Add(string.Format(Resources.MissingRequiredProperty, nameof(typedComponent.Linux.Name)));
            }
            if (string.IsNullOrWhiteSpace(typedComponent.Linux.Release))
            {
                validationResult.IsValid = false;
                validationResult.Messages.Add(string.Format(Resources.MissingRequiredProperty, nameof(typedComponent.Linux.Release)));
            }
            if (string.IsNullOrWhiteSpace(typedComponent.Linux.Version))
            {
                validationResult.IsValid = false;
                validationResult.Messages.Add(string.Format(Resources.MissingRequiredProperty, nameof(typedComponent.Linux.Version)));
            }

            return validationResult;
        }
    }
}
