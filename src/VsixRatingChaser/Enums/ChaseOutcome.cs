namespace VsixRatingChaser.Enums
{
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
