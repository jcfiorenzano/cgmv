using cgmv.Contracts;
using cgmv.Exceptions;
using Microsoft.VisualStudio.Services.Governance.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace cgmv.Validators
{
    /// <summary>
    /// Validator for Git components.
    /// </summary>
    public class GitComponentValidator : ITypedComponentValidator
    {
        /// <summary>
        /// Validate that the typed component is a git component with correct properties defined
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

            if (typedComponent.Git is null)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    Messages = new List<string> { string.Format(Resources.MissingComponentDefinition, ComponentType.Git) }
                };
            }

            var validationResult = new ValidationResult { IsValid = true };

            if (string.IsNullOrWhiteSpace(typedComponent.Git.CommitHash))
            {
                validationResult.IsValid = false;
                validationResult.Messages.Add(string.Format(Resources.MissingRequiredProperty, nameof(typedComponent.Git.CommitHash)));
            }
            if (typedComponent.Git.RepositoryUrl is null)
            {
                validationResult.IsValid = false;
                validationResult.Messages.Add(string.Format(Resources.MissingRequiredProperty, nameof(typedComponent.Git.RepositoryUrl)));
            }

            return validationResult;
        }
    }
}
