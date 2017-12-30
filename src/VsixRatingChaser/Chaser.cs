using System;
using VsixRatingChaser.Dtos;
using VsixRatingChaser.Enums;
using VsixRatingChaser.Interfaces;

namespace VsixRatingChaser
{
    /// <summary>
    /// gregt
    /// </summary>
    public class Chaser : IChaser
    {
        private bool _ratingHyperLinkClicked;

        /// <summary>
        /// gregt
        /// </summary>
        /// <param name="ratingDetailsDto"></param>
        /// <param name="extensionDetailsDto"></param>
        /// <returns></returns>
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
                    outcome = _ratingHyperLinkClicked ? ChaseOutcome.SuccessfullCallAndDialogShownToUserUrlClicked : ChaseOutcome.SuccessfullCallAndDialogShownToUserUrlNotClicked;
                }
                else
                {
                    outcome = ChaseOutcome.SuccessfullCallButDialogNotShownToUser;
                }
            }

            return outcome;
        }

        internal ChaseOutcome Validate(ExtensionDetailsDto extensionDetailsDto)
        {
            var outcome = ChaseOutcome.Unknown;

            if (string.IsNullOrWhiteSpace(extensionDetailsDto?.AuthorName))
            {
                outcome = ChaseOutcome.InvalidCallAsAuthorNameCannotBeBlank;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(extensionDetailsDto?.ExtensionName))
                {
                    outcome = ChaseOutcome.InvalidCallAsExtensionNameCannotBeBlank;
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(extensionDetailsDto?.MarketPlaceUrl))
                    {
                        outcome = ChaseOutcome.InvalidCallAsMarketplaceUrlCannotBeBlank;
                    }
                    else
                    {
                        if (!extensionDetailsDto.MarketPlaceUrl.ToLower()
                            .StartsWith(ChaseSettings.MarketplaceUrlPrefix.ToLower()))
                        {
                            outcome = ChaseOutcome.InvalidCallAsMarketplaceUrlIsNotTheVisualStudioMarketplaceDomain;
                        }
                    }
                }

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
            ratingDetailsDto.PreviousRatingRequest = DateTime.Now;
            ratingDetailsDto.RatingRequestCount++;
            ratingDetailsDto.SaveSettingsToStorage();
        }
    }
}