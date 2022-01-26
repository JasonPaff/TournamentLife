using System.Collections.Generic;
using System.Linq;
using System.Windows;
using LiveTracker.Models;
using LiveTracker.ViewModels.Datagrid_ViewModels;
using LiveTracker.ViewModels.Menu_ViewModels;
using LiveTracker.Views;
using LiveTracker.Commands;

namespace LiveTracker.Factories
{
    public static class TournamentFactory
    {
        /// <summary>
        /// Starts tournaments using a list of template ids
        /// </summary>
        /// <param name="templateIds">list of template ids</param>
        public static void StartTournaments(List<int> templateIds)
        {
            // null/zero check
            if (templateIds is null || templateIds.Count is 0)
                return;

            // menu view model
            var menuViewModel = MenuCommands.GetMenuViewModel();

            // data grid view model
            var dataGridViewModel = DataGridCommands.GetDataGridViewModel();

            // tournaments list
            var tournaments = new List<TournamentRunning>();

            // get all
            foreach (var id in templateIds)
                tournaments.Add(new TournamentRunning(menuViewModel?.Templates?.FirstOrDefault(i => i.TemplateId == id)));

            // add tournaments to data grid tracker
            dataGridViewModel.AddTournament(tournaments);
        }

        /// <summary>
        /// Starts a tournament using a tournament template
        /// </summary>
        /// <param name="tournamentTemplate"></param>
        public static void StartTournaments(TournamentTemplate tournamentTemplate)
        {
            // null/invalid id check
            if (tournamentTemplate is null)
                return;

            // start tournament
            StartTournaments(tournamentTemplate.TemplateId);
        }

        /// <summary>
        /// Starts a tournament using a template id
        /// </summary>
        /// <param name="templateId"></param>
        public static void StartTournaments(int templateId)
        {
            // invalid id check
            if (templateId is -1)
                return;

            // menu view model
            var menuViewModel = Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.LiveTrackerMenu.DataContext as MenuViewModel;

            // data grid view model
            var dataGridViewModel = Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.TournamentsDataGrid.DataContext as DataGridViewModel;

            // add new tournament based on the template to the data grid view model
            dataGridViewModel?.AddTournament(new TournamentRunning(menuViewModel?.Templates?.FirstOrDefault(i => i.TemplateId == templateId)));
        }

        /// <summary>
        /// Starts a tournament using a tournament running model
        /// may or may not have a template id
        /// </summary>
        /// <param name="tournamentTemplate">tournament to start</param>
        public static void StartTournaments(TournamentRunning tournament)
        {
            // null/invalid id check
            if (tournament is null)
                return;

            // data grid view model or bust
            if (Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.TournamentsDataGrid.DataContext is DataGridViewModel dataGridViewModel)
                dataGridViewModel?.AddTournament(tournament);
        }

        /// <summary>
        /// Starts tournaments using a list of running tournaments
        /// </summary>
        /// <param name="tournaments">list of tournaments</param>
        public static void StartTournaments(List<TournamentRunning> tournaments)
        {
            StartTournaments(new List<int>(tournaments.Where(i => i.TemplateId >= 1).Select(i => i.TemplateId)));
        }

        /// <summary>
        /// Starts a tournament using a session template
        /// </summary>
        /// <param name="sessionTemplate"></param>
        public static void StartTournaments(SessionTemplate sessionTemplate)
        {
            if (sessionTemplate is not null)
                StartTournaments(new List<int>(sessionTemplate.TemplateIds));
        }
    }
}