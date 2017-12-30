using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VsixRatingChaser.UnitTests
{
    [TestClass]
    public class UrlHelperTests
    {
        [TestMethod]
        [DataRow(null, null)]
        [DataRow("", "")]
        [DataRow("aNy", "aNy#review-details")]
        [DataRow("aNy#review-details", "aNy#review-details")]
        public void GetMarketPlaceUrlTest(string url, string expected)
        {
            var actual = UrlHelper.GetMarketPlaceUrl(url);
            
            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
