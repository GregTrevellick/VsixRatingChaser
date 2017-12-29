using VsixRatingChaser.Enums;

namespace VsixRatingChaser
{
    public class ChaseOutcomeDto 
    {
        public ChaseOutcomeDto()
        {
            RatingDialogShown = false;
            RatingHyperLinkClicked = null;
            Rejected = false;
            RejectionReason = RejectionReason.NotRejected;
            RejectionReasonDescription = RejectionReason.ToString();
        }

        public bool Rejected { get; set; }
        public bool RatingDialogShown { get; set; }
        public bool? RatingHyperLinkClicked { get; set; }
        public RejectionReason RejectionReason { get; set; }
        public string RejectionReasonDescription { get; set; }
    }
}