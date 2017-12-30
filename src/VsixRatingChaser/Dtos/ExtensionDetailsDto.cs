namespace VsixRatingChaser.Dtos
{
    /// <summary>
    /// Parameter data related to the Visual Studio extension
    /// </summary>
    public class ExtensionDetailsDto 
    {
        /// <summary>
        /// The calling visual studio extension author. Must be supplied. May contain spaces. Appears in the pop-up request for rating.
        /// </summary>
        public string AuthorName { get; set; }

        /// <summary>
        /// The calling visual studio extension name. Must be supplied. May contain spaces. Appears in the pop-up request for rating.
        /// </summary>
        public string ExtensionName { get; set; }

        /// <summary>
        /// The official visual studio marketplace URL for the calling visual studio extension name. Must be supplied. Used as a hyperlink in the pop-up request for rating. Must start with "https://marketplace.visualstudio.com/items?itemName=". May optionally end with "#review-details". e.g. https://marketplace.visualstudio.com/items?itemName=GregTrevellick.OpeninPaintNET#review-details
        /// </summary>
        public string MarketPlaceUrl { get; set; }
    }
}