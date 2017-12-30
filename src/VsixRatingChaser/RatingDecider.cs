using System;
using VsixRatingChaser.Interfaces;

namespace VsixRatingChaser
{
    internal class RatingDecider
    {
        internal bool ShouldShowDialog(IRatingDetailsDto ratingDetailsDto)
        {
            var exceededRatingRequestLimit = ExceededRatingRequestLimit(ratingDetailsDto.RatingRequestCount, ChaseSettings.RatingRequestLimit);

            var exceededChaseTimeGapLimit = ExceededRatingRequestGap(ratingDetailsDto.LastRatingRequest, ChaseSettings.RatingRequestGap, DateTime.Now);

            var shouldShowDialog = ShouldShowDialog(exceededRatingRequestLimit, exceededChaseTimeGapLimit);

            return shouldShowDialog;
        }

        internal bool ShouldShowDialog(bool exceededRatingRequestLimit, bool exceededChaseTimeGapLimit)//gregt unit test reqd
        {
            var shouldShowDialog = false;

            if (!exceededRatingRequestLimit)
            {
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
