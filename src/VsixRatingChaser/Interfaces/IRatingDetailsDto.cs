using System;
using Microsoft.VisualStudio.Shell;

namespace VsixRatingChaser.Interfaces
{
    /// <summary>
    /// A DTO containing parameters supplied to the package by the caller
    /// </summary>
    public interface IRatingDetailsDto : IProfileManager
    {
        /// <summary>
        /// The date/time the previous rating request took place
        /// </summary>
        DateTime PreviousRatingRequest { get; set; }

        /// <summary>
        /// The total number of times a request for ratings has taken place
        /// </summary>
        int RatingRequestCount { get; set; }
    }
}