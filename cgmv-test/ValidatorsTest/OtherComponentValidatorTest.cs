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
    public class OtherComponentValidatorTest
    {
        private OtherComponentValidator otherComponentValidator;
        
        [TestInitialize]
        public void TestInitialize()
        {
            otherComponentValidator = new OtherComponentValidator();
        }

        [TestMethod]
        public void IsValid_AllAttributesArePresent_ValidationSucceed()
        {
            TypedComponent otherComponent = new TypedComponent(new OtherComponent
            {
                Name = "test",
                Version = "1.0.0",
                DownloadUrl = new Uri("https://github/microsoft/testrepo.zip")
            });

            var expectedValidationResult = new ValidationResult { IsValid = true };
            otherComponentValidator.IsValid(otherComponent).Should().BeEquivalentTo(expectedValidationResult);
        }

        [TestMethod]
        public void IsValid_PropertyNamehIsNotSet_ValidationFailMessageIsRegistered()
        {
            TypedComponent otherComponent = new TypedComponent(new OtherComponent
            {
                Version = "1.0.0",
                DownloadUrl = new Uri("https://github/microsoft/testrepo.zip")
            });

            var validationResult = otherComponentValidator.IsValid(otherComponent);
            validationResult.IsValid.Should().BeFalse();
            validationResult.Messages.Should().HaveCount(1);
            validationResult.Messages[0].Should().BeEquivalentTo("The property name is required and was not specified");
        }

        [TestMethod]
        public void IsValid_PropertyVersionIsNotSet_ValidationFailMessageIsRegistered()
        {
            TypedComponent otherComponent = new TypedComponent(new OtherComponent
            {
                Name = "test",
                DownloadUrl = new Uri("https://github/microsoft/testrepo.zip")
            });

            var validationResult = otherComponentValidator.IsValid(otherComponent);
            validationResult.IsValid.Should().BeFalse();
            validationResult.Messages.Should().HaveCount(1);
            validationResult.Messages[0].Should().BeEquivalentTo("The property version is required and was not specified");
        }

        [TestMethod]
        public void IsValid_PropertyDownloadUrlIsNotSet_ValidationFailMessageIsRegistered()
        {
            TypedComponent otherComponent = new TypedComponent(new OtherComponent
            {
                Name = "test",
                Version = "1.0.0"
            });

            var validationResult = otherComponentValidator.IsValid(otherComponent);
            validationResult.IsValid.Should().BeFalse();
            validationResult.Messages.Should().HaveCount(1);
            validationResult.Messages[0].Should().BeEquivalentTo("The property downloadUrl is required and was not specified");
        }

        [TestMethod]
        public void IsValid_PropertiesAreNotSet_ValidationFailMessagesAreRegistered()
        {
            TypedComponent otherComponent = new TypedComponent(new OtherComponent
            {
            });

            var validationResult = otherComponentValidator.IsValid(otherComponent);
            validationResult.IsValid.Should().BeFalse();
            validationResult.Messages.Should().HaveCount(3);
            validationResult.Messages[0].Should().BeEquivalentTo("The property name is required and was not specified");
            validationResult.Messages[1].Should().BeEquivalentTo("The property version is required and was not specified");
            validationResult.Messages[2].Should().BeEquivalentTo("The property downloadUrl is required and was not specified");
        }

        public void IsValid_TypedComponentIsNull_ThrowArgumentNullException()
        {
            Action action = () => otherComponentValidator.IsValid(null);
            action.Should().Throw<ArgumentNullException>();
        }

        public void IsValid_GeneralComponentsAreNotPresent_ThrowArgumentNullException()
        {
            var typedComponent = new TypedComponent();
            typedComponent.Other = null;
            Action action = () => otherComponentValidator.IsValid(typedComponent);
            action.Should().Throw<MissingValidComponentException>();
        }
    }
}
