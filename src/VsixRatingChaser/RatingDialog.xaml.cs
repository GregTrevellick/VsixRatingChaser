using Microsoft.VisualStudio.PlatformUI;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using VsixRatingChaser.Dtos;

namespace VsixRatingChaser
{
    public partial class RatingDialog : DialogWindow
    {
        internal bool RatingHyperLinkClicked;
        private readonly ExtensionDetailsDto _extensionDetailsDto;

        internal RatingDialog(ExtensionDetailsDto extensionDetailsDto, int ratingRequestCount)
        {
            InitializeComponent();
            _extensionDetailsDto = extensionDetailsDto;
            InitializeRatingRequest(ratingRequestCount); 
        }

        private void InitializeRatingRequest(int ratingRequestCount)
        {
            HasMaximizeButton = true;
            HasMinimizeButton = true;
            ResizeMode = ResizeMode.CanResize;
            SizeToContent = SizeToContent.WidthAndHeight;
            MaxWidth = 800;
            Title = $"{_extensionDetailsDto.ExtensionName} (rating request {ratingRequestCount} of {ChaseSettings.RatingRequestLimit}";

            //SetWindowIcon();

            if (ratingRequestCount < ChaseSettings.RatingRequestLimit)
            {
                Title += $", next rating request will occur in {ChaseSettings.RatingRequestGapInMonths} months time)";
            }
            else
            {
                Title += ", you will not be pestered again)";
            }

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            AppTextChaseStatement.Text =
                Environment.NewLine + 
                $"If you have already rated {_extensionDetailsDto.ExtensionName} please accept my apologies and ignore the request below." +
                Environment.NewLine + Environment.NewLine + 
                $"I created {_extensionDetailsDto.ExtensionName} extension entirely unpaid in my personal free time. It is 100% free and I receive absolutely no income from it. It is not supported by or paid for in anyway by advertising." +
                Environment.NewLine + Environment.NewLine +
                $"If you find {_extensionDetailsDto.ExtensionName} useful please rate this extension via the link below - it only takes a few seconds (just click the stars at top of page after clicking the link, there's no need to write an actual review). " +
                Environment.NewLine + Environment.NewLine +
                $"{_extensionDetailsDto.ExtensionName} will not stop working or have reduced functionality if you don't rate it, nor will you be bombarded with requests for a rating. " +
                Environment.NewLine + Environment.NewLine +
                $"Given the zero cost of {_extensionDetailsDto.ExtensionName} it's the least you can do, and it will be very much appreciated.";

            AppTextClickForVsmp.Text = $"Click here to rate {_extensionDetailsDto.ExtensionName}";

            AppTextAppreciation.Text = $"Thank you, {_extensionDetailsDto.AuthorName}";

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

        //private void SetWindowIcon()
        //{
        //    var iconUri = GetIconUri();
        //    Icon = new BitmapImage(iconUri);
        //}

        //private Uri GetIconUri()
        //{
        //    var assemblyName = "VsixRatingChaser";
        //    var packUri = $"pack://application:,,,/{assemblyName};component/Resources/nugeticon_96x96_ggf_icon.ico";
        //    return new Uri(packUri, UriKind.RelativeOrAbsolute);
        //}

    }
}