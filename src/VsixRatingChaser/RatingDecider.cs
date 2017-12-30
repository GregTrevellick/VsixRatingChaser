using System;
using VsixRatingChaser.Interfaces;

namespace VsixRatingChaser
{
    internal class RatingDecider
    {
        internal bool ShouldShowDialog(IRatingDetailsDto ratingDetailsDto)//gregt unit test reqd
        {
            var shouldShowDialog = false;

            var exceededRatingRequestLimit = ExceededRatingRequestLimit(ratingDetailsDto.RatingRequestCount, ChaseSettings.RatingRequestLimit);

            if (!exceededRatingRequestLimit)
            {
                var exceededChaseTimeGapLimit = ExceededRatingRequestGap(ratingDetailsDto.LastRatingRequest, ChaseSettings.RatingRequestGap, DateTime.Now);

                if (exceededChaseTimeGapLimit) 
                {
                    shouldShowDialog = true;
                }
            }

            return shouldShowDialog;
        }

        internal bool ExceededRatingRequestLimit(int ratingRequestCount, int ratingRequestLimit)
        {
            return ratingRequestCount > ratingRequestLimit;
        }

        internal bool ExceededRatingRequestGap(DateTime lastRatingRequest, int ratingRequestGap, DateTime now)
        {
            //var acceptableDate = now.AddMonths(-1 * ratingRequestGap);
            var acceptableDate = now.AddSeconds(-1 * ratingRequestGap);//gregt change AddSeconds to AddMonths
            return lastRatingRequest < acceptableDate;
        }
    }
}
