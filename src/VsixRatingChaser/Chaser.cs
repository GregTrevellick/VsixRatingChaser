using System;
using VsixRatingChaser.Dtos;
using VsixRatingChaser.Enums;
using VsixRatingChaser.Interfaces;

namespace VsixRatingChaser
{
    public class Chaser : IChaser
    {
        private bool _ratingHyperLinkClicked;

        public ChaseOutcome Chase(IRatingDetailsDto ratingDetailsDto, ExtensionDetailsDto extensionDetailsDto)
        {
            var outcome = Validate(extensionDetailsDto);

            if (outcome == ChaseOutcome.Unknown)
            {
                var ratingDecider = new RatingDecider();
                var shouldShowDialog = ratingDecider.ShouldShowDialog(ratingDetailsDto);

                if (shouldShowDialog)
                {
                    ShowDialog(ratingDetailsDto, extensionDetailsDto);
                    if (_ratingHyperLinkClicked)
                    {
                        outcome = ChaseOutcome.SuccessfullCallAndDialogShownToUserUrlClicked;
                    }
                    else
                    {
                        outcome = ChaseOutcome.SuccessfullCallAndDialogShownToUserUrlNotClicked;
                    }
                }
                else
                {
                    outcome = ChaseOutcome.SuccessfullCallButDialogNotShownToUser;
                }
            }

            return outcome;
        }

        private ChaseOutcome Validate(ExtensionDetailsDto extensionDetailsDto)
        {
            var outcome = ChaseOutcome.Unknown;

            if (string.IsNullOrWhiteSpace(extensionDetailsDto.AuthorName))
            {
                outcome = ChaseOutcome.InvalidCallAsAuthorNameCannotBeBlank;
            }

            if (string.IsNullOrWhiteSpace(extensionDetailsDto.ExtensionName))
            {
                outcome = ChaseOutcome.InvalidCallAsExtensionNameCannotBeBlank;
            }

            if (string.IsNullOrWhiteSpace(extensionDetailsDto.MarketPlaceUrl))
            {
                outcome = ChaseOutcome.InvalidCallAsMarketplaceUrlUndefined;
            }

            if (!extensionDetailsDto.MarketPlaceUrl.ToLower()
                .StartsWith("https://marketplace.visualstudio.com/items?itemName=".ToLower()))
            {
                outcome = ChaseOutcome.InvalidCallAsMarketplaceUrlStartIsWrong;
            }

            return outcome;
        }

        private void ShowDialog(IRatingDetailsDto ratingDetailsDto, ExtensionDetailsDto extensionDetailsDto)
        {
            var ratingDialog = new RatingDialog(extensionDetailsDto);
            ratingDialog.Show();
            _ratingHyperLinkClicked = ratingDialog.RatingHyperLinkClicked;
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