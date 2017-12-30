using System;
using Microsoft.VisualStudio.Shell;

namespace VsixRatingChaser.Interfaces
{
    /// <summary>
    /// gregt
    /// </summary>
    public interface IRatingDetailsDto : IProfileManager
    {
        /// <summary>
        /// gregt
        /// </summary>
        DateTime LastRatingRequest { get; set; }

        /// <summary>
        /// gregt
        /// </summary>
        int RatingRequestCount { get; set; }
    }
}