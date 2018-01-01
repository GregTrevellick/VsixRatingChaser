[AppVeyorProjectUrl]: https://ci.appveyor.com/project/GregTrevellick/vsixratingchaser
[AppVeyorProjectBuildStatusBadgeSvg]: https://ci.appveyor.com/api/projects/status/5ism52msmffomkh3?svg=true
[GitHubRepoURL]: https://github.com/GregTrevellick/VsixRatingChaser
[GitHubRepoIssuesURL]: https://github.com/GregTrevellick/VsixRatingChaser/issues
[GitHubRepoPullRequestsURL]: https://github.com/GregTrevellick/VsixRatingChaser/pulls
[NugetUrl]: https://www.nuget.org/packages/OpenInApp.Common/
[VisualStudioURL]: https://www.visualstudio.com/
[CharityWareURL]: https://github.com/GregTrevellick/MiscellaneousArtefacts/wiki/Charity-Ware
[WhyURL]: https://github.com/GregTrevellick/MiscellaneousArtefacts/wiki/Why

# Vsix Rating Chaser 

[![Licence](https://img.shields.io/aur/license/yaourt.svg)](/LICENSE.txt)
[![Build status][AppVeyorProjectBuildStatusBadgeSvg]][AppVeyorProjectUrl]
[![CharityWare](https://img.shields.io/badge/Charity%20Ware-Thank%20You-brightgreen.svg)][CharityWareURL]

![Vsix Rating Chaser](NugetIcon_64x64.png "Vsix Rating Chaser Logo")

A package to gently encourage ratings / reviews for [Visual Studio][VisualStudioURL] extensions.

Available for download at the [nuget gallery][NugetUrl].

## Introduction

Ever noticed how few people ever bother to rate a visual studio extension, even ones that are completely free and fully feature-rich ?

For example the free [Developer Analytics Tools](https://marketplace.visualstudio.com/items?itemName=VisualStudioOnlineApplicationInsights.DeveloperAnalyticsTools) extension from Microsoft has over 140m installs but only 7 reviews, a ridiculous 0.000005%.

[GitHub Extension for Visual Studio](https://marketplace.visualstudio.com/items?itemName=GitHub.GitHubExtensionforVisualStudio) from GitHub Inc., also free, fairs slightly better at 57 reviews for 37m installs - still a pathetic 0.0002%.

Eric Woodruff's invaluable yet free [spell checker](https://marketplace.visualstudio.com/items?itemName=EWoodruff.VisualStudioSpellCheckerVS2017andLater) extension racks up 74k installs but a mere 18 reviews (0.02%).

My own [Jeopardy](https://marketplace.visualstudio.com/items?itemName=GregTrevellick.Jeopardy) free extension with it's measly 38 installs incredibly has 2 ratings (a mammoth 5%).

So I decided to build a package that visual studio extension authors could use to **gently** and **unobtrusively** encourage their users to rate their extensions.

## How It Works

Your visual studio extension will call into this package, and it will **three times** at **quarterly intervals**, present the user with a pop-up window asking for a rating.

It is deliberately low-key, and does not actually track if a rating or review has been made.

It does not (and could not) disable any functionality of your vsix if a rating / review hasn't been made.

The pop-up request explains how your extension was lovingly created for free by you, and how you don't get paid for it. 

I did consider making the text, frequency and quantity configurable, but in the interests of simplicity, and because this package is not aimed at money-making corporations (who, let's face it, can easily create their own equivalent package if they want to) I opted not to, leaving the wording hard-coded for the little guy, as per the screen shot below.

![Rating Request](\src\VsixRatingChaser\RatingRequestScreenshot.png)

## How To Use This Package

1. Ensure your vsix project is .Net 4.7 or above

1. Add a reference to assembly [Microsoft.VisualStudio.Shell.15.0](https://www.nuget.org/packages/Microsoft.VisualStudio.Shell.15.0) to your vsix project

1. Install [this package][NugetUrl] package to your vsix project

1. Add a class to your vsix application that inherits from DialogPage and implements IRatingDetailsDto (defined in this package). This class is used for storing data pertinent to this package as part of your extension's options, in a manner that makes the data invisible to users accessing the Tools | Options menu. *Since this class forms part of the options for your vsix it is recommended thar you locate the class in your vsix's 'options' folder if you have one*

1. Add a class to your vsix project which retrieves the user-hidden options, instantiates the VsixRatingChaser class, calls the class's 'Chase' method, passing appropriate parameter values. *It is recommended that, for moral and performance reasons, VsixRatingChaser is only invoked when your vsix functionality is invoked, not simply when your vsix package is cited / initialized*

That's it. 

The package takes care of persisting the date that the next pop-up is due, and monitors how many pop-ups the user has already been presented with, so that they don't get prompted to rate / review indefinitely.

The response from the call can optionally be interrogated, but essentially explains:
 - If the pop-up was not displayed or not 
 - If the pop-up was displayed then also indicates if the user clicked the rating Url hyperlink
 - The reason the pop-up could not be displayed if any data was invalid (e.g. the caller did not supply a name for the extension)

For example:

    using Microsoft.VisualStudio.Shell;
    using System;
    using VsixRatingChaser;
    using VsixRatingChaser.Dtos;
    using VsixRatingChaser.Enums;
    using VsixRatingChaser.Interfaces;
    
    namespace MyVsix
    {
        public class MyVsixRatingDetailsDto : DialogPage, IRatingDetailsDto
        {
            public DateTime PreviousRatingRequest { get; set; }
            public int RatingRequestCount { get; set; }
        }
    
        public class SampleCaller
        {
            public static ChaseOutcome MyVsixSampleCall()
            {
                var ratingDetailsDto = (IRatingDetailsDto)Microsoft.VisualStudio.Shell.GetDialogPage(typeof(MyVsixRatingDetailsDto));
    
                var extensionDetailsDto = new ExtensionDetailsDto
                {
                    AuthorName = "Greg Trevellick",
                    ExtensionName = "Open in Paint.NET",
                    MarketPlaceUrl = "https://marketplace.visualstudio.com/items?itemName=GregTrevellick.OpeninPaintNET"
                };
    
                var chaser = new Chaser();
    
                return chaser.Chase(ratingDetailsDto, extensionDetailsDto);
            }
        }
    }

There are plenty of other example implementations in my various Visual Studio extension [GitHub repos](https://github.com/GregTrevellick).

## Debugging

To actually see this package in action with your vsix do the following:

1. Advance your system clock four months and one day

1. Run your vsix locally in debug mode in the experimental instance

1. Trigger your vsix to call this package (i.e. invoking your vsix functionality within the experimental instance)

1. At this point your experimental instance should display a pop-up asking for a rating - the title should indicate that this is the first of three requests

1. Restart debugging your vsix - this time there is no rating request pop-up

1. Advance your system clock an additional four months

1. Restart debugging your vsix - this time a rating request pop-up appears (second of three)

1. Restart debugging your vsix - this time there is no rating request pop-up

1. Advance your system clock an additional four months (i.e. a year altogether)

1. Restart debugging your vsix - this time a rating request pop-up appears, indicating it is the third of three

1. Restart debugging your vsix - this time (and beyond) there is no rating request pop-up

1. Set your system clock back to the current date

## Debug Gotchas

 - Visual Studio uses your system date to periodically prompt you to re-sign into the IDE. Consequently, having adjusted the date above the IDE may ask you to sign in again, which you probably weren't expecting. 

 - Jetbrains Resharper also uses the system date in the same way, so may tell you your license has expired.

### License

Software License is available [here](/LICENSE.txt).

### Miscellaneous

Please consider this a [charity ware][CharityWareURL] package.

Bugs can be logged [here][GitHubRepoIssuesURL].

See the [change log](CHANGELOG.md) for release history.

[Why build this package?][WhyURL]