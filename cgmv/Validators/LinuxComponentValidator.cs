using cgmv.Contracts;
using cgmv.Exceptions;
using Microsoft.VisualStudio.Services.Governance.Contracts;
using System;
using System.Collections.Generic;

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
                return new ValidationResult
                {
                    IsValid = false,
                    Messages = new List<string> {Resources.MissingComponentType}
                };
            }

            if (typedComponent.Linux is null)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    Messages = new List<string> { string.Format(Resources.MissingComponentDefinition, ComponentType.Linux) }
                };
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
