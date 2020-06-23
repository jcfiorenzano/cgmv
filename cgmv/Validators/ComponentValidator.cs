using cgmv.Contracts;
using cgmv.Exceptions;
using Microsoft.VisualStudio.Services.Governance.Contracts;

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
            return getValidatorForType(typedComponent.Type).IsValid(typedComponent);
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
    }
}
