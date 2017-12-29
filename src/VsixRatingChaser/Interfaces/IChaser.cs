namespace VsixRatingChaser.Interfaces
{
    public interface IChaser
    {
        /// <summary>
        /// gregt
        /// </summary>
        IChaseOutcomeDto Chase(IRatingDetailsDto ratingDetailsDto, IExtensionDetailsDto extensionDetailsDto);
    }
}