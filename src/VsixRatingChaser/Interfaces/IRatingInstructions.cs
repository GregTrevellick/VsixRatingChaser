using VsixRatingChaser.Enums;

namespace VsixRatingChaser.Interfaces
{
    public interface IRatingInstructions : IRatingInstructionsUi ////////////////////////////,IRatingInstructionsLimits
    {
        AggressionLevel AggressionLevel { get; set; }
        CostCategory CostCategory { get; set; }
    }
}