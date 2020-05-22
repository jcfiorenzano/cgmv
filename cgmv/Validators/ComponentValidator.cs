using cgmv.Contracts;
using cgmv.Exceptions;
using Microsoft.VisualStudio.Services.Governance.Contracts;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace cgmv.Validators
{
    public static class ComponentValidator
    {
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
                ComponentType.Linux => new LinuxComponentValidator(),
                ComponentType.DockerImage => new DockerImageComponentValidator(),
                ComponentType.Maven => new MavenComponentValidator(),
                ComponentType.Pod => new PodComponentValidator(),
                ComponentType.RubyGems => new RubyGemsComponentValidator(),
                ComponentType.Git => new GitComponentValidator(),
                ComponentType.Other => new OtherComponentValidator(),
                _ => throw new ComponentTypeNotSupportedException(type)
            };
        }
    }
}
