using Microsoft.VisualStudio.PlatformUI;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;
using VsixRatingChaser.Interfaces;

namespace VsixRatingChaser
{
    public partial class RatingDialog : DialogWindow
    {
        internal bool RatingHyperLinkClicked;
        private IRatingInstructions _ratingInstructions;

        internal RatingDialog(IRatingInstructions ratingInstructions)
        {
            InitializeComponent();
            _ratingInstructions = ratingInstructions;
            InitializeReviewRequest(); 
        }

        private void InitializeReviewRequest()
        {
            HasMaximizeButton = true;
            HasMinimizeButton = true;
            ResizeMode = ResizeMode.CanResize;
            SizeToContent = SizeToContent.WidthAndHeight;
            Title = _ratingInstructions.VsixName;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            AppTextChaseStatement.Text =
                $"I, {_ratingInstructions.VsixAuthor}, created the {_ratingInstructions.VsixName} extension entirely unpaid in my personal free time. It is 100% free and I receive no income, direct or indirect, from it.{Environment.NewLine}All that I ask is that you rate this extension on the Visual Studio Market Place by clicking on the link below, it will be greatly appreciated.{Environment.NewLine}Thank you, {_ratingInstructions.VsixAuthor}";

            AppTextClickForVsmp.Text = "click here to place review";

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

        private void AppHyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            AppTextLinkClickedThankYou.Visibility = Visibility.Visible;
            RatingHyperLinkClicked = true;
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}