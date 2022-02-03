using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using Syncfusion.Windows.Shared;
using Tournament_Life.Helpers;
using Tournament_Life.Models.Bankroll_Model;
using Tournament_Life.Models.Sessions;
using Tournament_Life.Models.Tournaments;
using Tournament_Life.ViewModels.Datagrid_ViewModels;
using Tournament_Life.ViewModels.Results;
using Tournament_Life.Views;
using Tournament_Life.Views.Results;

namespace Tournament_Life.ViewModels.Menu_ViewModels
{
    public class MenuViewModel : NotificationObject
    {
        private bool _isSweepsCoinChecked;
        private bool _isKeepWindowOnTopChecked;

        private ObservableCollection<TournamentTemplate> _favoriteTemplates;
        private ObservableCollection<SessionTemplate> _favoriteSessions;
        private ObservableCollection<TournamentRunning> _recentlyFinishedTournaments;
        private ObservableCollection<Bankroll> _bankrolls;

        public MenuViewModel()
        {
            UpdateFavoriteTemplates();
            UpdateFavoriteSessions();
            LoadBankrolls(DatabaseHelper.LoadDatabase());
            LoadRecentlyFinishedTournaments();
            LoadPreferences();
        }

        public string BankrollTotalHeader { get; set; }
        public string FontColor { get; set; }
        public int FontSize { get; set; }
        public bool IsKeepWindowOnTopChecked
        {
            get => _isKeepWindowOnTopChecked;
            set
            {
                _isKeepWindowOnTopChecked = value;
                RaisePropertyChanged(nameof(IsKeepWindowOnTopChecked));
            }
        }
        public bool IsSweepsCoinChecked
        {
            get => _isSweepsCoinChecked;
            set
            {
                _isSweepsCoinChecked = value;
                RaisePropertyChanged(nameof(IsSweepsCoinChecked));
            }
        }

        public ObservableCollection<Bankroll> Bankrolls
        {
            get => _bankrolls;
            set
            {
                _bankrolls = value;
                RaisePropertyChanged(nameof(Bankrolls));
            }
        }
        public ObservableCollection<SessionTemplate> FavoriteSessions
        {
            get => _favoriteSessions;
            set
            {
                _favoriteSessions = value;
                RaisePropertyChanged(nameof(FavoriteSessions));
            }
        }
        public ObservableCollection<TournamentTemplate> FavoriteTemplates
        {
            get => _favoriteTemplates;
            set
            {
                _favoriteTemplates = value;
                RaisePropertyChanged(nameof(FavoriteTemplates));
            }
        }
        public ObservableCollection<SessionTemplate> Sessions { get; set; }
        public ObservableCollection<TournamentTemplate> Templates { get; set; }
        public ObservableCollection<TournamentRunning> RecentlyFinishedTournaments
        {
            get => _recentlyFinishedTournaments;
            set
            {
                _recentlyFinishedTournaments = value;
                RaisePropertyChanged(nameof(RecentlyFinishedTournaments));
            }
        }

        /// <summary>
        /// Add new tournament to the recently finished tournaments list
        /// </summary>
        /// <param name="tournament">tournament to add to the list</param>
        public void AddRecentlyFinished(TournamentRunning tournament)
        {
            // check for max and remove first if at max
            if(RecentlyFinishedTournaments.Count >= int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Menu", "RecentlyFinishedTournamentsMax"))) RecentlyFinishedTournaments.RemoveAt(0);

            // add tournament to list
            RecentlyFinishedTournaments.Add(tournament);

            // save list to file
            SaveRecentlyFinishedTournaments();
        }

        /// <summary>
        /// Load bankrolls from xml file
        /// </summary>
        public void LoadBankrolls(ObservableCollection<TournamentFinished> tournaments)
        {
            Bankrolls = new ObservableCollection<Bankroll>(BankrollHelper.LoadBankrolls());

            foreach(var bankroll in Bankrolls)
            {
                List<TournamentFinished> tournamentsFinished = tournaments.Where(i => i.Venue.Equals(bankroll.Venue, StringComparison.OrdinalIgnoreCase)).ToList();

                if (tournamentsFinished.Count is not 0) bankroll.TournamentProfit = (double) tournamentsFinished.Sum(i => i.Profit);

                bankroll.UpdateTotal();
            }

            UpdateBankrollsHeader();
        }

        /// <summary>
        /// Load initial preference values
        /// </summary>
        public void LoadPreferences()
        {
            // load preferences
            IsKeepWindowOnTopChecked = bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "KeepWindowOnTop"));
            IsSweepsCoinChecked = bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "ShowSweeps"));
            FontColor = PreferencesHelper.FindPreference("LiveTracker", "Window", "FontColor");
            FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));
        }

        /// <summary>
        /// Load recently finished tournaments from file
        /// </summary>
        public void LoadRecentlyFinishedTournaments()
        {
            // load recently finished tournaments
            RecentlyFinishedTournaments = TournamentHelper.LoadRecentlyFinishedTournaments();
        }

        /// <summary>
        /// Load the sessions from file
        /// </summary>
        private void LoadSessions()
        {
            // load sessions from templates
            Sessions = SessionTemplateHelper.LoadSessionTemplates(Templates);
        }

        /// <summary>
        /// Load the templates from file
        /// </summary>
        private void LoadTemplates()
        {
            // load templates from file
            Templates = TournamentTemplateHelper.LoadTemplates();
        }

        /// <summary>
        /// Save the passed in tournament to the recenlty finished tournaments file
        /// </summary>
        /// <param name="tournament">recently finished tournament</param>
        public void SaveRecentlyFinishedTournaments()
        {
            // save recently finished tournaments to file
            TournamentHelper.SaveRecentlyFinishedTournaments(RecentlyFinishedTournaments);
        }

        /// <summary>
        /// Undo a tournament from the recently finished list
        /// </summary>
        /// <param name="databaseId">tournament to undo</param>
        public void UndoRecentlyFinished(TournamentRunning tournament)
        {
            // null check
            if (tournament is null) return;

            // delete from database
            DatabaseHelper.DeleteRecord(tournament.DatabaseId);

            // add tournament to data grid
            if (Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.TournamentsDataGrid.DataContext is DataGridViewModel dataGridViewModel) dataGridViewModel.AddTournament(tournament);

            // remove from recently finished list
            if (RecentlyFinishedTournaments is not null && RecentlyFinishedTournaments.Count is not 0) RecentlyFinishedTournaments.Remove(tournament);

            // save running tournaments file
            SaveRecentlyFinishedTournaments();

            // reload database
            var updatedTournaments = DatabaseHelper.LoadDatabase();

            // update bankrolls
            LoadBankrolls(updatedTournaments);

            // see if quick results window is open and update it
            if (Application.Current.Windows.OfType<QuickResultsView>()?.FirstOrDefault()?.DataContext is QuickResultsViewModel quickResultsViewModel) quickResultsViewModel.Update(updatedTournaments);

            // see if tournament results window is open and update it
            if (Application.Current.Windows.OfType<TournamentsResultsView>()?.FirstOrDefault()?.DataContext is TournamentsResultsViewModel tournamentsResultsViewModel) tournamentsResultsViewModel.Update(updatedTournaments);

            // see if session results window is open and update it
            if (Application.Current.Windows.OfType<SessionResultsView>()?.FirstOrDefault()?.DataContext is SessionResultsViewModel sessionResultsViewModel) sessionResultsViewModel.Update(updatedTournaments);
        }

        /// <summary>
        /// sum all bankrolls for the menu item header
        /// </summary>
        public void UpdateBankrollsHeader()
        {
            // sum of all bankrolls for header
            BankrollTotalHeader = Bankrolls.Sum(i => i.Total).ToString("C2", new CultureInfo("en-US"));
        }

        /// <summary>
        /// Reload Favorite Sessions collection from Sessions
        /// </summary>
        public void UpdateFavoriteSessions()
        {
            // reload sessions from file
            LoadSessions();

            // sort favorite session by name
            FavoriteSessions = new ObservableCollection<SessionTemplate>(Sessions.OrderBy(i => i.SessionName));
        }

        /// <summary>
        /// Reload Favorite Templates collection from Templates
        /// </summary>
        public void UpdateFavoriteTemplates()
        {
            // reload templates from file
            LoadTemplates();

            // sort favorite templates by name
            FavoriteTemplates = new ObservableCollection<TournamentTemplate>(Templates.Where(i => i.IsFavorite is true).OrderBy(i => i.TournamentName));
        }
    }
}