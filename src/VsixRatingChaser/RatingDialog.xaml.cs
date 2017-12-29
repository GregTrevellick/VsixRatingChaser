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
        private readonly IExtensionDetailsDto _extensionDetailsDto;

        internal RatingDialog(IExtensionDetailsDto extensionDetailsDto)
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
                $"I, {_extensionDetailsDto.AuthorName}, created the {_extensionDetailsDto.ExtensionName} extension entirely unpaid in my personal free time. It is 100% free and I receive no income, direct or indirect, from it.{Environment.NewLine}All that I ask is that you rate this extension on the Visual Studio Market Place by clicking on the link below, it will be greatly appreciated.{Environment.NewLine}Thank you, {_extensionDetailsDto.AuthorName}";

            AppTextClickForVsmp.Text = "Click here to place review";

            var ratingRequestUrl = GetMarketPlaceUrl();
            AppHyperLink.NavigateUri = new Uri(ratingRequestUrl);
        }

        private string GetMarketPlaceUrl()
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