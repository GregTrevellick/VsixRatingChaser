namespace VsixRatingChaser
{
    internal class UrlHelper
    {
        internal static string GetMarketPlaceUrl(string url)//gregt unit test reqd
        {
            if (!url.ToLower().EndsWith("#review-details".ToLower()))
            {
                url += "#review-details";
            }

            return url;
        }
    }
}
