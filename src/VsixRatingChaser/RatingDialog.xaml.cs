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
            InitializeRatingRequest(); 
        }

        private void InitializeRatingRequest()
        {
            HasMaximizeButton = true;
            HasMinimizeButton = true;
            ResizeMode = ResizeMode.CanResize;
            SizeToContent = SizeToContent.WidthAndHeight;
            Title = _extensionDetailsDto.ExtensionName;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            AppTextChaseStatement.Text =
                $"I created this {_extensionDetailsDto.ExtensionName} extension entirely unpaid in my personal free time. It is 100% free and I receive absolutely no income from it - I built it simply to help the community. It is not supported by or paid for in anyway by advertising." +
                Environment.NewLine + Environment.NewLine +
                "So please rate this extension on the Visual Studio Marketplace website via the link below - it only takes a few seconds (just click the stars at top of page after clicking the link, there's no need to write an actual review unless you want to). The extension will not stop working or have reduced functionality if you don't rate it, nor will you be bombarded with requests for a rating, but given the cost of this extension it's the least you can do." +
                Environment.NewLine + Environment.NewLine +
                "You'll see this pop-up request a maximum of three times, at quarterly intervals." +
                Environment.NewLine + Environment.NewLine +
                $"Thank you, {_extensionDetailsDto.AuthorName}";

            AppTextClickForVsmp.Text = $"Click here to rate {_extensionDetailsDto.ExtensionName}";

            var ratingRequestUrl = UrlHelper.GetMarketPlaceUrl(_extensionDetailsDto.MarketPlaceUrl);
            AppHyperLink.NavigateUri = new Uri(ratingRequestUrl);
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