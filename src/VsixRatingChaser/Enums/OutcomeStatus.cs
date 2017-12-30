using System.ComponentModel;

namespace VsixRatingChaser.Enums
{
    public enum OutcomeStatus
    {
        [Description("Error occurred")]
        Unknown=0,
        [Description("Successful call but dialog not shown to user")]
        SuccesfulCallButDialogNotShownToUser,
        [Description("Successful call and dialog shown to user, but url not clicked")]
        SuccesfulCallAndDialogShownToUserUrlNotClicked,
        [Description("Successful call and dialog shown to user, and url clicked")]
        SuccesfulCallAndDialogShownToUserUrlClicked,
        [Description("Author Name Cannot Be Blank")]
        AuthorNameCannotBeBlank,
        [Description("Extension Name Cannot Be Blank")]
        ExtensionNameCannotBeBlank,
        [Description("Marketplace Url Start Is Wrong")]
        MarketplaceUrlStartIsWrong,
        [Description("Marketplace Url Undefined")]
        MarketplaceUrlUndefined,
    }
}
