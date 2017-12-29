using System.ComponentModel;

namespace VsixRatingChaser.Enums
{
    public enum RejectionReason
    {
        [Description("gregt0")]
        NotRejected = 0,
        AuthorNameCannotBeBlank,
        ExtensionNameCannotBeBlank,
        MarketplaceUrlStartIsWrong,
        MarketplaceUrlUndefined,
    }
}
