using Microsoft.VisualStudio.PlatformUI;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;
using VsixRatingChaser.Dtos;

namespace VsixRatingChaser
{
    public partial class RatingDialog : DialogWindow
    {
        internal bool RatingHyperLinkClicked;
        private readonly ExtensionDetailsDto _extensionDetailsDto;

        internal RatingDialog(ExtensionDetailsDto extensionDetailsDto)
        {
            InitializeComponent();
            _extensionDetailsDto = extensionDetailsDto;
            InitializeReviewRequest(); 
        }

        private void InitializeReviewRequest()
        {
            HasMaximizeButton = true;
            HasMinimizeButton = true;
            ResizeMode = ResizeMode.CanResize;
            SizeToContent = SizeToContent.WidthAndHeight;
            Title = _extensionDetailsDto.ExtensionName;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            AppTextChaseStatement.Text =
                $"I created this {_extensionDetailsDto.ExtensionName} extension entirely unpaid in my personal free time. It is 100% free and I receive absolutely no income from it - I built it simply to help the community." +
                Environment.NewLine + Environment.NewLine +
                "So please rate this extension on the Visual Studio Marketplace website via the link below - it only takes a few seconds. The extension will not stop working or have reduced functionality if you don't review it, nor will you be bombarded with requests for a review, but given the cost of this extension it's the least you can do." +
                Environment.NewLine + Environment.NewLine +
                "You'll see this pop-up request a maximum of three times, at quarterly intervals." +
                Environment.NewLine + Environment.NewLine +
                $"Thank you, {_extensionDetailsDto.AuthorName}";

            AppTextClickForVsmp.Text = $"Click here to create a review for {_extensionDetailsDto.ExtensionName}";

            var ratingRequestUrl = GetMarketPlaceUrl();
            AppHyperLink.NavigateUri = new Uri(ratingRequestUrl);
        }

        private string GetMarketPlaceUrl()//gregt unit test reqd
        {
            var url = _extensionDetailsDto.MarketPlaceUrl;

            if (!url.ToLower().EndsWith("#review-details".ToLower()))
            {
                url += "#review-details";
            }

            return url;
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