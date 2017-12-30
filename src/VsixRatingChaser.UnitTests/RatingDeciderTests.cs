using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VsixRatingChaser.UnitTests
{
    [TestClass]
    public class RatingDeciderTests
    {
        [DataTestMethod]
        [DataRow(0, 0, false)]
        [DataRow(0, 1, false)]
        [DataRow(1, 0, true)]
        [DataRow(1, 1, false)]
        [DataRow(-1, 1, false)]
        public void ExceededRatingRequestLimitTest(int ratingRequestCount, int ratingRequestLimit, bool expected)
        {
            // Arrange
            var sut = new RatingDecider();

            // Act
            var actual = sut.ExceededRatingRequestLimit(ratingRequestCount, ratingRequestLimit);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
