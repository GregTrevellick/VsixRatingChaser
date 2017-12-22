using System.ComponentModel;

namespace VsixRatingChaser
{
    public enum RejectionReason
    {
        [Description("gregt0")]
        NotRejected = 0,
        [Description("gregt1")]
        RatingInstructionsTooAggressiveForLow,
        [Description("gregt2")]
        RatingInstructionsTooAggressiveForMedium,
        [Description("gregt3")]
        HighlyAggressiveChasingNotSupported,
        DialogTypeUndefined,
        AggressionLevelUndefined,
        RatingRequestUrlUndefined,
        RatingRequestUrlStartIsWrong,
        RatingRequestUrlAnchorTagIsWrong,
        RatingRequestGapUnitUndefined,
        RatingRequestGapIsZero,
        RatingRequestCostCategoryIsZero,
        RatingRequestCostCategoryIsNotFree,
        VsixNameCannotBeBlank,
        VsixAuthorCannotBeBlank,
        ImageTooBig,
        ImageTooSmall
    }
}
