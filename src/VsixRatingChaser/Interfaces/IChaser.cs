namespace VsixRatingChaser.Interfaces
{
    public interface IChaser
    {
        /// <summary>
        /// gregt
        /// </summary>
        ChaseOutcomeDto Chase(IRatingDetailsDto ratingDetailsDto, IExtensionDetailsDto extensionDetailsDto);
    }
}