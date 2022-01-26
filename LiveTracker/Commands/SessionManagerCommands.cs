using System.Linq;
using System.Windows;
using System.Windows.Input;
using Syncfusion.UI.Xaml.Utility;
using LiveTracker.Views.Session_Manager_Views;
using LiveTracker.ViewModels.Session_Manager_ViewModels;
using LiveTracker.Enums;
using LiveTracker.Models;
using LiveTracker.Helpers;
using LiveTracker.Views;
using LiveTracker.ViewModels.Menu_ViewModels;
using System.Collections.ObjectModel;
using Tournament_Life.ViewModels;
using Tournament_Life.Views;
using LiveTracker.Models.Sessions;

namespace LiveTracker.Commands
{
    public static class SessionManagerCommands
    {
        public static ICommand MenuItemCommand => new BaseCommand(MenuItem);

        /// <summary>
        /// Add selected tournaments to the session
        /// </summary>
        private static void Add()
        {
            SessionManagerViewModel sessionManagerViewModel = GetSessionManagerViewModel();

            foreach (var tournament in sessionManagerViewModel.TemplateList.Where(i => i.IsSelected))
            {
                var item = new SessionListBoxItem()
                {
                    Description = tournament.Description,
                    DisplayString = tournament.DisplayString,
                    IsSelected = false,
                    Name = tournament.Name,
                    StartTime = tournament.StartTime,
                    TemplateId = tournament.TemplateId
                };

                sessionManagerViewModel.SessionList.Add(item);
            }

            sessionManagerViewModel.SessionList = SessionTemplateHelper.SortListBoxItemsByStartTime(sessionManagerViewModel.SessionList);

            sessionManagerViewModel.ClearSelectedTournaments();
        }

        /// <summary>
        /// clear selected tournaments in the template list
        /// </summary>
        private static void Clear()
        {
            GetSessionManagerViewModel().ClearSelectedTournaments();
        }

        /// <summary>
        /// Copy a selected session into the session manager
        /// </summary>
        private static void Copy()
        {
            SessionManagerViewModel sessionManagerViewModel = GetSessionManagerViewModel();

            ObservableCollection<SessionTemplate> sessions = SessionTemplateHelper.LoadSessionTemplates(new ObservableCollection<TournamentTemplate>(sessionManagerViewModel.Templates));

            if (sessions is null || sessions.Count is 0)
            {
                var okayVM1 = new OkViewModel("No sessions found to copy", "No Sessions Found");
                var okayWindow1 = new OkView(okayVM1)
                {
                    Owner = Application.Current.Windows.OfType<SessionManagerView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                okayWindow1.ShowDialog();

                return;
            }

            var vm = new SessionListViewModel(new ObservableCollection<TournamentTemplate>(sessionManagerViewModel.Templates), SessionManagerMode.Copy);
            var window = new SessionListView(vm)
            {
                Owner = Application.Current.Windows.OfType<SessionManagerView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            window.ShowDialog();

            if (vm.Saved is false)
                return;

            sessionManagerViewModel.Mode = SessionManagerMode.Copy;

            sessionManagerViewModel.SessionName = "";

            sessionManagerViewModel.LoadSession(vm.SelectedSession.Id);
        }

        /// <summary>
        /// Close the session manager window
        /// </summary>
        private static void CloseWindow()
        {
            SessionManagerViewModel sessionManagerViewModel = GetSessionManagerViewModel();

            if (sessionManagerViewModel.SessionName.Length is not 0 || sessionManagerViewModel.SessionList.Count is not 0)
            {
                var vm = new YesNoViewModel("Are you sure you want to close and cancel this session?", "Cancel Session");
                var window = new YesNoView(vm)
                {
                    Owner = Application.Current.Windows.OfType<SessionManagerView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                window.ShowDialog();

                if (vm.Saved is false)
                    return;
            }

            if (Application.Current.Windows.OfType<SessionManagerView>()?.FirstOrDefault() is SessionManagerView sessionManagerView)
                sessionManagerView.Close();
        }

        /// <summary>
        ///  Delete the selected session
        /// </summary>
        private static void Delete()
        {
            SessionManagerViewModel sessionManagerViewModel = GetSessionManagerViewModel();

            var sessions = SessionTemplateHelper.LoadSessionTemplates(new ObservableCollection<TournamentTemplate>(sessionManagerViewModel.Templates));

            if (sessions is null || sessions.Count is 0)
            {
                var okayVM1 = new OkViewModel("No sessions found to delete", "No Sessions Found");
                var okayWindow1 = new OkView(okayVM1)
                {
                    Owner = Application.Current.Windows.OfType<SessionManagerView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                okayWindow1.ShowDialog();

                return;
            }

            var vm = new SessionListViewModel(new ObservableCollection<TournamentTemplate>(sessionManagerViewModel.Templates), SessionManagerMode.Delete, false);
            var window = new SessionListView(vm)
            {
                Owner = Application.Current.Windows.OfType<SessionManagerView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            window.ShowDialog();
        }

        /// <summary>
        /// Edit the selected session
        /// </summary>
        private static void Edit()
        {
            SessionManagerViewModel sessionManagerViewModel = GetSessionManagerViewModel();

            ObservableCollection<SessionTemplate> sessions = SessionTemplateHelper.LoadSessionTemplates(new ObservableCollection<TournamentTemplate>(sessionManagerViewModel.Templates));

            if (sessions is null || sessions.Count is 0)
            {
                var okayVM1 = new OkViewModel("No sessions found to edit", "No Sessions Found");
                var okayWindow1 = new OkView(okayVM1)
                {
                    Owner = Application.Current.Windows.OfType<SessionManagerView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                okayWindow1.ShowDialog();

                return;
            }

            var vm = new SessionListViewModel(new ObservableCollection<TournamentTemplate>(sessionManagerViewModel.Templates), SessionManagerMode.Edit);
            var window = new SessionListView(vm)
            {
                Owner = Application.Current.Windows.OfType<SessionManagerView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            window.ShowDialog();

            if (vm.Saved is false)
                return;

            sessionManagerViewModel.Mode = SessionManagerMode.Edit;

            sessionManagerViewModel.SessionName = vm.SelectedSession.Name;

            sessionManagerViewModel.LoadSession(vm.SelectedSession.Id);
        }

        /// <summary>
        /// Return the session manager view model
        /// </summary>
        /// <returns></returns>
        public static SessionManagerViewModel GetSessionManagerViewModel()
        {
            return Application.Current.Windows.OfType<SessionManagerView>().FirstOrDefault()?.DataContext as SessionManagerViewModel;
        }

        /// <summary>
        /// call corresponding command from session manager
        /// </summary>
        /// <param name="parameter">command to call</param>
        public static void MenuItem(object parameter)
        {
            switch (parameter as string)
            {
                case "Add":
                    Add();
                    break;
                case "Clear":
                    Clear();
                    break;
                case "Copy":
                    Copy();
                    break;
                case "Close":
                    CloseWindow();
                    break;
                case "Delete":
                    Delete();
                    break;
                case "Edit":
                    Edit();
                    break;
                case "New":
                    New();
                    break;
                case "Remove":
                    Remove();
                    break;
                case "Save":
                    Save();
                    break;
            }
        }

        /// <summary>
        /// reset manager for a new session
        /// </summary>
        private static void New()
        {
            SessionManagerViewModel sessionManagerViewModel = GetSessionManagerViewModel();

            if (sessionManagerViewModel.SessionName.Length is not 0 || sessionManagerViewModel.SessionList.Count is not 0)
            {
                var vm = new YesNoViewModel("Are you sure you want to start a new session?", "New Session");
                var window = new YesNoView(vm)
                {
                    Owner = Application.Current.Windows.OfType<SessionManagerView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                window.ShowDialog();

                if (vm.Saved is false)
                    return;
            }

            sessionManagerViewModel.SessionList.Clear();

            sessionManagerViewModel.SessionName = "";

            sessionManagerViewModel.ClearSelectedTournaments();

            sessionManagerViewModel.Mode = SessionManagerMode.New;
        }

        /// <summary>
        /// Remove the selected tournaments from the session list
        /// </summary>
        private static void Remove()
        {
            SessionManagerViewModel sessionManagerViewModel = GetSessionManagerViewModel();

            foreach (var tournament in sessionManagerViewModel.SessionList.Where(i => i.IsSelected).ToList())
                sessionManagerViewModel.SessionList.Remove(tournament);
        }

        /// <summary>
        /// Save session to xml file
        /// </summary>
        private static void Save()
        {
            SessionManagerViewModel sessionManagerViewModel = GetSessionManagerViewModel();

            if (sessionManagerViewModel.SessionName.Trim() is "")
            {
                var theVm = new OkViewModel("Session name can't be blank", "Name Blank");
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.Windows.OfType<SessionManagerView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                return;
            }

            if (sessionManagerViewModel.SessionList.Count is 0)
            {
                var theVm = new OkViewModel("Must have at least one tournament in the session before saving", "Empty Session");
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.Windows.OfType<SessionManagerView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                return;
            }

            if (sessionManagerViewModel.SessionName.Trim().Length > 46)
            {
                var theVm = new OkViewModel("Session name cannot be longer than 46 characters", "Session Name Too Long");
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.Windows.OfType<SessionManagerView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                return;
            }

            var vm = new YesNoViewModel("Are you sure you want to save this session?", "Save Session");

            if (sessionManagerViewModel.Mode is SessionManagerMode.Edit)
                vm = new YesNoViewModel("Are you sure you want to save this edited session?", "Save Edited Session");

            var window = new YesNoView(vm)
            {
                Owner = Application.Current.Windows.OfType<SessionManagerView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            window.ShowDialog();

            if (vm.Saved is false)
                return;

            if (sessionManagerViewModel.Mode is SessionManagerMode.Edit)
                SessionTemplateHelper.ReplaceSession(sessionManagerViewModel.CreateSessionTemplate());
            else
                SessionTemplateHelper.SaveSession(sessionManagerViewModel.CreateSessionTemplate());

            if (Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault().LiveTrackerMenu?.DataContext is MenuViewModel menuViewModel)
                menuViewModel.UpdateFavoriteSessions();

            var okayVM1 = new OkViewModel($"{sessionManagerViewModel.SessionName} was saved successfully", "Success");

            if (sessionManagerViewModel.Mode is SessionManagerMode.Edit)
                okayVM1 = new OkViewModel($"{sessionManagerViewModel.SessionName} was edited successfully", "Success");

            var okayWindow1 = new OkView(okayVM1)
            {
                Owner = Application.Current.Windows.OfType<SessionManagerView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            okayWindow1.ShowDialog();

            sessionManagerViewModel.Mode = SessionManagerMode.New;

            sessionManagerViewModel.ClearSelectedTournaments();

            sessionManagerViewModel.SessionList.Clear();

            sessionManagerViewModel.SessionName = "";
        }
    }
}