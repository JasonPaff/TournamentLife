using Tournament_Life.Views;
using Tournament_Life.Views.Menu_Views;
using Tournament_Life.Views.Session_Manager_Views;
using Tournament_Life.Views.Template_Manager_Views;
using Syncfusion.UI.Xaml.Utility;
using Syncfusion.Windows.Shared;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Tournament_Life.Factories;
using Tournament_Life.Helpers;
using Tournament_Life.Models.Tournaments;
using Tournament_Life.ViewModels.Menu_ViewModels;
using Tournament_Life.ViewModels.Session_Manager_ViewModels;
using Tournament_Life.ViewModels.Template_Manager_ViewModels;
using Tournament_Life.Views.Datagrid_Views;

namespace Tournament_Life.ViewModels.Datagrid_ViewModels
{
    public class CreateDuplicateViewModel : NotificationObject
    {
        bool _closeAfterStartMenuOptionIsChecked;
        bool _saveNewTournamentMenuOptionIsChecked;
        bool _showStartTournamentMenuOptionIsChecked;

        public ICommand CloseCommand => new BaseCommand(Close);
        public ICommand OptionsCommand => new BaseCommand(OptionsTaxi);
        public ICommand StartCommand => new BaseCommand(Start);

        public CreateDuplicateViewModel(TournamentRunning tournamentData)
        {
            // null check
            if (tournamentData is null) return;

            // initialize the quick start preferences
            InitializePreferences();

            // load starting data using passed in tournament data
            TournamentData = new TournamentRunning(tournamentData);

            // set to non-template tournament
            TournamentData.TemplateId = -1;

            // favorite flag to false
            TournamentData.IsFavorite = false;

            // title
            Title = "Duplicate Tournament";

            // load formats string into formats
            Formats = TournamentData.FormatString;

            // load starting date and time
            StartDate = DateTime.Now;
            StartTime = TournamentData.StartTime;

            // check sng status
            if (TournamentData.IsSng) SngCheckBox = true;

            // check fixed bounty status
            if (TournamentData.IsBovadaBounty) BovadaCheckBox = true;
        }

        public bool BovadaCheckBox { get; set; }
        public string CloseAfterStartMenuOptionHeaderText { get; set; }
        public bool CloseAfterStartMenuOptionIsChecked { get; set; }
        public bool CloseAfterStartMenuOptionVisibility { get; set; }
        public int FontSize { get; set; }
        public string Formats { get; set; }
        public bool Saved { get; set; } = false;
        public bool SaveNewTemplateCheckBox { get; set; }
        public string SaveNewTournamentMenuOptionHeaderText { get; set; }
        public bool SaveNewTournamentMenuOptionIsChecked { get; set; }
        public string ShowStartTournamentMenuOptionHeaderText { get; set; }
        public bool ShowStartTournamentMenuOptionIsChecked { get; set; }
        public bool SngCheckBox { get; set; }
        public string StartButtonText { get; set; } = "Start";
        public string StartButtonToolTipText { get; set; } = "Start the tournament";
        public DateTime StartDate { get; set; }
        public DateTime StartTime { get; set; }
        public string Theme { get; set; }
        public bool ThisWindow { get; set; }
        public string Title { get; set; }
        public TournamentRunning TournamentData { get; set; }

        /// <summary>
        /// Cancel any note text and close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Close(object parameter)
        {
            // find window
            GetWindow()?.Close();
        }

        /// <summary>
        /// close after start option
        /// </summary>
        private void CloseAfterStartMenuOption()
        {
            // pull the property value from the preferences file
            _closeAfterStartMenuOptionIsChecked = !bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "CloseCreateCopyAfterStartOption"));

            // update preference file
            PreferencesHelper.UpdatePreference("LiveTracker", "Window", "CloseCreateCopyAfterStartOption", _closeAfterStartMenuOptionIsChecked);

            // start button text if close quick start option is checked
            if (_closeAfterStartMenuOptionIsChecked is false) { StartButtonText = "Start & Close"; StartButtonToolTipText = "Start this tournament and close the window"; } else { StartButtonText = "Start"; StartButtonToolTipText = "Start the tournament"; }
        }

        /// <summary>
        /// gets this window
        /// </summary>
        private CreateDuplicateView GetWindow()
        {
            // holds our window
            CreateDuplicateView view = null;

            // get all tournament profit graphs
            var windows = Application.Current.Windows.OfType<CreateDuplicateView>();


            // loop through all graphs
            foreach (var window in windows)
            {
                // get view model of window
                var vm = window.DataContext as CreateDuplicateViewModel;

                // flag this chart in view model
                ThisWindow = true;

                // store window with flag and remove flag from this chart
                if (vm.ThisWindow) { view = window; ThisWindow = false; break; }
            }

            // return this window
            return view;
        }

        /// <summary>
        /// initialize the quick start preferences
        /// </summary>
        private void InitializePreferences()
        {
            // load preferences from file
            _closeAfterStartMenuOptionIsChecked = bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "CloseCreateCopyAfterStartOption"));
            _saveNewTournamentMenuOptionIsChecked = bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "SaveNewTournamentCreateCopyOption"));
            _showStartTournamentMenuOptionIsChecked = bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "ShowStartTournamentCreateCopyOption"));
            SaveNewTemplateCheckBox = bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "SaveNewTournamentCreateCopyOption"));
            CloseAfterStartMenuOptionIsChecked = bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "CloseCreateCopyAfterStartOption"));
            CloseAfterStartMenuOptionHeaderText = "Keep the Duplicate Tournament window open after starting a tournament";
            SaveNewTournamentMenuOptionIsChecked = bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "SaveNewTournamentCreateCopyOption"));
            ShowStartTournamentMenuOptionIsChecked = bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "ShowStartTournamentCreateCopyOption"));

            // start button text if close quick start option is checked
            if (_closeAfterStartMenuOptionIsChecked is false) { StartButtonText = "Start & Close"; StartButtonToolTipText = "Start the tournament and close this window"; }

            // menu option header text
            SaveNewTournamentMenuOptionHeaderText = "Keep the Save to Tournaments File option selected by default";
            ShowStartTournamentMenuOptionHeaderText = "Show a confirmation message before starting tournaments";

            // font size
            FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));

            // visual theme
            Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");

            // set title
            Title = "Quick Start Tournaments";
        }

        /// <summary>
        /// options for the quick start window
        /// </summary>
        /// <param name="parameter">option to interact with</param>
        private void OptionsTaxi(object parameter)
        {
            switch (parameter as string)
            {
                case "CloseAfterStartOption":
                    CloseAfterStartMenuOption();
                    break;
                case "SaveNewTournamentOption":
                    SaveNewTournamentOption();
                    break;
                case "ShowStartTournamentOption":
                    ShowStartTournamentOption();
                    break;
            }
        }

        /// <summary>
        /// Save new tournament option
        /// </summary>
        private void SaveNewTournamentOption()
        {
            // pull the property value from the preferences file
            _saveNewTournamentMenuOptionIsChecked = !bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "SaveNewTournamentCreateCopyOption"));

            // update preference file
            PreferencesHelper.UpdatePreference("LiveTracker", "Window", "SaveNewTournamentCreateCopyOption", _saveNewTournamentMenuOptionIsChecked);
        }

        /// <summary>
        /// Show start tournament option
        /// </summary>
        private void ShowStartTournamentOption()
        {
            // pull the property value from the preferences file
            _showStartTournamentMenuOptionIsChecked = !bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "ShowStartTournamentCreateCopyOption"));

            // update preference file
            PreferencesHelper.UpdatePreference("LiveTracker", "Window", "ShowStartTournamentCreateCopyOption", _showStartTournamentMenuOptionIsChecked);
        }

        /// <summary>
        /// Save any note text and close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Start(object parameter)
        {
            // null check
            if (TournamentData is null || TournamentData.TournamentName is null) return;

            // trim tournament name
            TournamentData.TournamentName = TournamentData.TournamentName.Trim();

            // check for blank name
            if (TournamentData.TournamentName.Length is 0)
            {
                // ok view model
                var theVm = new OkViewModel("Tournament name can't be blank", "Blank Name");

                // create/show ok window
                var theWindow = new OkView(theVm)
                {
                    Owner = GetWindow(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                // set focus to the name text box
                GetWindow()?.NameTextBox.Focus();

                // exit
                return;
            }

            // check for length over 50
            if (TournamentData.TournamentName.Length > 50)
            {
                // ok view model
                var theVm = new OkViewModel("Tournament name can't be over 50 characters", "Name Too Long");

                // create/show ok window
                var theWindow = new OkView(theVm)
                {
                    Owner = GetWindow(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                // set focus to the name text box
                GetWindow()?.NameTextBox.Focus();

                // exit
                return;
            }

            // copy tournament data into a temp var
            var tournament = new TournamentRunning(TournamentData);

            // confirmation message
            var vm = new YesNoViewModel("Are you sure you want to start this tournament?\n\n" + tournament.Description, "Start Tournament");
            var window = new YesNoView(vm)
            {
                Owner = GetWindow(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            if (_showStartTournamentMenuOptionIsChecked) { window.ShowDialog(); if (vm.Saved is false) return; }

            // update start and end time
            tournament.StartTime = new DateTime(StartDate.Year, StartDate.Month, StartDate.Day, StartTime.Hour, StartTime.Minute, StartTime.Second);
            tournament.EndTime = tournament.StartTime;

            // clear formats
            tournament.Formats.Clear();

            // pull formats from format string and add to tournament formats
            var splitString = Formats.Split(',');
            for (int c = 0; c < splitString.Length; c++) tournament.Formats.Add(splitString[c].Trim());

            // sng check box
            if (SngCheckBox) tournament.IsSng = true;

            // Bovada bounty check box
            if (BovadaCheckBox) tournament.IsBovadaBounty = true;

            // set to non-template tournament
            tournament.TemplateId = -1;

            // favorite flag to false
            tournament.IsFavorite = false;

            // create new tournament so initial time and status gets set correctly
            TournamentData = new TournamentRunning(tournament);

            // add any potential new formats, game types, or venues to the xml file
            TournamentTemplateDataHelper.AddVenue(TournamentData.Venue);
            TournamentTemplateDataHelper.AddGameType(TournamentData.GameType);
            foreach (var format in TournamentData.Formats) TournamentTemplateDataHelper.AddFormat(format);

            // if they didn't save as a new tournament then skip ahead
            if (SaveNewTemplateCheckBox)
            {
                // get new template id number
                TournamentData.TemplateId = TournamentTemplateHelper.GetNewTemplateId();

                // get time zone
                TournamentData.TimeZoneName = TimeZoneInfo.Local.StandardName;

                // save template
                TournamentTemplateHelper.SaveTournamentTemplate(TournamentData);

                // update favorite templates list
                if (Application.Current.Windows.OfType<LiveTrackerWindowView>()?.FirstOrDefault()?.LiveTrackerMenu.DataContext is MenuViewModel menuViewModel) menuViewModel.UpdateFavoriteTemplates();

                // update template manager
                if (Application.Current.Windows.OfType<TemplateManagerView>()?.FirstOrDefault()?.DataContext is TemplateManagerViewModel templateManagerViewModel) templateManagerViewModel.Update();

                // update session manager
                if (Application.Current.Windows.OfType<SessionManagerView>().FirstOrDefault()?.DataContext is SessionManagerViewModel sessionManagerViewModel) sessionManagerViewModel.UpdateTemplates();

                // get select tournaments view model and update templates
                if (Application.Current.Windows.OfType<SelectTemplatesView>().FirstOrDefault()?.DataContext is SelectTemplatesViewModel selectTemplatesViewModel) selectTemplatesViewModel.Reload();
            }

            // start tournament
            TournamentFactory.StartTournaments(TournamentData);

            // flag as saved
            Saved = true;

            // exit
            if (_closeAfterStartMenuOptionIsChecked is false) Close(null);
        }
    }
}
