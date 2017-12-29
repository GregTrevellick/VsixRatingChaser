using System;
using VsixRatingChaser.Enums;
using VsixRatingChaser.Interfaces;

namespace VsixRatingChaser
{
    public class Chaser : IChaser
    {
        private bool ratingHyperLinkClicked;

        public IChaseVerdict Chase(IHiddenChaserOptions hiddenChaserOptions, IRatingInstructions ratingInstructions)
        {
            var chaseVerdict = ValidateRatingInstructions(ratingInstructions);

            if (!chaseVerdict.Rejected)
            {
                var ratingDecider = new RatingDecider();
                var shouldShowDialog = ratingDecider.ShouldShowDialog(hiddenChaserOptions);

                if (shouldShowDialog)
                {
                    ShowDialog(hiddenChaserOptions, ratingInstructions);
                    chaseVerdict.RatingDialogShown = true;
                    chaseVerdict.RatingHyperLinkClicked = ratingHyperLinkClicked;
                }
            }

            return chaseVerdict;
        }

        private ChaseVerdict ValidateRatingInstructions(IRatingInstructions ratingInstructions)
        {
            var chaseVerdict = new ChaseVerdict();

            if (string.IsNullOrWhiteSpace(ratingInstructions.VsixAuthor))
            {
                chaseVerdict.Rejected = true;
                chaseVerdict.RejectionReason = RejectionReason.VsixAuthorCannotBeBlank;
            }

            if (string.IsNullOrWhiteSpace(ratingInstructions.VsixName))
            {
                chaseVerdict.Rejected = true;
                chaseVerdict.RejectionReason = RejectionReason.VsixNameCannotBeBlank;
            }

            return chaseVerdict;
        }

        private void ShowDialog(IHiddenChaserOptions hiddenChaserOptions, IRatingInstructions ratingInstructions)
        {
            var ratingDialog = new RatingDialog(ratingInstructions);

            ratingDialog.Show();

            ratingHyperLinkClicked = ratingDialog.RatingHyperLinkClicked;

            PersistHiddenChaserOptions(hiddenChaserOptions);
        }

        private void PersistHiddenChaserOptions(IHiddenChaserOptions hiddenChaserOptions)
        {
            hiddenChaserOptions.LastRatingRequest = DateTime.Now;
            hiddenChaserOptions.RatingRequestCount++;
            hiddenChaserOptions.SaveSettingsToStorage();
        }
    }
}










//if (string.IsNullOrWhiteSpace(ratingInstructions.RatingRequestUrl))
//{
//    chaseVerdict.Rejected = true;
//    chaseVerdict.RejectionReason = RejectionReason.RatingRequestUrlUndefined;
//}

//if (!ratingInstructions.RatingRequestUrl.ToLower()
//    .StartsWith("https://marketplace.visualstudio.com/items?itemName=".ToLower()))
//{
//    chaseVerdict.Rejected = true;
//    chaseVerdict.RejectionReason = RejectionReason.RatingRequestUrlStartIsWrong;
//}

//if (!ratingInstructions.RatingRequestUrl.ToLower().EndsWith("#review-details".ToLower()))
//{
//    chaseVerdict.Rejected = true;
//    chaseVerdict.RejectionReason = RejectionReason.RatingRequestUrlAnchorTagIsWrong;
//}

//if (!ratingInstructions.RatingRequestUrl.ToLower()
//    .StartsWith("https://marketplace.visualstudio.com/items?itemName=".ToLower()))
//{
//    chaseVerdict.Rejected = true;
//    chaseVerdict.RejectionReason = RejectionReason.RatingRequestUrlStartIsWrong;
//}
