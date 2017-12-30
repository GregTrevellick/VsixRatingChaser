namespace VsixRatingChaser
{
    internal class UrlHelper
    {
        internal static string GetMarketPlaceUrl(string url)
        {
            if (!string.IsNullOrWhiteSpace(url))
            {
                if (!url.ToLower().EndsWith("#review-details".ToLower()))
                {
                    url += "#review-details";
                }
            }

            return url;
        }
    }
}
