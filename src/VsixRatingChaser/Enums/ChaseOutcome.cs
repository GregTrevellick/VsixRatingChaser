namespace VsixRatingChaser.Enums
{
    /// <summary>
    /// gregt
    /// </summary>
    public enum ChaseOutcome
    {
        Unknown = 0,
        SuccessfullCallButDialogNotShownToUser,
        SuccessfullCallAndDialogShownToUserUrlNotClicked,
        SuccessfullCallAndDialogShownToUserUrlClicked,
        InvalidCallAsAuthorNameCannotBeBlank,
        InvalidCallAsExtensionNameCannotBeBlank,
        InvalidCallAsMarketplaceUrlPrefixIsWrong,
        InvalidCallAsMarketplaceUrlUndefined,
    }
}
