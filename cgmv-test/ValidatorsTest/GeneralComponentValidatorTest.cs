using cgmv.Contracts;
using cgmv.Validators;
using FluentAssertions;
using Microsoft.VisualStudio.Services.Governance.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace cgmv_test
{
    [TestClass]
    public class GeneralComponentValidatorTest
    {
        private GeneralComponentValidator generalComponentValidator;
        
        [TestInitialize]
        public void TestInitialize()
        {
            generalComponentValidator = new GeneralComponentValidator();
        }

        [TestMethod]
        public void IsValid_AllAttributesArePresent_ValidationSucceed()
        {
            TypedComponent npmTypedComponent = new TypedComponent(new NpmComponent
            {
                Name = "test",
                Version = "1.0.0"
            });

            var expectedValidationResult = new ValidationResult { IsValid = true };
            generalComponentValidator.IsValid(npmTypedComponent).Should().BeEquivalentTo(expectedValidationResult);
        }

        [TestMethod]
        public void IsValid_PropertyNameIsNotSet_ValidationFailMessageIsRegistered()
        {
            TypedComponent npmComponent = new TypedComponent(new NpmComponent
            {
                Version = "1.0.0"
            });

            var validationResult = generalComponentValidator.IsValid(npmComponent);
            validationResult.IsValid.Should().BeFalse();
            validationResult.Messages.Should().HaveCount(1);
            validationResult.Messages[0].Should().BeEquivalentTo("The property name is required and was not specified. This happens if the property has a typo or was omitted");
        }

        [TestMethod]
        public void IsValid_PropertyVersionIsNot_ValidationFailMessageIsRegistered()
        {
            TypedComponent npmComponent = new TypedComponent(new NpmComponent
            {
                Name = "test"
            });

            var validationResult = generalComponentValidator.IsValid(npmComponent);
            validationResult.IsValid.Should().BeFalse();
            validationResult.Messages.Should().HaveCount(1);
            validationResult.Messages[0].Should().BeEquivalentTo("The property version is required and was not specified. This happens if the property has a typo or was omitted");
        }

        [TestMethod]
        public void IsValid_PropertyVersionAndNameAreNotSet_ValidationFailMessageIsRegistered()
        {
            TypedComponent npmComponent = new TypedComponent(new NpmComponent
            {
            });

            var validationResult = generalComponentValidator.IsValid(npmComponent);
            validationResult.IsValid.Should().BeFalse();
            validationResult.Messages.Should().HaveCount(2);
            validationResult.Messages[0].Should().BeEquivalentTo("The property name is required and was not specified. This happens if the property has a typo or was omitted");
            validationResult.Messages[1].Should().BeEquivalentTo("The property version is required and was not specified. This happens if the property has a typo or was omitted");
        }

        [TestMethod]
        public void IsValid_TypedComponentIsNull_ThrowArgumentNullException()
        {
            Func<ValidationResult> action = () => generalComponentValidator.IsValid(null);
            action.Should().NotThrow<Exception>();
            var result = action();
            result.IsValid.Should().BeFalse();
            result.Messages.Should().HaveCount(1);
            result.Messages.First().Should().BeEquivalentTo("The component type is missing.");
        }

        [TestMethod]
        public void IsValid_GeneralComponentsAreNotPresent_ThrowArgumentNullException()
        {
            var typedComponent = new TypedComponent()
            {
                Type = ComponentType.Npm
            };
            Func<ValidationResult> action = () => generalComponentValidator.IsValid(typedComponent);
            action.Should().NotThrow<Exception>();
            var validationResult = action();
            validationResult.IsValid.Should().BeFalse();
            validationResult.Messages.Should().HaveCount(1);
            validationResult.Messages.First().Should().BeEquivalentTo("The component of type Npm do not have a component definition.");
        }
    }
}
