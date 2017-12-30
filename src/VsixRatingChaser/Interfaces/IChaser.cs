using VsixRatingChaser.Dtos;
using VsixRatingChaser.Enums;

namespace VsixRatingChaser.Interfaces
{
    public interface IChaser
    {
        ChaseOutcome Chase(IRatingDetailsDto ratingDetailsDto, ExtensionDetailsDto extensionDetailsDto);
    }
}