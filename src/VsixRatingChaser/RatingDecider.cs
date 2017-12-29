using System;
using VsixRatingChaser.Interfaces;

namespace VsixRatingChaser
{
    internal class RatingDecider
    {
        internal bool ShouldShowDialog(IHiddenChaserOptions hiddenChaserOptions)
        {
            var shouldShowDialog = false;

            var exceededRatingRequestLimit = ExceededRatingRequestLimit(hiddenChaserOptions.RatingRequestCount, 9999);//gregt store 9999 centrally

            if (!exceededRatingRequestLimit)
            {
                var exceededChaseTimeGapLimit = ExceededRatingRequestGap(hiddenChaserOptions.LastRatingRequest, 2);//gregt store this hard coded value somewhere central

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
