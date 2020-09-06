using cgmv.Contracts;
using cgmv.Exceptions;
using Microsoft.VisualStudio.Services.Governance.Contracts;
using System.Collections.Generic;

namespace cgmv.Validators
{
    /// <summary>
    /// Validator factory
    /// </summary>
    public static class ComponentValidator
    {
        /// <summary>
        /// Pick the right validator given the component's type.
        /// </summary>
        /// <param name="typedComponent">Component to validate</param>
        /// <returns>Validation results.</returns>
        public static ValidationResult IsComponentValid(TypedComponent typedComponent)
        {
            try
            {
                var validationResult = ValidateComponenTypeDefinition(typedComponent);
                return validationResult.IsValid
                    ? getValidatorForType(typedComponent.Type).IsValid(typedComponent)
                    : validationResult;
            }
            catch (ComponentTypeNotSupportedException ex)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    Messages = new List<string> { ex.Message }
                };
            }
        }

        private static ITypedComponentValidator getValidatorForType(ComponentType type)
        {
            return type switch {
                ComponentType.Npm => new GeneralComponentValidator(),
                ComponentType.NuGet => new GeneralComponentValidator(),
                ComponentType.Pip => new GeneralComponentValidator(),
                ComponentType.Cargo => new GeneralComponentValidator(),
                ComponentType.Go => new GeneralComponentValidator(),
                ComponentType.Pod => new GeneralComponentValidator(),
                ComponentType.RubyGems => new GeneralComponentValidator(),
                ComponentType.Linux => new LinuxComponentValidator(),
                ComponentType.DockerImage => new DockerImageComponentValidator(),
                ComponentType.Maven => new MavenComponentValidator(),
                ComponentType.Git => new GitComponentValidator(),
                ComponentType.Other => new OtherComponentValidator(),
                _ => throw new ComponentTypeNotSupportedException(type)
            };
        }

        private static ValidationResult ValidateComponenTypeDefinition(TypedComponent component)
        {
            if (component.Cargo != null && component.Type != ComponentType.Cargo)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    Messages = new List<string> { string.Format(Resources.IncorrectComponentDefinition, component.Type, ComponentType.Cargo) }
                };
            }
            if (component.DockerImage != null && component.Type != ComponentType.DockerImage)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    Messages = new List<string> { string.Format(Resources.IncorrectComponentDefinition, component.Type, ComponentType.DockerImage) }
                };
            }
            if (component.Git != null && component.Type != ComponentType.Git)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    Messages = new List<string> { string.Format(Resources.IncorrectComponentDefinition, component.Type, ComponentType.Git) }
                };
            }
            if (component.Go != null && component.Type != ComponentType.Go)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    Messages = new List<string> { string.Format(Resources.IncorrectComponentDefinition, component.Type, ComponentType.Go) }
                };
            }
            if (component.Linux != null && component.Type != ComponentType.Linux)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    Messages = new List<string> { string.Format(Resources.IncorrectComponentDefinition, component.Type, ComponentType.Linux) }
                };
            }
            if (component.Maven != null && component.Type != ComponentType.Maven)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    Messages = new List<string> { string.Format(Resources.IncorrectComponentDefinition, component.Type, ComponentType.Maven) }
                };
            }
            if (component.Npm != null && component.Type != ComponentType.Npm)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    Messages = new List<string> { string.Format(Resources.IncorrectComponentDefinition, component.Type, ComponentType.Npm) }
                };
            }
            if (component.NuGet != null && component.Type != ComponentType.NuGet)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    Messages = new List<string> { string.Format(Resources.IncorrectComponentDefinition, component.Type, ComponentType.NuGet) }
                };
            }
            if (component.Other != null && component.Type != ComponentType.Other)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    Messages = new List<string> { string.Format(Resources.IncorrectComponentDefinition, component.Type, ComponentType.Other) }
                };
            }
            if (component.Pip != null && component.Type != ComponentType.Pip)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    Messages = new List<string> { string.Format(Resources.IncorrectComponentDefinition, component.Type, ComponentType.Pip) }
                };
            }
            if (component.Pod != null && component.Type != ComponentType.Pod)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    Messages = new List<string> { string.Format(Resources.IncorrectComponentDefinition, component.Type, ComponentType.Pod) }
                };
            }
            if (component.RubyGems != null && component.Type != ComponentType.RubyGems)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    Messages = new List<string> { string.Format(Resources.IncorrectComponentDefinition, component.Type, ComponentType.RubyGems) }
                };
            }

            return new ValidationResult
            {
                IsValid = true
            };
        }
    }
}
