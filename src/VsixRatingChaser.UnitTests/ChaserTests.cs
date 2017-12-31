using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VsixRatingChaser.Dtos;
using VsixRatingChaser.Enums;
using VsixRatingChaser.Interfaces;

namespace VsixRatingChaser.UnitTests
{
    [TestClass]
    public class ChaserTests
    {
        [TestMethod]
        public void ValidateRatingDetailsDtoTest1()
        {
            // Arrange
            var sut = new Chaser();

            // Act
            var actual = sut.ValidateRatingDetailsDto(0, DateTime.MaxValue);

            // Assert
            Assert.AreEqual(ChaseOutcome.Unknown, actual);
        }

        [TestMethod]
        public void ValidateRatingDetailsDtoTest2()
        {
            // Arrange
            var sut = new Chaser();

            // Act
            var actual = sut.ValidateRatingDetailsDto(0, DateTime.MinValue);

            // Assert
            Assert.AreEqual(ChaseOutcome.Unknown, actual);
        }

        [TestMethod]
        public void ValidateRatingDetailsDtoTest3()
        {
            // Arrange
            var sut = new Chaser();

            // Act
            var actual = sut.ValidateRatingDetailsDto(1, DateTime.MaxValue);

            // Assert
            Assert.AreEqual(ChaseOutcome.InvalidCallAsNonFirstCallButPreviousRatingRequestDateIsNotInPast, actual);
        }

        [TestMethod]
        public void ValidateRatingDetailsDtoTest4()
        {
            // Arrange
            var sut = new Chaser();

            // Act
            var actual = sut.ValidateRatingDetailsDto(1, DateTime.MinValue);

            // Assert
            Assert.AreEqual(ChaseOutcome.InvalidCallAsNonFirstCallButNoPreviousRatingRequestDateSpecified, actual);
        }

        [TestMethod]
        public void ValidateRatingDetailsDtoTest5()
        {
            // Arrange
            var sut = new Chaser();

            // Act
            var actual = sut.ValidateRatingDetailsDto(-1, DateTime.MinValue);

            // Assert
            Assert.AreEqual(ChaseOutcome.InvalidCallAsRatingRequestCountIsNegative, actual);
        }

        [TestMethod]
        public void ValidateTest1()
        {
            var actual = ValidateTest(null);
            
            // Assert
            Assert.AreEqual(ChaseOutcome.InvalidCallAsAuthorNameCannotBeBlank, actual);
        }

        [TestMethod]
        public void ValidateTest2()
        {
            var actual = ValidateTest(new ExtensionDetailsDto());

            // Assert
            Assert.AreEqual(ChaseOutcome.InvalidCallAsAuthorNameCannotBeBlank, actual);
        }

        [TestMethod]
        public void ValidateTest3()
        {
            var actual = ValidateTest(new ExtensionDetailsDto {AuthorName = "any"});

            // Assert
            Assert.AreEqual(ChaseOutcome.InvalidCallAsExtensionNameCannotBeBlank, actual);
        }

        [TestMethod]
        public void ValidateTest4()
        {
            var actual = ValidateTest(new ExtensionDetailsDto { AuthorName = "any", ExtensionName="any" });

            // Assert
            Assert.AreEqual(ChaseOutcome.InvalidCallAsMarketplaceUrlCannotBeBlank, actual);
        }

        [TestMethod]
        public void ValidateTest5()
        {
            var actual = ValidateTest(new ExtensionDetailsDto { AuthorName = "any", ExtensionName = "any", MarketPlaceUrl = "any" });

            // Assert
            Assert.AreEqual(ChaseOutcome.InvalidCallAsMarketplaceUrlIsNotTheVisualStudioMarketplaceDomain, actual);
        }

        private ChaseOutcome ValidateTest(ExtensionDetailsDto extensionDetailsDto)
        {
            //Arrange
            var sut = new Chaser();

            //Act
            var actual = sut.Validate(extensionDetailsDto);
            return actual;
        }

        [TestMethod]
        public void IsInaugrualInvocationTest1()
        {
            // Arrange

            // Act
            var actual = Chaser.IsInaugrualInvocation(0, DateTime.MaxValue);

            // Assert
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void IsInaugrualInvocationTest2()
        {
            // Arrange

            // Act
            var actual = Chaser.IsInaugrualInvocation(0, DateTime.MinValue);

            // Assert
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void IsInaugrualInvocationTest3()
        {
            // Arrange

            // Act
            var actual = Chaser.IsInaugrualInvocation(1, DateTime.MaxValue);

            // Assert
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void IsInaugrualInvocationTest4()
        {
            // Arrange

            // Act
            var actual = Chaser.IsInaugrualInvocation(1, DateTime.MinValue);

            // Assert
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void IsInaugrualInvocationTest5()
        {
            // Arrange

            // Act
            var actual = Chaser.IsInaugrualInvocation(-1, DateTime.MinValue);

            // Assert
            Assert.AreEqual(false, actual);
        }
    }
}
