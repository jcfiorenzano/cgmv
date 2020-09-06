using cgmv.Contracts;
using Microsoft.VisualStudio.Services.Governance.Contracts;
using System.Collections.Generic;

namespace cgmv.Validators
{
    /// <summary>
    /// Validator for Maven components.
    /// </summary>
    public class MavenComponentValidator : ITypedComponentValidator
    {
        /// <summary>
        /// Validate that the typed component is a maven component with correct properties defined
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

            if (typedComponent.Maven is null)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    Messages = new List<string> { string.Format(Resources.MissingComponentDefinition, ComponentType.Maven) }
                };
            }

            var validationResult = new ValidationResult { IsValid = true, Messages = new List<string>()};
            
            if (string.IsNullOrWhiteSpace(typedComponent.Maven.GroupId))
            {
                validationResult.IsValid = false;
                validationResult.Messages.Add(string.Format(Resources.MissingRequiredProperty, nameof(typedComponent.Maven.GroupId)));
            }
            if (string.IsNullOrWhiteSpace(typedComponent.Maven.ArtifactId))
            {
                validationResult.IsValid = false;
                validationResult.Messages.Add(string.Format(Resources.MissingRequiredProperty, nameof(typedComponent.Maven.ArtifactId)));
            }
            if (string.IsNullOrWhiteSpace(typedComponent.Maven.Version))
            {
                validationResult.IsValid = false;
                validationResult.Messages.Add(string.Format(Resources.MissingRequiredProperty, nameof(typedComponent.Maven.Version)));
            }


            return validationResult;

        }
    }
}
