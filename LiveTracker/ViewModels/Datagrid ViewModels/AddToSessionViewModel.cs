using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Syncfusion.UI.Xaml.Utility;
using Syncfusion.Windows.Shared;
using Tournament_Life.Commands;
using Tournament_Life.Helpers;
using Tournament_Life.Models.Sessions;
using Tournament_Life.Models.Tournaments;
using Tournament_Life.Views;
using Tournament_Life.Views.Datagrid_Views;

namespace Tournament_Life.ViewModels.Datagrid_ViewModels
{
    public class AddToSessionViewModel : NotificationObject
    {
        private TournamentRunning _tournament;
        private ObservableCollection<SessionTemplate> _sessions;

        public AddToSessionViewModel(TournamentRunning tournament)
        {
            _tournament = tournament;

            LoadPreferences();

            CreateSessionListBoxItems();
        }

        public int FontSize { get; set; }
        public string Theme { get; set; }
        public ObservableCollection<SessionListBoxItem> SessionList { get; set; }
        public ICommand SaveCommand => new BaseCommand(Save);
        public ICommand CloseCommand => new BaseCommand(CloseWindow);

        /// <summary>
        /// Adds the tournament to the selected sessions
        /// </summary>
        private void AddTournamentToSessions()
        {
            foreach (var item in SessionList.Where(i => i.IsSelected))
            {
                // Find selected session
                var session = _sessions.FirstOrDefault(i => i.SessionId == item.SessionId);

                // Add tournament to session
                session.AddTemplateToSession(_tournament.TemplateId);

                // Update session file
                SessionTemplateHelper.ReplaceSession(session);
            }
        }

        /// <summary>
        /// Close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void CloseWindow(object parameter)
        {
            Application.Current.Windows.OfType<AddToSessionView>().FirstOrDefault()?.Close();
        }

        /// <summary>
        /// Show confirmation before saving
        /// </summary>
        private bool ConfirmSave()
        {
            var sessionNames = "";

            // create string from selected session names
            foreach (var temp in SessionList.Where(i => i.IsSelected)) sessionNames += temp.Name + "\n";

            var vm = new YesNoViewModel($"Are you sure you want to add \n\n {_tournament.TournamentName}\n\nto the session below?\n\n" + sessionNames, "Add To Session");
            if (SessionList.Count(i => i.IsSelected) > 1) vm = new YesNoViewModel("Are you sure you want to add \n\n" + _tournament.TournamentName + "\n\nto the sessions below?\n\n" + sessionNames, "Add To Sessions");

            var window = new YesNoView(vm)
            {
                Owner = Application.Current.Windows.OfType<AddToSessionView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            window.ShowDialog();

            if (vm.Saved) return true;

            return false;
        }

        /// <summary>
        /// Create the list box items for all the sessions
        /// </summary>
        private void CreateSessionListBoxItems()
        {
            // Grab all saved sessions from the sessions file
            _sessions = SessionTemplateHelper.LoadSessionTemplates(MenuCommands.GetMenuViewModel().Templates);

            SessionList = new ObservableCollection<SessionListBoxItem>();

            foreach (var session in _sessions)
            {
                var item = new SessionListBoxItem()
                {
                    Description = session.Description,
                    IsSelected = false,
                    Name = session.SessionName,
                    SessionId = session.SessionId
                };

                // Tag sessions that already contain the tournament
                if (session.TemplateIds.Any(i => i == _tournament.TemplateId))
                    item.DisplayString = session.SessionName + " - Already in Session";
                else
                    item.DisplayString = session.SessionName;

                SessionList.Add(item);
            }
        }

        /// <summary>
        /// Load initial preferences
        /// </summary>
        private void LoadPreferences()
        {
            FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));

            Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");
        }

        /// <summary>
        /// Save and close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Save(object parameter)
        {
            if (SessionList.Any(i => i.IsSelected) is false || ConfirmSave() is false) return;

            AddTournamentToSessions();

            CloseWindow(null);
        }
    }
}
