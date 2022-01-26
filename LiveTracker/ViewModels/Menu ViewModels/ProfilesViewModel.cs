using LiveTracker.Commands;
using LiveTracker.Enums;
using LiveTracker.Helpers;
using LiveTracker.Models;
using LiveTracker.Models.Profile;
using LiveTracker.ViewModels.Datagrid_ViewModels;
using LiveTracker.ViewModels.Results;
using LiveTracker.ViewModels.Session_Manager_ViewModels;
using LiveTracker.ViewModels.Template_Manager_ViewModels;
using LiveTracker.Views;
using LiveTracker.Views.Graph_Views;
using LiveTracker.Views.Menu_Views;
using LiveTracker.Views.Results;
using LiveTracker.Views.Session_Manager_Views;
using LiveTracker.Views.Template_Manager_Views;
using Syncfusion.UI.Xaml.Utility;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using Tournament_Life.ViewModels;
using Tournament_Life.ViewModels.Results;
using Tournament_Life.Views;
using Tournament_Life.Views.Graph_Views;
using Tournament_Life.Views.Results;

namespace LiveTracker.ViewModels.Menu_ViewModels
{
    public class ProfilesViewModel : NotificationObject
    {
        public ICommand NewCommand => new BaseCommand(NewProfile);
        public ICommand CancelCommand => new BaseCommand(Cancel);
        public ICommand DeleteCommand => new BaseCommand(DeleteProfile);
        public ICommand LoadCommand => new BaseCommand(LoadProfile);

        public ProfilesViewModel()
        {
            // font size
            FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));

            // visual theme
            Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");

            // load profiles list
            LoadProfiles();

            Title = "Profile Manager";
        }

        public int FontSize { get; set; }
        public string NewProfileText { get; set;}
        public ObservableCollection<ProfileListBoxItem> ProfileList { get; set; }
        public ObservableCollection<string> Profiles { get; set; }
        public ProfileListBoxItem ProfileSelectedItem { get; set; }
        public bool Saved { get; set; }
        public string Theme { get; set; }
        public string Title { get; set; }

        /// <summary>
        /// Cancel any note text and close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Cancel(object parameter)
        {
            // making sure
            Saved = false;

            // find window
            var window = Application.Current.Windows.OfType<ProfilesView>().FirstOrDefault();

            // close window
            window?.Close();
        }

        /// <summary>
        /// Create some default templates
        /// </summary>
        private void CreateDefaultTemplates()
        {
            // load xml file
            var doc = XmlHelper.LoadXmlFile("Templates.xml");
            if (doc is null) 
                return;

            // get template nodes
            var templates = doc?.DocumentElement?.SelectNodes("Template");
            if (templates is null || templates.Count is 0) 
                return;

            // get template id that we can increment from
            var id = TournamentTemplateHelper.GetNewTemplateId();

            // loop templates
            foreach (XmlNode templateNode in templates)
            {
                // load template data from file into blank template
                var template = new TournamentTemplate
                {
                    AddonBaseCost = decimal.Parse(templateNode?.SelectSingleNode("AddonCost")?.InnerText ?? "0", new CultureInfo("en-US")),
                    AddonRakeCost = decimal.Parse(templateNode.SelectSingleNode("AddonRake")?.InnerText ?? "0", new CultureInfo("en-US")),
                    BlindLevels = int.Parse(templateNode.SelectSingleNode("BlindLevels")?.InnerText ?? "0", new CultureInfo("en-US")),
                    Bounty = decimal.Parse(templateNode.SelectSingleNode("Bounty")?.InnerText ?? "0", new CultureInfo("en-US")),
                    BuyinBaseCost = decimal.Parse(templateNode.SelectSingleNode("BuyinCost")?.InnerText ?? "0", new CultureInfo("en-US")),
                    BuyinRakeCost = decimal.Parse(templateNode.SelectSingleNode("BuyinRake")?.InnerText ?? "0", new CultureInfo("en-US")),
                    Entrants = int.Parse(templateNode.SelectSingleNode("Entrants")?.InnerText ?? "0", new CultureInfo("en-US")),
                    EntrantsPaid = int.Parse(templateNode.SelectSingleNode("EntrantsPaid")?.InnerText ?? "0", new CultureInfo("en-US")),
                    GameType = templateNode.SelectSingleNode("GameType")?.InnerText ?? "",
                    Guarantee = decimal.Parse(templateNode.SelectSingleNode("Guarantee")?.InnerText ?? "0", new CultureInfo("en-US")),
                    IsBovadaBounty = bool.Parse(templateNode.SelectSingleNode("IsBovadaBounty")?.InnerText ?? "False"),
                    IsFavorite = false,
                    IsSng = bool.Parse(templateNode.SelectSingleNode("IsSng")?.InnerText ?? "False"),
                    LateReg = int.Parse(templateNode.SelectSingleNode("LateReg")?.InnerText ?? "0", new CultureInfo("en-US")),
                    RebuyBaseCost = decimal.Parse(templateNode.SelectSingleNode("RebuyCost")?.InnerText ?? "0", new CultureInfo("en-US")),
                    RebuyRakeCost = decimal.Parse(templateNode.SelectSingleNode("RebuyRake")?.InnerText ?? "0", new CultureInfo("en-US")),
                    SngPayouts = templateNode.SelectSingleNode("SngPayouts")?.InnerText ?? "",
                    StackSizeAddon = int.Parse(templateNode.SelectSingleNode("StackSizeAddon")?.InnerText ?? "0", new CultureInfo("en-US")),
                    StackSizeRebuy = int.Parse(templateNode.SelectSingleNode("StackSizeRebuy")?.InnerText ?? "0", new CultureInfo("en-US")),
                    StackSizeStarting = int.Parse(templateNode.SelectSingleNode("StackSizeStarting")?.InnerText ?? "0", new CultureInfo("en-US")),
                    StartTime = DateTime.Parse(templateNode.SelectSingleNode("StartTime")?.InnerText ?? "1/1/1111 12:00 AM", new CultureInfo("en-US")),
                    TableSize = int.Parse(templateNode.SelectSingleNode("TableSize")?.InnerText ?? "9", new CultureInfo("en-US")),
                    TimeZoneName = templateNode.SelectSingleNode("TimeZoneName")?.InnerText ?? TimeZoneInfo.Local.StandardName,
                    TournamentName = templateNode.SelectSingleNode("TournamentName")?.InnerText ?? "",
                    Venue = templateNode.SelectSingleNode("SiteName")?.InnerText ?? "",
                };

                // add formats
                var formatNodes = templateNode.SelectSingleNode("Formats")?.ChildNodes;
                if (formatNodes is not null) foreach (XmlNode formatNode in formatNodes) template.Formats.Add(formatNode.InnerText);

                // give template new id number
                template.TemplateId = id++;

                // update template start times for time zone changes
                if (template.TimeZoneName.Length is 0) template.TimeZoneName = TimeZoneInfo.Local.StandardName;
                template.StartTime = TimeZoneInfo.ConvertTime(template.StartTime, TimeZoneInfo.FindSystemTimeZoneById(template.TimeZoneName), TimeZoneInfo.FindSystemTimeZoneById(TimeZoneInfo.Local.StandardName));
                template.TimeZoneName = TimeZoneInfo.Local.StandardName;

                // make sure start time is 1111/1/1 + the hour and day + 0 seconds
                template.StartTime = new DateTime(1111, 1, 1, template.StartTime.Hour, template.StartTime.Minute, 0);

                // add new game types to template data
                TournamentTemplateDataHelper.AddGameType(template.GameType);

                // add new venues to template data
                TournamentTemplateDataHelper.AddVenue(template.Venue);

                // add new formats to template data
                foreach (var format in template.Formats) TournamentTemplateDataHelper.AddFormat(format);

                TournamentTemplateHelper.SaveTournamentTemplate(template);
            }
        }

        /// <summary>
        /// creates list box items from templates
        /// </summary>
        private ObservableCollection<ProfileListBoxItem> CreateListBoxItems()
        {
            // temp list for list box items
            var list = new ObservableCollection<ProfileListBoxItem>();

            // create the list box items from the profiles list
            foreach (var profile in Profiles)
            {
                var listBoxItem = new ProfileListBoxItem()
                {
                    IsSelected = false,
                    Name = profile,
                };

                list.Add(listBoxItem);
            }

            // return the list of list box items
            return list;
        }

        /// <summary>
        /// Load the profiles list
        /// </summary>
        private void LoadProfiles()
        {
            // get venues from tournament template data file
            Profiles = new ObservableCollection<string>(ProfileHelper.LoadProfiles());

            // list box item list for the templates
            ProfileList = CreateListBoxItems();

            // clear text
            NewProfileText = "";
        }

        /// <summary>
        /// called when the user clicks the delete button
        /// deletes selected profile
        /// </summary>
        /// <param name="parameter"></param>
        private void DeleteProfile(object parameter)
        {
            // nothing selected
            if(ProfileSelectedItem is null)
            {
                // create/show error message
                var okayVM1 = new OkViewModel("No profile selected", "None Selected");
                var okayWindow1 = new OkView(okayVM1) { Owner = Application.Current.Windows.OfType<ProfilesView>().FirstOrDefault(), WindowStartupLocation = WindowStartupLocation.CenterOwner, }; okayWindow1.ShowDialog();

                // exit
                return;
            }

            // can't delete selected profile
            if(ProfileSelectedItem.Name == ProfileHelper.GetCurrentProfile())
            {
                // yes/no view model
                var v = new OkViewModel("You can't delete the currently loaded profile", "Can't Delete");

                // create/show yes/no window
                var windo = new OkView(v)
                {
                    Owner = Application.Current.Windows.OfType<ProfilesView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                windo.ShowDialog();

                // exit
                return;
            }

            // yes/no view model
            var vm = new YesNoViewModel("Are you sure you want to delete " + ProfileSelectedItem.Name + "?", "Delete Profile");

            // create/show yes/no window
            var window = new YesNoView(vm)
            {
                Owner = Application.Current.Windows.OfType<ProfilesView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };

            window.ShowDialog();

            // exit if not saved
            if(vm.Saved is false) return;

            // delete profile
            ProfileHelper.DeleteProfile(ProfileSelectedItem.Name);

            // reload profiles
            LoadProfiles();
        }

        /// <summary>
        /// Creates a new profile
        /// </summary>
        /// <param name="parameter"></param>
        private void NewProfile(object parameter)
        {
            // create/show new profile window
            var vm = new AddProfileViewModel();
            var window = new AddProfileView(vm)
            {
                Owner = Application.Current.Windows.OfType<ProfilesView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            window.ShowDialog();

            // not saved
            if(vm.Saved is false)
                return;

            Title = "Profile Manager - Creating ...";

            // reload profiles
            LoadProfiles();

            // selected profile
            var selected = ProfileList.Where(i => i.Name == vm.Text.Trim()).FirstOrDefault();

            // set selected profile
            ProfileSelectedItem = selected;

            // load new profile
            LoadNewProfile(vm.Text.Trim(), vm.ImportDefaults);
        }

        /// <summary>
        /// load a selected profile
        /// </summary>
        /// <param name="parameter"></param>
        private void LoadProfile(object parameter)
        {
            // nothing selected
            if (ProfileSelectedItem is null)
            {
                // create/show error message
                var okayVM1 = new OkViewModel("No profile selected", "None Selected");
                var okayWindow1 = new OkView(okayVM1) { Owner = Application.Current.Windows.OfType<ProfilesView>().FirstOrDefault(), WindowStartupLocation = WindowStartupLocation.CenterOwner, }; okayWindow1.ShowDialog();

                // exit
                return;
            }

            // profile already loaded
            if (ProfileSelectedItem.Name == ProfileHelper.GetCurrentProfile())
            {
                // yes/no view model
                var v = new OkViewModel($"The profile {ProfileSelectedItem.Name} is already loaded", "Already Loaded");

                // create/show yes/no window
                var win = new OkView(v)
                {
                    Owner = Application.Current.Windows.OfType<ProfilesView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                win.ShowDialog();

                // exit
                return;
            }

            // yes/no view model
            var vm = new YesNoViewModel($"Are you sure you want to load {ProfileSelectedItem.Name}?", "Load Profile");

            // create/show yes/no window
            var windo = new YesNoView(vm)
            {
                Owner = Application.Current.Windows.OfType<ProfilesView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            windo.ShowDialog();

            // not saved, exit
            if (vm.Saved is false) return;

            // live tracker view model
            if (Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.DataContext is not LiveTrackerViewModel liveTrackerViewModel) return;

            // find menu view model
            if (Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.LiveTrackerMenu?.DataContext is not MenuViewModel menuViewModel) return;

            // data grid view model
            if (Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.TournamentsDataGrid.DataContext is not DataGridViewModel dataGridViewModel) return;

            // save running tournaments
            dataGridViewModel.UpdateTournamentProperties(null);

            // clear running tournaments
            dataGridViewModel.RunningTournaments = new ObservableCollection<TournamentRunning>();

            // clear visible tournaments
            dataGridViewModel.VisibleTournaments = new ObservableCollection<TournamentRunning>();

            // update current profile
            ProfileHelper.SetCurrentProfile(ProfileSelectedItem.Name);

            // load database
            var tournaments = DatabaseHelper.LoadDatabase();

            // reload templates
            menuViewModel.UpdateFavoriteTemplates();

            // reload sessions
            menuViewModel.UpdateFavoriteSessions();

            // reload bankrolls
            menuViewModel.LoadBankrolls(tournaments);

            // reload recent tournaments
            menuViewModel.LoadRecentlyFinishedTournaments();

            // load running tournaments
            dataGridViewModel.LoadRunningTournaments();

            // load data grid preferences
            dataGridViewModel.InitializeLiveTrackerPreferences();

            // load live tracker window preferences
            liveTrackerViewModel.InitializePreferences();

            // load menu preferences
            menuViewModel.LoadPreferences();

            // update visibility on the tournaments data grid
            dataGridViewModel.Visibility = (TournamentVisibility)int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Menu", "TournamentVisibility"));

            // reset data grid
            DataGridCommands.DataGridLoaded();

            // find if quick results view is open
            if (Application.Current.Windows.OfType<QuickResultsView>().FirstOrDefault()?.DataContext is QuickResultsViewModel quickResultsViewModel) quickResultsViewModel.Update(tournaments);

            // find if template manager is open and reload
            if (Application.Current.Windows.OfType<TemplateManagerView>().FirstOrDefault()?.DataContext is TemplateManagerViewModel templateManagerViewModel) templateManagerViewModel.Reload();

            // find if session manager is open and reload
            if (Application.Current.Windows.OfType<SessionManagerView>().FirstOrDefault()?.DataContext is SessionManagerViewModel sessionManagerViewModel) sessionManagerViewModel.Reload();

            // find if session results view is open
            if (Application.Current.Windows.OfType<SessionResultsView>().FirstOrDefault()?.DataContext is SessionResultsViewModel sessionResultsViewModel) sessionResultsViewModel.Update(tournaments);

            // find if tournament results view is open
            if (Application.Current.Windows.OfType<TournamentsResultsView>().FirstOrDefault()?.DataContext is TournamentsResultsViewModel tournamentsResultsViewModel) tournamentsResultsViewModel.Update(tournaments);

            // get any open tournaments view windows
            var tournamentsViewWindows = Application.Current.Windows.OfType<TournamentsView>();

            // close all the windows
            if (tournamentsViewWindows is not null) foreach (var window in tournamentsViewWindows.ToList()) window.Close();

            // get any open create tournament filter windows
            var tournamentFilterWindows = Application.Current.Windows.OfType<CreateTournamentFilterView>();

            // close all the windows
            if (tournamentFilterWindows is not null) foreach (var window in tournamentFilterWindows.ToList()) window.Close();

            // get any open tournament profit graphs
            var profitWindows = Application.Current.Windows.OfType<TournamentProfitGraphView>();

            // close all the windows
            if (profitWindows is not null) foreach (var window in profitWindows.ToList()) window.Close();

            // get any open tournament profit no label graphs
            var noLabelProfitWindows = Application.Current.Windows.OfType<TournamentProfitGraphNoLabelView>();

            // close all the windows
            if (noLabelProfitWindows is not null) foreach (var window in noLabelProfitWindows.ToList()) window.Close();

            // get any open session profit graphs
            var sessionProfitWindows = Application.Current.Windows.OfType<SessionProfitGraphView>();

            // close all the windows
            if (sessionProfitWindows is not null) foreach (var window in sessionProfitWindows.ToList()) window.Close();

            // get any open session profit no label graphs
            var sessionNoLabelProfitWindows = Application.Current.Windows.OfType<SessionProfitGraphNoLabelView>();

            // close all the windows
            if (sessionNoLabelProfitWindows is not null) foreach (var window in sessionNoLabelProfitWindows.ToList()) window.Close();

            // get any open finish position graphs
            var finishWindows = Application.Current.Windows.OfType<FinishPositionGraphView>();

            // close all the windows
            if (finishWindows is not null) foreach (var window in finishWindows.ToList()) window.Close();

            // get any open tournament buy-in/roi graphs
            var buyinRoiWindows = Application.Current.Windows.OfType<TournamentBuyinRoiHistogramView>();

            // close all the windows
            if (buyinRoiWindows is not null) foreach (var window in buyinRoiWindows.ToList()) window.Close();

            // get any open tournament format/roi graphs
            var formatRoiWindows = Application.Current.Windows.OfType<TournamentFormatRoiChartView>();

            // close all the windows
            if (formatRoiWindows is not null) foreach (var window in formatRoiWindows.ToList()) window.Close();

            // get any open tournament game type/roi graphs
            var gameTypeRoiWindows = Application.Current.Windows.OfType<TournamentGameTypeRoiChartView>();

            // close all the windows
            if (gameTypeRoiWindows is not null) foreach (var window in gameTypeRoiWindows.ToList()) window.Close();

            // get any open tournament venue/roi graphs
            var venueRoiWindows = Application.Current.Windows.OfType<TournamentVenueRoiChartView>();

            // close all the windows
            if (venueRoiWindows is not null) foreach (var window in venueRoiWindows.ToList()) window.Close();

            // close profiles window
            Cancel(null);
        }

        /// <summary>
        /// load a brand new profile
        /// </summary>
        /// <param name="parameter"></param>
        private void LoadNewProfile(string profile, bool importDefaults)
        {
            // no profile selected
            if (ProfileSelectedItem is null)
                return;

            // profile already loaded
            if (ProfileSelectedItem.Name == ProfileHelper.GetCurrentProfile())
            {
                // yes/no view model
                var v = new OkViewModel("Profile is already loaded", "Profile Loaded");

                // create/show yes/no window
                var win = new OkView(v)
                {
                    Owner = Application.Current.Windows.OfType<ProfilesView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                win.ShowDialog();

                // exit
                return;
            }

            // live tracker view model
            if (Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.DataContext is not LiveTrackerViewModel liveTrackerViewModel)
                return;

            // find menu view model
            if (Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.LiveTrackerMenu?.DataContext is not MenuViewModel menuViewModel)
                return;

            // data grid view model
            if (Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.TournamentsDataGrid.DataContext is not DataGridViewModel dataGridViewModel)
                return;

            // save running tournaments
            dataGridViewModel.UpdateTournamentProperties(null);

            // clear running tournaments
            dataGridViewModel.RunningTournaments = new ObservableCollection<TournamentRunning>();

            // clear visible tournaments
            dataGridViewModel.VisibleTournaments = new ObservableCollection<TournamentRunning>();

            // update current profile
            ProfileHelper.SetCurrentProfile(ProfileSelectedItem.Name);

            // load database
            var tournaments = DatabaseHelper.LoadDatabase();

            // reload sessions
            menuViewModel.UpdateFavoriteSessions();

            // reload bankrolls
            menuViewModel.LoadBankrolls(tournaments);

            // reload recent tournaments
            menuViewModel.LoadRecentlyFinishedTournaments();

            // load running tournaments
            dataGridViewModel.LoadRunningTournaments();

            // load data grid preferences
            dataGridViewModel.InitializeLiveTrackerPreferences();

            // load live tracker window preferences
            liveTrackerViewModel.InitializePreferences();

            // load menu preferences
            menuViewModel.LoadPreferences();

            // update visibility on the tournaments data grid
            dataGridViewModel.Visibility = (TournamentVisibility)int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Menu", "TournamentVisibility"));

            // reset data grid
            DataGridCommands.DataGridLoaded();

            // create default tournament data file
            TournamentTemplateDataHelper.CreateDefaultTemplateDataFile(profile);

            if (importDefaults is true)
            {
                // some starter formats
                TournamentTemplateDataHelper.AddFormat("1R1A");
                TournamentTemplateDataHelper.AddFormat("2R1A");
                TournamentTemplateDataHelper.AddFormat("3R1A");
                TournamentTemplateDataHelper.AddFormat("2x-Chance");
                TournamentTemplateDataHelper.AddFormat("Ante Up");
                TournamentTemplateDataHelper.AddFormat("Big Ante");
                TournamentTemplateDataHelper.AddFormat("Bounty");
                TournamentTemplateDataHelper.AddFormat("Deep");
                TournamentTemplateDataHelper.AddFormat("Freezeout");
                TournamentTemplateDataHelper.AddFormat("Heads-Up");
                TournamentTemplateDataHelper.AddFormat("Hyper-Turbo");
                TournamentTemplateDataHelper.AddFormat("Jackpot Sng");
                TournamentTemplateDataHelper.AddFormat("Progressive Bounty");
                TournamentTemplateDataHelper.AddFormat("Progressive Total Bounty");
                TournamentTemplateDataHelper.AddFormat("Re-Entry");
                TournamentTemplateDataHelper.AddFormat("Rebuy");
                TournamentTemplateDataHelper.AddFormat("Satellite");
                TournamentTemplateDataHelper.AddFormat("Shootout");
                TournamentTemplateDataHelper.AddFormat("Shovefest");
                TournamentTemplateDataHelper.AddFormat("Super Bounty");
                TournamentTemplateDataHelper.AddFormat("Super Deep");
                TournamentTemplateDataHelper.AddFormat("Sng");
                TournamentTemplateDataHelper.AddFormat("Total Bounty");
                TournamentTemplateDataHelper.AddFormat("Turbo");
                List<string> defaultFormats = new List<string> { "Freezeout", "Deep" };
                TournamentTemplateDataHelper.SetDefaultFormats(defaultFormats);

                // some starter game types
                TournamentTemplateDataHelper.AddGameType("NLHE");
                TournamentTemplateDataHelper.AddGameType("PLO");
                TournamentTemplateDataHelper.AddGameType("PLO8");
                TournamentTemplateDataHelper.AddGameType("FLHE");
                TournamentTemplateDataHelper.SetDefaultGameType("NLHE");

                // some starter venues
                TournamentTemplateDataHelper.AddVenue("Global Poker");
                TournamentTemplateDataHelper.AddVenue("BetOnline");
                TournamentTemplateDataHelper.AddVenue("Americas Cardroom");
                TournamentTemplateDataHelper.AddVenue("Ignition");
                TournamentTemplateDataHelper.SetDefaultVenue("Global Poker");

                // some starter tournaments
                CreateDefaultTemplates();
            }

            // reload templates
            menuViewModel.UpdateFavoriteTemplates();

            // find if quick results view is open
            if (Application.Current.Windows.OfType<QuickResultsView>().FirstOrDefault()?.DataContext is QuickResultsViewModel quickResultsViewModel) quickResultsViewModel.Update(tournaments);

            // find if template manager is open and reload
            if (Application.Current.Windows.OfType<TemplateManagerView>().FirstOrDefault()?.DataContext is TemplateManagerViewModel templateManagerViewModel) templateManagerViewModel.Reload();

            // find if session manager is open and reload
            if (Application.Current.Windows.OfType<SessionManagerView>().FirstOrDefault()?.DataContext is SessionManagerViewModel sessionManagerViewModel) sessionManagerViewModel.Reload();

            // find if session results view is open
            if (Application.Current.Windows.OfType<SessionResultsView>().FirstOrDefault()?.DataContext is SessionResultsViewModel sessionResultsViewModel) sessionResultsViewModel.Update(tournaments);

            // find if tournament results view is open
            if (Application.Current.Windows.OfType<TournamentsResultsView>().FirstOrDefault()?.DataContext is TournamentsResultsViewModel tournamentsResultsViewModel) tournamentsResultsViewModel.Update(tournaments);

            // get any open tournaments view windows
            var tournamentsViewWindows = Application.Current.Windows.OfType<TournamentsView>();

            // close all the windows
            if (tournamentsViewWindows is not null) foreach (var window in tournamentsViewWindows.ToList()) window.Close();

            // get any open create tournament filter windows
            var tournamentFilterWindows = Application.Current.Windows.OfType<CreateTournamentFilterView>();

            // close all the windows
            if (tournamentFilterWindows is not null) foreach (var window in tournamentFilterWindows.ToList()) window.Close();

            // get any open tournament profit graphs
            var profitWindows = Application.Current.Windows.OfType<TournamentProfitGraphView>();

            // close all the windows
            if (profitWindows is not null) foreach (var window in profitWindows.ToList()) window.Close();

            // get any open tournament profit no label graphs
            var noLabelProfitWindows = Application.Current.Windows.OfType<TournamentProfitGraphNoLabelView>();

            // close all the windows
            if (noLabelProfitWindows is not null) foreach (var window in noLabelProfitWindows.ToList()) window.Close();

            // get any open session profit graphs
            var sessionProfitWindows = Application.Current.Windows.OfType<SessionProfitGraphView>();

            // close all the windows
            if (sessionProfitWindows is not null) foreach (var window in sessionProfitWindows.ToList()) window.Close();

            // get any open session profit no label graphs
            var sessionNoLabelProfitWindows = Application.Current.Windows.OfType<SessionProfitGraphNoLabelView>();

            // close all the windows
            if (sessionNoLabelProfitWindows is not null) foreach (var window in sessionNoLabelProfitWindows.ToList()) window.Close();

            // get any open finish position graphs
            var finishWindows = Application.Current.Windows.OfType<FinishPositionGraphView>();

            // close all the windows
            if (finishWindows is not null) foreach (var window in finishWindows.ToList()) window.Close();

            // get any open tournament buy-in/roi graphs
            var buyinRoiWindows = Application.Current.Windows.OfType<TournamentBuyinRoiHistogramView>();

            // close all the windows
            if (buyinRoiWindows is not null) foreach (var window in buyinRoiWindows.ToList()) window.Close();

            // get any open tournament format/roi graphs
            var formatRoiWindows = Application.Current.Windows.OfType<TournamentFormatRoiChartView>();

            // close all the windows
            if (formatRoiWindows is not null) foreach (var window in formatRoiWindows.ToList()) window.Close();

            // get any open tournament game type/roi graphs
            var gameTypeRoiWindows = Application.Current.Windows.OfType<TournamentGameTypeRoiChartView>();

            // close all the windows
            if (gameTypeRoiWindows is not null) foreach (var window in gameTypeRoiWindows.ToList()) window.Close();

            // get any open tournament venue/roi graphs
            var venueRoiWindows = Application.Current.Windows.OfType<TournamentVenueRoiChartView>();

            // close all the windows
            if (venueRoiWindows is not null) foreach (var window in venueRoiWindows.ToList()) window.Close();

            // close profiles window
            Cancel(null);
        }
    }
}
