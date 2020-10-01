using System;
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

        [TestMethod]
        public void ExceededRatingRequestGapTest1()
        {
            // Arrange
            var sut = new RatingDecider();
            var ratingRequestGap = 3;
            var lastRatingRequest = new DateTime(2017, 6, 1, 0, 0, 0);
            var now = new DateTime(2017, 5, 1, 0, 0, 0);
            var expected = false;

            // Act
            var actual = sut.ExceededRatingRequestGap(lastRatingRequest, ratingRequestGap, now);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ExceededRatingRequestGapTest2()
        {
            // Arrange
            var sut = new RatingDecider();
            var ratingRequestGap = 3;
            var lastRatingRequest = new DateTime(2017, 6, 1, 0, 0, 0);
            var now = new DateTime(2017, 6, 1, 0, 0, 0);
            var expected = false;

            // Act
            var actual = sut.ExceededRatingRequestGap(lastRatingRequest, ratingRequestGap, now);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ExceededRatingRequestGapTest3()
        {
            // Arrange
            var sut = new RatingDecider();
            var ratingRequestGap = 3;
            var lastRatingRequest = new DateTime(2017, 6, 1, 0, 0, 0);
            var now = new DateTime(2017, 7, 1, 0, 0, 0);
            var expected = false;

            // Act
            var actual = sut.ExceededRatingRequestGap(lastRatingRequest, ratingRequestGap, now);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ExceededRatingRequestGapTest4()
        {
            // Arrange
            var sut = new RatingDecider();
            var ratingRequestGap = 3;
            var lastRatingRequest = new DateTime(2017, 6, 1, 0, 0, 0);
            var now = new DateTime(2017, 10, 2, 0, 0, 0);
            var expected = true;

            // Act
            var actual = sut.ExceededRatingRequestGap(lastRatingRequest, ratingRequestGap, now);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [DataTestMethod]
        [DataRow(false, false, false)]
        [DataRow(true, false, false)]
        [DataRow(false, true, true)]
        [DataRow(true, true, false)]
        public void ShouldShowDialogTest(bool exceededRatingRequestLimit, bool exceededChaseTimeGapLimit, bool expected)
        {
            // Arrange
            var sut = new RatingDecider();

            // Act
            var actual = sut.ShouldShowDialog(exceededRatingRequestLimit, exceededChaseTimeGapLimit);

            // Assert
            Assert.AreEqual(expected, actual);
        }

    }
}
