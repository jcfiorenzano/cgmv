using cgmv.Contracts;
using cgmv.Exceptions;
using cgmv.Validators;
using FluentAssertions;
using Microsoft.VisualStudio.Services.Governance.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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
            TypedComponent LinuxComponent = new TypedComponent(new LinuxComponent
            {
                Name = "test",
                Release = "LTS",
                Distribution = LinuxDistribution.Alpine,
                Version = "1.0.0"
                
            });

            var expectedValidationResult = new ValidationResult { IsValid = true };
            linuxComponentValidator.IsValid(LinuxComponent).Should().BeEquivalentTo(expectedValidationResult);
        }

        [TestMethod]
        public void IsValid_PropertyNameIsNotSet_ValidationFailMessageIsRegistered()
        {
            TypedComponent LinuxComponent = new TypedComponent(new LinuxComponent
            {
                Release = "LTS",
                Distribution = LinuxDistribution.Alpine,
                Version = "1.0.0"
            });

            var validationResult = linuxComponentValidator.IsValid(LinuxComponent);
            validationResult.IsValid.Should().BeFalse();
            validationResult.Messages.Should().HaveCount(1);
            validationResult.Messages[0].Should().BeEquivalentTo("The property name is required and was not specified");
        }

        [TestMethod]
        public void IsValid_PropertyReleaseIsNotSet_ValidationFailMessageIsRegistered()
        {
            TypedComponent LinuxComponent = new TypedComponent(new LinuxComponent
            {
                Name = "test",
                Distribution = LinuxDistribution.Alpine,
                Version = "1.0.0"
            });

            var validationResult = linuxComponentValidator.IsValid(LinuxComponent);
            validationResult.IsValid.Should().BeFalse();
            validationResult.Messages.Should().HaveCount(1);
            validationResult.Messages[0].Should().BeEquivalentTo("The property release is required and was not specified");
        }

        [TestMethod]
        public void IsValid_PropertyVersionIsNotSet_ValidationFailMessageIsRegistered()
        {
            TypedComponent LinuxComponent = new TypedComponent(new LinuxComponent
            {
                Name = "test",
                Release = "LTS",
                Distribution = LinuxDistribution.Alpine,
            });

            var validationResult = linuxComponentValidator.IsValid(LinuxComponent);
            validationResult.IsValid.Should().BeFalse();
            validationResult.Messages.Should().HaveCount(1);
            validationResult.Messages[0].Should().BeEquivalentTo("The property version is required and was not specified");
        }

        [TestMethod]
        public void IsValid_PropertiesAreNotSet_ValidationFailMessagesAreRegistered()
        {
            TypedComponent LinuxComponent = new TypedComponent(new LinuxComponent
            {
            });

            var validationResult = linuxComponentValidator.IsValid(LinuxComponent);
            validationResult.IsValid.Should().BeFalse();
            validationResult.Messages.Should().HaveCount(3);
            validationResult.Messages[0].Should().BeEquivalentTo("The property name is required and was not specified");
            validationResult.Messages[1].Should().BeEquivalentTo("The property release is required and was not specified");
            validationResult.Messages[2].Should().BeEquivalentTo("The property version is required and was not specified");
        }

        public void IsValid_TypedComponentIsNull_ThrowArgumentNullException()
        {
            Action action = () => linuxComponentValidator.IsValid(null);
            action.Should().Throw<ArgumentNullException>();
        }

        public void IsValid_GeneralComponentsAreNotPresent_ThrowArgumentNullException()
        {
            var typedComponent = new TypedComponent();
            typedComponent.Linux = null;
            Action action = () => linuxComponentValidator.IsValid(typedComponent);
            action.Should().Throw<MissingValidComponentException>();
        }
    }
}
