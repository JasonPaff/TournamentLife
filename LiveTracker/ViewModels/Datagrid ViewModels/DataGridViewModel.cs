using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using LiveTracker.Enums;
using LiveTracker.Helpers;
using LiveTracker.Models;
using LiveTracker.Views;
using Syncfusion.UI.Xaml.Utility;
using Syncfusion.Windows.Shared;
using LiveTracker.ViewModels.Menu_ViewModels;
using System.Threading.Tasks;
using System.Threading;

namespace LiveTracker.ViewModels.Datagrid_ViewModels
{
    public class DataGridViewModel : NotificationObject
    {
        private TournamentRunning _selectedTournament;
        private ObservableCollection<TournamentRunning> _runningTournaments;
        private TournamentVisibility _visibility;
        private ObservableCollection<TournamentRunning> _visibleTournaments;

        public DataGridViewModel()
        {
            InitializeLiveTrackerPreferences();

            LoadRunningTournaments();

            UpdateTitleBar();

            Task.Run(() => UpdateRunningTournamentStatus());
        }

        public string AddonCountHeader { get; set; }
        public string AddonHeader { get; set; }
        public string BountyHeader { get; set; }
        public string BuyinHeader { get; set; }
        public string CurrencySymbol { get; set; }
        public string EntrantsHeader { get; set; }
        public string EntrantsPaidHeader { get; set; }
        public string FontColor { get; set; }
        public int FontSize { get; set; }
        public string FinishPositionHeader { get; set; }
        public bool IsContextMenuOpen { get; set; }
        public string JackpotSpinMultiplierHeader { get; set; }
        public string PrizeWonHeader { get; set; }
        public string RebuyCountHeader { get; set; }
        public string RebuyHeader { get; set; }
        public ObservableCollection<TournamentRunning> RunningTournaments
        {
            get => _runningTournaments;
            set
            {
                _runningTournaments = value;
                RaisePropertyChanged(nameof(RunningTournaments));
            }
        }
        public TournamentRunning SelectedTournament
        {
            get => _selectedTournament;
            set
            {
                _selectedTournament = value;
                RaisePropertyChanged(nameof(SelectedTournament));
            }
        }
        public bool ShowRowHeader { get; set;}
        public bool SingleRowMode { get; set; }
        public string StartTimeHeader { get; set; }
        public DateTime Time { get; set; }
        public string TotalCostHeader { get; set; }
        public string TournamentNameHeader { get; set; }
        public string UpdateEndTimeNow { get; set; }
        public string UpdateStartTimeNow { get; set; }
        public string VenueHeader { get; set; }
        public TournamentVisibility Visibility
        {
            get => _visibility;
            set
            {
                _visibility = value;
                UpdateTournamentVisibility(null);
                RaisePropertyChanged(nameof(Visibility));
            }
        }
        public ObservableCollection<TournamentRunning> VisibleTournaments
        {
            get => _visibleTournaments;
            set
            {
                _visibleTournaments = value;
                RaisePropertyChanged(nameof(VisibleTournaments));
            }
        }

        /// <summary>
        /// add a tournament to the running tournaments collection
        /// </summary>
        /// <param name="newTournament">tournament to add</param>
        public void AddTournament(TournamentRunning newTournament)
        {
            // add commands to notify about property changes and visibility changes
            newTournament.UpdateVisibleStatus = new BaseCommand(UpdateTournamentVisibility);
            newTournament.UpdateProperties = new BaseCommand(UpdateTournamentProperties);

            // set currency symbol
            newTournament.CurrencySymbol = CurrencySymbol;

            // add to running tournaments
            RunningTournaments.Add(newTournament);

            // save running tournaments to file
            TournamentHelper.SaveRunningTournaments(RunningTournaments.ToList());

            // update visibility
            UpdateTournamentVisibility(null);
        }

        /// <summary>
        /// add a tournament to the running tournaments collection
        /// </summary>
        /// <param name="newTournament">tournament to add</param>
        public void AddTournament(List<TournamentRunning> newTournaments)
        {
            // loop through new tournaments
            foreach(var newTournament in newTournaments)
            {
                // add commands to notify about property changes and visibility changes
                newTournament.UpdateVisibleStatus = new BaseCommand(UpdateTournamentVisibility);
                newTournament.UpdateProperties = new BaseCommand(UpdateTournamentProperties);

                // set currency symbol
                newTournament.CurrencySymbol = CurrencySymbol;

                // add to running tournaments
                RunningTournaments.Add(newTournament);
            }

            // update visibility
            UpdateTournamentVisibility(null);

            // save running tournaments to file
            TournamentHelper.SaveRunningTournaments(RunningTournaments.ToList());
        }

        /// <summary>
        /// returns the list of visible tournaments in the running tournaments list
        /// </summary>
        /// <returns>list of visible tournaments</returns>
        private IEnumerable<TournamentRunning> GetVisibleTournamentsFromRunningTournaments()
        {
            // temp collection
            var tournaments = new ObservableCollection<TournamentRunning>();

            // null/zero check
            if (RunningTournaments is null || RunningTournaments.Count is 0) return tournaments;

            // sort running tournaments based on visibility status (tournaments handle changing their own status)
            switch (Visibility)
            {
                case TournamentVisibility.ShowAll:
                    tournaments = RunningTournaments;
                    break;
                case TournamentVisibility.ShowStarted:
                    tournaments = new ObservableCollection<TournamentRunning>(RunningTournaments.Where(i =>
                        i.TournamentStartingVisibility is TournamentVisibility.ShowStarted));
                    break;
                case TournamentVisibility.ShowFive:
                    tournaments = new ObservableCollection<TournamentRunning>(RunningTournaments.Where(i =>
                        i.TournamentStartingVisibility is TournamentVisibility.ShowStarted ||
                        i.TournamentStartingVisibility is TournamentVisibility.ShowFive));
                    break;
                case TournamentVisibility.ShowFifteen:
                    tournaments = new ObservableCollection<TournamentRunning>(RunningTournaments.Where(i =>
                        i.TournamentStartingVisibility is TournamentVisibility.ShowStarted ||
                        i.TournamentStartingVisibility is TournamentVisibility.ShowFive ||
                        i.TournamentStartingVisibility is TournamentVisibility.ShowFifteen));
                    break;
                case TournamentVisibility.ShowThirty:
                    tournaments = new ObservableCollection<TournamentRunning>(RunningTournaments.Where(i =>
                        i.TournamentStartingVisibility is TournamentVisibility.ShowStarted ||
                        i.TournamentStartingVisibility is TournamentVisibility.ShowFive ||
                        i.TournamentStartingVisibility is TournamentVisibility.ShowFifteen ||
                        i.TournamentStartingVisibility is TournamentVisibility.ShowThirty));
                    break;
                case TournamentVisibility.ShowSixty:
                    tournaments = new ObservableCollection<TournamentRunning>(RunningTournaments.Where(i =>
                        i.TournamentStartingVisibility is TournamentVisibility.ShowStarted ||
                        i.TournamentStartingVisibility is TournamentVisibility.ShowFive ||
                        i.TournamentStartingVisibility is TournamentVisibility.ShowFifteen ||
                        i.TournamentStartingVisibility is TournamentVisibility.ShowThirty ||
                        i.TournamentStartingVisibility is TournamentVisibility.ShowSixty));
                    break;
                case TournamentVisibility.Queued:
                    tournaments = new ObservableCollection<TournamentRunning>(RunningTournaments.Where(i =>
                        i.TournamentStartingVisibility is TournamentVisibility.Queued));
                    break;
            }

            // return tournaments
            return tournaments;
        }

        /// <summary>
        /// Load initial window preferences
        /// </summary>
        public void InitializeLiveTrackerPreferences()
        {
            // starting visibility
            Visibility = (TournamentVisibility)int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Menu", "TournamentVisibility"));

            // column headers
            if (bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGrid", "ShortColumnHeaders")))
            {
                AddonCountHeader = "A";
                AddonHeader = "A$";
                BountyHeader = "B";
                BuyinHeader = "B$";
                EntrantsHeader = "E";
                EntrantsPaidHeader = "EP";
                FinishPositionHeader = "F";
                JackpotSpinMultiplierHeader = "J";
                PrizeWonHeader = "P";
                RebuyCountHeader = "R";
                RebuyHeader = "R$";
                StartTimeHeader = "S";
                TotalCostHeader = "T$";
                TournamentNameHeader = "N";
                VenueHeader = "V";
            }
            else
            {
                AddonCountHeader = "Addon";
                AddonHeader = "Addon Cost";
                BountyHeader = "Bounties";
                BuyinHeader = "Buyin Cost";
                EntrantsHeader = "Entrants";
                EntrantsPaidHeader = "# Paid";
                FinishPositionHeader = "Finish";
                JackpotSpinMultiplierHeader = "Jackpot";
                PrizeWonHeader = "Prize Won";
                RebuyCountHeader = "Rebuy(s)";
                RebuyHeader = "Rebuy Cost";
                StartTimeHeader = "Start Time";
                TotalCostHeader = "Total Cost";
                TournamentNameHeader = "Tournament Name";
                VenueHeader = "Venue";
            }

            // show tournament counter in first row
            ShowRowHeader = bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGrid", "ShowRowHeaders"));

            // show running/visible tournaments in a single row with a combo box to change the visible one
            SingleRowMode = bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGrid", "SingleRowMode"));

            //// show dollar sign or sweeps coins
            if (bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "ShowSweeps")) is true) CurrencySymbol = "SC ";
            if (bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "ShowSweeps")) is false) CurrencySymbol = "$";

            // font preferences
            FontColor = PreferencesHelper.FindPreference("LiveTracker", "Window", "FontColor");
            FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));
        }

        /// <summary>
        /// Load tournaments that were running when you closed the program last, if any
        /// </summary>
        public void LoadRunningTournaments()
        {
            // initialize collection
            RunningTournaments = new ObservableCollection<TournamentRunning>();

            // load running tournaments from file
            var tournaments = TournamentHelper.LoadRunningTournaments();

            // null/zero check
            if(tournaments is null || tournaments.Count is 0) return;

            // add tournaments
            foreach(var tournament in tournaments)
            {
                tournament.CurrencySymbol = CurrencySymbol;
                AddTournament(tournament);
            }
        }

        /// <summary>
        /// Save running tournaments to xml file
        /// </summary>
        public void SaveRunningTournaments()
        {
            // save running tournaments data to file
            TournamentHelper.SaveRunningTournaments(new List<TournamentRunning>(RunningTournaments));
        }

        /// <summary>
        /// update running tournaments if templates were changed
        /// </summary>
        public void UpdateRunningTournaments()
        {
            // menu view model
            if (Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.LiveTrackerMenu?.DataContext is not MenuViewModel menuViewModel) return;

            // null/zero check
            if (menuViewModel.Templates is null || menuViewModel.Templates.Count is 0) return;

            // null/zero check
            if (RunningTournaments is null || RunningTournaments.Count is 0) return;

            // update running tournaments favorite status in case it changed
            foreach (var tournament in RunningTournaments)
            {
                // no matching template, set favorite to false, set id to -1, and go next
                if (menuViewModel.Templates.Any(i => i.TemplateId == tournament.TemplateId) is false)
                {
                    tournament.TemplateId = -1;
                    tournament.IsFavorite = false;
                    continue;
                }

                // set tournaments favorite status to templates favorite status
                tournament.IsFavorite = menuViewModel.Templates.FirstOrDefault(i => i.TemplateId == tournament.TemplateId).IsFavorite;

                // save running tournaments
                SaveRunningTournaments();
            }
        }

        /// <summary>
        /// Update strings used on update time context menu
        /// </summary>
        public void UpdateTimeStrings()
        {
            // current time
            Time = DateTime.Now;

            // update context menu time strings
            UpdateStartTimeNow = "Update Starting Time to " + Time.ToString("h:mm tt", new CultureInfo("en-US"));
            UpdateEndTimeNow = "Update Ending Time to " + Time.ToString("h:mm tt", new CultureInfo("en-US"));
        }

        /// <summary>
        /// updates the title bar
        /// </summary>
        public void UpdateTitleBar()
        {
            // live tracker view model
            if(Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.DataContext is not LiveTrackerViewModel liveTrackerViewModel) return;

            // tell live tracker window to update its title
            liveTrackerViewModel.UpdateTitle();
        }

        /// <summary>
        /// updates running tournaments status
        /// </summary>
        private void UpdateRunningTournamentStatus()
        {
            // loop from program start until program end
            while (true)
            {
                // pause for 5 seconds before every loop
                Thread.Sleep(5000);

                // perform any tournament status updates
                if(RunningTournaments is not null && RunningTournaments.Count is not 0)
                    foreach (var tournament in RunningTournaments)
                        tournament.UpdateTournamentStatus();
            }
        }

        /// <summary>
        /// Called by the running tournaments to let us know when their visibility changes
        /// </summary>
        /// <param name="parameter">not used</param>
        public void UpdateTournamentProperties(object parameter)
        {
            // save running tournaments data to file
            TournamentHelper.SaveRunningTournaments(new List<TournamentRunning>(RunningTournaments));
        }

        /// <summary>
        /// Called by the running tournaments to let us know their visibility has changed
        /// </summary>
        /// <param name="parameter">not used</param>
        public void UpdateTournamentVisibility(object parameter)
        {
            // load running tournaments into visible tournaments sorted by start time
            VisibleTournaments = new ObservableCollection<TournamentRunning>(GetVisibleTournamentsFromRunningTournaments().OrderBy(i => i.StartTime));
        }
    }
}