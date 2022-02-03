using System.Linq;
using System.Windows;
using System.Windows.Input;
using Syncfusion.UI.Xaml.Utility;
using Tournament_Life.ViewModels.Graph_ViewModels;
using Tournament_Life.ViewModels.Results;
using Tournament_Life.Views.Graph_Views;
using Tournament_Life.Views.Results;

namespace Tournament_Life.Commands
{
    public static class SessionResultsCommands
    {
        public static ICommand MenuItem => new BaseCommand(MenuItemCommand);

        /// <summary>
        /// close session results window
        /// </summary>
        private static void Exit()
        {
            Application.Current.Windows.OfType<SessionResultsView>()?.FirstOrDefault().Close();
        }

        /// <summary>
        /// Creates and shows the finish position graph for the selected session
        /// </summary>
        private static void FinishPositionGraph()
        {
            SessionResultsViewModel sessionResultsViewModel = GetSessionResultsViewModel();

            if (sessionResultsViewModel.SelectedSession is null)
                return;

            // get selected session from session results view model
            var selectedSession = sessionResultsViewModel.Sessions.Where(i => i.ID == sessionResultsViewModel.SelectedSession.SessionId)?.FirstOrDefault();

            if (selectedSession.Tournaments is null || selectedSession.Tournaments.Count is 0)
                return;

            var vm = new FinishPositionGraphViewModel(selectedSession);
            var window = new FinishPositionGraphView(vm)
            {
                Owner = Application.Current.Windows.OfType<SessionResultsView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            window.Show();
        }

        /// <summary>
        /// return the session results window view model
        /// </summary>
        /// <returns></returns>
        public static SessionResultsViewModel GetSessionResultsViewModel()
        {
            return Application.Current.Windows.OfType<SessionResultsView>().FirstOrDefault().DataContext as SessionResultsViewModel;
        }

        /// <summary>
        /// Parse the menu item clicked parameter into method calls
        /// </summary>
        /// <param name="parameter">menu item that was clicked name parameter</param>
        private static void MenuItemCommand(object parameter)
        {
            switch (parameter as string)
            {
                case "Exit":
                    Exit();
                    break;
                case "FinishPositionGraph":
                    FinishPositionGraph();
                    break;
                case "ProfitGraph":
                    ProfitGraph();
                    break;
                case "SessionProfitGraph":
                    SessionProfitGraph();
                    break;
                case "ViewTournaments":
                    ViewTournaments();
                    break;
            }
        }

        /// <summary>
        /// Launch tournaments profit graph
        /// </summary>
        private static void ProfitGraph()
        {
            SessionResultsViewModel sessionResultsViewModel = GetSessionResultsViewModel();

            if (sessionResultsViewModel.SelectedSession is null)
                return;

            var selectedSession = sessionResultsViewModel.Sessions.Where(i => i.ID == sessionResultsViewModel.SelectedSession.SessionId)?.FirstOrDefault();

            if (selectedSession.Tournaments is null || selectedSession.Tournaments.Count is <= 1)
                return;

            if (selectedSession.TournamentCount <= 600)
            {
                var vm = new TournamentProfitGraphViewModel(selectedSession);
                var window = new TournamentProfitGraphView(vm)
                {
                    Owner = Application.Current.Windows.OfType<SessionResultsView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner
                };
                window.Show();

                // After opening switch owner to main window
                // so that the graph will open over the session results window
                // but not close when the session results window is closed
                window.Owner = Application.Current.MainWindow;

                return;
            }


            var vmm = new TournamentProfitGraphNoLabelViewModel(selectedSession);
            var windoww = new TournamentProfitGraphNoLabelView(vmm)
            {
                Owner = Application.Current.Windows.OfType<SessionResultsView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            windoww.Show();

            // After opening switch owner to main window
            // so that the graph will open over the session results window
            // but not close when the session results window is closed
            windoww.Owner = Application.Current.MainWindow;
        }

        /// <summary>
        /// Launch sessions profit graph
        /// </summary>
        private static void SessionProfitGraph()
        {
            SessionResultsViewModel sessionResultsViewModel = GetSessionResultsViewModel();

            var sessions = sessionResultsViewModel.Sessions;

            if (sessions is null || sessions.Count is <= 1)
                return;

            if (sessions.Count <= 600)
            {
                var vm = new SessionProfitGraphViewModel(sessions);
                var window = new SessionProfitGraphView(vm)
                {
                    Owner = Application.Current.Windows.OfType<SessionResultsView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner
                };
                window.Show();
                return;
            }

            var vmm = new SessionProfitGraphNoLabelViewModel(sessions);
            var windoww = new SessionProfitGraphNoLabelView(vmm)
            {
                Owner = Application.Current.Windows.OfType<SessionResultsView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            windoww.Show();
        }

        /// <summary>
        /// launch view tournaments window
        /// </summary>
        private static void ViewTournaments()
        {
            SessionResultsViewModel sessionResultsViewModel = GetSessionResultsViewModel();

            if (sessionResultsViewModel.SelectedSession is null)
                return;

            var selectedSession = sessionResultsViewModel.Sessions.Where(i => i.ID == sessionResultsViewModel.SelectedSession.SessionId)?.FirstOrDefault();

            if (selectedSession is null || selectedSession.Tournaments is null || selectedSession.Tournaments.Count is 0)
                return;

            var vm = new TournamentsViewModel(selectedSession);
            var window = new TournamentsView(vm)
            {
                Owner = Application.Current.Windows.OfType<SessionResultsView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            window.Show();
        }
    }
}
