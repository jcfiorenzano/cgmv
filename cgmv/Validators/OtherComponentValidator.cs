using cgmv.Contracts;
using Microsoft.VisualStudio.Services.Governance.Contracts;
using System.Collections.Generic;

namespace cgmv.Validators
{
    /// <summary>
    /// Validator for Other components.
    /// </summary>
    public class OtherComponentValidator : ITypedComponentValidator
    {
        /// <summary>
        /// Validate that the typed component is a other component with correct properties defined
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
                    Messages = new List<string> { Resources.MissingComponentType }
                };
            }

            if (typedComponent.Other is null)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    Messages = new List<string> { string.Format(Resources.MissingComponentDefinition, ComponentType.Other) }
                };
            }

            var validationResult = new ValidationResult { IsValid = true };

            if (string.IsNullOrWhiteSpace(typedComponent.Other.Name))
            {
                validationResult.IsValid = false;
                validationResult.Messages.Add(string.Format(Resources.MissingRequiredProperty, nameof(typedComponent.Other.Name)));
            }
            if (string.IsNullOrWhiteSpace(typedComponent.Other.Version))
            {
                validationResult.IsValid = false;
                validationResult.Messages.Add(string.Format(Resources.MissingRequiredProperty, nameof(typedComponent.Other.Version)));
            }
            if (typedComponent.Other.DownloadUrl is null)
            {
                validationResult.IsValid = false;
                validationResult.Messages.Add(string.Format(Resources.MissingRequiredProperty, nameof(typedComponent.Other.DownloadUrl)));
            }

            return validationResult;
        }
    }
}
