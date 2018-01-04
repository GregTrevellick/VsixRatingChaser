using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using VsixRatingChaser;
using VsixRatingChaser.Dtos;
using VsixRatingChaser.Interfaces;

namespace DebugConsole
{
    public class Program
    {
        //[STAThread]
        public static void Main(string[] args)
        {
            //var extensionDetailsDto = new ExtensionDetailsDto();
            //extensionDetailsDto.AuthorName = "auth";
            //extensionDetailsDto.ExtensionName = "vsix";
            //extensionDetailsDto.MarketPlaceUrl = "http://www.bbc.co.uk";

            //var debugPackage = new DebugPackage();
            //debugPackage.DoIt2();
            //var chaser = new Chaser();
            //chaser.Chase(debugPackage.HiddenChaserOptions, extensionDetailsDto);
        }
    }

    //[ProvideOptionPage(typeof(HiddenRatingDetailsDto), "Vsix.Name", "Core.Constants.CategorySubLevelGeneral", 0, 0, true)]
    //[Guid("1A6FEFA3-DAFD-4AC1-B947-8D94B07DA75B")]
    //public sealed class DebugPackage : Package
    //{
    //    public IRatingDetailsDto HiddenChaserOptions { get; set; }

    //    //protected override void Initialize()
    //    public void DoIt2()
    //    {
    //        Initialize();
    //        HiddenChaserOptions = (IRatingDetailsDto)GetDialogPage(typeof(HiddenRatingDetailsDto));
    //        HiddenChaserOptions.PreviousRatingRequest = DateTime.Now.AddMonths(-5);
    //        HiddenChaserOptions.RatingRequestCount = 1;
    //    }
    //}

    //public class HiddenRatingDetailsDto : DialogPage, IRatingDetailsDto
    //{
    //    public DateTime PreviousRatingRequest { get; set; }
    //    public int RatingRequestCount { get; set; }
    //}
}
