using cgmv.Contracts;
using cgmv.Exceptions;
using Microsoft.VisualStudio.Services.Governance.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace cgmv.Validators
{
    public class MavenComponentValidator : ITypedComponentValidator
    {
        public ValidationResult IsValid(TypedComponent typedComponent)
        {
            if (typedComponent is null)
            {
                throw new ArgumentNullException(nameof(typedComponent));
            }

            if (typedComponent.Maven is null)
            {
                throw new MissingValidComponentException();
            }

            var validationResult = new ValidationResult { IsValid = true };
            
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
