using System;
using System.IO;
using System.Windows.Media.Imaging;
using VsixRatingChaser.Enums;

namespace VsixRatingChaser
{
    public class Chaser : IChaser
    {
        private bool ratingHyperLinkClicked;
        private BitmapImage bitmapImage;

        public IChaseVerdict Chase(IHiddenChaserOptions hiddenRatingChaserOptions, IRatingInstructions ratingInstructions)
        {
            var chaseVerdict = ValidateRatingInstructions(ratingInstructions);

            if (!chaseVerdict.Rejected)
            {
                var ratingDecider = new RatingDecider();
                var aggressionLimit = GetAggressionLimit(ratingInstructions.AggressionLevel);
                var shouldShowDialog = ratingDecider.ShouldShowDialog(hiddenRatingChaserOptions, aggressionLimit);///////////////////////////////////, ratingInstructions, aggressionLimit);

                if (shouldShowDialog)
                {

                    //gregt hide image in rating chaser popup if image not 16x16
                    if (ratingInstructions.ImageByteArray != null)
                    {
                        bitmapImage = GetBitmapImage(ratingInstructions.ImageByteArray);
                        var height = bitmapImage.PixelHeight;
                        var width = bitmapImage.PixelWidth;

                        if ((height < 16 || width < 16) || (height > 16 || width > 16))
                        {
                            //chaseVerdict.RejectionReason = RejectionReason.ImageTooBig;
                        }
                    }


                    ShowDialog(hiddenRatingChaserOptions, ratingInstructions, aggressionLimit);
                    chaseVerdict.RatingDialogShown = true;
                    chaseVerdict.RatingHyperLinkClicked = ratingHyperLinkClicked;
                }
            }

            return chaseVerdict;
        }

        private AggressionLimit GetAggressionLimit(AggressionLevel aggressionLevel)
        {
            var aggressionLimit = new AggressionLimit();

            switch (aggressionLevel)
            {
                case AggressionLevel.Low:
                    aggressionLimit.RatingRequestGap = 3;
                    aggressionLimit.RatingRequestGapUnit = RatingRequestGapUnit.Months;
                    aggressionLimit.RatingRequestLimit = 3;
                    break;
                case AggressionLevel.Medium:
                    aggressionLimit.RatingRequestGap = 30;
                    aggressionLimit.RatingRequestGapUnit = RatingRequestGapUnit.Days;
                    aggressionLimit.RatingRequestLimit = 6;
                    break;
                case AggressionLevel.High:
                    aggressionLimit.RatingRequestGap = 2;
                    aggressionLimit.RatingRequestGapUnit = RatingRequestGapUnit.Seconds;
                    aggressionLimit.RatingRequestLimit = 9999;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(aggressionLevel), aggressionLevel, null);
            }

            return aggressionLimit;
        }

        private ChaseVerdict ValidateRatingInstructions(IRatingInstructions ratingInstructions)
        {
            var chaseVerdict = new ChaseVerdict();

            if (ratingInstructions.CostCategory == 0)
            {
                chaseVerdict.Rejected = true;
                chaseVerdict.RejectionReason = RejectionReason.RatingRequestCostCategoryIsZero;
            }

            if (ratingInstructions.CostCategory != CostCategory.Free)
            {
                chaseVerdict.Rejected = true;
                chaseVerdict.RejectionReason = RejectionReason.RatingRequestCostCategoryIsNotFree;
            }     

            //if (ratingInstructions.RatingRequestGap == 0)
            //{
            //    chaseVerdict.Rejected = true;
            //    chaseVerdict.RejectionReason = RejectionReason.RatingRequestGapIsZero;
            //}

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

            //if (ratingInstructions.RatingRequestGapUnit == 0)
            //{
            //    chaseVerdict.Rejected = true;
            //    chaseVerdict.RejectionReason = RejectionReason.RatingRequestGapUnitUndefined;
            //}

            if (ratingInstructions.DialogType == 0)
            {
                chaseVerdict.Rejected = true;
                chaseVerdict.RejectionReason = RejectionReason.DialogTypeUndefined;
            }

            if (ratingInstructions.AggressionLevel == 0)
            {
                chaseVerdict.Rejected = true;
                chaseVerdict.RejectionReason = RejectionReason.AggressionLevelUndefined;
            }

            //if (ratingInstructions.AggressionLevel == AggressionLevel.Low)
            //{
            //    if (ratingInstructions.RatingRequestGap < 90 ||
            //        ratingInstructions.PackageLoadedLimit < 10000 ||
            //        ratingInstructions.RatingRequestLimit > 3)
            //    {
            //        chaseVerdict.Rejected = true;
            //        chaseVerdict.RejectionReason = RejectionReason.RatingInstructionsTooAggressiveForLow;
            //    }
            //}

            //if (ratingInstructions.AggressionLevel == AggressionLevel.Medium)
            //{
            //    if (ratingInstructions.RatingRequestGap < 30 ||
            //        ratingInstructions.PackageLoadedLimit < 1000 ||
            //        ratingInstructions.RatingRequestLimit > 9)
            //    {
            //        chaseVerdict.Rejected = true;
            //        chaseVerdict.RejectionReason = RejectionReason.RatingInstructionsTooAggressiveForMedium;
            //    }
            //}

            //gregt reimplement this later
            //if (ratingInstructions.AggressionLevel == AggressionLevel.High)
            //{
            //    chaseVerdict.Rejected = true;
            //    chaseVerdict.RejectionReason = RejectionReason.HighlyAggressiveChasingNotSupported;
            //}

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

        private BitmapImage GetBitmapImage(byte[] imageByteArray)
        {
            var memoryStream = new MemoryStream(imageByteArray);
            var bitmapImage = GetBitmapImage(memoryStream);
            return bitmapImage;
        }

        private BitmapImage GetBitmapImage(MemoryStream memoryStream)
        {
            var bitmapImage = new BitmapImage();

            bitmapImage.BeginInit();
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.StreamSource = memoryStream;
            bitmapImage.EndInit();

            return bitmapImage;
        }

        private void ShowDialog(IHiddenChaserOptions hiddenChaserOptions, IRatingInstructions ratingInstructions, AggressionLimit aggressionLimit)
        {
            var ratingDialog = new RatingDialog(ratingInstructions, bitmapImage, aggressionLimit);

            switch (ratingInstructions.DialogType)
            {
                case DialogType.Modal:
                    ratingDialog.ShowModal();
                    break;
                default:
                    ratingDialog.Show();
                    break;
            }

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