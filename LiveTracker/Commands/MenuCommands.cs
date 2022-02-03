using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using Syncfusion.UI.Xaml.Utility;
using Tournament_Life.Enums;
using Tournament_Life.Factories;
using Tournament_Life.Helpers;
using Tournament_Life.Models.Sessions;
using Tournament_Life.Models.Tournaments;
using Tournament_Life.ViewModels;
using Tournament_Life.ViewModels.Bankroll_ViewModels;
using Tournament_Life.ViewModels.Datagrid_ViewModels;
using Tournament_Life.ViewModels.Menu_ViewModels;
using Tournament_Life.ViewModels.Options;
using Tournament_Life.ViewModels.Results;
using Tournament_Life.ViewModels.Session_Manager_ViewModels;
using Tournament_Life.ViewModels.Template_Manager_ViewModels;
using Tournament_Life.Views;
using Tournament_Life.Views.Bankroll_Views;
using Tournament_Life.Views.Menu_Views;
using Tournament_Life.Views.Options;
using Tournament_Life.Views.Results;
using Tournament_Life.Views.Session_Manager_Views;
using Tournament_Life.Views.Template_Manager_Views;

namespace Tournament_Life.Commands
{
    public static class MenuCommands
    {
        public static ICommand MenuItem => new BaseCommand(MenuItemCommand);
        public static ICommand FavoriteTemplateMenuItem => new BaseCommand(FavoriteTemplateMenuItemCommand);
        public static ICommand CopyTemplateCommand => new BaseCommand(CopyTemplate);
        public static ICommand DeleteTemplateCommand => new BaseCommand(DeleteTemplate);
        public static ICommand EditTemplateCommand => new BaseCommand(EditTemplate);
        public static ICommand FavoriteSessionMenuItem => new BaseCommand(FavoriteSessionMenuItemCommand);
        public static ICommand UndoRecentlyFinishedTournamentMenuItem => new BaseCommand(UndoRecentlyFinishedTournament);
        public static ICommand UnpinFromFavorite => new BaseCommand(UnpinFromFavorites);
        public static ICommand VisibilityChangedCommand => new BaseCommand(VisibilityChanged);

        /// <summary>
        /// Add a new bankroll to bankroll tracking
        /// </summary>
        private static void AddBankroll()
        {
            var vm = new AddBankrollViewModel();
            var window = new AddBankrollView(vm)
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            window.ShowDialog();
        }

        /// <summary>
        /// Log a new deposit to a bankroll
        /// </summary>
        private static void BankrollTransaction()
        {
            MenuViewModel menuViewModel = GetMenuViewModel();

            if (menuViewModel.Bankrolls.Count is 0)
            {
                var theVm = new OkViewModel("Create at least one bankroll first", "No Bankrolls");
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.MainWindow,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                return;
            }

            var vm = new BankrollTransactionViewModel(menuViewModel.Bankrolls);
            var window = new BankrollTransactionView(vm)
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            window.ShowDialog();
        }

        /// <summary>
        /// Check for updates to program
        /// </summary>
        private static void CheckForUpdates()
        {
            //AutoUpdater.Mandatory = true;

            //AutoUpdater.ReportErrors = true;

            //AutoUpdater.Start("", new NetworkCredential("", ""));

            Assembly assembly = Assembly.GetEntryAssembly();

            var vm = new OkViewModel($"Current Version: {assembly.GetName().Version}\n\nPlease check the web address below for new releases.\n\nhttps://github.com/SleepyBookworm/Tournament-Life/releases", "Check for Updates");
            var window = new OkView(vm)
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            window.ShowDialog();
        }

        /// <summary>
        /// Check the software version
        /// </summary>
        private static void CheckVersion()
        {
            Assembly assembly = Assembly.GetEntryAssembly();

            var okayVM1 = new OkViewModel($"Version {assembly.GetName().Version}", "Software Version");
            var okayWindow1 = new OkView(okayVM1)
            {
                Owner = Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            okayWindow1.ShowDialog();
        }

        /// <summary>
        /// copy favorite tournament
        /// </summary>
        /// <param name="id"></param>
        private static void CopyTemplate(object parameter)
        {
            // make sure template manager is open
            TemplateManager();

            // tournament to copy
            TournamentTemplate copy = TournamentTemplateHelper.LoadTemplate((int)parameter);

            TemplateManagerViewModel templateManagerViewModel = TemplateManagerCommands.GetTemplateManagerViewModel();

            // null check on tournament
            if (copy is null || templateManagerViewModel is null)
                return;

            // copy template into new template
            templateManagerViewModel.Template = new TournamentTemplate(copy);

            // see if new game type is in the combo box, add it if not
            if (templateManagerViewModel.GameTypes.Any(i => i == templateManagerViewModel.Template.GameType) is false)
                templateManagerViewModel.GameTypes.Add(templateManagerViewModel.Template.GameType);

            // set selected game type
            templateManagerViewModel.SelectedGameType = templateManagerViewModel.Template.GameType;

            // add any new venue
            if (templateManagerViewModel.Venues.Any(i => i == templateManagerViewModel.Template.Venue) is false)
                templateManagerViewModel.Venues.Add(templateManagerViewModel.Template.Venue);

            // set selected venue
            templateManagerViewModel.SelectedVenue = templateManagerViewModel.Template.Venue;

            // add any new formats to the formats list
            foreach (var newFormat in templateManagerViewModel.Template.Formats) if (templateManagerViewModel.Formats.Any(i => i == newFormat) is false)
                    templateManagerViewModel.Formats.Add(newFormat);

            // create format check list box items
            templateManagerViewModel.CreateFormatListBoxItems();
            templateManagerViewModel.DeselectFormats();

            // set selected formats
            foreach (var item in templateManagerViewModel.FormatsList) if (templateManagerViewModel.Template.Formats.Any(i => i == item.DisplayString))
                    item.IsSelected = true;

            // set sng
            templateManagerViewModel.SngCheckBox = templateManagerViewModel.Template.IsSng;

            // set Bovada
            templateManagerViewModel.BovadaCheckBox = templateManagerViewModel.Template.IsBovadaBounty;

            // set mode
            templateManagerViewModel.Mode = TemplateManagerMode.Copy;

            // give template manager the focus
            Application.Current.Windows.OfType<TemplateManagerView>().FirstOrDefault().Activate();

            // create/show success message
            var okayVM1 = new OkViewModel(copy.TournamentName + " was copied successfully", "Copy Tournament");
            var okayWindow1 = new OkView(okayVM1)
            {
                Owner = Application.Current.Windows.OfType<TemplateManagerView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            okayWindow1.ShowDialog();
        }

        /// <summary>
        /// Load the session manager with the currently running template tournaments
        /// </summary>
        private static void CreateSessionFromRunning()
        {
            MenuViewModel menuViewModel = GetMenuViewModel();

            DataGridViewModel dataGridViewModel = DataGridCommands.GetDataGridViewModel();

            if(dataGridViewModel.RunningTournaments.Count is 0)
            {
                var theVm = new OkViewModel("No tournaments running", "None Running");

                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.MainWindow,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                return;
            }

            if(dataGridViewModel.RunningTournaments.Where(i => i.TemplateId is -1).Count() == dataGridViewModel.RunningTournaments.Count)
            {
                var theVm = new OkViewModel("Can't make a session from tournaments that are not saved to the tournaments file", "No Saved Tournaments Running");

                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.MainWindow,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                return;
            }

            if (Application.Current.Windows.OfType<SessionManagerView>().Any())
            {
                if (Application.Current.Windows.OfType<SessionManagerView>().FirstOrDefault()?.DataContext is not SessionManagerViewModel sessionManagerViewModel) return;

                var win = Application.Current.Windows.OfType<SessionManagerView>().FirstOrDefault();
                if (win.WindowState is WindowState.Minimized) win.WindowState = WindowState.Normal;

                win.Activate();
                win.Topmost = true;
                win.Topmost = false;
                win.Focus();

                if (sessionManagerViewModel.SessionName.Length is not 0 || sessionManagerViewModel.SessionList.Count is not 0)
                {
                    var vmm = new YesNoViewModel("A session already exists, are you sure you want to create a new session?", "Existing Session");
                    var windoww = new YesNoView(vmm)
                    {
                        Owner = Application.Current.Windows.OfType<SessionManagerView>().FirstOrDefault(),
                        WindowStartupLocation = WindowStartupLocation.CenterOwner,
                    };
                    windoww.ShowDialog();

                    if (vmm.Saved is false) return;
                }

                sessionManagerViewModel.SessionList.Clear();

                sessionManagerViewModel.CreateSessionListBoxItems(new ObservableCollection<int>(dataGridViewModel.RunningTournaments.Select(i => i.TemplateId).Where(i => i is not -1)));

                sessionManagerViewModel.SessionName = "";

                return;
            }

            var vm = new SessionManagerViewModel(new ObservableCollection<int>(dataGridViewModel.RunningTournaments.Select(i => i.TemplateId).Where(i => i is not -1)));
            var window = new SessionManagerView(vm);
            window.Left = Application.Current.MainWindow.Left + (Application.Current.MainWindow.Width / 8);
            window.Top = Application.Current.MainWindow.Top - 40;
            window.Show();
        }

        /// <summary>
        /// show the database manager
        /// </summary>
        private static void DatabaseManager()
        {
            var vm = new DatabaseViewModel();
            var window = new DatabaseView(vm)
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            window.ShowDialog();
        }

        /// <summary>
        /// delete favorite template
        /// </summary>
        /// <param name="id"></param>
        private static void DeleteTemplate(object parameter)
        {
            var tournament = TournamentTemplateHelper.LoadTemplate((int)parameter);

            var vm = new YesNoViewModel($"Are you sure you want to delete {tournament.TournamentName}?\n(This cannot be undone!)", "Delete Tournament");
            var window = new YesNoView(vm)
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            window.ShowDialog();

            if (vm.Saved is false) return;

            SessionTemplateHelper.RemoveTemplateFromSessions(tournament);

            TournamentTemplateHelper.DeleteTournamentTemplate(tournament);

            if (Application.Current.Windows.OfType<LiveTrackerWindowView>()?.FirstOrDefault()?.LiveTrackerMenu?.DataContext is MenuViewModel menuViewModel)
            {
                menuViewModel.UpdateFavoriteTemplates();

                menuViewModel.UpdateFavoriteSessions();
            }

            if (Application.Current.Windows.OfType<TemplateManagerView>()?.FirstOrDefault()?.DataContext is TemplateManagerViewModel templateManagerViewModel)
            {
                templateManagerViewModel.LoadTemplates();

                templateManagerViewModel.LoadDefaults();

                templateManagerViewModel.SetTitle();
            }

            if (Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.TournamentsDataGrid?.DataContext is DataGridViewModel dataGridViewModel)
                dataGridViewModel.UpdateRunningTournaments();

            if (Application.Current.Windows.OfType<SessionManagerView>().FirstOrDefault()?.DataContext is SessionManagerViewModel sessionManagerViewModel)
                sessionManagerViewModel.UpdateTemplates();

            if (Application.Current.Windows.OfType<SelectTemplatesView>().FirstOrDefault()?.DataContext is SelectTemplatesViewModel selectTemplatesViewModel)
                selectTemplatesViewModel.Reload();

            var okayVM1 = new OkViewModel($"{tournament.TournamentName} deleted successfully", "Deleted");
            var okayWindow1 = new OkView(okayVM1)
            {
                Owner = Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            okayWindow1.ShowDialog();
        }

        /// <summary>
        /// shows a donation link
        /// </summary>
        private static void Donate()
        {
            var vm = new OkViewModel($"If you are enjoying Tournament Life and would like to \ndonate to the creator please use the donation link below.\n\nhttps://www.paypal.me/sleepybookworm", "Donation");
            var window = new OkView(vm)
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            window.ShowDialog();
        }

        /// <summary>
        ///  edit favorite template
        /// </summary>
        /// <param name="id"></param>
        private static void EditTemplate(object parameter)
        {
            // make sure template manager is open
            TemplateManager();

            // tournament to copy
            var copy = TournamentTemplateHelper.LoadTemplate((int)parameter);

            TemplateManagerViewModel templateManagerViewModel = TemplateManagerCommands.GetTemplateManagerViewModel();

            if (copy is null)
                return;

            // copy template into new template
            templateManagerViewModel.Template = new TournamentTemplate(copy);

            // see if new game type is in the combo box, add it if not
            if (templateManagerViewModel.GameTypes.Any(i => i == templateManagerViewModel.Template.GameType) is false)
                templateManagerViewModel.GameTypes.Add(templateManagerViewModel.Template.GameType);

            // set selected game type
            templateManagerViewModel.SelectedGameType = templateManagerViewModel.Template.GameType;

            // add any new venue
            if (templateManagerViewModel.Venues.Any(i => i == templateManagerViewModel.Template.Venue) is false)
                templateManagerViewModel.Venues.Add(templateManagerViewModel.Template.Venue);

            // set selected venue
            templateManagerViewModel.SelectedVenue = templateManagerViewModel.Template.Venue;

            // add any new formats to the formats list
            foreach (var newFormat in templateManagerViewModel.Template.Formats) if (templateManagerViewModel.Formats.Any(i => i == newFormat) is false)
                    templateManagerViewModel.Formats.Add(newFormat);

            // create format check list box items
            templateManagerViewModel.CreateFormatListBoxItems();
            templateManagerViewModel.DeselectFormats();

            // set selected formats
            foreach (var item in templateManagerViewModel.FormatsList) if (templateManagerViewModel.Template.Formats.Any(i => i == item.DisplayString))
                item.IsSelected = true;

            // set sng
            templateManagerViewModel.SngCheckBox = templateManagerViewModel.Template.IsSng;

            // set Bovada
            templateManagerViewModel.BovadaCheckBox = templateManagerViewModel.Template.IsBovadaBounty;

            // set mode
            templateManagerViewModel.Mode = TemplateManagerMode.Edit;

            // create/show success message
            var okayVM1 = new OkViewModel($"{copy.TournamentName} is ready for editing", "Edit Tournament");
            var okayWindow1 = new OkView(okayVM1)
            {
                Owner = Application.Current.Windows.OfType<TemplateManagerView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            okayWindow1.ShowDialog();

            // give template manager the focus
            Application.Current.Windows.OfType<TemplateManagerView>().FirstOrDefault().Activate();
        }

        /// <summary>
        /// Closes the window
        /// </summary>
        private static void Exit()
        {
            Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.Close();
        }

        /// <summary>
        /// Starts a group of tournaments from a click on a favorite session template menu item
        /// </summary>
        /// <param name="parameter">id of the session that was clicked</param>
        private static void FavoriteSessionMenuItemCommand(object parameter)
        {
            MenuViewModel menuViewModel = GetMenuViewModel();

            if (menuViewModel is null)
                return;

            // remove "Session" from the string and parse so we're left with just the session id number
            int sessionId = int.Parse(new string(((string)parameter ?? string.Empty).Where(char.IsDigit).ToArray()));

            // fine the matching session template from file
            SessionTemplate session = menuViewModel.FavoriteSessions.FirstOrDefault(i => i.SessionId == sessionId);

            if (session is null)
                return;

            // update session names string
            session?.UpdateTournamentNamesString(menuViewModel.Templates);

            // empty session error message
            if (session.TemplateIds is null || session.TemplateIds.Count is 0)
            {
                var okayVM1 = new OkViewModel("Session was empty", "Empty Session");
                var okayWindow1 = new OkView(okayVM1)
                {
                    Owner = Application.Current.MainWindow,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                okayWindow1.ShowDialog();
            }

            var vm = new SessionStartViewModel(session);
            var window = new SessionStartView(vm)
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            window.ShowDialog();
        }

        /// <summary>
        /// Starts a tournament from a click on a favorite templates menu items
        /// </summary>
        /// <param name="parameter">id of template clicked</param>
        private static void FavoriteTemplateMenuItemCommand(object parameter)
        {
            MenuViewModel menuViewModel = GetMenuViewModel();

            if (menuViewModel is null)
                return;

            // remove "Templates" from the string and parse so we're left with just the template id number
            int templateId = int.Parse(new string(((string)parameter ?? string.Empty).Where(char.IsDigit).ToArray()));

            TournamentTemplate template = menuViewModel?.FavoriteTemplates.FirstOrDefault(i => i.TemplateId == templateId);

            if(template is not null)
                TournamentFactory.StartTournaments(template);
        }

        /// <summary>
        /// returns the menu view model
        /// </summary>
        /// <returns>menu view model</returns>
        public static MenuViewModel GetMenuViewModel()
        {
            return Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.LiveTrackerMenu?.DataContext as MenuViewModel;
        }

        /// <summary>
        /// Toggles locking the window as top most
        /// </summary>
        private static void KeepWindowOnTop()
        {
            // find opposite of the current keep window of top preference
            var keepWindowOnTop = !bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "KeepWindowOnTop"));

            // update preference with new value
            PreferencesHelper.UpdatePreference("LiveTracker", "Window", "KeepWindowOnTop", keepWindowOnTop);

            // change the keep window on top property to the new value
            if (Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.DataContext is LiveTrackerViewModel liveTrackerViewModel)
                liveTrackerViewModel.KeepWindowOnTop = keepWindowOnTop;
        }

        /// <summary>
        /// Parse the menu item clicked parameter into method calls
        /// </summary>
        /// <param name="parameter">menu item that was clicked name parameter</param>
        private static void MenuItemCommand(object parameter)
        {
            var menuItem = parameter as string;

            switch (menuItem)
            {
                case "AddBankrollMenuItem":
                    AddBankroll();
                    break;
                case "CreateSessionFromRunningMenuItem":
                    CreateSessionFromRunning();
                    break;
                case "DatabaseMenuItem":
                    DatabaseManager();
                    break;
                case "DonateMenuItem":
                    Donate();
                    break;
                case "ExitMenuItem":
                    Exit();
                    break;
                case "KeepWindowOnTopMenuItem":
                    KeepWindowOnTop();
                    break;
                case "NewTransactionMenuItem":
                    BankrollTransaction();
                    break;
                case "OptionsMenuItem":
                    Options();
                    break;
                case "QuickResultsMenuItem":
                    QuickResults();
                    break;
                case "ProfilesMenuItem":
                    ProfileManager();
                    break;
                case "RegisterMenuItem":
                    Register();
                    break;
                case "RemoveBankrollMenuItem":
                    RemoveBankroll();
                    break;
                case "SelectTemplateMenuItem":
                    SelectTemplate();
                    break;
                case "SessionManagerMenuItem":
                    SessionManager();
                    break;
                case "SessionResults":
                    SessionResults();
                    break;
                case "SweepsCoinMenuItem":
                    SweepsCoinChecked();
                    break;
                case "TemplateManagerMenuItem":
                    TemplateManager();
                    break;
                case "TournamentResultsView":
                    TournamentResults();
                    break;
                case "QuickTemplateMenuItem":
                    QuickTemplate();
                    break;
                case "UpdateMenuItem":
                    CheckForUpdates();
                    break;
                case "UsageGuideMenuItem":
                    UsageGuide();
                    break;
                case "VersionMenuItem":
                    CheckVersion();
                    break;
                case "ViewTransactionsMenuItem":
                    ViewTransactions();
                    break;
            }
        }

        /// <summary>
        /// Show the options window
        /// </summary>
        private static void Options()
        {
            var vm = new OptionsViewModel();
            var window = new OptionsView(vm)
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            window.ShowDialog();
        }

        /// <summary>
        /// Manage user profiles
        /// </summary>
        private static void ProfileManager()
        {
            var vm = new ProfilesViewModel();
            var window = new ProfilesView(vm)
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            window.ShowDialog();
        }

        /// <summary>
        /// launch the quick results window
        /// </summary>
        private static void QuickResults()
        {
            if (Application.Current.Windows.OfType<QuickResultsView>().Any())
            {
                var win = Application.Current.Windows.OfType<QuickResultsView>().FirstOrDefault();

                if (win.WindowState is WindowState.Minimized)
                    win.WindowState = WindowState.Normal;

                win.Activate();
                win.Topmost = true;
                win.Topmost = false;
                win.Focus();

                return;
            }

            var vm = new QuickResultsViewModel();
            var window = new QuickResultsView(vm);
            window.Left = Application.Current.MainWindow.Left + (Application.Current.MainWindow.Width / 2) - (window.Width / 2);
            window.Top = Application.Current.MainWindow.Top - 40;
            window.Show();
        }

        /// <summary>
        /// Show the quick template window
        /// </summary>
        private static void QuickTemplate()
        {
            if (Application.Current.Windows.OfType<QuickStartView>().Any())
            {
                var win = Application.Current.Windows.OfType<QuickStartView>().FirstOrDefault();

                if (win.WindowState is WindowState.Minimized) win.WindowState = WindowState.Normal;

                win.Activate();
                win.Topmost = true;
                win.Topmost = false;
                win.Focus();

                return;
            }

            var vm = new QuickStartViewModel();
            var window = new QuickStartView(vm);
            window.Left = Application.Current.MainWindow.Left + (Application.Current.MainWindow.Width / 2) - (window.Width / 2);
            window.Top = Application.Current.MainWindow.Top - 40;
            window.Show();
        }

        /// <summary>
        /// Launch the registration window
        /// </summary>
        private static void Register()
        {
            var vm = new RegisterViewModel();
            var window = new RegisterView(vm)
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            window.ShowDialog();
        }

        /// <summary>
        /// Remove a bankroll from the bankrolls xml file
        /// </summary>
        private static void RemoveBankroll()
        {
            MenuViewModel menuViewModel = GetMenuViewModel();

            // check for bankrolls
            if (menuViewModel.Bankrolls.Count is 0)
            {
                var theVm = new OkViewModel("Create at least one bankroll first", "No Bankrolls");
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.MainWindow,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                return;
            }

            var vm = new RemoveBankrollViewModel();
            var window = new RemoveBankrollView(vm)
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            window.ShowDialog();
        }

        /// <summary>
        /// Show the select template window
        /// </summary>
        private static void SelectTemplate()
        {
            MenuViewModel menuViewModel = GetMenuViewModel();

            if (menuViewModel.Templates is null || menuViewModel.Templates.Count is 0)
            {
                var vm = new OkViewModel("No tournaments found, create some using the Tournament Creator or Quick Start", "No Tournaments");
                var win = new OkView(vm)
                {
                    Owner = Application.Current.MainWindow,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                win.ShowDialog();

                return;
            }

            if (Application.Current.Windows.OfType<SelectTemplatesView>().Any())
            {
                var win = Application.Current.Windows.OfType<SelectTemplatesView>().FirstOrDefault();

                if (win.WindowState is WindowState.Minimized)
                    win.WindowState = WindowState.Normal;

                win.Activate();
                win.Topmost = true;
                win.Topmost = false;
                win.Focus();

                return;
            }

            var window = new SelectTemplatesView(new SelectTemplatesViewModel());
            window.Left = Application.Current.MainWindow.Left + (Application.Current.MainWindow.Width / 2) - (window.Width / 2);
            window.Top = Application.Current.MainWindow.Top - 40;
            window.Show();
        }

        /// <summary>
        /// launch session manager window
        /// </summary>
        private static void SessionManager()
        {
            MenuViewModel menuViewModel = GetMenuViewModel();

            if (menuViewModel.Templates.Count is 0)
            {
                var theVm = new OkViewModel("No tournaments created, try making some using the Tournament Manager first", "No Tournaments");
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.MainWindow,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                return;
            }

            if (Application.Current.Windows.OfType<SessionManagerView>().Any())
            {
                var win = Application.Current.Windows.OfType<SessionManagerView>().FirstOrDefault();

                if (win.WindowState is WindowState.Minimized)
                    win.WindowState = WindowState.Normal;

                win.Activate();
                win.Topmost = true;
                win.Topmost = false;
                win.Focus();

                return;
            }

            var vm = new SessionManagerViewModel();
            var window = new SessionManagerView(vm);
            window.Left = Application.Current.MainWindow.Left + (Application.Current.MainWindow.Width / 8);
            window.Top = Application.Current.MainWindow.Top - 40;
            window.Show();
        }

        /// <summary>
        /// Launch session results window
        /// </summary>
        private static void SessionResults()
        {
            if (Application.Current.Windows.OfType<SessionResultsView>().Any())
            {
                var win = Application.Current.Windows.OfType<SessionResultsView>().FirstOrDefault();

                if (win.WindowState is WindowState.Minimized)
                    win.WindowState = WindowState.Normal;

                win.Activate();
                win.Topmost = true;
                win.Topmost = false;
                win.Focus();

                return;
            }

            var vm = new SessionResultsViewModel();
            var window = new SessionResultsView(vm);
            window.Left = Application.Current.MainWindow.Left + (Application.Current.MainWindow.Width / 2) - (window.Width / 2);
            window.Top = Application.Current.MainWindow.Top - 40;
            window.Show();
        }

        /// <summary>
        /// Switch the currency symbol between sweeps coins and dollars
        /// </summary>
        public static void SweepsCoinChecked()
        {
            var showSweeps = !bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "ShowSweeps"));

            PreferencesHelper.UpdatePreference("LiveTracker", "Window", "ShowSweeps", showSweeps);

            if (Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.DataContext is LiveTrackerViewModel liveTrackerViewModel)
                liveTrackerViewModel.ShowSweepsCoins = showSweeps;

            if (Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.TournamentsDataGrid?.DataContext is DataGridViewModel dataGridViewModel)
            {
                if (bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "ShowSweeps")))
                    dataGridViewModel.CurrencySymbol = "SC ";
                else
                    dataGridViewModel.CurrencySymbol = "$";

                foreach (var tournament in dataGridViewModel.RunningTournaments)
                {
                    if (bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "ShowSweeps")))
                        tournament.CurrencySymbol = "SC ";
                    else
                        tournament.CurrencySymbol = "$";
                }
            }

            if (Application.Current.Windows.OfType<SessionResultsView>().FirstOrDefault()?.DataContext is SessionResultsViewModel sessionResultsViewModel)
                sessionResultsViewModel.ChangeCurrencySymbol();
        }

        /// <summary>
        /// Launch the template manager window
        /// </summary>
        private static void TemplateManager()
        {
            if(Application.Current.Windows.OfType<TemplateManagerView>().Any())
            {
                var win = Application.Current.Windows.OfType<TemplateManagerView>().FirstOrDefault();

                if (win.WindowState is WindowState.Minimized)
                    win.WindowState = WindowState.Normal;

                win.Activate();
                win.Topmost = true;
                win.Topmost = false;
                win.Focus();

                return;
            }

            var vm = new TemplateManagerViewModel();
            var window = new TemplateManagerView(vm);
            window.Left = Application.Current.MainWindow.Left - (Application.Current.MainWindow.Width / 4);
            window.Top = Application.Current.MainWindow.Top - 40;
            window.Show();
        }

        /// <summary>
        /// launch tournaments results window
        /// </summary>
        private static void TournamentResults()
        {
            if (Application.Current.Windows.OfType<TournamentsResultsView>().Any())
            {
                var win = Application.Current.Windows.OfType<TournamentsResultsView>().FirstOrDefault();

                if (win.WindowState is WindowState.Minimized)
                    win.WindowState = WindowState.Normal;

                win.Activate();
                win.Topmost = true;
                win.Topmost = false;
                win.Focus();

                return;
            }

            var vm = new TournamentsResultsViewModel();
            var window = new TournamentsResultsView(vm);
            window.Left = Application.Current.MainWindow.Left + (Application.Current.MainWindow.Width / 2) - (window.Width / 2);
            window.Top = Application.Current.MainWindow.Top - 40;
            window.Show();
        }

        /// <summary>
        /// Remove a recently finished tournament from the database and return to the running tournament list
        /// </summary>
        /// <param name="parameter">database id of tournament</param>
        private static void UndoRecentlyFinishedTournament(object parameter)
        {
            MenuViewModel menuViewModel = GetMenuViewModel();

            TournamentRunning tournament = menuViewModel.RecentlyFinishedTournaments.FirstOrDefault(i => i.DatabaseId == (int) parameter);

            var vm = new YesNoViewModel($"Are you sure you want to restore {tournament.TournamentName}?", "Restore Tournament");
            var window = new YesNoView(vm)
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            window.ShowDialog();

            if (vm.Saved is false)
                return;

            menuViewModel.UndoRecentlyFinished(tournament);
        }

        /// <summary>
        /// Remove a tournament template from the favorites menu
        /// </summary>
        private static void UnpinFromFavorites(object parameter)
        {
            TournamentTemplate template = TournamentTemplateHelper.LoadTemplate((int)parameter);

            if (template.TemplateId is -1) return;

            template.IsFavorite = false;

            TournamentTemplateHelper.SaveTournamentTemplate(template, true);

            if (Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.LiveTrackerMenu?.DataContext is MenuViewModel menuViewModel)
                menuViewModel.UpdateFavoriteTemplates();

            if (Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.TournamentsDataGrid?.DataContext is DataGridViewModel dataGridViewModel)
                foreach (var tournament in dataGridViewModel.RunningTournaments)
                    if (tournament.TemplateId == (int) parameter)
                        tournament.IsFavorite = false;

            if (Application.Current.Windows.OfType<TemplateManagerView>().FirstOrDefault()?.DataContext is TemplateManagerViewModel templateManagerViewModel)
                templateManagerViewModel.Reload();
        }

        /// <summary>
        /// show usage guide on website
        /// </summary>
        private static void UsageGuide()
        {

        }

        /// <summary>
        /// Launch the view transactions window
        /// </summary>
        private static void ViewTransactions()
        {
            MenuViewModel menuViewModel = GetMenuViewModel();

            if (menuViewModel.Bankrolls.Count is 0)
            {
                var theVm = new OkViewModel("No bankrolls created", "No Bankrolls");
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.MainWindow,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                return;
            }

            if (menuViewModel.Bankrolls.Any(i => i.Transactions.Count is > 0) is false)
            {
                var theVm = new OkViewModel("No transactions in any of the bankrolls", "No Transactions");
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.MainWindow,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                return;
            }

            if (Application.Current.Windows.OfType<ViewTransactionsView>().Any())
            {
                var win = Application.Current.Windows.OfType<ViewTransactionsView>().FirstOrDefault();

                if (win.WindowState is WindowState.Minimized)
                    win.WindowState = WindowState.Normal;

                win.Activate();
                win.Topmost = true;
                win.Topmost = false;
                win.Focus();

                return;
            }

            var vm = new ViewTransactionsViewModel(menuViewModel.Bankrolls);
            var window = new ViewTransactionsView(vm);
            window.Left = Application.Current.MainWindow.Left + (Application.Current.MainWindow.Width / 3);
            window.Top = Application.Current.MainWindow.Top - 40;
            window.Show();
        }

        /// <summary>
        /// Visibility changed in title bar combo box
        /// </summary>
        private static void VisibilityChanged(object parameter)
        {
            switch (parameter as string)
            {
                case "Show All Tournaments": PreferencesHelper.UpdatePreference("LiveTracker", "Menu", "TournamentVisibility", (int)TournamentVisibility.ShowAll); break;
                case "Show Started Tournaments": PreferencesHelper.UpdatePreference("LiveTracker", "Menu", "TournamentVisibility", (int)TournamentVisibility.ShowStarted); break;
                case "Show Started and Starting in 5 Minutes": PreferencesHelper.UpdatePreference("LiveTracker", "Menu", "TournamentVisibility", (int)TournamentVisibility.ShowFive); break;
                case "Show Started and Starting in 15 Minutes": PreferencesHelper.UpdatePreference("LiveTracker", "Menu", "TournamentVisibility", (int)TournamentVisibility.ShowFifteen); break;
                case "Show Started and Starting in 30 Minutes": PreferencesHelper.UpdatePreference("LiveTracker", "Menu", "TournamentVisibility", (int)TournamentVisibility.ShowThirty); break;
                case "Show Started and Starting in 60 Minutes": PreferencesHelper.UpdatePreference("LiveTracker", "Menu", "TournamentVisibility", (int)TournamentVisibility.ShowSixty); break;
            }

            DataGridCommands.GetDataGridViewModel().Visibility = (TournamentVisibility)int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Menu", "TournamentVisibility"));
        }
    }
}