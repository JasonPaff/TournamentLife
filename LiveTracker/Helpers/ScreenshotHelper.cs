using System;
using System.Globalization;
using System.IO;
using System.Windows;
using LiveTracker.Models;
using Tournament_Life.ViewModels;
using Tournament_Life.Views;

namespace LiveTracker.Helpers
{
    public static class ScreenshotHelper
    {
        /// <summary>
        /// Delete a saved screenshot
        /// </summary>
        /// <param name="tournament">tournament screenshot is attached to</param>
        public static void DeleteScreenshot(TournamentRunning tournament, Window owner)
        {
            // null check
            if (tournament is null || owner is null) return;

            // check for file
            if (File.Exists(tournament.ScreenshotFilename) is false) return;

            // delete screenshot
            File.Delete(tournament.ScreenshotFilename);

            // ok view model
            var theVm = new OkViewModel("Screenshot Deleted", "Deleted");

            // create/show ok window
            var theWindow = new OkView(theVm)
            {
                Owner = owner,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            theWindow.ShowDialog();
        }

        /// <summary>
        /// Save a screenshot to the screenshots folder
        /// </summary>
        /// <param name="tournament">tournament screenshot is attached to</param>
        public static void SaveScreenshot(string pictureFilename, TournamentRunning tournament, Window owner)
        {
            // no file
            if (pictureFilename.Length is 0) return;

            // get current profile name
            var profileName = ProfileHelper.GetCurrentProfile();

            // check for and create directory
            if (!Directory.Exists("Screenshots\\" + profileName)) Directory.CreateDirectory("Screenshots\\" + profileName);

            // base screenshot file name
            tournament.ScreenshotFilename = "Screenshots\\" + profileName + "\\";

            // generate new file name
            tournament.ScreenshotFilename += String.Format(new CultureInfo("en-US"), "{0} {1}_{2}_{3} {4}-{5}", tournament.TournamentName, tournament.StartTime.Month, tournament.StartTime.Day,
                                                                                                                    tournament.StartTime.Year, tournament.StartTime.Hour, tournament.StartTime.Minute);
            tournament.ScreenshotFilename += String.Format(new CultureInfo("en-US"), " {0}-{1}-{2}", tournament.FinishPosition, tournament.EntrantsPaid, tournament.Entrants);

            // add correct file extension
            switch (pictureFilename.Substring(pictureFilename.Length - 3, 3))
            {
                case "png":
                    tournament.ScreenshotFilename += ".png";
                    break;
                case "jpg":
                    tournament.ScreenshotFilename += ".jpg";
                    break;
                case "peg":
                    tournament.ScreenshotFilename += ".jpeg";
                    break;
                case "jpe":
                    tournament.ScreenshotFilename += ".jpe";
                    break;
                case "fif":
                    tournament.ScreenshotFilename += ".jfif";
                    break;
                case "bmp":
                    tournament.ScreenshotFilename += ".bmp";
                    break;
            }

            // check for duplicate file and change file name
            var random = new Random();
            while (true)
            {
                if (File.Exists(tournament.ScreenshotFilename))
                {
                    // generate new file name
                    tournament.ScreenshotFilename = "Screenshots\\" + profileName + "\\";
                    tournament.ScreenshotFilename += string.Format(new CultureInfo("en-US"), "{0} {1}_{2}_{3} {4}-{5}", tournament.TournamentName, tournament.StartTime.Month, tournament.StartTime.Day,
                                                                                                                    tournament.StartTime.Year, tournament.StartTime.Hour, tournament.StartTime.Minute);
                    tournament.ScreenshotFilename += string.Format(new CultureInfo("en-US"), " {0}-{1}-{2} {3}", tournament.FinishPosition, tournament.EntrantsPaid, tournament.Entrants, random.Next(100, 1000));

                    // add correct file extension
                    switch (pictureFilename.Substring(pictureFilename.Length - 3, 3))
                    {
                        case "png":
                            tournament.ScreenshotFilename += ".png";
                            break;
                        case "jpg":
                            tournament.ScreenshotFilename += ".jpg";
                            break;
                        case "peg":
                            tournament.ScreenshotFilename += ".jpeg";
                            break;
                        case "jpe":
                            tournament.ScreenshotFilename += ".jpe";
                            break;
                        case "fif":
                            tournament.ScreenshotFilename += ".jfif";
                            break;
                        case "bmp":
                            tournament.ScreenshotFilename+= ".bmp";
                            break;
                    }
                    continue;
                }
                break;
            }

            // copy image file to new location
            File.Copy(pictureFilename, tournament.ScreenshotFilename, true);

            // ok view model
            var theVm = new OkViewModel("Screenshot Saved!", "Saved");

            // create/show ok window
            var theWindow = new OkView(theVm)
            {
                Owner = owner,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            theWindow.ShowDialog();
        }
    }
}
