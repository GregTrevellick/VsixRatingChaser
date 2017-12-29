using VsixRatingChaser.Enums;

namespace VsixRatingChaser.Dtos
{
    public class ChaseOutcomeDto 
    {
        public ChaseOutcomeDto()
        {
            ReviewRequestDialogShown = false;
            MarketplaceHyperLinkClicked = null;
            Rejected = false;
            RejectionReason = RejectionReason.NotRejected;
            RejectionReasonDescription = RejectionReason.ToString();
        }

        /// <summary>
        /// if the review request pop-up was shown - false except for month 3/6/9
        /// </summary>
        public bool ReviewRequestDialogShown { get; set; }

        /// <summary>
        /// whether or not user click the marketplace url supplied
        /// </summary>
        public bool? MarketplaceHyperLinkClicked { get; set; }

        public bool Rejected { get; set; }
        internal RejectionReason RejectionReason { get; set; }
        public string RejectionReasonDescription { get; set; }
    }
}