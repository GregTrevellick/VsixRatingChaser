using VsixRatingChaser.Dtos;
using VsixRatingChaser.Enums;

namespace VsixRatingChaser.Interfaces
{
    /// <summary>
    /// gregt
    /// </summary>
    public interface IChaser
    {
        /// <summary>
        /// gregt
        /// </summary>
        /// <param name="ratingDetailsDto"></param>
        /// <param name="extensionDetailsDto"></param>
        /// <returns></returns>
        ChaseOutcome Chase(IRatingDetailsDto ratingDetailsDto, ExtensionDetailsDto extensionDetailsDto);
    }
}