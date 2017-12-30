using System;
using VsixRatingChaser.Interfaces;

namespace VsixRatingChaser
{
    internal class RatingDecider
    {
        internal bool ShouldShowDialog(IRatingDetailsDto ratingDetailsDto)
        {
            var shouldShowDialog = false;

            var exceededRatingRequestLimit = ExceededRatingRequestLimit(ratingDetailsDto.RatingRequestCount, ChaseSettings.RatingRequestLimit);

            if (!exceededRatingRequestLimit)
            {
                var exceededChaseTimeGapLimit = ExceededRatingRequestGap(ratingDetailsDto.LastRatingRequest, ChaseSettings.RatingRequestGap);

                if (exceededChaseTimeGapLimit) 
                {
                    shouldShowDialog = true;
                }
            }

            return shouldShowDialog;
        }

        private bool ExceededRatingRequestLimit(int ratingRequestCount, int ratingRequestLimit)//gregt unit test reqd
        {
            return ratingRequestCount > ratingRequestLimit;
        }

        private bool ExceededRatingRequestGap(DateTime lastRatingRequest, int ratingRequestGap)//gregt unit test reqd
        {
            var now = DateTime.Now;
            var acceptableDate = now.AddSeconds(-1 * ratingRequestGap);//gregt change AddSeconds to AddMonths
            return lastRatingRequest < acceptableDate;
        }
    }
}
