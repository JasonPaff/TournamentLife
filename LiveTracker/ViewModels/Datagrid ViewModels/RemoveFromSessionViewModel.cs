using System.Linq;
using LiveTracker.Helpers;
using LiveTracker.Views.Datagrid_Views;
using Syncfusion.Windows.Shared;
using System.Windows;
using System.Windows.Input;
using Syncfusion.UI.Xaml.Utility;
using LiveTracker.Models;
using System.Collections.ObjectModel;
using LiveTracker.ViewModels.Menu_ViewModels;
using LiveTracker.Views;
using LiveTracker.Models.Sessions;
using Tournament_Life.ViewModels;
using Tournament_Life.Views;

namespace LiveTracker.ViewModels.Datagrid_ViewModels
{
    public class RemoveFromSessionViewModel : NotificationObject
    {
        private TournamentRunning _template;

        public ICommand SaveCommand => new BaseCommand(Save);
        public ICommand CancelCommand => new BaseCommand(Cancel);

        public RemoveFromSessionViewModel(TournamentRunning template, ObservableCollection<SessionTemplate> sessions)
        {
            // font size
            FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));

            // visual theme
            Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");

            // menu view model (holds templates we need to update)
            if (Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.LiveTrackerMenu?.DataContext is not MenuViewModel menuViewModel) return;

            // copy containing sessions
            Sessions = sessions;

            // create session list box items
            SessionList = new ObservableCollection<SessionListBoxItem>();
            foreach (var session in Sessions)
            {
                var item = new SessionListBoxItem()
                {
                    Description = session.Description,
                    DisplayString = session.SessionName,
                    IsSelected = false,
                    Name = session.SessionName,
                    SessionId = session.SessionId
                };

                SessionList.Add(item);
            }

            // store template
            _template = template;
        }

        public int FontSize { get; set; }
        public string Theme { get; set; }
        public ObservableCollection<SessionTemplate> Sessions { get; set; }
        public ObservableCollection<SessionListBoxItem> SessionList { get; set; }
        public ListBoxItem SelectedSession { get; set; }

        /// <summary>
        /// Cancel and close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Cancel(object parameter)
        {
            Application.Current.Windows.OfType<RemoveFromSessionView>().FirstOrDefault().Close();
        }

        /// <summary>
        /// Save and close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Save(object parameter)
        {
            // zero selected check
            if (SessionList.Any(i => i.IsSelected) is false) return;

            // names of sessions we are removing
            var sessionNames = "";

            // get names of selected sessions
            foreach (var temp in SessionList.Where(i => i.IsSelected)) sessionNames += temp.Name + "\n";

            // yes/no window view model
            var vm = new YesNoViewModel("Are you sure you want to remove \n\n" + _template.TournamentName + "\n\nfrom the sessions below?\n\n" + sessionNames, "Remove From Sessions");

            // multiple sessions message
            if (SessionList.Count(i => i.IsSelected) > 1) vm = new YesNoViewModel("Are you sure you want to remove \n\n" + _template.TournamentName + "\n\nfrom the sessions below?\n\n" + sessionNames, "Remove From Sessions");

            // single session message
            if (SessionList.Count(i => i.IsSelected) is 1) vm = new YesNoViewModel("Are you sure you want tor remove \n\n" + _template.TournamentName + "\n\nfrom the session below?\n\n" + sessionNames, "Remove From Session");

            // create/show yes/no window
            var window = new YesNoView(vm)
            {
                Owner = Application.Current.Windows.OfType<RemoveFromSessionView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            window.ShowDialog();

            // didn't save
            if (vm.Saved is false) return;

            // loop each selected session
            foreach (var item in SessionList.Where(i => i.IsSelected))
            {
                // get session
                var session = Sessions.FirstOrDefault(i => i.SessionId == item.SessionId);

                // remove template from session
                session.RemoveTemplateFromSession(_template.TemplateId);

                // save updated session
                SessionTemplateHelper.ReplaceSession(session);
            }

            // find window
            var win = Application.Current.Windows.OfType<RemoveFromSessionView>().FirstOrDefault();

            // close window
            win?.Close();
        }
    }
}
