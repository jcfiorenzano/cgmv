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
            validationResult.Messages[0].Should().BeEquivalentTo("The property name is required and was not specified");
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
            validationResult.Messages[0].Should().BeEquivalentTo("The property version is required and was not specified");
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
            validationResult.Messages[0].Should().BeEquivalentTo("The property name is required and was not specified");
            validationResult.Messages[1].Should().BeEquivalentTo("The property version is required and was not specified");
        }

        public void IsValid_TypedComponentIsNull_ThrowArgumentNullException()
        {
            Action action = () => generalComponentValidator.IsValid(null);
            action.Should().Throw<ArgumentNullException>();
        }

        public void IsValid_GeneralComponentsAreNotPresent_ThrowArgumentNullException()
        {
            var typedComponent = new TypedComponent();
            Action action = () => generalComponentValidator.IsValid(typedComponent);
            action.Should().Throw<MissingValidComponentException>();
        }
    }
}
