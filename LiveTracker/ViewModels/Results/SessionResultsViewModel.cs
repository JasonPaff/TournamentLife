using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using LiveTracker.Helpers;
using LiveTracker.Models;
using LiveTracker.Models.Sessions;
using LiveTracker.ViewModels.Graph_ViewModels;
using LiveTracker.Views.Graph_Views;
using LiveTracker.Views.Results;
using Syncfusion.UI.Xaml.Utility;
using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels.Graph_ViewModels;
using Tournament_Life.ViewModels.Results;
using Tournament_Life.Views.Results;

namespace LiveTracker.ViewModels.Results
{
    public class SessionResultsViewModel : NotificationObject
    {
        private SessionStats _stats;
        private SessionListBoxItem _selectedSession;
        private ObservableCollection<SessionListBoxItem> _tempList;

        public SessionResultsViewModel()
        {
            // font size
            FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));

            // visual theme
            Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");

            // set currency symbol
            LoadHeaders();

            // get stats
            _stats = new SessionStats();

            // load sessions
            Sessions = new ObservableCollection<SessionModel>(_stats.Sessions.OrderByDescending(i => i.StartTime));

            // create session list box items
            CreateListBoxItems();

            // filter sessions stats
            LoadSessionStats();

            // filter selected session stats
            LoadSelectedSessionStats();

            // copy list over
            SessionList = _tempList;

            // set selected to top of the list
            if (SessionList.Count is not 0) SelectedSession = SessionList[0];

            // update title
            Title = "Session Results - " + ProfileHelper.GetCurrentProfile();
        }

        public string CurrencySymbol { get; set; }
        public int FontSize { get; set; }
        public string MoneyPerTournamentHeader { get; set; }
        public SessionListBoxItem SelectedSession
        {
            get => _selectedSession;
            set
            {
                if(_selectedSession == value) return;

                _selectedSession = value;
                RaisePropertyChanged(nameof(SelectedSession));

                LoadSelectedSessionStats();
            }
        }
        public ObservableCollection<SessionListBoxItem> SessionList { get; set; }
        public ObservableCollection<SessionModel> Sessions { get; set; }
        public string Theme { get; set; }
        public string Title { get; set; }

        #region Stat Variables

        public string SessionsAverageBuyins { get; set; }
        public string SessionsAverageProfit { get; set; }
        public string SessionsAverageTime { get; set; }
        public string SessionsAverageTournaments { get; set; }
        public string DollarsPerTournament { get; set; }
        public string SessionsFieldBeaten { get; set; }
        public string Hourly { get; set; }
        public string SessionsItm { get; set; }
        public string SessionsLeastBuyins { get; set; }
        public string SessionsLeastProfit { get; set; }
        public string SessionsLeastTournaments { get; set; }
        public string SessionsLongestTime { get; set; }
        public string SessionsLosingStreak { get; set; }
        public string SessionsLost { get; set; }
        public string SessionsMostBuyins { get; set; }
        public string SessionsMostProfit { get; set; }
        public string SessionsMostTournaments { get; set; }
        public string SessionsRoi { get; set; }
        public string SessionsPlayed { get; set; }
        public string SessionsTotalPrizes { get; set; }
        public string SessionsMostPrizes { get; set; }
        public string SessionsLeastPrizes { get; set; }
        public string SessionsAveragePrizes { get; set; }
        public string SessionsShortestTime { get; set; }
        public string SessionsTotalBuyins { get; set; }
        public string SessionsTotalProfit { get; set; }
        public string SessionsTotalTime { get; set; }
        public string SessionsTotalTournaments { get; set; }
        public string SessionsWinningStreak { get; set; }
        public string SessionsWon { get; set; }
        public string SelectedAverageCost { get; set; }
        public string SelectedAverageFinish { get; set; }
        public string SelectedAverageLength { get; set; }
        public string SelectedAveragePrizeWon { get; set; }
        public string SelectedAverageProfit { get; set; }
        public string SelectedEarlyMidFinish { get; set; }
        public string SelectedEarlyFinish { get; set; }
        public string SelectedFinalTables { get; set; }
        public string SelectedItmPercentage { get; set; }
        public string SelectedLateFinish { get; set; }
        public string SelectedMidFinish { get; set; }
        public string SelectedMidLateFinish { get; set; }
        public string SelectedMinCashes { get; set; }
        public string SelectedNearBubbles { get; set; }
        public string SelectedProfitPerTournament { get; set; }
        public string SelectedRoi { get; set; }
        public string SelectedTournamentCount { get; set; }
        public string SelectedSessionLength { get; set; }
        public string SelectedStoneBubbles { get; set; }
        public string SelectedTotalCost { get; set; }
        public string SelectedTotalPrizesWon { get; set; }
        public string SelectedTotalProfit { get; set; }

        #endregion

        /// <summary>
        /// change currency symbol for the window
        /// </summary>
        public void ChangeCurrencySymbol()
        {
            LoadHeaders();
            LoadSessionStats();
            LoadSelectedSessionStats();
        }

        /// <summary>
        /// create session list box items
        /// </summary>
        private void CreateListBoxItems()
        {
            // new session list box item collection
            _tempList = new ObservableCollection<SessionListBoxItem>();

            // loop through sessions
            foreach (var session in Sessions)
            {
                // create list box item
                var item = new SessionListBoxItem()
                {
                    IsSelected = false,
                    SessionId = session.ID
                };

                // temp string for the display string
                string temp;

                // session # part of string
                if (session.ID > 0 && session.ID < 10) temp = "Session #0" + session.ID + " -- "; else temp = "Session #" + session.ID + " -- ";

                // time part of string
                temp += session.StartTime.ToString("MM/dd/yyyy hh:mm tt", new CultureInfo("en-US"));
                temp += " to ";
                temp += session.EndTime.ToString("MM/dd/yyyy hh:mm tt", new CultureInfo("en-US"));

                // set string to display string
                item.DisplayString = temp;

                // set tool-tip to the names of the tournaments in the session
                foreach (var t in session.Tournaments) item.Description += "\n" + t.TournamentName;

                // remove blank line from start of description
                item.Description = item.Description.Substring(1);

                // add item to session
                _tempList.Add(item);
            }

            // copy final sessions list
            SessionList = _tempList;
        }

        /// <summary>
        /// Filter
        /// </summary>
        public void Filter()
        {
            // set title
            Title = "Session Results - " + ProfileHelper.GetCurrentProfile() + " - FILTERING";

            // filter
            Task.Run(() => Filtering());
        }

        /// <summary>
        /// Filtering
        /// </summary>
        private void Filtering()
        {
            // set title
            Title = "Session Results - " + ProfileHelper.GetCurrentProfile();
        }

        /// <summary>
        /// load stat headers
        /// </summary>
        public void LoadHeaders()
        {
            if (bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "ShowSweeps"))) { MoneyPerTournamentHeader = "SC/Tournament"; CurrencySymbol="SC "; } else { CurrencySymbol = "$"; MoneyPerTournamentHeader = "$/Tournament"; }
        }

        /// <summary>
        /// Filter the stats from the sessions list
        /// </summary>
        private void LoadSessionStats()
        {
            if (_stats is null)
            {
                SessionsPlayed = "0";
                SessionsWon = "0";
                SessionsLost = "0";
                SessionsWinningStreak = "0";
                SessionsLosingStreak = "0";
                SessionsTotalTournaments = "0";
                SessionsTotalBuyins = CurrencySymbol + "0.00";
                SessionsMostBuyins = CurrencySymbol + "0.00";
                SessionsLeastBuyins = CurrencySymbol + "0.00";
                SessionsAverageBuyins = CurrencySymbol + "0.00";
                SessionsTotalProfit = CurrencySymbol + "0.00";
                SessionsMostProfit = CurrencySymbol + "0.00";
                SessionsLeastProfit = CurrencySymbol + "0.00";
                SessionsAverageProfit = CurrencySymbol + "0.00";
                SessionsTotalPrizes = CurrencySymbol + "0.00";
                SessionsMostPrizes = CurrencySymbol + "0.00";
                SessionsLeastPrizes = CurrencySymbol + "0.00";
                SessionsAveragePrizes = CurrencySymbol + "0.00";
                Hourly = CurrencySymbol + "0.00";
                DollarsPerTournament = CurrencySymbol + "0.00";
                SessionsRoi = "0%";
                SessionsItm = "0%";
                SessionsFieldBeaten = "0%";
                SessionsTotalTournaments = "0";
                SessionsMostTournaments = "0";
                SessionsLeastTournaments = "0";
                SessionsAverageTournaments = "0";
                SessionsTotalTime = string.Format(new CultureInfo("en-US"), "0 hours, 0 minutes");
                SessionsLongestTime = string.Format(new CultureInfo("en-US"), "0 hours, 0 minutes");
                SessionsShortestTime = string.Format(new CultureInfo("en-US"), "0 hours, 0 minutes");
                SessionsAverageTime = string.Format(new CultureInfo("en-US"), "0 hours, 0 minutes");
                return;
            }

            SessionsPlayed = _stats.Played.ToString(new CultureInfo("en-US"));
            SessionsWon = _stats.Won.ToString(new CultureInfo("en-US"));
            SessionsLost = _stats.Lost.ToString(new CultureInfo("en-US"));

            SessionsWinningStreak = _stats.WinningStreak.ToString(new CultureInfo("en-US"));
            SessionsLosingStreak = _stats.LosingStreak.ToString(new CultureInfo("en-US"));

            SessionsTotalTournaments = _stats.TotalTournaments.ToString(new CultureInfo("en-US"));

            SessionsTotalBuyins = _stats.TotalCost.ToString("C2", new CultureInfo("en-US"));
            if (CurrencySymbol == "SC ") SessionsTotalBuyins = SessionsTotalBuyins.Replace("$", "SC ");

            SessionsMostBuyins = _stats.MostCost.ToString("C2", new CultureInfo("en-US"));
            if (CurrencySymbol == "SC ") SessionsMostBuyins = SessionsMostBuyins.Replace("$", "SC ");

            SessionsLeastBuyins = _stats.LeastCost.ToString("C2", new CultureInfo("en-US"));
            if (CurrencySymbol == "SC ") SessionsLeastBuyins = SessionsLeastBuyins.Replace("$", "SC ");

            SessionsAverageBuyins = _stats.AverageCost.ToString("C2", new CultureInfo("en-US"));
            if (CurrencySymbol == "SC ") SessionsAverageBuyins = SessionsAverageBuyins.Replace("$", "SC ");

            SessionsTotalProfit = _stats.TotalProfit.ToString("C2", new CultureInfo("en-US"));
            if (CurrencySymbol == "SC ") SessionsTotalProfit = SessionsTotalProfit.Replace("$", "SC ");

            SessionsMostProfit = _stats.MostProfit.ToString("C2", new CultureInfo("en-US"));
            if (CurrencySymbol == "SC ") SessionsMostProfit = SessionsMostProfit.Replace("$", "SC ");

            SessionsLeastProfit = _stats.LeastProfit.ToString("C2", new CultureInfo("en-US"));
            if (CurrencySymbol == "SC ") SessionsLeastProfit = SessionsLeastProfit.Replace("$", "SC ");

            SessionsAverageProfit = _stats.AverageProfit.ToString("C2", new CultureInfo("en-US"));
            if (CurrencySymbol == "SC ") SessionsAverageProfit = SessionsAverageProfit.Replace("$", "SC ");

            SessionsTotalPrizes = _stats.TotalPrizes.ToString("C2", new CultureInfo("en-US"));
            if (CurrencySymbol == "SC ") SessionsTotalPrizes = SessionsTotalPrizes.Replace("$", "SC ");

            SessionsMostPrizes = _stats.MostPrizes.ToString("C2", new CultureInfo("en-US"));
            if (CurrencySymbol == "SC ") SessionsMostPrizes = SessionsMostPrizes.Replace("$", "SC ");

            SessionsLeastPrizes = _stats.LeastPrizes.ToString("C2", new CultureInfo("en-US"));
            if (CurrencySymbol == "SC ") SessionsLeastPrizes = SessionsLeastPrizes.Replace("$", "SC ");

            SessionsAveragePrizes = _stats.AveragePrizes.ToString("C2", new CultureInfo("en-US"));
            if (CurrencySymbol == "SC ") SessionsAveragePrizes = SessionsAveragePrizes.Replace("$", "SC ");

            Hourly = _stats.Hourly.ToString("C2", new CultureInfo("en-US"));
            if (CurrencySymbol == "SC ") Hourly = Hourly.Replace("$", "SC ");

            DollarsPerTournament = _stats.DollarsPerTournament.ToString("C2", new CultureInfo("en-US"));
            if(CurrencySymbol == "SC ") DollarsPerTournament = DollarsPerTournament.Replace("$", "SC ");

            SessionsRoi = _stats.Roi.ToString("P", new CultureInfo("en-US"));
            SessionsItm = _stats.Itm.ToString("P", new CultureInfo("en-US"));
            SessionsFieldBeaten = _stats.FieldBeaten.ToString("P", new CultureInfo("en-US"));

            SessionsAverageTournaments = _stats.AverageTournaments.ToString("G2", new CultureInfo("en-US"));
            SessionsMostTournaments = _stats.MostTournaments.ToString(new CultureInfo("en-US"));
            SessionsLeastTournaments = _stats.LeastTournaments.ToString(new CultureInfo("en-US"));

            if (_stats.TotalTime.Days > 0) SessionsTotalTime = string.Format(new CultureInfo("en-US"), "{0} day(s), {1} hour(s), {2} minute(s)", _stats.TotalTime.Days, _stats.TotalTime.Hours, _stats.TotalTime.Minutes);
            else SessionsTotalTime = string.Format(new CultureInfo("en-US"), "{0} hour(s), {1} minute(s)", _stats.TotalTime.Hours, _stats.TotalTime.Minutes);

            if (_stats.LongestTime.Days > 0) SessionsLongestTime = string.Format(new CultureInfo("en-US"), "{0} day(s), {1} hour(s), {2} minute(s)", _stats.LongestTime.Days, _stats.LongestTime.Hours, _stats.LongestTime.Minutes);
            else SessionsLongestTime = string.Format(new CultureInfo("en-US"), "{0} hour(s), {1} minute(s)", _stats.LongestTime.Hours, _stats.LongestTime.Minutes);

            if (_stats.ShortestTime.Days > 0) SessionsShortestTime = string.Format(new CultureInfo("en-US"), "{0} day(s), {1} hour(s), {2} minute(s)", _stats.ShortestTime.Days, _stats.ShortestTime.Hours, _stats.ShortestTime.Minutes);
            else SessionsShortestTime = string.Format(new CultureInfo("en-US"), "{0} hour(s), {1} minute(s)", _stats.ShortestTime.Hours, _stats.ShortestTime.Minutes);

            if (_stats.AverageTime.Days > 0) SessionsAverageTime = string.Format(new CultureInfo("en-US"), "{0} day(s), {1} hour(s), {2} minute(s)", _stats.AverageTime.Days, _stats.AverageTime.Hours, _stats.AverageTime.Minutes);
            else SessionsAverageTime = string.Format(new CultureInfo("en-US"), "{0} hour(s), {1} minute(s)", _stats.AverageTime.Hours, _stats.AverageTime.Minutes);

            if (_stats.TotalTime == TimeSpan.Zero)
            {
                SessionsTotalTime = string.Format(new CultureInfo("en-US"), "0 hours, 0 minutes");
                SessionsLongestTime = string.Format(new CultureInfo("en-US"), "0 hours, 0 minutes");
                SessionsShortestTime = string.Format(new CultureInfo("en-US"), "0 hours, 0 minutes");
                SessionsAverageTime = string.Format(new CultureInfo("en-US"), "0 hours, 0 minutes");
            }
        }

        /// <summary>
        /// load stats for the selected session
        /// </summary>
        private void LoadSelectedSessionStats()
        {
            if (SelectedSession is null)
            {
                SelectedTournamentCount = "0";
                SelectedSessionLength = "0 hours, 0 minutes";
                SelectedTotalProfit = CurrencySymbol + "0.00";
                SelectedTotalCost = CurrencySymbol + "0.00";
                SelectedTotalPrizesWon = CurrencySymbol + "0.00";
                SelectedAverageLength = "0 hours, 0 minutes";
                SelectedAverageProfit = CurrencySymbol + "0.00";
                SelectedAverageCost = CurrencySymbol + "0.00";
                SelectedAveragePrizeWon = CurrencySymbol + "0.00";
                SelectedProfitPerTournament = CurrencySymbol + "0.00";
                SelectedItmPercentage = "0%";
                SelectedRoi = "0%";
                SelectedAverageFinish = "0%";
                SelectedLateFinish = "0%";
                SelectedMidLateFinish = "0%";
                SelectedMidFinish = "0%";
                SelectedEarlyMidFinish = "0%";
                SelectedEarlyFinish = "0%";
                SelectedFinalTables = "0";
                SelectedMinCashes = "0";
                SelectedNearBubbles = "0";
                SelectedStoneBubbles = "0";
                return;
            }

            // get selected session
            var selectedSession = Sessions.Where(i => i.ID == SelectedSession.SessionId)?.FirstOrDefault();
            if (selectedSession is null) return;

            SelectedTournamentCount = selectedSession.TournamentCount.ToString(new CultureInfo("en-US"));

            TimeSpan time = selectedSession.SessionLength;
            if (time.Days > 0) SelectedSessionLength = string.Format(new CultureInfo("en-US"), "{0} day(s), {1} hour(s), {2} minute(s)", time.Days, time.Hours, time.Minutes);
            else SelectedSessionLength = string.Format(new CultureInfo("en-US"), "{0} hour(s), {1} minute(s)", time.Hours, time.Minutes);

            SelectedTotalProfit = selectedSession.TotalProfit.ToString("C2", new CultureInfo("en-US"));
            if (CurrencySymbol == "SC ") SelectedTotalProfit = SelectedTotalProfit.Replace("$", "SC ");

            SelectedTotalCost = selectedSession.TotalCost.ToString("C2", new CultureInfo("en-US"));
            if (CurrencySymbol == "SC ") SelectedTotalCost = SelectedTotalCost.Replace("$", "SC ");

            SelectedTotalPrizesWon = selectedSession.PrizesWonTotal.ToString("C2", new CultureInfo("en-US"));
            if (CurrencySymbol == "SC ") SelectedTotalPrizesWon = SelectedTotalPrizesWon.Replace("$", "SC ");

            time = selectedSession.AverageLength;
            if (time.Days > 0) SelectedAverageLength = string.Format(new CultureInfo("en-US"), "{0} day(s), {1} hour(s), {2} minute(s)", time.Days, time.Hours, time.Minutes);
            else SelectedAverageLength = string.Format(new CultureInfo("en-US"), "{0} hour(s), {1} minute(s)", time.Hours, time.Minutes);

            SelectedAverageCost = selectedSession.AverageTotalCost.ToString("C2", new CultureInfo("en-US"));
            if (CurrencySymbol == "SC ") SelectedAverageCost = SelectedAverageCost.Replace("$", "SC ");

            SelectedAveragePrizeWon = selectedSession.AveragePrizeWon.ToString("C2", new CultureInfo("en-US"));
            if (CurrencySymbol == "SC ") SelectedAveragePrizeWon = SelectedAveragePrizeWon.Replace("$", "SC ");

            SelectedAverageProfit = selectedSession.AverageProfitWon.ToString("C2", new CultureInfo("en-US"));
            if (CurrencySymbol == "SC ") SelectedAverageProfit = SelectedAverageProfit.Replace("$", "SC ");

            SelectedProfitPerTournament = selectedSession.ProfitPerTournament.ToString("C2", new CultureInfo("en-US"));
            if (CurrencySymbol == "SC ") SelectedProfitPerTournament = SelectedProfitPerTournament.Replace("$", "SC ");

            SelectedItmPercentage = selectedSession.Itm.ToString("P", new CultureInfo("en-US"));
            SelectedRoi = selectedSession.Roi.ToString("P", new CultureInfo("en-US"));

            SelectedAverageFinish = selectedSession.AveragePercentBeaten.ToString("P", new CultureInfo("en-US"));
            SelectedLateFinish = selectedSession.LatePercentFinish.ToString("P", new CultureInfo("en-US"));
            SelectedMidLateFinish = selectedSession.MidLatePercentFinish.ToString("P", new CultureInfo("en-US"));
            SelectedMidFinish = selectedSession.MidPercentFinish.ToString("P", new CultureInfo("en-US"));
            SelectedEarlyMidFinish = selectedSession.EarlyMidPercentFinish.ToString("P", new CultureInfo("en-US"));
            SelectedEarlyFinish = selectedSession.EarlyPercentFinish.ToString("P", new CultureInfo("en-US"));

            SelectedFinalTables = selectedSession.FinalTables.ToString(new CultureInfo("en-US"));
            SelectedMinCashes = selectedSession.MinCashes.ToString(new CultureInfo("en-US"));
            SelectedNearBubbles = selectedSession.NearBubbles.ToString(new CultureInfo("en-US"));
            SelectedStoneBubbles = selectedSession.StoneBubbles.ToString(new CultureInfo("en-US"));
        }

        /// <summary>
        /// update stats
        /// </summary>
        public void Update()
        {
            // set title
            Title = "Session Results - " + ProfileHelper.GetCurrentProfile() + " - LOADING";

            Updating();
        }

        /// <summary>
        /// update stats using passed in tournaments
        /// </summary>
        /// <param name="tournaments"></param>
        public void Update(ObservableCollection<TournamentFinished> tournaments)
        {
            // get stats
            _stats = new SessionStats(tournaments);

            // load sessions
            Sessions = new ObservableCollection<SessionModel>(_stats.Sessions.OrderByDescending(i => i.StartTime));

            // create session list box items
            CreateListBoxItems();

            // filter sessions stats
            LoadSessionStats();

            // filter selected session stats
            LoadSelectedSessionStats();

            // set selected to top of the list
            if (SessionList.Count is not 0) SelectedSession = SessionList[0];

            // update title
            Title = "Session Results - " + ProfileHelper.GetCurrentProfile();

            // get all tournaments views
            var tournamentWindows = Application.Current.Windows.OfType<TournamentsView>();

            // loop views and update
            foreach (var window in tournamentWindows)
            {
                if (window.DataContext is not TournamentsViewModel vm) continue;

                // update tournaments with update session
                vm.UpdateSession(Sessions.FirstOrDefault(i => i.ID == vm.SessionId));
            }
        }

        /// <summary>
        /// updating
        /// </summary>
        private void Updating()
        {
            // get stats
            _stats = new SessionStats();

            // load sessions
            Sessions = new ObservableCollection<SessionModel>(_stats.Sessions.OrderByDescending(i => i.StartTime));

            // create session list box items
            CreateListBoxItems();

            // filter sessions stats
            LoadSessionStats();

            // filter selected session stats
            LoadSelectedSessionStats();

            // set selected to top of the list
            if (SessionList.Count is not 0) SelectedSession = SessionList[0];

            // update title
            Title = "Session Results - " + ProfileHelper.GetCurrentProfile();

            // get all tournaments views
            var tournamentWindows = Application.Current.Windows.OfType<TournamentsView>();

            // loop views and update
            foreach (var window in tournamentWindows)
            {
                if (window.DataContext is not TournamentsViewModel vm) continue;

                vm.UpdateSession(Sessions.FirstOrDefault(i => i.ID == vm.SessionId));
            }
        }
    }
}