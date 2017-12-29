using Microsoft.VisualStudio.PlatformUI;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using VsixRatingChaser.Interfaces;

namespace VsixRatingChaser
{
    public partial class RatingDialog : DialogWindow
    {
        internal bool RatingHyperLinkClicked;
        private IRatingInstructions _ratingInstructions;

        internal RatingDialog(IRatingInstructions ratingInstructions, BitmapImage bitmapImage)
        {
            InitializeComponent();
            _ratingInstructions = ratingInstructions;
            InitializeReviewRequest(bitmapImage);
        }

        private void InitializeReviewRequest(BitmapImage bitmapImage)
        {
            HasMaximizeButton = true;
            HasMinimizeButton = true;
            SizeToContent = SizeToContent.WidthAndHeight;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            if (_ratingInstructions.ImageByteArray != null)
            {
                SetAppImage(bitmapImage);
            }

            Title = _ratingInstructions.VsixName;
            AppTextChaseStatement.Text = "Hi there," +
                                         "I have a day job coding " +
                                         "but I wrote this 100% free extension unpaid in my personal free time." +
                                         Environment.NewLine + Environment.NewLine +
                                         " I make no money at all from this extension (I've even made the source code publicly available on GitHub under a very generous licence) " +
                                         Environment.NewLine + Environment.NewLine +
                                         "and wrote it in order to give something back to the industry that has given me so much over the years. " +
                                         Environment.NewLine + Environment.NewLine +
                                         "The only reward I get is learning new coding techniques. " +
                                         Environment.NewLine + Environment.NewLine +
                                         "All I ask is that you rate this extension on the Visual Studio Market Place by clicking <here>, it will be greatly appreciated. " +
                                         Environment.NewLine + Environment.NewLine +
                                         "Thank you, " + _ratingInstructions.VsixAuthor +
                                         Environment.NewLine + Environment.NewLine +
                                         "P.S. I promise this extension won't hassle you for a review.";

            AppTextClickForVsmp.Text = "click here";
            var ratingRequestUrl = GetRatingRequestUrl();
            AppHyperLink.NavigateUri = new Uri(ratingRequestUrl);
        }

        private string GetRatingRequestUrl()
        {
            var ratingRequestUrl =
                $@"https://marketplace.visualstudio.com/items?itemName={_ratingInstructions.VsixAuthor}.{
                        _ratingInstructions.VsixName
                    }#review-details";
            return ratingRequestUrl;
        }

        private void SetAppImage(BitmapImage bitmapImage)
        {
        //    var memoryStream = new MemoryStream(_ratingInstructions.ImageByteArray);
        //    var bitmapImage = GetBitmapImage(memoryStream);
        //    var height = bitmapImage.PixelHeight;
        //    var width = bitmapImage.PixelWidth;

            Icon = bitmapImage;
            AppImage.Source = bitmapImage;
        }

        //private static BitmapImage GetBitmapImage(MemoryStream memoryStream)
        //{
        //    var bitmapImage = new BitmapImage();

        //    bitmapImage.BeginInit();
        //    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
        //    bitmapImage.StreamSource = memoryStream;
        //    bitmapImage.EndInit();

        //    return bitmapImage;
        //}

        private void AppHyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            AppTextThankYou.Visibility = Visibility.Visible;
            RatingHyperLinkClicked = true;
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
        
        private void AppBtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AppBtnHelp_OnClick(object sender, RoutedEventArgs e)
        {
            if (AppTextExplain.Visibility == Visibility.Visible)
            {
                AppTextExplain.Visibility = Visibility.Collapsed;
            }
            else
            {
                AppTextExplain.Text = "this is message gregt of 9999. The next message will be gregt seconds from now.";
                AppTextExplain.Visibility = Visibility.Visible;
            }
        }
    }
}