using cgmv.Contracts;
using Microsoft.VisualStudio.Services.Governance.Contracts;
using System.Collections.Generic;

namespace cgmv.Validators
{
    /// <summary>
    /// Validator for components that uses name and version as the main properties.
    /// </summary>
    public class GeneralComponentValidator : ITypedComponentValidator
    {
        /// <summary>
        /// Validate that is a valid component with correct properties defined
        /// This validator is used for:
        /// - npm
        /// - nuget
        /// - cargo
        /// - Go
        /// - Pip
        /// - Pod
        /// - RubyGems
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

            if (typedComponent.Npm != null)
            {
                return ValidateGeneralProperties(typedComponent.Npm.Name, typedComponent.Npm.Version);
            }
            if (typedComponent.NuGet != null)
            {
                return ValidateGeneralProperties(typedComponent.NuGet.Name, typedComponent.NuGet.Version);
            }
            if (typedComponent.Cargo != null)
            {
                return ValidateGeneralProperties(typedComponent.Cargo.Name, typedComponent.Cargo.Version);
            }
            if (typedComponent.Go != null)
            {
                return ValidateGeneralProperties(typedComponent.Go.Name, typedComponent.Go.Version);
            }
            if (typedComponent.Pip != null)
            {
                return ValidateGeneralProperties(typedComponent.Pip.Name, typedComponent.Pip.Version);
            }
            if (typedComponent.Pod != null)
            {
                return ValidateGeneralProperties(typedComponent.Pod.Name, typedComponent.Pod.Version);
            }
            if (typedComponent.RubyGems != null)
            {
                return ValidateGeneralProperties(typedComponent.RubyGems.Name, typedComponent.RubyGems.Version);
            }

            return new ValidationResult
            {
                IsValid = false,
                Messages = new List<string> { string.Format(Resources.MissingComponentDefinition, typedComponent.Type) }
            };
        }

        private ValidationResult ValidateGeneralProperties(string name, string version)
        {
            var validationResult = new ValidationResult { IsValid = true };

            if (string.IsNullOrWhiteSpace(name))
            {
                validationResult.IsValid = false;
                validationResult.Messages.Add(string.Format(Resources.MissingRequiredProperty, nameof(name)));
            }
            if (string.IsNullOrWhiteSpace(version))
            {
                validationResult.IsValid = false;
                validationResult.Messages.Add(string.Format(Resources.MissingRequiredProperty, nameof(version)));
            }

            return validationResult;
        }
    }
}
