using cgmv.Contracts;
using cgmv.Exceptions;
using Microsoft.VisualStudio.Services.Governance.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace cgmv.Validators
{
    public class OtherComponentValidator : ITypedComponentValidator
    {
        public ValidationResult IsValid(TypedComponent typedComponent)
        {
            if (typedComponent is null)
            {
                throw new ArgumentNullException(nameof(typedComponent));
            }

            if (typedComponent.Other is null)
            {
                throw new MissingValidComponentException();
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
