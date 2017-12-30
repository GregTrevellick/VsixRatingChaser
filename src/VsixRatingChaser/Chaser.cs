using System;
using VsixRatingChaser.Dtos;
using VsixRatingChaser.Enums;
using VsixRatingChaser.Interfaces;

namespace VsixRatingChaser
{
    public class Chaser : IChaser
    {
        private bool _ratingHyperLinkClicked;

        public ChaseOutcomeDto Chase(IRatingDetailsDto ratingDetailsDto, ExtensionDetailsDto extensionDetailsDto)
        {
            var outcome = Validate(extensionDetailsDto);

            if (outcome.OutcomeStatus == OutcomeStatus.Unknown)
            {
                var ratingDecider = new RatingDecider();
                var shouldShowDialog = ratingDecider.ShouldShowDialog(ratingDetailsDto);

                if (shouldShowDialog)
                {
                    ShowDialog(ratingDetailsDto, extensionDetailsDto);
                    if (_ratingHyperLinkClicked)
                    {
                        outcome.OutcomeStatus = OutcomeStatus.SuccesfulCallAndDialogShownToUserUrlClicked;
                    }
                    else
                    {
                        outcome.OutcomeStatus = OutcomeStatus.SuccesfulCallAndDialogShownToUserUrlNotClicked;
                    }
                }
                else
                {
                    outcome.OutcomeStatus = OutcomeStatus.SuccesfulCallButDialogNotShownToUser;
                }
            }

            return outcome;
        }

        private ChaseOutcomeDto Validate(ExtensionDetailsDto extensionDetailsDto)
        {
            var outcome = new ChaseOutcomeDto();

            if (string.IsNullOrWhiteSpace(extensionDetailsDto.AuthorName))
            {
                outcome.OutcomeStatus = OutcomeStatus.AuthorNameCannotBeBlank;
            }

            if (string.IsNullOrWhiteSpace(extensionDetailsDto.ExtensionName))
            {
                outcome.OutcomeStatus = OutcomeStatus.ExtensionNameCannotBeBlank;
            }

            if (string.IsNullOrWhiteSpace(extensionDetailsDto.MarketPlaceUrl))
            {
                outcome.OutcomeStatus = OutcomeStatus.MarketplaceUrlUndefined;
            }

            if (!extensionDetailsDto.MarketPlaceUrl.ToLower()
                .StartsWith("https://marketplace.visualstudio.com/items?itemName=".ToLower()))
            {
                outcome.OutcomeStatus = OutcomeStatus.MarketplaceUrlStartIsWrong;
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