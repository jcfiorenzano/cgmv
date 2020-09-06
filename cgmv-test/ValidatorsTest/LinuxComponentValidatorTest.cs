using cgmv.Contracts;
using cgmv.Exceptions;
using cgmv.Validators;
using FluentAssertions;
using Microsoft.VisualStudio.Services.Governance.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace cgmv_test
{
    [TestClass]
    public class LinuxComponentValidatorTest
    {
        private LinuxComponentValidator linuxComponentValidator;
        
        [TestInitialize]
        public void TestInitialize()
        {
            linuxComponentValidator = new LinuxComponentValidator();
        }

        [TestMethod]
        public void IsValid_AllAttributesArePresent_ValidationSucceed()
        {
            TypedComponent linuxComponent = new TypedComponent(new LinuxComponent
            {
                Name = "test",
                Release = "LTS",
                Distribution = LinuxDistribution.Alpine,
                Version = "1.0.0"
                
            });

            var expectedValidationResult = new ValidationResult { IsValid = true };
            linuxComponentValidator.IsValid(linuxComponent).Should().BeEquivalentTo(expectedValidationResult);
        }

        [TestMethod]
        public void IsValid_PropertyNameIsNotSet_ValidationFailMessageIsRegistered()
        {
            TypedComponent linuxComponent = new TypedComponent(new LinuxComponent
            {
                Release = "LTS",
                Distribution = LinuxDistribution.Alpine,
                Version = "1.0.0"
            });

            var validationResult = linuxComponentValidator.IsValid(linuxComponent);
            validationResult.IsValid.Should().BeFalse();
            validationResult.Messages.Should().HaveCount(1);
            validationResult.Messages[0].Should().BeEquivalentTo("The property name is required and was not specified. This happens if the property has a typo or was omitted");
        }

        [TestMethod]
        public void IsValid_PropertyReleaseIsNotSet_ValidationFailMessageIsRegistered()
        {
            TypedComponent linuxComponent = new TypedComponent(new LinuxComponent
            {
                Name = "test",
                Distribution = LinuxDistribution.Alpine,
                Version = "1.0.0"
            });

            var validationResult = linuxComponentValidator.IsValid(linuxComponent);
            validationResult.IsValid.Should().BeFalse();
            validationResult.Messages.Should().HaveCount(1);
            validationResult.Messages[0].Should().BeEquivalentTo("The property release is required and was not specified. This happens if the property has a typo or was omitted");
        }

        [TestMethod]
        public void IsValid_PropertyVersionIsNotSet_ValidationFailMessageIsRegistered()
        {
            TypedComponent linuxComponent = new TypedComponent(new LinuxComponent
            {
                Name = "test",
                Release = "LTS",
                Distribution = LinuxDistribution.Alpine,
            });

            var validationResult = linuxComponentValidator.IsValid(linuxComponent);
            validationResult.IsValid.Should().BeFalse();
            validationResult.Messages.Should().HaveCount(1);
            validationResult.Messages[0].Should().BeEquivalentTo("The property version is required and was not specified. This happens if the property has a typo or was omitted");
        }

        [TestMethod]
        public void IsValid_PropertiesAreNotSet_ValidationFailMessagesAreRegistered()
        {
            TypedComponent linuxComponent = new TypedComponent(new LinuxComponent
            {
            });

            var validationResult = linuxComponentValidator.IsValid(linuxComponent);
            validationResult.IsValid.Should().BeFalse();
            validationResult.Messages.Should().HaveCount(3);
            validationResult.Messages[0].Should().BeEquivalentTo("The property name is required and was not specified. This happens if the property has a typo or was omitted");
            validationResult.Messages[1].Should().BeEquivalentTo("The property release is required and was not specified. This happens if the property has a typo or was omitted");
            validationResult.Messages[2].Should().BeEquivalentTo("The property version is required and was not specified. This happens if the property has a typo or was omitted");
        }

        [TestMethod]
        public void IsValid_TypedComponentIsNull_ThrowArgumentNullException()
        {
            Func<ValidationResult> action = () => linuxComponentValidator.IsValid(null);
            action.Should().NotThrow<Exception>();
            var result = action();
            result.IsValid.Should().BeFalse();
            result.Messages.Should().HaveCount(1);
            result.Messages.First().Should().BeEquivalentTo("The component type is missing.");
        }

        [TestMethod]
        public void IsValid_GeneralComponentsAreNotPresent_ThrowArgumentNullException()
        {
            var typedComponent = new TypedComponent
            {
                Linux = null
            };
            Func<ValidationResult> action = () => linuxComponentValidator.IsValid(typedComponent);
            action.Should().NotThrow<Exception>();
            var result = action();
            result.IsValid.Should().BeFalse();
            result.Messages.Should().HaveCount(1);
            result.Messages.First().Should().BeEquivalentTo("The component of type Linux do not have a component definition.");
        }
    }
}
