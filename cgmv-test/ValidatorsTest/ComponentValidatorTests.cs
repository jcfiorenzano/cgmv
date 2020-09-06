using cgmv.Validators;
using FluentAssertions;
using Microsoft.VisualStudio.Services.Governance.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace cgmv_test
{
    [TestClass]
    public class ComponentValidatorTests
    {
        [TestMethod]
        public void IsComponentValid_ComponentDefinitionNotMatchComponentType_ComponentIsInvalid()
        {
            var invalidTypedComponent = new TypedComponent
            {
                Type = ComponentType.Npm,
                Cargo = new CargoComponent()
            };

            var result = ComponentValidator.IsComponentValid(invalidTypedComponent);

            result.IsValid.Should().BeFalse();
            result.Messages.Should().HaveCount(1);
            result.Messages.First().Should().BeEquivalentTo("Incorrect component definition. The component type is Npm, but the component definition is for: Cargo.");
        }

        [TestMethod]
        public void IsComponentValid_ValidComponent_ValidResultIsReturned()
        {
            var validTypedComponent = new TypedComponent
            {
                Type = ComponentType.Npm,
                Npm = new NpmComponent
                { 
                    Name = "test",
                    Version ="1.0.0"
                }
            };

            var result = ComponentValidator.IsComponentValid(validTypedComponent);

            result.IsValid.Should().BeTrue();
            result.Messages.Should().HaveCount(0);
        }
    }
}
