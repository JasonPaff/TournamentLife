using System.Globalization;
using LiveTracker.Helpers;
using LiveTracker.Models.Sessions;
using Syncfusion.Windows.Shared;
using LiveTracker.Models.Tournaments;
using System.Collections.ObjectModel;
using LiveTracker.Models;

namespace LiveTracker.ViewModels.Results
{
    public class QuickResultsViewModel : NotificationObject
    {
        public QuickResultsViewModel()
        {
            // font size
            FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));

            // visual theme
            Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");

            // update results window
            Update(DatabaseHelper.LoadDatabase());
        }

        public int FontSize { get; set; }
        public string Hours
        {
            get
            {
                if (SessionStats is null) return "0";
                return SessionStats.TotalTime.TotalHours.ToString("N2");
            }
        }
        public string Theme { get; set; }
        public string TimePlayed
        {
            get
            {
                if (SessionStats is null) return "0 days 0 hours 0 minutes";

                if (SessionStats.TotalTime.Days > 0)
                    return string.Format(new CultureInfo("en-US"), "{0} day(s) {1} hour(s) {2} minute(s)", SessionStats.TotalTime.Days, SessionStats.TotalTime.Hours, SessionStats.TotalTime.Minutes);
                else
                    return string.Format(new CultureInfo("en-US"), "{0} hour(s), {1} minute(s)", SessionStats.TotalTime.Hours, SessionStats.TotalTime.Minutes);
            }
        }
        public string Title { get; set; }
        public SessionStats SessionStats { get; set; }
        public TournamentStats TournamentStats { get; set; }

        /// <summary>
        /// update stats
        /// </summary>
        public void Update(ObservableCollection<TournamentFinished> tournaments)
        {
            // session stats
            SessionStats = new SessionStats(tournaments);

            // tournament stats
            TournamentStats = new TournamentStats(tournaments);

            // update title
            Title = "Results Overview - " + ProfileHelper.GetCurrentProfile();
        }
    }
}