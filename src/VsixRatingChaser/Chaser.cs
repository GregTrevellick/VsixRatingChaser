using System;
using VsixRatingChaser.Enums;
using VsixRatingChaser.Interfaces;

namespace VsixRatingChaser
{
    public class Chaser : IChaser
    {
        private bool ratingHyperLinkClicked;

        public IChaseOutcomeDto Chase(IRatingDetailsDto ratingDetailsDto, IExtensionDetailsDto extensionDetailsDto)
        {
            var chaseVerdict = Validate(extensionDetailsDto);

            if (!chaseVerdict.Rejected)
            {
                var ratingDecider = new RatingDecider();
                var shouldShowDialog = ratingDecider.ShouldShowDialog(ratingDetailsDto);

                if (shouldShowDialog)
                {
                    ShowDialog(ratingDetailsDto, extensionDetailsDto);
                    chaseVerdict.RatingDialogShown = true;
                    chaseVerdict.RatingHyperLinkClicked = ratingHyperLinkClicked;
                }
            }

            return chaseVerdict;
        }

        private ChaseOutcome Validate(IExtensionDetailsDto extensionDetailsDto)
        {
            var chaseVerdict = new ChaseOutcome();

            if (string.IsNullOrWhiteSpace(extensionDetailsDto.AuthorName))
            {
                chaseVerdict.Rejected = true;
                chaseVerdict.RejectionReason = RejectionReason.VsixAuthorCannotBeBlank;
            }

            if (string.IsNullOrWhiteSpace(extensionDetailsDto.ExtensionName))
            {
                chaseVerdict.Rejected = true;
                chaseVerdict.RejectionReason = RejectionReason.VsixNameCannotBeBlank;
            }

            if (string.IsNullOrWhiteSpace(extensionDetailsDto.MarketPlaceUrl))
            {
                chaseVerdict.Rejected = true;
                chaseVerdict.RejectionReason = RejectionReason.RatingRequestUrlUndefined;
            }

            if (!extensionDetailsDto.MarketPlaceUrl.ToLower()
                .StartsWith("https://marketplace.visualstudio.com/items?itemName=".ToLower()))
            {
                chaseVerdict.Rejected = true;
                chaseVerdict.RejectionReason = RejectionReason.RatingRequestUrlStartIsWrong;
            }

            return chaseVerdict;
        }

        private void ShowDialog(IRatingDetailsDto ratingDetailsDto, IExtensionDetailsDto extensionDetailsDto)
        {
            var ratingDialog = new RatingDialog(extensionDetailsDto);
            ratingDialog.Show();
            ratingHyperLinkClicked = ratingDialog.RatingHyperLinkClicked;
            PersistRatingDetails(ratingDetailsDto);
        }

        private void PersistRatingDetails(IRatingDetailsDto ratingDetailsDto)
        {
            ratingDetailsDto.LastRatingRequest = DateTime.Now;
            ratingDetailsDto.RatingRequestCount++;
            ratingDetailsDto.SaveSettingsToStorage();
        }
    }
}

