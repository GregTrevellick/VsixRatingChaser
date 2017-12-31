using System;
using VsixRatingChaser.Interfaces;

namespace VsixRatingChaser
{
    internal class RatingDecider
    {
        internal bool ShouldShowDialog(IRatingDetailsDto ratingDetailsDto)
        {
            var exceededRatingRequestLimit = ExceededRatingRequestLimit(ratingDetailsDto.RatingRequestCount, ChaseSettings.RatingRequestLimit);

            var exceededChaseTimeGapLimit = ExceededRatingRequestGap(ratingDetailsDto.PreviousRatingRequest, ChaseSettings.RatingRequestGapInMonths, DateTime.Now);

            var shouldShowDialog = ShouldShowDialog(exceededRatingRequestLimit, exceededChaseTimeGapLimit);

            return shouldShowDialog;
        }

        internal bool ShouldShowDialog(bool exceededRatingRequestLimit, bool exceededChaseTimeGapLimit)
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

        internal bool ExceededRatingRequestGap(DateTime previousRatingRequest, int ratingRequestGapInMonths, DateTime now)
        {
            //var acceptableDate = now.AddMonths(-1 * ratingRequestGapInMonths);
            var acceptableDate = now.AddSeconds(-1 * ratingRequestGapInMonths);//gregt change AddSeconds to AddMonths
            return previousRatingRequest < acceptableDate;
        }
    }
}
