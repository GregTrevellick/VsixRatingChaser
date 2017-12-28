using VsixRatingChaser.Enums;

namespace VsixRatingChaser
{
    public class AggressionLimit
    {
        public int RatingRequestGap { get; set; }
        public RatingRequestGapUnit RatingRequestGapUnit { get; set; }
        public int RatingRequestLimit { get; set; }
    }
}