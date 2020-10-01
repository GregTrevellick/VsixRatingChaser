using System;
using VsixRatingChaser.Dtos;
using VsixRatingChaser.Enums;
using VsixRatingChaser.Interfaces;

namespace VsixRatingChaser
{
    public class Chaser : IChaser
    {
        private bool _ratingHyperLinkClicked;

        /// <summary>
        /// Validates the request and conditionally displays a pop-up to user asking for an online rating / review
        /// </summary>
        /// <param name="ratingDetailsDto">Parameter data related to the request for ratings</param>
        /// <param name="extensionDetailsDto">Parameter data related to the Visual Studio extension</param>
        /// <returns>The result of the package invocation</returns>
        public ChaseOutcome Chase(IRatingDetailsDto ratingDetailsDto, ExtensionDetailsDto extensionDetailsDto)
        {
            var outcome = Validate(extensionDetailsDto);

            if (outcome == ChaseOutcome.Unknown)
            {



                //first time ever
                if (ratingDetailsDto.RatingRequestCount == 0 &&
                    ratingDetailsDto.PreviousRatingRequest == DateTime.MinValue)//gregt unit test
                {
                    ratingDetailsDto.PreviousRatingRequest = DateTime.Now;
                    ratingDetailsDto.SaveSettingsToStorage();
                }

                //if pop up shown previously then it should have a date
                if (ratingDetailsDto.RatingRequestCount > 0 &&
                    ratingDetailsDto.PreviousRatingRequest == DateTime.MinValue) //gregt unit test
                {
                    //gregt error
                }




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
            //TODO validate name & author > 3 long

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
            var ratingDialog = new RatingDialog(extensionDetailsDto, ratingDetailsDto.RatingRequestCount);
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