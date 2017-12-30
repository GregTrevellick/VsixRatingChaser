using VsixRatingChaser.Dtos;
using VsixRatingChaser.Enums;

namespace VsixRatingChaser.Interfaces
{
    public interface IChaser
    {
        /// <summary>
        /// gregt
        /// </summary>
        ChaseOutcome Chase(IRatingDetailsDto ratingDetailsDto, ExtensionDetailsDto extensionDetailsDto);
    }
}