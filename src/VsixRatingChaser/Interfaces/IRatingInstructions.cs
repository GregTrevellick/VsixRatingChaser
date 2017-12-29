namespace VsixRatingChaser.Interfaces
{
    public interface IRatingInstructions 
    {
        byte[] ImageByteArray { get; set; }//gregt rename to 16x16
        string VsixName { get; set; }
        string VsixAuthor { get; set; }
    }
}