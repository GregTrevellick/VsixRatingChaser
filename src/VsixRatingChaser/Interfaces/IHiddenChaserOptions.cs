using System;
using Microsoft.VisualStudio.Shell;

namespace VsixRatingChaser.Interfaces
{
    public interface IHiddenChaserOptions : IProfileManager
    {
        DateTime LastRatingRequest { get; set; }
        int RatingRequestCount { get; set; }
    }
}