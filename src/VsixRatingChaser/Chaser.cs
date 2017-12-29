using System;
using System.IO;
using System.Windows.Media.Imaging;
using VsixRatingChaser.Enums;
using VsixRatingChaser.Interfaces;

namespace VsixRatingChaser
{
    public class Chaser : IChaser
    {
        private bool ratingHyperLinkClicked;
        private BitmapImage bitmapImage;

        public IChaseVerdict Chase(IHiddenChaserOptions hiddenChaserOptions, IRatingInstructions ratingInstructions)
        {
            var chaseVerdict = ValidateRatingInstructions(ratingInstructions);

            if (!chaseVerdict.Rejected)
            {
                var ratingDecider = new RatingDecider();
                var shouldShowDialog = ratingDecider.ShouldShowDialog(hiddenChaserOptions);

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

        private void ShowDialog(IHiddenChaserOptions hiddenChaserOptions, IRatingInstructions ratingInstructions)
        {
            var ratingDialog = new RatingDialog(ratingInstructions, bitmapImage);

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