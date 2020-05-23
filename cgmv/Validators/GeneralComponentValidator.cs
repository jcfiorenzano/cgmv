using cgmv.Contracts;
using cgmv.Exceptions;
using Microsoft.VisualStudio.Services.Governance.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace cgmv.Validators
{
    public class GeneralComponentValidator : ITypedComponentValidator
    {
        public ValidationResult IsValid(TypedComponent typedComponent)
        {
            if (typedComponent == null)
            {
                throw new ArgumentNullException(nameof(typedComponent));
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

            throw new MissingValidComponentException();
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
