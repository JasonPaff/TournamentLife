using AutoUpdaterDotNET;
using LiveTracker.Helpers;
using Syncfusion.Windows.Shared;
using System;
using System.IO;
using System.Net;

namespace LiveTracker.ViewModels
{
    public class LiveTrackerViewModel : NotificationObject
    {
        public LiveTrackerViewModel()
        {
            // check for correct folders
            FolderCheck();

            // check for profiles, get current profile
            ProfileCheck();

            // check for updates
            //UpdateCheck();

            // initial preferences
            InitializePreferences();
        }

        public string FontColor { get; set; }
        public int FontSize { get; set; }
        public bool IsDarkMode { get; set; }
        public bool KeepWindowOnTop { get; set; }
        public string SelectedVisibility { get; set; }
        public bool ShowSweepsCoins { get; set; }
        public string Title { get; set; }
        public string Theme { get; set; }

        /// <summary>
        /// checks for the needed folders
        /// </summary>
        private void FolderCheck()
        {
            // programs directory
            var dir = System.AppDomain.CurrentDomain.BaseDirectory;

            //  create preferences folder if it doesn't exist
            if (Directory.Exists(dir + "Preferences") is false)
                Directory.CreateDirectory(dir + "Preferences");

            // create screenshot fold if it doesn't exist
            if (Directory.Exists(dir + "Screenshots") is false)
                Directory.CreateDirectory(dir + "Screenshots");
        }

        /// <summary>
        /// Initialize window preferences
        /// </summary>
        public void InitializePreferences()
        {
            // create default file if one doesn't exist
            _ = new PreferencesHelper(ProfileHelper.GetCurrentProfile());

            // force dark mode only
            PreferencesHelper.UpdatePreference("LiveTracker", "Window", "Theme", "MaterialDark");

            // force font size 12
            PreferencesHelper.UpdatePreference("LiveTracker", "Window", "FontSize", 12);

            // load preferences from file
            KeepWindowOnTop = bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "KeepWindowOnTop"));
            IsDarkMode = bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "IsDarkMode"));
            FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));
            FontColor = PreferencesHelper.FindPreference("LiveTracker", "Window", "FontColor");
            ShowSweepsCoins = bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "ShowSweeps"));
            Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");

            // set initial tournament visibility
            SetSelectedVisibility();

            // update title
            UpdateTitle();
        }

        /// <summary>
        /// Check for a profile, create one if nothing found
        /// </summary>
        public void ProfileCheck()
        {
            // no preferences file, create default
            if(XmlHelper.LoadXmlFile(XmlHelper.PreferencesFolderName + XmlHelper.ProfilesFileName) is null)
                ProfileHelper.CreateDefaultProfile();
        }

        /// <summary>
        /// set the initial visibility status
        /// </summary>
        /// <param name="selection"></param>
        public void SetSelectedVisibility()
        {
            switch (int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Menu", "TournamentVisibility")))
            {
                case 0:
                    SelectedVisibility = "Show All Tournaments";
                    break;
                case 1:
                    SelectedVisibility = "Show Started Tournaments";
                    break;
                case 2:
                    SelectedVisibility = "Show Starting in 5 Minutes";
                    break;
                case 3:
                    SelectedVisibility = "Show Starting in 15 Minutes";
                    break;
                case 4:
                    SelectedVisibility = "Show Starting in 30 Minutes";
                    break;
                case 5:
                    SelectedVisibility = "Show Starting in 60 Minutes";
                    break;
            }
        }

        /// <summary>
        /// Check for updates to the program
        /// </summary>
        public void UpdateCheck()
        {
            // set download path
            AutoUpdater.DownloadPath = Environment.CurrentDirectory;

            // run update as administrator
            AutoUpdater.RunUpdateAsAdmin = true;

            // start updater
            AutoUpdater.Start("", new NetworkCredential("", ""));
        }

        /// <summary>
        /// update the title based on current profile and tournament visibility
        /// </summary>
        /// <param name="visibility">tournament visibility</param>
        public void UpdateTitle()
        {
            Title = "Tournament Life - " + ProfileHelper.GetCurrentProfile();
        }
    }
}