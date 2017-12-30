using VsixRatingChaser.Enums;

namespace VsixRatingChaser.Dtos
{
    public class ChaseOutcomeDto 
    {
        internal OutcomeStatus OutcomeStatus { get; set; }

        public string RejectionReasonDescription => OutcomeStatus.GetDescription();
    }
}