using VsixRatingChaser.Dtos;

namespace VsixRatingChaser.Interfaces
{
    public interface IChaser
    {
        /// <summary>
        /// gregt
        /// </summary>
        ChaseOutcomeDto Chase(IRatingDetailsDto ratingDetailsDto, ExtensionDetailsDto extensionDetailsDto);
    }
}