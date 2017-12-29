using VsixRatingChaser.Enums;

namespace VsixRatingChaser.Interfaces
{
    public interface IChaseOutcomeDto
    {
        bool Rejected{ get; set; }
        bool RatingDialogShown { get; set; }
        bool? RatingHyperLinkClicked { get; set; }
        RejectionReason RejectionReason { get; set; }
        string RejectionReasonDescription { get; set; }
    }
}