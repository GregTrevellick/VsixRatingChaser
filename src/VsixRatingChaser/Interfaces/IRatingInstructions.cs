using VsixRatingChaser.Enums;

namespace VsixRatingChaser.Interfaces
{
    public interface IRatingInstructions 
    {
        byte[] ImageByteArray { get; set; }//gregt rename to 16x16
        DialogType DialogType { get; set; }
        string VsixName { get; set; }
        string VsixAuthor { get; set; }
    }
}