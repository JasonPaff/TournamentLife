using System.Collections.ObjectModel;
using System.Windows.Input;
using LiveTracker.Helpers;
using LiveTracker.Models;
using Syncfusion.UI.Xaml.Utility;
using Syncfusion.Windows.Shared;
using LiveTracker.Models.Tournaments;
using System.Threading.Tasks;
using System.Windows;
using Tournament_Life.Views.Results;
using System.Linq;

namespace Tournament_Life.ViewModels.Results
{
    public class TournamentsResultsViewModel : NotificationObject
    {
        private bool _loading;

        public ICommand AfterLoadCommand => new BaseCommand(Load);
        public ICommand AfterCloseCommand => new BaseCommand(Close);

        public TournamentsResultsViewModel()
        {
            // font size
            FontSize = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));

            // visual theme
            Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");

            // set title
            SetTitle("LOADING");

            // no filter
            CurrentFilter = "All Results";
        }

        public string CurrentFilter { get; set; }
        public string FilterName { get; set; }
        public double FontSize { get; set; }
        public string Theme { get; set; }
        public string Title { get; set; }
        public TournamentStats Stats { get; set; }
        public ObservableCollection<TournamentFinished> Tournaments { get; set; }
        public ObservableCollection<TournamentFinished> UnfilteredTournaments { get; set; }

        /// <summary>
        /// called after window is closed to close the create filter view if open
        /// </summary>
        /// <param name="parameter"></param>
        private void Close(object parameter)
        {
            // get create filter view model
            var vm = Application.Current.Windows.OfType<CreateTournamentFilterView>()?.FirstOrDefault()?.DataContext as CreateTournamentFilterViewModel;

            // view model exists, call cancel command
            if (vm is not null) vm.CancelCommand.Execute(null);
        }

        /// <summary>
        /// Load starting values
        /// </summary>
        private void Load(object parameter)
        {
            // flag loading
            _loading = true;

            // load
            Task.Run(() => Loading());
        }

        /// <summary>
        /// Does the loading
        /// </summary>
        private void Loading()
        {
            // load tournaments
            Tournaments = DatabaseHelper.LoadDatabase();

            // load backup
            UnfilteredTournaments = new ObservableCollection<TournamentFinished>(Tournaments);

            // load tournament stats
            Stats = new TournamentStats(Tournaments);

            // update title
            SetTitle("All Results");

            // remove flag
            _loading = false;
        }

        /// <summary>
        /// update stats
        /// </summary>
        public void Update()
        {
            while (_loading) {}

            // flag loading
            _loading = true;

            // set title
            SetTitle("LOADING");

            // update
            Task.Run(() => Updating());
        }

        /// <summary>
        /// update status using passed in tournaments
        /// </summary>
        /// <param name="tournaments"></param>
        public void Update(ObservableCollection<TournamentFinished> tournaments)
        {
            while (_loading) { }

            // flag loading
            _loading = true;

            // set title
            SetTitle("LOADING");

            // load backup
            UnfilteredTournaments = new ObservableCollection<TournamentFinished>(tournaments);

            // load tournament stats
            Stats = new TournamentStats(tournaments);

            // update title
            SetTitle("All Results");

            // remove flag
            _loading = false;
        }

        /// <summary>
        /// updating
        /// </summary>
        private void Updating()
        {
            // load tournaments
            Tournaments = DatabaseHelper.LoadDatabase();

            // load backup
            UnfilteredTournaments = new ObservableCollection<TournamentFinished>(Tournaments);

            // load tournament stats
            Stats = new TournamentStats(Tournaments);

            // update title
            SetTitle("All Results");

            // remove flag
            _loading = false;
        }

        /// <summary>
        /// filter tournaments
        /// </summary>
        public void Filter()
        {
            while (_loading) {}

            // flag loading
            _loading = true;

            // set title
            SetTitle("FILTERING");

            // filter
            Task.Run(() => Filtering());
        }

        /// <summary>
        /// filtering tournaments
        /// </summary>
        private void Filtering()
        {
            // load tournament stats
            Stats = new TournamentStats(Tournaments);

            // update title
            SetTitle(FilterName);

            // remove flag
            _loading = false;
        }

        /// <summary>
        /// set the title
        /// </summary>
        /// <param name="title"></param>
        public void SetTitle(string text)
        {
            // update title
            Title = $"Tournament Results - {ProfileHelper.GetCurrentProfile()} - {text}";
        }
    }
}