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
    public class GitComponentValidatorTest
    {
        private GitComponentValidator gitComponentValidator;
        
        [TestInitialize]
        public void TestInitialize()
        {
            gitComponentValidator = new GitComponentValidator();
        }

        [TestMethod]
        public void IsValid_AllAttributesArePresent_ValidationSucceed()
        {
            TypedComponent gitComponent = new TypedComponent(new GitComponent
            {
                CommitHash = "bc6bc6b1e51024faee6070940a647845ab8c2c63",
                RepositoryUrl = new Uri("https://github/microsoft/testrepo")
            });

            var expectedValidationResult = new ValidationResult { IsValid = true };
            gitComponentValidator.IsValid(gitComponent).Should().BeEquivalentTo(expectedValidationResult);
        }

        [TestMethod]
        public void IsValid_PropertyCommitHashIsNotSet_ValidationFailMessageIsRegistered()
        {
            TypedComponent gitComponent = new TypedComponent(new GitComponent
            {
                RepositoryUrl = new Uri("https://github/microsoft/testrepo")
            });

            var validationResult = gitComponentValidator.IsValid(gitComponent);
            validationResult.IsValid.Should().BeFalse();
            validationResult.Messages.Should().HaveCount(1);
            validationResult.Messages[0].Should().BeEquivalentTo("The property commitHash is required and was not specified");
        }

        [TestMethod]
        public void IsValid_PropertyRepositoryUrlIsNotSet_ValidationFailMessageIsRegistered()
        {
            TypedComponent gitComponent = new TypedComponent(new GitComponent
            {
                CommitHash = "bc6bc6b1e51024faee6070940a647845ab8c2c63"
            });

            var validationResult = gitComponentValidator.IsValid(gitComponent);
            validationResult.IsValid.Should().BeFalse();
            validationResult.Messages.Should().HaveCount(1);
            validationResult.Messages[0].Should().BeEquivalentTo("The property RepositoryUrl is required and was not specified");
        }

        [TestMethod]
        public void IsValid_PropertiesAreNotSet_ValidationFailMessagesAreRegistered()
        {
            TypedComponent gitComponent = new TypedComponent(new GitComponent
            {
            });

            var validationResult = gitComponentValidator.IsValid(gitComponent);
            validationResult.IsValid.Should().BeFalse();
            validationResult.Messages.Should().HaveCount(2);
            validationResult.Messages[0].Should().BeEquivalentTo("The property commitHash is required and was not specified");
            validationResult.Messages[1].Should().BeEquivalentTo("The property RepositoryUrl is required and was not specified");
        }

        public void IsValid_TypedComponentIsNull_ThrowArgumentNullException()
        {
            Action action = () => gitComponentValidator.IsValid(null);
            action.Should().Throw<ArgumentNullException>();
        }

        public void IsValid_GeneralComponentsAreNotPresent_ThrowArgumentNullException()
        {
            var typedComponent = new TypedComponent();
            typedComponent.Git = null;
            Action action = () => gitComponentValidator.IsValid(typedComponent);
            action.Should().Throw<MissingValidComponentException>();
        }
    }
}
