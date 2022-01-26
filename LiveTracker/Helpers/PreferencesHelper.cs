using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Xml;
using LiveTracker.Enums;
using Syncfusion.Windows.Shared;

namespace LiveTracker.Helpers
{
    public class PreferencesHelper
    {
        private readonly string _filename;

        private const bool DefaultLiveTrackerAddonCountColumnVisibility = true;
        private const bool DefaultLiveTrackerAddonTotalCostColumnVisibility = false;
        private const bool DefaultLiveTrackerBlindLevelsColumnVisibility = true;
        private const bool DefaultLiveTrackerBountyColumnVisibility = false;
        private const bool DefaultLiveTrackerBuyinTotalCostColumnVisibility = false;
        private const bool DefaultLiveTrackerEntrantsColumnVisibility = true;
        private const bool DefaultLiveTrackerEntrantsPaidColumnVisibility = true;
        private const bool DefaultLiveTrackerFinishPositionColumnVisibility = true;
        private const bool DefaultLiveTrackerJackpotSpinMultiplierColumnVisibility = false;
        private const bool DefaultLiveTrackerPrizeWonColumnVisibility = true;
        private const bool DefaultLiveTrackerRebuyCountColumnVisibility = true;
        private const bool DefaultLiveTrackerRebuyTotalCostColumnVisibility = false;
        private const bool DefaultLiveTrackerStartTimeColumnVisibility = true;
        private const bool DefaultLiveTrackerTotalCostColumnVisibility = true;
        private const bool DefaultLiveTrackerVenueColumnVisibility = false;

        private const double DefaultLiveTrackerAddonCountColumnWidth = 57;
        private const double DefaultLiveTrackerAddonTotalCostColumnWidth = 85;
        private const double DefaultLiveTrackerBountyCountColumnWidth = 67;
        private const double DefaultLiveTrackerBuyinTotalCostColumnWidth = 87;
        private const double DefaultLiveTrackerEntrantsColumnWidth = 62;
        private const double DefaultLiveTrackerEntrantsPaidColumnWidth = 48;
        private const double DefaultLiveTrackerFinishPositionColumnWidth = 50;
        private const double DefaultLiveTrackerJackpotSpinMultiplierColumnWidth = 64;
        private const double DefaultLiveTrackerPrizeWonColumnWidth = 78;
        private const double DefaultLiveTrackerRebuyCountColumnWidth = 64;
        private const double DefaultLiveTrackerRebuyTotalCostColumnWidth = 85;
        private const double DefaultLiveTrackerStartTimeColumnWidth = 77;
        private const double DefaultLiveTrackerTotalCostColumnWidth = 80;
        private const double DefaultLiveTrackerTournamentNameColumnWidth = 181;
        private const double DefaultLiveTrackerVenueColumnWidth = 80;

        private const int DefaultLiveTrackerAddonCountColumnOrder = 8;
        private const int DefaultLiveTrackerAddonTotalCostColumnOrder = 5;
        private const int DefaultLiveTrackerBountyCountColumnOrder = 9;
        private const int DefaultLiveTrackerBuyinTotalCostColumnOrder = 3;
        private const int DefaultLiveTrackerEntrantsColumnOrder = 11;
        private const int DefaultLiveTrackerEntrantsPaidColumnOrder = 12;
        private const int DefaultLiveTrackerFinishPositionColumnOrder = 13;
        private const int DefaultLiveTrackerJackpotSpinMultiplierColumnOrder = 10;
        private const int DefaultLiveTrackerPrizeWonColumnOrder = 14;
        private const int DefaultLiveTrackerRebuyCountColumnOrder = 7;
        private const int DefaultLiveTrackerRebuyTotalCostColumnOrder = 4;
        private const int DefaultLiveTrackerStartTimeColumnOrder = 2;
        private const int DefaultLiveTrackerTotalCostColumnOrder = 6;
        private const int DefaultLiveTrackerTournamentNameColumnOrder = 0;
        private const int DefaultLiveTrackerVenueColumnOrder = 1;

        private const double DefaultTemplateManagerAddonTotalCostColumnWidth = 85;
        private const double DefaultTemplateManagerAddonRakeCostColumnWidth = 87;
        private const double DefaultTemplateManagerAddonBaseCostColumnWidth = 87;
        private const double DefaultTemplateManagerBlindLevelsColumnWidth = 67;
        private const double DefaultTemplateManagerBovadaColumnWidth = 67;
        private const double DefaultTemplateManagerBovadaBountyColumnWidth = 67;
        private const double DefaultTemplateManagerBuyinBaseCostColumnWidth = 87;
        private const double DefaultTemplateManagerBuyinRakeCostColumnWidth = 87;
        private const double DefaultTemplateManagerBuyinTotalCostColumnWidth = 87;
        private const double DefaultTemplateManagerEntrantsColumnWidth = 62;
        private const double DefaultTemplateManagerEntrantsPaidColumnWidth = 48;
        private const double DefaultTemplateManagerFormatsColumnWidth = 50;
        private const double DefaultTemplateManagerGameTypeColumnWidth = 50;
        private const double DefaultTemplateManagerGuaranteeColumnWidth = 50;
        private const double DefaultTemplateManagerLateRegColumnWidth = 67;
        private const double DefaultTemplateManagerRebuyTotalCostColumnWidth = 85;
        private const double DefaultTemplateManagerRebuyRakeCostColumnWidth = 87;
        private const double DefaultTemplateManagerRebuyBaseCostColumnWidth = 87;
        private const double DefaultTemplateManagerSngColumnWidth = 67;
        private const double DefaultTemplateManagerStackSizeStartingColumnWidth = 77;
        private const double DefaultTemplateManagerStackSizeRebuyColumnWidth = 77;
        private const double DefaultTemplateManagerStackSizeAddonColumnWidth = 77;
        private const double DefaultTemplateManagerStartTimeColumnWidth = 77;
        private const double DefaultTemplateManagerTableSizeColumnWidth = 80;
        private const double DefaultTemplateManagerTournamentNameColumnWidth = 131;
        private const double DefaultTemplateManagerVenueColumnWidth = 80;

        private const int DefaultTemplateManagerAddonTotalCostColumnOrder = 12;
        private const int DefaultTemplateManagerAddonRakeCostColumnOrder = 14;
        private const int DefaultTemplateManagerAddonBaseCostColumnOrder = 13;
        private const int DefaultTemplateManagerBlindLevelsColumnOrder = 20;
        private const int DefaultTemplateManagerBovadaColumnOrder = 25;
        private const int DefaultTemplateManagerBovadaBountyColumnOrder = 15;
        private const int DefaultTemplateManagerBuyinBaseCostColumnOrder = 7;
        private const int DefaultTemplateManagerBuyinRakeCostColumnOrder = 8;
        private const int DefaultTemplateManagerBuyinTotalCostColumnOrder = 6;
        private const int DefaultTemplateManagerEntrantsColumnOrder = 22;
        private const int DefaultTemplateManagerEntrantsPaidColumnOrder = 23;
        private const int DefaultTemplateManagerFormatsColumnOrder = 3;
        private const int DefaultTemplateManagerGameTypeColumnOrder = 1;
        private const int DefaultTemplateManagerGuaranteeColumnOrder = 5;
        private const int DefaultTemplateManagerLateRegColumnOrder = 21;
        private const int DefaultTemplateManagerRebuyTotalCostColumnOrder = 9;
        private const int DefaultTemplateManagerRebuyRakeCostColumnOrder = 11;
        private const int DefaultTemplateManagerRebuyBaseCostColumnOrder = 10;
        private const int DefaultTemplateManagerSngColumnOrder = 24;
        private const int DefaultTemplateManagerStackSizeStartingColumnOrder = 16;
        private const int DefaultTemplateManagerStackSizeRebuyColumnOrder = 17;
        private const int DefaultTemplateManagerStackSizeAddonColumnOrder = 18;
        private const int DefaultTemplateManagerStartTimeColumnOrder = 4;
        private const int DefaultTemplateManagerTableSizeColumnOrder = 19;
        private const int DefaultTemplateManagerTournamentNameColumnOrder = 2;
        private const int DefaultTemplateManagerVenueColumnOrder = 0;

        private const int DefaultTournamentsViewAddonTotalCostColumnOrder = 17;
        private const int DefaultTournamentsViewAddonRakeCostColumnOrder = 16;
        private const int DefaultTournamentsViewAddonCountColumnOrder = 22;
        private const int DefaultTournamentsViewAddonBaseCostColumnOrder = 15;
        private const int DefaultTournamentsViewBlindLevelsColumnOrder = 28;
        private const int DefaultTournamentsViewBountyCountColumnOrder = 27;
        private const int DefaultTournamentsViewBovadaColumnOrder = 18;
        private const int DefaultTournamentsViewBovadaBountyColumnOrder = 36;
        private const int DefaultTournamentsViewBuyinBaseCostColumnOrder = 11;
        private const int DefaultTournamentsViewBuyinRakeCostColumnOrder = 12;
        private const int DefaultTournamentsViewBuyinTotalCostColumnOrder = 10;
        private const int DefaultTournamentsViewEndDateColumnOrder = 6;
        private const int DefaultTournamentsViewEndTimeColumnOrder = 7;
        private const int DefaultTournamentsViewEntrantsColumnOrder = 30;
        private const int DefaultTournamentsViewEntrantsPaidColumnOrder = 31;
        private const int DefaultTournamentsViewFinishPositionColumnOrder = 32;
        private const int DefaultTournamentsViewFormatsColumnOrder = 3;
        private const int DefaultTournamentsViewGameTypeColumnOrder = 1;
        private const int DefaultTournamentsViewGuaranteeColumnOrder = 9;
        private const int DefaultTournamentsViewJackpotSpinMultiplierColumnOrder = 23;
        private const int DefaultTournamentsViewLateRegColumnOrder = 29;
        private const int DefaultTournamentsViewLengthColumnOrder = 8;
        private const int DefaultTournamentsViewNoteColumnOrder = 38;
        private const int DefaultTournamentsViewPrizeWonColumnOrder = 33;
        private const int DefaultTournamentsViewProfitColumnOrder = 34;
        private const int DefaultTournamentsViewRebuyTotalCostColumnOrder = 21;
        private const int DefaultTournamentsViewRebuyRakeCostColumnOrder = 30;
        private const int DefaultTournamentsViewRebuyBaseCostColumnOrder = 14;
        private const int DefaultTournamentsViewRebuyCountColumnOrder = 13;
        private const int DefaultTournamentsViewScreenshotColumnOrder = 37;
        private const int DefaultTournamentsViewSngColumnOrder = 35;
        private const int DefaultTournamentsViewStackSizeStartingColumnOrder = 26;
        private const int DefaultTournamentsViewStackSizeRebuyColumnOrder = 25;
        private const int DefaultTournamentsViewStackSizeAddonColumnOrder = 24;
        private const int DefaultTournamentsViewStartTimeColumnOrder = 4;
        private const int DefaultTournamentsViewStartDateColumnOrder = 5;
        private const int DefaultTournamentsViewTableSizeColumnOrder = 27;
        private const int DefaultTournamentsViewTotalCostColumnOrder = 20;
        private const int DefaultTournamentsViewTournamentNameColumnOrder = 2;
        private const int DefaultTournamentsViewVenueColumnOrder = 0;

        private const int DefaultLiveTrackerFontSize = 12;

        private const int DefaultLiveTrackerRecentlyFinishedTournamentsMax = 10;
        private const string DefaultLiveTrackerTheme = "MaterialDark";
        private const bool DefaultLiveTrackerShowRowHeaders = true;
        private const bool DefaultLiveTrackerIsDarkMode = true;
        private const bool DefaultLiveTrackerShortColumnHeaders = false;
        private const bool DefaultLiveTrackerSingleRowMode = false;

        private const TournamentVisibility DefaultLiveTrackerTournamentVisibility = TournamentVisibility.ShowAll;
        private const AnimationTypes DefaultLiveTrackerMenuAnimationType = AnimationTypes.Slide;
        private const int DefaultLiveTrackerWindowHeight = 50;
        private const int DefaultLiveTrackerWindowWidth = 50;
        private const bool DefaultLiveTrackerKeepWindowOnTop = false;

        private const string DefaultLiveTrackerSelectTournamentsDefaultVenue = "All Venues";
        private const bool DefaultLiveTrackerSelectTournamentsCloseOnStart = true;
        private const bool DefaultLiveTrackerSelectTournamentsConfirmStart = true;
        private const bool DefaultLiveTrackerSelectTournamentsMultiToggle = true;
        private const string DefaultLiveTrackerFontColor = "White";

        private const bool CloseQuickStartAfterStartOption = false;
        private const bool CloseCreateCopyAfterStartOption = false;
        private const bool SaveNewTournamentOption = true;
        private const bool SaveNewTournamentCreateCopyOption = false;
        private const bool ShowStartTournamentOption = true;
        private const bool ShowStartTournamentCreateCopyOption = true;

        private const int DefaultMaxShowInView = 100;

        private const int DefaultStoneBubble = 1;
        private const int DefaultNearBubble = 5;
        private const int DefaultMinCash = 5;

        private const bool DefaultLiveTrackerSessionStartConfirmAlpha = false;
        private const bool DefaultLiveTrackerSessionStartConfirmStart = false;
        private const bool DefaultLiveTrackerSessionStartMultiToggle = false;
        private const bool DefaultLiveTrackerSessionStartRemoveToggle = true;

        private const bool DefaultLiveTrackerSortCancelTournamentsByStartTime = false;

        public PreferencesHelper(string profile)
        {
            _filename = XmlHelper.PreferencesFolderName + profile + XmlHelper.PreferencesFileName;

            // no file then create one
            if(!File.Exists(_filename)) CreateDefaultPreferencesFile();

            PreferencesXmlDocument = XmlHelper.LoadXmlFile(_filename);
        }

        public XmlDocument PreferencesXmlDocument { get; set; }

        /// <summary>
        /// create preferences the user might not have
        /// </summary>
        /// <param name="pref"></param>
        private static void CheckForPreference(string window, string category, string pref)
        {
            // get xml file
            var doc = XmlHelper.LoadXmlFile(XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.PreferencesFileName);

            // no file, leave
            if (doc is null) return;

            // check for preferences
            if (pref == "ShowSweeps")
            {
                // try to find preference
                var testPref = XmlHelper.LoadXmlFile(XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.PreferencesFileName).DocumentElement?.SelectSingleNode(window)?.SelectSingleNode(category)?.SelectSingleNode("ShowSweeps")?.InnerText;

                // found it, leave
                if (testPref is not null) return;

                // create show sweeps preference
                XmlNode node = doc.CreateElement("ShowSweeps");
                node.InnerText = "False";

                // add to xml file
                doc.DocumentElement?.SelectSingleNode(window)?.SelectSingleNode(category)?.AppendChild(node);

                // save xml file
                doc.Save(XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.PreferencesFileName);
            }

            if (pref == "SelectTournamentsCloseOnStart")
            {
                // try to find preference
                var testPref = XmlHelper.LoadXmlFile(XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.PreferencesFileName).DocumentElement?.SelectSingleNode(window)?.SelectSingleNode(category)?.SelectSingleNode("SelectTournamentsCloseOnStart")?.InnerText;

                // found it, leave
                if (testPref is not null) return;

                // create show sweeps preference
                XmlNode node = doc.CreateElement("SelectTournamentsCloseOnStart");
                node.InnerText = "True";

                // add to xml file
                doc.DocumentElement?.SelectSingleNode(window)?.SelectSingleNode(category)?.AppendChild(node);

                // save xml file
                doc.Save(XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.PreferencesFileName);
            }

            if (pref == "SelectTournamentsConfirmStart")
            {
                // try to find preference
                var testPref = XmlHelper.LoadXmlFile(XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.PreferencesFileName).DocumentElement?.SelectSingleNode(window)?.SelectSingleNode(category)?.SelectSingleNode("SelectTournamentsConfirmStart")?.InnerText;

                // found it, leave
                if (testPref is not null) return;

                // create show sweeps preference
                XmlNode node = doc.CreateElement("SelectTournamentsConfirmStart");
                node.InnerText = "True";

                // add to xml file
                doc.DocumentElement?.SelectSingleNode(window)?.SelectSingleNode(category)?.AppendChild(node);

                // save xml file
                doc.Save(XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.PreferencesFileName);
            }

            if (pref == "SelectTournamentsMultiToggle")
            {
                // try to find preference
                var testPref = XmlHelper.LoadXmlFile(XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.PreferencesFileName).DocumentElement?.SelectSingleNode(window)?.SelectSingleNode(category)?.SelectSingleNode("SelectTournamentsMultiToggle")?.InnerText;

                // found it, leave
                if (testPref is not null) return;

                // create show sweeps preference
                XmlNode node = doc.CreateElement("SelectTournamentsMultiToggle");
                node.InnerText = "True";

                // add to xml file
                doc.DocumentElement?.SelectSingleNode(window)?.SelectSingleNode(category)?.AppendChild(node);

                // save xml file
                doc.Save(XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.PreferencesFileName);
            }

            // check for preferences
            if (pref == "CloseQuickStartAfterStartOption")
            {
                // try to find preference
                var testPref = XmlHelper.LoadXmlFile(XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.PreferencesFileName).DocumentElement?.SelectSingleNode(window)?.SelectSingleNode(category)?.SelectSingleNode("CloseQuickStartAfterStartOption")?.InnerText;

                // found it, leave
                if (testPref is not null) return;

                // create show sweeps preference
                XmlNode node = doc.CreateElement("CloseQuickStartAfterStartOption");
                node.InnerText = "False";

                // add to xml file
                doc.DocumentElement?.SelectSingleNode(window)?.SelectSingleNode(category)?.AppendChild(node);

                // save xml file
                doc.Save(XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.PreferencesFileName);
            }

            // check for preferences
            if (pref == "SaveNewTournamentOption")
            {
                // try to find preference
                var testPref = XmlHelper.LoadXmlFile(XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.PreferencesFileName).DocumentElement?.SelectSingleNode(window)?.SelectSingleNode(category)?.SelectSingleNode("SaveNewTournamentOption")?.InnerText;

                // found it, leave
                if (testPref is not null) return;

                // create show sweeps preference
                XmlNode node = doc.CreateElement("SaveNewTournamentOption");
                node.InnerText = "True";

                // add to xml file
                doc.DocumentElement?.SelectSingleNode(window)?.SelectSingleNode(category)?.AppendChild(node);

                // save xml file
                doc.Save(XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.PreferencesFileName);
            }

            // check for preferences
            if (pref == "CloseCreateCopyAfterStartOption")
            {
                // try to find preference
                var testPref = XmlHelper.LoadXmlFile(XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.PreferencesFileName).DocumentElement?.SelectSingleNode(window)?.SelectSingleNode(category)?.SelectSingleNode("CloseCreateCopyAfterStartOption")?.InnerText;

                // found it, leave
                if (testPref is not null) return;

                // create show sweeps preference
                XmlNode node = doc.CreateElement("CloseCreateCopyAfterStartOption");
                node.InnerText = "False";

                // add to xml file
                doc.DocumentElement?.SelectSingleNode(window)?.SelectSingleNode(category)?.AppendChild(node);

                // save xml file
                doc.Save(XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.PreferencesFileName);
            }

            // check for preferences
            if (pref == "SaveNewTournamentCreateCopyOption")
            {
                // try to find preference
                var testPref = XmlHelper.LoadXmlFile(XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.PreferencesFileName).DocumentElement?.SelectSingleNode(window)?.SelectSingleNode(category)?.SelectSingleNode("SaveNewTournamentCreateCopyOption")?.InnerText;

                // found it, leave
                if (testPref is not null) return;

                // create show sweeps preference
                XmlNode node = doc.CreateElement("SaveNewTournamentCreateCopyOption");
                node.InnerText = "False";

                // add to xml file
                doc.DocumentElement?.SelectSingleNode(window)?.SelectSingleNode(category)?.AppendChild(node);

                // save xml file
                doc.Save(XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.PreferencesFileName);
            }

            // check for preferences
            if (pref == "ShowStartTournamentOption")
            {
                // try to find preference
                var testPref = XmlHelper.LoadXmlFile(XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.PreferencesFileName).DocumentElement?.SelectSingleNode(window)?.SelectSingleNode(category)?.SelectSingleNode("ShowStartTournamentOption")?.InnerText;

                // found it, leave
                if (testPref is not null) return;

                // create show sweeps preference
                XmlNode node = doc.CreateElement("ShowStartTournamentOption");
                node.InnerText = "True";

                // add to xml file
                doc.DocumentElement?.SelectSingleNode(window)?.SelectSingleNode(category)?.AppendChild(node);

                // save xml file
                doc.Save(XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.PreferencesFileName);
            }

            // check for preferences
            if (pref == "ShowStartTournamentCreateCopyOption")
            {
                // try to find preference
                var testPref = XmlHelper.LoadXmlFile(XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.PreferencesFileName).DocumentElement?.SelectSingleNode(window)?.SelectSingleNode(category)?.SelectSingleNode("ShowStartTournamentCreateCopyOption")?.InnerText;

                // found it, leave
                if (testPref is not null) return;

                // create show sweeps preference
                XmlNode node = doc.CreateElement("ShowStartTournamentCreateCopyOption");
                node.InnerText = "True";

                // add to xml file
                doc.DocumentElement?.SelectSingleNode(window)?.SelectSingleNode(category)?.AppendChild(node);

                // save xml file
                doc.Save(XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.PreferencesFileName);
            }

            if (pref == "SessionStartConfirmStart")
            {
                // try to find preference
                var testPref = XmlHelper.LoadXmlFile(XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.PreferencesFileName).DocumentElement?.SelectSingleNode(window)?.SelectSingleNode(category)?.SelectSingleNode("SessionStartConfirmStart")?.InnerText;

                // found it, leave
                if (testPref is not null) return;

                // create show sweeps preference
                XmlNode node = doc.CreateElement("SessionStartConfirmStart");
                node.InnerText = "False";

                // add to xml file
                doc.DocumentElement?.SelectSingleNode(window)?.SelectSingleNode(category)?.AppendChild(node);

                // save xml file
                doc.Save(XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.PreferencesFileName);
            }

            if (pref == "SessionStartMultiToggle")
            {
                // try to find preference
                var testPref = XmlHelper.LoadXmlFile(XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.PreferencesFileName).DocumentElement?.SelectSingleNode(window)?.SelectSingleNode(category)?.SelectSingleNode("SessionStartMultiToggle")?.InnerText;

                // found it, leave
                if (testPref is not null) return;

                // create show sweeps preference
                XmlNode node = doc.CreateElement("SessionStartMultiToggle");
                node.InnerText = "False";

                // add to xml file
                doc.DocumentElement?.SelectSingleNode(window)?.SelectSingleNode(category)?.AppendChild(node);

                // save xml file
                doc.Save(XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.PreferencesFileName);
            }

            if (pref == "SessionStartConfirmAlpha")
            {
                // try to find preference
                var testPref = XmlHelper.LoadXmlFile(XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.PreferencesFileName).DocumentElement?.SelectSingleNode(window)?.SelectSingleNode(category)?.SelectSingleNode("SessionStartConfirmAlpha")?.InnerText;

                // found it, leave
                if (testPref is not null) return;

                // create show sweeps preference
                XmlNode node = doc.CreateElement("SessionStartConfirmAlpha");
                node.InnerText = "False";

                // add to xml file
                doc.DocumentElement?.SelectSingleNode(window)?.SelectSingleNode(category)?.AppendChild(node);

                // save xml file
                doc.Save(XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.PreferencesFileName);
            }

            if (pref == "SessionStartRemoveToggle")
            {
                // try to find preference
                var testPref = XmlHelper.LoadXmlFile(XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.PreferencesFileName).DocumentElement?.SelectSingleNode(window)?.SelectSingleNode(category)?.SelectSingleNode("SessionStartRemoveToggle")?.InnerText;

                // found it, leave
                if (testPref is not null) return;

                // create show sweeps preference
                XmlNode node = doc.CreateElement("SessionStartRemoveToggle");
                node.InnerText = "True";

                // add to xml file
                doc.DocumentElement?.SelectSingleNode(window)?.SelectSingleNode(category)?.AppendChild(node);

                // save xml file
                doc.Save(XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.PreferencesFileName);
            }

            if (pref == "SortCancelTournamentsByStartTime")
            {
                // try to find preference
                var testPref = XmlHelper.LoadXmlFile(XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.PreferencesFileName).DocumentElement?.SelectSingleNode(window)?.SelectSingleNode(category)?.SelectSingleNode("SortCancelTournamentsByStartTime")?.InnerText;

                // found it, leave
                if (testPref is not null) return;

                // create show sweeps preference
                XmlNode node = doc.CreateElement("SortCancelTournamentsByStartTime");
                node.InnerText = "False";

                // add to xml file
                doc.DocumentElement?.SelectSingleNode(window)?.SelectSingleNode(category)?.AppendChild(node);

                // save xml file
                doc.Save(XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.PreferencesFileName);
            }
        }

        /// <summary>
        /// Create a default preferences file
        /// </summary>
        private void CreateDefaultPreferencesFile()
        {
            // new xml document
            var preferencesFile = new XmlDocument();

            // create root node
            XmlNode preferencesRootNode = preferencesFile.CreateElement("Preferences");

            // add preferences node to xml document
            preferencesFile.AppendChild(preferencesRootNode);

            // save preferences file
            preferencesFile.Save(_filename);

            // load preferences file into property
            PreferencesXmlDocument = XmlHelper.LoadXmlFile(_filename);

            // create default preferences
            DefaultLiveTrackerNodes();
        }

        /// <summary>
        /// Default live tracker preferences
        /// </summary>
        private void DefaultLiveTrackerNodes()
        {
            // create live tracker window node
            XmlNode liveTrackerWindow = PreferencesXmlDocument.CreateElement("LiveTracker");

            // create/add live tracker top window node
            XmlNode liveTrackerCategory = PreferencesXmlDocument.CreateElement("Window");

            XmlNode liveTrackerPreference = PreferencesXmlDocument.CreateElement("KeepWindowOnTop");
            liveTrackerPreference.InnerText = DefaultLiveTrackerKeepWindowOnTop.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            // create/add live tracker show sweeps coin symbol
            liveTrackerPreference = PreferencesXmlDocument.CreateElement("ShowSweeps");
            liveTrackerPreference.InnerText = "False";
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            // create/add live tracker window height
            liveTrackerPreference = PreferencesXmlDocument.CreateElement("WindowHeight");
            liveTrackerPreference.InnerText = DefaultLiveTrackerWindowHeight.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            // create/add live tracker window width
            liveTrackerPreference = PreferencesXmlDocument.CreateElement("WindowWidth");
            liveTrackerPreference.InnerText = DefaultLiveTrackerWindowWidth.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            // create/add live tracker is dark mode
            liveTrackerPreference = PreferencesXmlDocument.CreateElement("IsDarkMode");
            liveTrackerPreference.InnerText = DefaultLiveTrackerIsDarkMode.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            // create/add live tracker close quick start after start option
            liveTrackerPreference = PreferencesXmlDocument.CreateElement("CloseQuickStartAfterStartOption");
            liveTrackerPreference.InnerText = CloseQuickStartAfterStartOption.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            // create/add live tracker save new tournament option
            liveTrackerPreference = PreferencesXmlDocument.CreateElement("SaveNewTournamentOption");
            liveTrackerPreference.InnerText = SaveNewTournamentOption.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            // create/add live tracker close quick start after start option
            liveTrackerPreference = PreferencesXmlDocument.CreateElement("CloseCreateCopyAfterStartOption");
            liveTrackerPreference.InnerText = CloseCreateCopyAfterStartOption.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            // create/add live tracker save new tournament option
            liveTrackerPreference = PreferencesXmlDocument.CreateElement("SaveNewTournamentCreateCopyOption");
            liveTrackerPreference.InnerText = SaveNewTournamentCreateCopyOption.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            // create/add live tracker save new tournament option
            liveTrackerPreference = PreferencesXmlDocument.CreateElement("ShowStartTournamentOption");
            liveTrackerPreference.InnerText = ShowStartTournamentOption.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            // create/add live tracker save new tournament option
            liveTrackerPreference = PreferencesXmlDocument.CreateElement("ShowStartTournamentCreateCopyOption");
            liveTrackerPreference.InnerText = ShowStartTournamentCreateCopyOption.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            // create/add live tracker save new tournament option
            liveTrackerPreference = PreferencesXmlDocument.CreateElement("SortCancelTournamentsByStartTime");
            liveTrackerPreference.InnerText = DefaultLiveTrackerSortCancelTournamentsByStartTime.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            // create/add session start confirm start
            liveTrackerPreference = PreferencesXmlDocument.CreateElement("SessionStartConfirmStart");
            liveTrackerPreference.InnerText = (DefaultLiveTrackerSessionStartConfirmStart).ToString();
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            // create/add session start confirm alpha
            liveTrackerPreference = PreferencesXmlDocument.CreateElement("SessionStartConfirmAlpha");
            liveTrackerPreference.InnerText = (DefaultLiveTrackerSessionStartConfirmAlpha).ToString();
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            // create/add session start multi toggle
            liveTrackerPreference = PreferencesXmlDocument.CreateElement("SessionStartMultiToggle");
            liveTrackerPreference.InnerText = (DefaultLiveTrackerSessionStartMultiToggle).ToString();
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            // create/add session start remove toggle
            liveTrackerPreference = PreferencesXmlDocument.CreateElement("SessionStartRemoveToggle");
            liveTrackerPreference.InnerText = (DefaultLiveTrackerSessionStartRemoveToggle).ToString();
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            // create/add live tracker theme
            liveTrackerPreference = PreferencesXmlDocument.CreateElement("Theme");
            liveTrackerPreference.InnerText = DefaultLiveTrackerTheme.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            // create/add live tracker font size
            liveTrackerPreference = PreferencesXmlDocument.CreateElement("FontSize");
            liveTrackerPreference.InnerText = DefaultLiveTrackerFontSize.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            // create/add live tracker font color
            liveTrackerPreference = PreferencesXmlDocument.CreateElement("FontColor");
            liveTrackerPreference.InnerText = DefaultLiveTrackerFontColor.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            // create/add live tracker show data grid
            liveTrackerPreference = PreferencesXmlDocument.CreateElement("SingleRowMode");
            liveTrackerPreference.InnerText = DefaultLiveTrackerFontColor.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            // create/add stone bubble
            liveTrackerPreference = PreferencesXmlDocument.CreateElement("StoneBubble");
            liveTrackerPreference.InnerText = DefaultStoneBubble.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            // create/add stone bubble
            liveTrackerPreference = PreferencesXmlDocument.CreateElement("NearBubble");
            liveTrackerPreference.InnerText = DefaultNearBubble.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            // create/add stone bubble
            liveTrackerPreference = PreferencesXmlDocument.CreateElement("MinCash");
            liveTrackerPreference.InnerText = DefaultMinCash.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            // add window category to window
            liveTrackerWindow.AppendChild(liveTrackerCategory);

            // create/add live tracker tournament visibility
            liveTrackerCategory = PreferencesXmlDocument.CreateElement("Menu");

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("TournamentVisibility");
            liveTrackerPreference.InnerText = ((int)DefaultLiveTrackerTournamentVisibility).ToString();
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            // create/add live tracker menu animation type
            liveTrackerPreference = PreferencesXmlDocument.CreateElement("MenuAnimationType");
            liveTrackerPreference.InnerText = ((int)DefaultLiveTrackerMenuAnimationType).ToString();
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            // create/add recently finished tournaments max
            liveTrackerPreference = PreferencesXmlDocument.CreateElement("RecentlyFinishedTournamentsMax");
            liveTrackerPreference.InnerText = (DefaultLiveTrackerRecentlyFinishedTournamentsMax).ToString();
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            // create/add select tournaments default venue
            liveTrackerPreference = PreferencesXmlDocument.CreateElement("SelectTournamentsDefaultVenue");
            liveTrackerPreference.InnerText = (DefaultLiveTrackerSelectTournamentsDefaultVenue);
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            // create/add select tournaments close on start
            liveTrackerPreference = PreferencesXmlDocument.CreateElement("SelectTournamentsCloseOnStart");
            liveTrackerPreference.InnerText = (DefaultLiveTrackerSelectTournamentsCloseOnStart).ToString();
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            // create/add select tournaments confirm start
            liveTrackerPreference = PreferencesXmlDocument.CreateElement("SelectTournamentsConfirmStart");
            liveTrackerPreference.InnerText = (DefaultLiveTrackerSelectTournamentsConfirmStart).ToString();
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            // create/add select tournaments multi toggle
            liveTrackerPreference = PreferencesXmlDocument.CreateElement("SelectTournamentsMultiToggle");
            liveTrackerPreference.InnerText = (DefaultLiveTrackerSelectTournamentsMultiToggle).ToString();
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            // add menu category to window
            liveTrackerWindow.AppendChild(liveTrackerCategory);

            #region Column Width

            // create/add live track data column width
            liveTrackerCategory = PreferencesXmlDocument.CreateElement("DataGridColumnWidth");

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("AddonCountColumnWidth");
            liveTrackerPreference.InnerText = DefaultLiveTrackerAddonCountColumnWidth.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("AddonTotalCostColumnWidth");
            liveTrackerPreference.InnerText = DefaultLiveTrackerAddonTotalCostColumnWidth.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("BountyCountColumnWidth");
            liveTrackerPreference.InnerText = DefaultLiveTrackerBountyCountColumnWidth.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("BuyinTotalCostColumnWidth");
            liveTrackerPreference.InnerText = DefaultLiveTrackerBuyinTotalCostColumnWidth.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("EntrantsColumnWidth");
            liveTrackerPreference.InnerText = DefaultLiveTrackerEntrantsColumnWidth.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("EntrantsPaidColumnWidth");
            liveTrackerPreference.InnerText = DefaultLiveTrackerEntrantsPaidColumnWidth.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("FinishPositionColumnWidth");
            liveTrackerPreference.InnerText = DefaultLiveTrackerFinishPositionColumnWidth.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("JackpotSpinMultiplierColumnWidth");
            liveTrackerPreference.InnerText = DefaultLiveTrackerJackpotSpinMultiplierColumnWidth.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("PrizeWonColumnWidth");
            liveTrackerPreference.InnerText = DefaultLiveTrackerPrizeWonColumnWidth.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("RebuyCountColumnWidth");
            liveTrackerPreference.InnerText = DefaultLiveTrackerRebuyCountColumnWidth.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("RebuyTotalCostColumnWidth");
            liveTrackerPreference.InnerText = DefaultLiveTrackerRebuyTotalCostColumnWidth.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("StartTimeColumnWidth");
            liveTrackerPreference.InnerText = DefaultLiveTrackerStartTimeColumnWidth.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("TotalCostColumnWidth");
            liveTrackerPreference.InnerText = DefaultLiveTrackerTotalCostColumnWidth.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("TournamentNameColumnWidth");
            liveTrackerPreference.InnerText = DefaultLiveTrackerTournamentNameColumnWidth.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("VenueColumnWidth");
            liveTrackerPreference.InnerText = DefaultLiveTrackerVenueColumnWidth.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            #endregion

            // add column width to window
            liveTrackerWindow.AppendChild(liveTrackerCategory);

            #region Column Visibility

            // create/add live track data column visibility
            liveTrackerCategory = PreferencesXmlDocument.CreateElement("DataGridColumnVisibility");
            liveTrackerPreference = PreferencesXmlDocument.CreateElement("AddonCountColumnVisible");
            liveTrackerPreference.InnerText = DefaultLiveTrackerAddonCountColumnVisibility.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            // create/add live tracker addon total cost column visible
            liveTrackerPreference = PreferencesXmlDocument.CreateElement("AddonTotalCostColumnVisible");
            liveTrackerPreference.InnerText = DefaultLiveTrackerAddonTotalCostColumnVisibility.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            // create/add live tracker blind levels column visible
            liveTrackerPreference = PreferencesXmlDocument.CreateElement("BlindLevelsColumnVisible");
            liveTrackerPreference.InnerText = DefaultLiveTrackerBlindLevelsColumnVisibility.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            // create/add live tracker bounty column visible
            liveTrackerPreference = PreferencesXmlDocument.CreateElement("BountyColumnVisible");
            liveTrackerPreference.InnerText = DefaultLiveTrackerBountyColumnVisibility.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            // create/add live tracker column visible
            liveTrackerPreference = PreferencesXmlDocument.CreateElement("BuyinTotalCostColumnVisible");
            liveTrackerPreference.InnerText = DefaultLiveTrackerBuyinTotalCostColumnVisibility.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            // create/add live tracker column visible
            liveTrackerPreference = PreferencesXmlDocument.CreateElement("EntrantsColumnVisible");
            liveTrackerPreference.InnerText = DefaultLiveTrackerEntrantsColumnVisibility.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            // create/add live tracker column visible
            liveTrackerPreference = PreferencesXmlDocument.CreateElement("EntrantsPaidColumnVisible");
            liveTrackerPreference.InnerText = DefaultLiveTrackerEntrantsPaidColumnVisibility.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            // create/add live tracker column visible
            liveTrackerPreference = PreferencesXmlDocument.CreateElement("FinishPositionColumnVisible");
            liveTrackerPreference.InnerText = DefaultLiveTrackerFinishPositionColumnVisibility.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            // create/add live tracker column visible
            liveTrackerPreference = PreferencesXmlDocument.CreateElement("JackpotSpinMultiplierColumnVisible");
            liveTrackerPreference.InnerText = DefaultLiveTrackerJackpotSpinMultiplierColumnVisibility.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            // create/add live tracker column visible
            liveTrackerPreference = PreferencesXmlDocument.CreateElement("PrizeWonColumnVisible");
            liveTrackerPreference.InnerText = DefaultLiveTrackerPrizeWonColumnVisibility.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            // create/add live tracker column visible
            liveTrackerPreference = PreferencesXmlDocument.CreateElement("RebuyCountColumnVisible");
            liveTrackerPreference.InnerText = DefaultLiveTrackerRebuyCountColumnVisibility.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            // create/add live tracker column visible
            liveTrackerPreference = PreferencesXmlDocument.CreateElement("RebuyTotalCostColumnVisible");
            liveTrackerPreference.InnerText = DefaultLiveTrackerRebuyTotalCostColumnVisibility.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            // create/add live tracker column visible
            liveTrackerPreference = PreferencesXmlDocument.CreateElement("StartTimeColumnVisible");
            liveTrackerPreference.InnerText = DefaultLiveTrackerStartTimeColumnVisibility.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            // create/add live tracker column visible
            liveTrackerPreference = PreferencesXmlDocument.CreateElement("TotalCostColumnVisible");
            liveTrackerPreference.InnerText = DefaultLiveTrackerTotalCostColumnVisibility.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            // create/add live tracker venue column visible
            liveTrackerPreference = PreferencesXmlDocument.CreateElement("VenueColumnVisible");
            liveTrackerPreference.InnerText = DefaultLiveTrackerVenueColumnVisibility.ToString();
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            #endregion

            // add column visibility to window
            liveTrackerWindow.AppendChild(liveTrackerCategory);

            #region Column Order

            // create/add live track data column order
            liveTrackerCategory = PreferencesXmlDocument.CreateElement("DataGridColumnOrder");

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("AddonCountColumnOrder");
            liveTrackerPreference.InnerText = DefaultLiveTrackerAddonCountColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("AddonTotalCostColumnOrder");
            liveTrackerPreference.InnerText = DefaultLiveTrackerAddonTotalCostColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("BountyCountColumnOrder");
            liveTrackerPreference.InnerText = DefaultLiveTrackerBountyCountColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("BuyinTotalCostColumnOrder");
            liveTrackerPreference.InnerText = DefaultLiveTrackerBuyinTotalCostColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("EntrantsColumnOrder");
            liveTrackerPreference.InnerText = DefaultLiveTrackerEntrantsColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("EntrantsPaidColumnOrder");
            liveTrackerPreference.InnerText = DefaultLiveTrackerEntrantsPaidColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("FinishPositionColumnOrder");
            liveTrackerPreference.InnerText = DefaultLiveTrackerFinishPositionColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("JackpotSpinMultiplierColumnOrder");
            liveTrackerPreference.InnerText = DefaultLiveTrackerJackpotSpinMultiplierColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("PrizeWonColumnOrder");
            liveTrackerPreference.InnerText = DefaultLiveTrackerPrizeWonColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("RebuyCountColumnOrder");
            liveTrackerPreference.InnerText = DefaultLiveTrackerRebuyCountColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("RebuyTotalCostColumnOrder");
            liveTrackerPreference.InnerText = DefaultLiveTrackerRebuyTotalCostColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("StartTimeColumnOrder");
            liveTrackerPreference.InnerText = DefaultLiveTrackerStartTimeColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("TotalCostColumnOrder");
            liveTrackerPreference.InnerText = DefaultLiveTrackerTotalCostColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("TournamentNameColumnOrder");
            liveTrackerPreference.InnerText = DefaultLiveTrackerTournamentNameColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("VenueColumnOrder");
            liveTrackerPreference.InnerText = DefaultLiveTrackerVenueColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            #endregion

            // add column order to window
            liveTrackerWindow.AppendChild(liveTrackerCategory);

            // add data grid category
            liveTrackerCategory = PreferencesXmlDocument.CreateElement("DataGrid");

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("ShortColumnHeaders");
            liveTrackerPreference.InnerText = DefaultLiveTrackerShortColumnHeaders.ToString();
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("ShowRowHeaders");
            liveTrackerPreference.InnerText = DefaultLiveTrackerShowRowHeaders.ToString();
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("SingleRowMode");
            liveTrackerPreference.InnerText = DefaultLiveTrackerSingleRowMode.ToString();
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("MaxShowInView");
            liveTrackerPreference.InnerText = DefaultMaxShowInView.ToString();
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            // add data grid to window
            liveTrackerWindow.AppendChild(liveTrackerCategory);

            #region Template Manager

            // add template manager width category
            liveTrackerCategory = PreferencesXmlDocument.CreateElement("TemplateManagerColumnWidth");

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("AddonRakeCostColumnWidth");
            liveTrackerPreference.InnerText = DefaultTemplateManagerAddonRakeCostColumnWidth.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("AddonBaseCostColumnWidth");
            liveTrackerPreference.InnerText = DefaultTemplateManagerAddonBaseCostColumnWidth.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("AddonTotalCostColumnWidth");
            liveTrackerPreference.InnerText = DefaultTemplateManagerAddonTotalCostColumnWidth.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("BlindLevelsWidth");
            liveTrackerPreference.InnerText = DefaultTemplateManagerBlindLevelsColumnWidth.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("BovadaBountyColumnWidth");
            liveTrackerPreference.InnerText = DefaultTemplateManagerBovadaBountyColumnWidth.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("BovadaColumnWidth");
            liveTrackerPreference.InnerText = DefaultTemplateManagerBovadaColumnWidth.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("BuyinBaseCostColumnWidth");
            liveTrackerPreference.InnerText = DefaultTemplateManagerBuyinBaseCostColumnWidth.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("BuyinRakeCostColumnWidth");
            liveTrackerPreference.InnerText = DefaultTemplateManagerBuyinRakeCostColumnWidth.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("BuyinTotalCostColumnWidth");
            liveTrackerPreference.InnerText = DefaultTemplateManagerBuyinTotalCostColumnWidth.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("EntrantsColumnWidth");
            liveTrackerPreference.InnerText = DefaultTemplateManagerEntrantsColumnWidth.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("EntrantsPaidColumnWidth");
            liveTrackerPreference.InnerText = DefaultTemplateManagerEntrantsPaidColumnWidth.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("FormatsColumnWidth");
            liveTrackerPreference.InnerText = DefaultTemplateManagerFormatsColumnWidth.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("GameTypeColumnWidth");
            liveTrackerPreference.InnerText = DefaultTemplateManagerGameTypeColumnWidth.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("GuaranteeColumnWidth");
            liveTrackerPreference.InnerText = DefaultTemplateManagerGuaranteeColumnWidth.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("LateRegColumnWidth");
            liveTrackerPreference.InnerText = DefaultTemplateManagerLateRegColumnWidth.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("RebuyBaseCostColumnWidth");
            liveTrackerPreference.InnerText = DefaultTemplateManagerRebuyBaseCostColumnWidth.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("RebuyRakeCostColumnWidth");
            liveTrackerPreference.InnerText = DefaultTemplateManagerRebuyRakeCostColumnWidth.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("RebuyTotalCostColumnWidth");
            liveTrackerPreference.InnerText = DefaultTemplateManagerRebuyTotalCostColumnWidth.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("SngColumnWidth");
            liveTrackerPreference.InnerText = DefaultTemplateManagerSngColumnWidth.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("StackSizeAddonColumnWidth");
            liveTrackerPreference.InnerText = DefaultTemplateManagerStackSizeAddonColumnWidth.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("StackSizeRebuyColumnWidth");
            liveTrackerPreference.InnerText = DefaultTemplateManagerStackSizeRebuyColumnWidth.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("StackSizeStartingColumnWidth");
            liveTrackerPreference.InnerText = DefaultTemplateManagerStackSizeStartingColumnWidth.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("StartTimeColumnWidth");
            liveTrackerPreference.InnerText = DefaultTemplateManagerStartTimeColumnWidth.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("TableSizeColumnWidth");
            liveTrackerPreference.InnerText = DefaultTemplateManagerTableSizeColumnWidth.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("TournamentNameColumnWidth");
            liveTrackerPreference.InnerText = DefaultTemplateManagerTournamentNameColumnWidth.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("VenueColumnWidth");
            liveTrackerPreference.InnerText = DefaultTemplateManagerVenueColumnWidth.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            // add template manager width to window
            liveTrackerWindow.AppendChild(liveTrackerCategory);

            // add template manager order category
            liveTrackerCategory = PreferencesXmlDocument.CreateElement("TemplateManagerColumnOrder");

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("AddonRakeCostColumnOrder");
            liveTrackerPreference.InnerText = DefaultTemplateManagerAddonRakeCostColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("AddonBaseCostColumnOrder");
            liveTrackerPreference.InnerText = DefaultTemplateManagerAddonBaseCostColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("AddonTotalCostColumnOrder");
            liveTrackerPreference.InnerText = DefaultTemplateManagerAddonTotalCostColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("BlindLevelsColumnOrder");
            liveTrackerPreference.InnerText = DefaultTemplateManagerBlindLevelsColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("BovadaBountyColumnOrder");
            liveTrackerPreference.InnerText = DefaultTemplateManagerBovadaBountyColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("BovadaColumnOrder");
            liveTrackerPreference.InnerText = DefaultTemplateManagerBovadaColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("BuyinBaseCostColumnOrder");
            liveTrackerPreference.InnerText = DefaultTemplateManagerBuyinBaseCostColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("BuyinRakeCostColumnOrder");
            liveTrackerPreference.InnerText = DefaultTemplateManagerBuyinRakeCostColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("BuyinTotalCostColumnOrder");
            liveTrackerPreference.InnerText = DefaultTemplateManagerBuyinTotalCostColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("EntrantsColumnOrder");
            liveTrackerPreference.InnerText = DefaultTemplateManagerEntrantsColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("EntrantsPaidColumnOrder");
            liveTrackerPreference.InnerText = DefaultTemplateManagerEntrantsPaidColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("FormatsColumnOrder");
            liveTrackerPreference.InnerText = DefaultTemplateManagerFormatsColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("GameTypeColumnOrder");
            liveTrackerPreference.InnerText = DefaultTemplateManagerGameTypeColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("GuaranteeColumnOrder");
            liveTrackerPreference.InnerText = DefaultTemplateManagerGuaranteeColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("LateRegColumnOrder");
            liveTrackerPreference.InnerText = DefaultTemplateManagerLateRegColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("RebuyBaseCostColumnOrder");
            liveTrackerPreference.InnerText = DefaultTemplateManagerRebuyBaseCostColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("RebuyRakeCostColumnOrder");
            liveTrackerPreference.InnerText = DefaultTemplateManagerRebuyRakeCostColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("RebuyTotalCostColumnOrder");
            liveTrackerPreference.InnerText = DefaultTemplateManagerRebuyTotalCostColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("SngColumnOrder");
            liveTrackerPreference.InnerText = DefaultTemplateManagerSngColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("StackSizeAddonColumnOrder");
            liveTrackerPreference.InnerText = DefaultTemplateManagerStackSizeAddonColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("StackSizeRebuyColumnOrder");
            liveTrackerPreference.InnerText = DefaultTemplateManagerStackSizeRebuyColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("StackSizeStartingColumnOrder");
            liveTrackerPreference.InnerText = DefaultTemplateManagerStackSizeStartingColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("StartTimeColumnOrder");
            liveTrackerPreference.InnerText = DefaultTemplateManagerStartTimeColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("TableSizeColumnOrder");
            liveTrackerPreference.InnerText = DefaultTemplateManagerTableSizeColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("TournamentNameColumnOrder");
            liveTrackerPreference.InnerText = DefaultTemplateManagerTournamentNameColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("VenueColumnOrder");
            liveTrackerPreference.InnerText = DefaultTemplateManagerVenueColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            // add template manager order to window
            liveTrackerWindow.AppendChild(liveTrackerCategory);

            #endregion

            #region Tournaments View

            // add template manager order category
            liveTrackerCategory = PreferencesXmlDocument.CreateElement("TournamentsViewColumnOrder");

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("AddonRakeCostColumnOrder");
            liveTrackerPreference.InnerText = DefaultTournamentsViewAddonRakeCostColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("AddonBaseCostColumnOrder");
            liveTrackerPreference.InnerText = DefaultTournamentsViewAddonBaseCostColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("AddonCountColumnOrder");
            liveTrackerPreference.InnerText = DefaultTournamentsViewAddonCountColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("AddonTotalCostColumnOrder");
            liveTrackerPreference.InnerText = DefaultTournamentsViewAddonTotalCostColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("BlindLevelsColumnOrder");
            liveTrackerPreference.InnerText = DefaultTournamentsViewBlindLevelsColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("BountyCountColumnOrder");
            liveTrackerPreference.InnerText = DefaultTournamentsViewBountyCountColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("BovadaBountyColumnOrder");
            liveTrackerPreference.InnerText = DefaultTournamentsViewBovadaBountyColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("BovadaColumnOrder");
            liveTrackerPreference.InnerText = DefaultTournamentsViewBovadaColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("BuyinBaseCostColumnOrder");
            liveTrackerPreference.InnerText = DefaultTournamentsViewBuyinBaseCostColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("BuyinRakeCostColumnOrder");
            liveTrackerPreference.InnerText = DefaultTournamentsViewBuyinRakeCostColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("BuyinTotalCostColumnOrder");
            liveTrackerPreference.InnerText = DefaultTournamentsViewBuyinTotalCostColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("EndDateColumnOrder");
            liveTrackerPreference.InnerText = DefaultTournamentsViewEndDateColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("EndTimeColumnOrder");
            liveTrackerPreference.InnerText = DefaultTournamentsViewEndTimeColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("EntrantsColumnOrder");
            liveTrackerPreference.InnerText = DefaultTournamentsViewEntrantsColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("EntrantsPaidColumnOrder");
            liveTrackerPreference.InnerText = DefaultTournamentsViewEntrantsPaidColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("FinishPositionColumnOrder");
            liveTrackerPreference.InnerText = DefaultTournamentsViewFinishPositionColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("FormatsColumnOrder");
            liveTrackerPreference.InnerText = DefaultTournamentsViewFormatsColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("GameTypeColumnOrder");
            liveTrackerPreference.InnerText = DefaultTournamentsViewGameTypeColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("GuaranteeColumnOrder");
            liveTrackerPreference.InnerText = DefaultTournamentsViewGuaranteeColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("JackpotSpinMultiplierColumnOrder");
            liveTrackerPreference.InnerText = DefaultTournamentsViewJackpotSpinMultiplierColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("LateRegColumnOrder");
            liveTrackerPreference.InnerText = DefaultTournamentsViewLateRegColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("LengthColumnOrder");
            liveTrackerPreference.InnerText = DefaultTournamentsViewLengthColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("NoteColumnOrder");
            liveTrackerPreference.InnerText = DefaultTournamentsViewNoteColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("PrizeWonColumnOrder");
            liveTrackerPreference.InnerText = DefaultTournamentsViewPrizeWonColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("ProfitColumnOrder");
            liveTrackerPreference.InnerText = DefaultTournamentsViewProfitColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("RebuyBaseCostColumnOrder");
            liveTrackerPreference.InnerText = DefaultTournamentsViewRebuyBaseCostColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("RebuyCountColumnOrder");
            liveTrackerPreference.InnerText = DefaultTournamentsViewRebuyCountColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("RebuyRakeCostColumnOrder");
            liveTrackerPreference.InnerText = DefaultTournamentsViewRebuyRakeCostColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("RebuyTotalCostColumnOrder");
            liveTrackerPreference.InnerText = DefaultTournamentsViewRebuyTotalCostColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("ScreenshotColumnOrder");
            liveTrackerPreference.InnerText = DefaultTournamentsViewScreenshotColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("SngColumnOrder");
            liveTrackerPreference.InnerText = DefaultTournamentsViewSngColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("StackSizeAddonColumnOrder");
            liveTrackerPreference.InnerText = DefaultTournamentsViewStackSizeAddonColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("StackSizeRebuyColumnOrder");
            liveTrackerPreference.InnerText = DefaultTournamentsViewStackSizeRebuyColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("StackSizeStartingColumnOrder");
            liveTrackerPreference.InnerText = DefaultTournamentsViewStackSizeStartingColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("StartDateColumnOrder");
            liveTrackerPreference.InnerText = DefaultTournamentsViewStartDateColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("StartTimeColumnOrder");
            liveTrackerPreference.InnerText = DefaultTournamentsViewStartTimeColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("TableSizeColumnOrder");
            liveTrackerPreference.InnerText = DefaultTournamentsViewTableSizeColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("TotalCostColumnOrder");
            liveTrackerPreference.InnerText = DefaultTournamentsViewTotalCostColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("TournamentNameColumnOrder");
            liveTrackerPreference.InnerText = DefaultTournamentsViewTournamentNameColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            liveTrackerPreference = PreferencesXmlDocument.CreateElement("VenueColumnOrder");
            liveTrackerPreference.InnerText = DefaultTournamentsViewVenueColumnOrder.ToString(new CultureInfo("en-US"));
            liveTrackerCategory.AppendChild(liveTrackerPreference);

            // add template manager order to window
            liveTrackerWindow.AppendChild(liveTrackerCategory);

            #endregion

            // add live tracker window to preference files
            PreferencesXmlDocument.DocumentElement?.AppendChild(liveTrackerWindow);

            try { PreferencesXmlDocument.Save(_filename); }
            catch (IOException ex) { Debug.WriteLine(ex.Message); }

        }

        /// <summary>
        /// Update a preference in the preferences file
        /// </summary>
        /// <param name="window">window category is in</param>
        /// <param name="category">category preference is in</param>
        /// <param name="preference">preference</param>
        /// <param name="value">value to change the preference to</param>
        /// <returns>true is successful</returns>
        public static bool UpdatePreference(string window, string category, string preference, object value)
        {
            // load preferences xml file
            var preferencesXmlDocument = XmlHelper.LoadXmlFile(XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.PreferencesFileName);

            // if null value or null node return a fail
            if (preferencesXmlDocument.DocumentElement is not null && (value is null || preferencesXmlDocument.DocumentElement.SelectSingleNode(window)?.SelectSingleNode(category)?.SelectSingleNode(preference) is null)) return false;

            // create new node with old preference name and new value
            var newNode = preferencesXmlDocument.CreateElement(preference);
            newNode.InnerText = value.ToString() ?? string.Empty;

            // find old preference node
            var oldNode = preferencesXmlDocument.DocumentElement?.SelectSingleNode(window)?.SelectSingleNode(category)?.SelectSingleNode(preference);

            // if preference doesn't exist, return failure
            if (oldNode is null) return false;

            // replace old node with new node
            preferencesXmlDocument.DocumentElement.SelectSingleNode(window)?.SelectSingleNode(category)?.ReplaceChild(newNode, oldNode);

            // save file
            try { preferencesXmlDocument.Save(XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.PreferencesFileName); }
            catch (IOException ex) { Debug.WriteLine(ex.Message); }

            // return success
            return true;
        }

        /// <summary>
        /// Find a preference value from the preferences file
        /// </summary>
        /// <param name="window">window we want preference for</param>
        /// <param name="category">category preference is in</param>
        /// <param name="preference">preference we want</param>
        /// <returns>preference value as a string</returns>
        public static string FindPreference(string window, string category, string preference)
        {
            // create preferences user might not have
            CheckForPreference(window, category, preference);

            // try to pull preference
            var pref = XmlHelper.LoadXmlFile(XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.PreferencesFileName).DocumentElement?.SelectSingleNode(window)?.SelectSingleNode(category)?.SelectSingleNode(preference)?.InnerText;

            // create a new file if not found
            if (pref is null) new PreferencesHelper(ProfileHelper.GetCurrentProfile()).CreateDefaultPreferencesFile();

            //return PreferencesXmlDocument.DocumentElement?.SelectSingleNode(window)?.SelectSingleNode(category)?.SelectSingleNode(preference)?.InnerText;
            return XmlHelper.LoadXmlFile(XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.PreferencesFileName).DocumentElement?.SelectSingleNode(window)?.SelectSingleNode(category)?.SelectSingleNode(preference)?.InnerText;
        }
    }
}