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
    public class DockerImageComponentValidatorTest
    {
        private DockerImageComponentValidator dockerImageComponentValidator;
        
        [TestInitialize]
        public void TestInitialize()
        {
            dockerImageComponentValidator = new DockerImageComponentValidator();
        }

        [TestMethod]
        public void IsValid_AllAttributesArePresent_ValidationSucceed()
        {
            TypedComponent dockerImageComponent = new TypedComponent(new DockerImageComponent
            {
                Digest = "sha256:ab657c035f067b01766424eb8a0dc21a908088f4c5fdd663ba86c2b9e4dad485",
            });

            var expectedValidationResult = new ValidationResult { IsValid = true };
            dockerImageComponentValidator.IsValid(dockerImageComponent).Should().BeEquivalentTo(expectedValidationResult);
        }

        [TestMethod]
        public void IsValid_PropertyDigestIsNotSet_ValidationFailMessageIsRegistered()
        {
            TypedComponent dockerImageComponent = new TypedComponent(new DockerImageComponent
            {
            });

            var validationResult = dockerImageComponentValidator.IsValid(dockerImageComponent);
            validationResult.IsValid.Should().BeFalse();
            validationResult.Messages.Should().HaveCount(1);
            validationResult.Messages[0].Should().BeEquivalentTo("The property digest is required and was not specified. This happens if the property has a typo or was omitted");
        }

        public void IsValid_TypedComponentIsNull_ThrowArgumentNullException()
        {
            Action action = () => dockerImageComponentValidator.IsValid(null);
            action.Should().Throw<ArgumentNullException>();
        }

        public void IsValid_GeneralComponentsAreNotPresent_ThrowArgumentNullException()
        {
            var typedComponent = new TypedComponent();
            typedComponent.DockerImage = null;
            Action action = () => dockerImageComponentValidator.IsValid(typedComponent);
            action.Should().Throw<MissingValidComponentException>();
        }
    }
}
