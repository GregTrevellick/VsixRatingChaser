using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VsixRatingChaser.Dtos;
using VsixRatingChaser.Enums;

namespace VsixRatingChaser.UnitTests
{
    [TestClass]
    public class ChaserTests
    {
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
            Assert.AreEqual(ChaseOutcome.InvalidCallAsMarketplaceUrlUndefined, actual);
        }

        [TestMethod]
        public void ValidateTest5()
        {
            var actual = ValidateTest(new ExtensionDetailsDto { AuthorName = "any", ExtensionName = "any", MarketPlaceUrl = "any" });

            // Assert
            Assert.AreEqual(ChaseOutcome.InvalidCallAsMarketplaceUrlPrefixIsWrong, actual);
        }

        private ChaseOutcome ValidateTest(ExtensionDetailsDto extensionDetailsDto)
        {
            //Arrange
            var sut = new Chaser();

            //Act
            var actual = sut.Validate(extensionDetailsDto);
            return actual;
        }
    }
}
