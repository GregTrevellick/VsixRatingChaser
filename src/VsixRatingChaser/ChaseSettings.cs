namespace VsixRatingChaser
{
    internal class ChaseSettings
    {
        internal const string MarketplaceUrlPrefix = "https://marketplace.visualstudio.com/items?itemName=";
        internal static int RatingRequestGap = 4;//gregt rename with suffix 'Months'
        internal static int RatingRequestLimit = 9999;//gregt set to 3 [chasings]
    }
}
