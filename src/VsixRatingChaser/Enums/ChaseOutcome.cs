namespace VsixRatingChaser.Enums
{
    /// <summary>
    /// A list of possible outcomes from invoking the package
    /// </summary>
    public enum ChaseOutcome
    {
        Unknown = 0,
        SuccessfullCallButDialogNotShownToUser,
        SuccessfullCallAndDialogShownToUserUrlNotClicked,
        SuccessfullCallAndDialogShownToUserUrlClicked,
        InvalidCallAsAuthorNameCannotBeBlank,
        InvalidCallAsExtensionNameCannotBeBlank,
        InvalidCallAsMarketplaceUrlIsNotTheVisualStudioMarketplaceDomain,
        InvalidCallAsMarketplaceUrlCannotBeBlank,
    }
}
