using VsixRatingChaser.Dtos;
using VsixRatingChaser.Enums;

namespace VsixRatingChaser.Interfaces
{
    public interface IChaser
    {
        /// <summary>
        /// Validates the request and conditionally displays a pop-up to user asking for an online review
        /// </summary>
        /// <param name="ratingDetailsDto">Parameter data related to the request for ratings</param>
        /// <param name="extensionDetailsDto">Parameter data related to the Visual Studio extension</param>
        /// <returns>The result of the package invocation</returns>
        ChaseOutcome Chase(IRatingDetailsDto ratingDetailsDto, ExtensionDetailsDto extensionDetailsDto);
    }
}