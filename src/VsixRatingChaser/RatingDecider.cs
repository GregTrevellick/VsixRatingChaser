using System;
using VsixRatingChaser.Enums;
using VsixRatingChaser.Interfaces;

namespace VsixRatingChaser
{
    internal class RatingDecider
    {
        internal bool ShouldShowDialog(IHiddenChaserOptions hiddenChaserOptions, AggressionLimit aggressionLimit)
        {
            var shouldShowDialog = false;

            var exceededRatingRequestLimit = ExceededRatingRequestLimit(hiddenChaserOptions.RatingRequestCount, aggressionLimit.RatingRequestLimit);

            if (!exceededRatingRequestLimit)
            {
                var exceededChaseTimeGapLimit = ExceededRatingRequestGap(hiddenChaserOptions.LastRatingRequest, aggressionLimit.RatingRequestGap, aggressionLimit.RatingRequestGapUnit);

                if (exceededChaseTimeGapLimit) 
                {
                    shouldShowDialog = true;
                }
            }

            return shouldShowDialog;
        }

        private bool ExceededRatingRequestLimit(int ratingRequestCount, int ratingRequestLimit)
        {
            return ratingRequestCount > ratingRequestLimit;
        }

        private bool ExceededRatingRequestGap(DateTime lastRatingRequest, int ratingRequestGap, RatingRequestGapUnit ratingRequestGapUnit)
        {
            DateTime acceptableDate;
            var now = DateTime.Now;

            switch (ratingRequestGapUnit)
            {
                case RatingRequestGapUnit.Seconds:
                    acceptableDate = now.AddSeconds(-1 * ratingRequestGap);
                    break;
                case RatingRequestGapUnit.Minutes:
                    acceptableDate = now.AddMinutes(-1 * ratingRequestGap);
                    break;
                case RatingRequestGapUnit.Hours:
                    acceptableDate = now.AddHours(-1 * ratingRequestGap);
                    break;
                case RatingRequestGapUnit.Days:
                    acceptableDate = now.AddDays(-1 * ratingRequestGap);
                    break;
                case RatingRequestGapUnit.Months:
                    acceptableDate = now.AddMonths(-1 * ratingRequestGap);
                    break;
                case RatingRequestGapUnit.Years:
                    acceptableDate = now.AddYears(-1 * ratingRequestGap);
                    break;
                default:
                    acceptableDate = now;
                    break;
            }
            
            return lastRatingRequest < acceptableDate;
        }
    }
}
