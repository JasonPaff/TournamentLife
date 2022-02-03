using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Syncfusion.UI.Xaml.Utility;
using Syncfusion.Windows.Shared;
using Tournament_Life.Commands;
using Tournament_Life.Enums;
using Tournament_Life.Helpers;
using Tournament_Life.Models;
using Tournament_Life.Models.Tournaments;
using Tournament_Life.Views;
using Tournament_Life.Views.Session_Manager_Views;

namespace Tournament_Life.ViewModels.Session_Manager_ViewModels
{
    public class SessionListViewModel : NotificationObject
    {
        bool _closeAfterSave;
        bool _confirmSave;
        public SessionManagerMode _mode;

        public ICommand CloseCommand => new BaseCommand(CloseWindow);
        public ICommand SaveCommand => new BaseCommand(Save);

        public SessionListViewModel(ObservableCollection<TournamentTemplate> templates, SessionManagerMode mode)
        {
            Init(templates, mode, true, true);
        }
        public SessionListViewModel(ObservableCollection<TournamentTemplate> templates, SessionManagerMode mode, bool closeOnEnter = true)
        {
            Init(templates, mode, closeOnEnter, true);
        }

        public string SaveButtonText { get; set; }
        public string SaveButtonToolTipText { get; set; }
        public string FontColor { get; set; }
        public int FontSize { get; set; }
        public bool Saved { get; set; }
        public ObservableCollection<ListBoxItem> SessionList { get; set; }
        public ListBoxItem SelectedSession { get; set; }
        public string Theme { get; set; }
        public string Title { get; set; }

        /// <summary>
        /// checks session manager to see if session name and session list are blank
        /// </summary>
        private bool CheckForSession()
        {
            SessionManagerViewModel sessionManagerViewModel = SessionManagerCommands.GetSessionManagerViewModel();

            if (sessionManagerViewModel.SessionName.Length is not 0 || sessionManagerViewModel.SessionList.Count is not 0)
                return true;

            return false;
        }

        /// <summary>
        /// close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void CloseWindow(object parameter)
        {
            Application.Current.Windows.OfType<SessionListView>().FirstOrDefault().Close();
        }

        /// <summary>
        /// setup session list starting preferences
        /// </summary>
        private void Init(ObservableCollection<TournamentTemplate> templates, SessionManagerMode mode, bool closeOnEnter, bool confirmSave)
        {
            FontColor = PreferencesHelper.FindPreference("LiveTracker", "Window", "FontColor");
            FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));
            Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");

            SessionList = new ObservableCollection<ListBoxItem>();
            foreach (var session in SessionTemplateHelper.LoadSessionTemplates(templates))
            {
                var item = new ListBoxItem()
                {
                    Id = session.SessionId,
                    IsSelected = false,
                    Name = session.SessionName,
                    Description = session.Description
                };

                SessionList.Add(item);
            }

            _closeAfterSave = closeOnEnter;

            _confirmSave = confirmSave;

            _mode = mode;

            InitGuiContent();
        }

        /// <summary>
        /// initialize the starting text for gui content
        /// </summary>
        public void InitGuiContent()
        {
            Title = "";
            SaveButtonText = "";
            SaveButtonToolTipText = "";

            if (_mode is SessionManagerMode.Copy)
            {
                Title = "Select a Session to Copy";
                SaveButtonText = "Copy Session";
                SaveButtonToolTipText = "Select a session to copy";
            }
            else if (_mode is SessionManagerMode.Delete)
            {
                Title = "Select a Session to Delete";
                SaveButtonText = "Delete Session";
                SaveButtonToolTipText = "Select a session to delete";
            }
            else if (_mode is SessionManagerMode.Edit)
            {
                Title = "Select a Session to Edit";
                SaveButtonText = "Edit Session";
                SaveButtonToolTipText = "Select a session to edit";
            }
        }

        /// <summary>
        /// save and close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Save(object parameter)
        {
            if (SessionList is null || SessionList.Any(i => i.IsSelected) is false)
                return;

            var messageText = "";
            var titleText = "";

            if (_mode is SessionManagerMode.Copy)
            {
                messageText = $"Are you sure you want to copy {SelectedSession.Name}?";
                _confirmSave = CheckForSession();
            }
            else if (_mode is SessionManagerMode.Delete)
            {
                messageText = $"Are you sure you want to delete {SelectedSession.Name}?";
                titleText = "Delete Session";
            }
            else if (_mode is SessionManagerMode.Edit)
            {
                messageText = $"Are you sure you want to edit {SelectedSession.Name}?";
                titleText = "Edit Session";
                _confirmSave = CheckForSession();
            }

            if (_confirmSave)
            {
                var vm1 = new YesNoViewModel(messageText, titleText);
                var window1 = new YesNoView(vm1)
                {
                    Owner = Application.Current.Windows.OfType<SessionListView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                window1.ShowDialog();

                if (vm1.Saved is false)
                    return;
            }

            if (_mode is SessionManagerMode.Delete)
            {
                SessionTemplateHelper.DeleteSession(SelectedSession.Id);

                var successMessage = "Session was deleted successfully";

                successMessage = $"{SelectedSession.Name} was deleted successfully";

                SessionList.Remove(SelectedSession);

                var okayVM1 = new OkViewModel(successMessage, "Success");
                var okayWindow1 = new OkView(okayVM1)
                {
                    Owner = Application.Current.Windows.OfType<SessionListView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                okayWindow1.ShowDialog();
            }

            Saved = true;

            MenuCommands.GetMenuViewModel().UpdateFavoriteSessions();

            if (_closeAfterSave || SessionList.Count is 0)
                CloseWindow(null);
        }
    }
}