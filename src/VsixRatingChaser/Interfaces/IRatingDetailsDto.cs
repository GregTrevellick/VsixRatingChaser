using System;
using Microsoft.VisualStudio.Shell;

namespace VsixRatingChaser.Interfaces
{
    public interface IRatingDetailsDto : IProfileManager
    {
        DateTime LastRatingRequest { get; set; }
        int RatingRequestCount { get; set; }
    }
}