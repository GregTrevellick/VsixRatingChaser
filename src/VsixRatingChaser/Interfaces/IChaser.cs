namespace VsixRatingChaser.Interfaces
{
    public interface IChaser
    {
        /// <summary>
        /// gregt
        /// </summary>
        /// <param name="hiddenChaserOptions"></param>
        /// <param name="ratingInstructions"></param>
        IChaseVerdict Chase(IHiddenChaserOptions hiddenChaserOptions, IRatingInstructions ratingInstructions);
    }
}