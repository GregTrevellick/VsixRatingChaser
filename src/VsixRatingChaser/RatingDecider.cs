using System;
using VsixRatingChaser.Interfaces;

namespace VsixRatingChaser
{
    internal class RatingDecider
    {
        internal bool ShouldShowDialog(IRatingDetailsDto hiddenChaserOptions)
        {
            var shouldShowDialog = false;

            var exceededRatingRequestLimit = ExceededRatingRequestLimit(hiddenChaserOptions.RatingRequestCount, ChaseSettings.RatingRequestLimit);

            if (!exceededRatingRequestLimit)
            {
                var exceededChaseTimeGapLimit = ExceededRatingRequestGap(hiddenChaserOptions.LastRatingRequest, ChaseSettings.RatingRequestGap);

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

        private bool ExceededRatingRequestGap(DateTime lastRatingRequest, int ratingRequestGap)
        {
            var now = DateTime.Now;
            var acceptableDate = now.AddSeconds(-1 * ratingRequestGap);//gregt change to 3 months
            return lastRatingRequest < acceptableDate;
        }
    }
}
