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
    public class MavenComponentValidatorTest
    {
        private MavenComponentValidator mavenComponentValidator;
        
        [TestInitialize]
        public void TestInitialize()
        {
            mavenComponentValidator = new MavenComponentValidator();
        }

        [TestMethod]
        public void IsValid_AllAttributesArePresent_ValidationSucceed()
        {
            TypedComponent mavenComponent = new TypedComponent(new MavenComponent
            {
                GroupId = "testGroupId",
                ArtifactId = "testArtifactId",
                Version = "1.0.0"
                
            });

            var expectedValidationResult = new ValidationResult { IsValid = true };
            mavenComponentValidator.IsValid(mavenComponent).Should().BeEquivalentTo(expectedValidationResult);
        }

        [TestMethod]
        public void IsValid_PropertyGroupIdIsNotSet_ValidationFailMessageIsRegistered()
        {
            TypedComponent mavenComponent = new TypedComponent(new MavenComponent
            {
                ArtifactId = "testArtifactId",
                Version = "1.0.0"
            });

            var validationResult = mavenComponentValidator.IsValid(mavenComponent);
            validationResult.IsValid.Should().BeFalse();
            validationResult.Messages.Should().HaveCount(1);
            validationResult.Messages[0].Should().BeEquivalentTo("The property groupId is required and was not specified");
        }

        [TestMethod]
        public void IsValid_PropertyArtifactIdIsNotSet_ValidationFailMessageIsRegistered()
        {
            TypedComponent mavenComponent = new TypedComponent(new MavenComponent
            {
                GroupId = "testGroupId",
                Version = "1.0.0"
            });

            var validationResult = mavenComponentValidator.IsValid(mavenComponent);
            validationResult.IsValid.Should().BeFalse();
            validationResult.Messages.Should().HaveCount(1);
            validationResult.Messages[0].Should().BeEquivalentTo("The property artifactId is required and was not specified");
        }

        [TestMethod]
        public void IsValid_PropertyVersionIsNotSet_ValidationFailMessageIsRegistered()
        {
            TypedComponent mavenComponent = new TypedComponent(new MavenComponent
            {
                GroupId = "testGroupId",
                ArtifactId = "testArtifactId"
            });

            var validationResult = mavenComponentValidator.IsValid(mavenComponent);
            validationResult.IsValid.Should().BeFalse();
            validationResult.Messages.Should().HaveCount(1);
            validationResult.Messages[0].Should().BeEquivalentTo("The property version is required and was not specified");
        }

        [TestMethod]
        public void IsValid_PropertiesAreNotSet_ValidationFailMessagesAreRegistered()
        {
            TypedComponent mavenComponent = new TypedComponent(new MavenComponent
            {
            });

            var validationResult = mavenComponentValidator.IsValid(mavenComponent);
            validationResult.IsValid.Should().BeFalse();
            validationResult.Messages.Should().HaveCount(3);
            validationResult.Messages[0].Should().BeEquivalentTo("The property groupId is required and was not specified");
            validationResult.Messages[1].Should().BeEquivalentTo("The property artifactId is required and was not specified");
            validationResult.Messages[2].Should().BeEquivalentTo("The property version is required and was not specified");
        }

        public void IsValid_TypedComponentIsNull_ThrowArgumentNullException()
        {
            Action action = () => mavenComponentValidator.IsValid(null);
            action.Should().Throw<ArgumentNullException>();
        }

        public void IsValid_GeneralComponentsAreNotPresent_ThrowArgumentNullException()
        {
            var typedComponent = new TypedComponent();
            typedComponent.Maven = null;
            Action action = () => mavenComponentValidator.IsValid(typedComponent);
            action.Should().Throw<MissingValidComponentException>();
        }
    }
}
