using LiveTracker.Helpers;
using LiveTracker.Models;
using LiveTracker.ViewModels.Datagrid_ViewModels;
using LiveTracker.ViewModels.Menu_ViewModels;
using LiveTracker.Views;
using LiveTracker.Views.Datagrid_Views;
using LiveTracker.Views.Results;
using LiveTracker.ViewModels.Results;
using Syncfusion.UI.Xaml.Grid;
using Syncfusion.UI.Xaml.Utility;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Tournament_Life.ViewModels;
using Tournament_Life.ViewModels.Results;
using Tournament_Life.Views;
using Tournament_Life.Views.Results;
using Tournament_Life.ViewModels.Datagrid_ViewModels;
using Tournament_Life.Views.Datagrid_Views;
using System.Diagnostics;
using System.Collections.Generic;
using LiveTracker.Enums;
using System;
using System.ComponentModel;

namespace LiveTracker.Commands
{
    public static class DataGridCommands
    {
        public static ICommand ChangeColumnVisibility => new BaseCommand(ChangeColumnVisibilityCommand);
        public static ICommand ChangeTemplateDataMenuItem => new BaseCommand(ChangeTemplateData);
        public static ICommand ContextMenuItem => new BaseCommand(ContextMenuItemCommand);
        public static ICommand FinishTournamentMenuItem => new BaseCommand(FinishTournament);

        /// <summary>
        /// launch add to sessions window for the selected tournament
        /// </summary>
        private static void AddToSession()
        {
            // grab selected tournament
            TournamentRunning tournament = GetDataGridViewModel()?.SelectedTournament;

            if (tournament is null || IsSavedTemplate(tournament) is false)
                return;

            if (MenuCommands.GetMenuViewModel().Sessions is null || MenuCommands.GetMenuViewModel().Sessions.Count is 0)
            {
                var vvm = new OkViewModel("No sessions created, use the Session Creator to make some", "No Sessions Created");
                var wwindow = new OkView(vvm)
                {
                    Owner = Application.Current.MainWindow,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                wwindow.ShowDialog();

                return;
            }

            var vm = new AddToSessionViewModel(tournament);
            var window = new AddToSessionView(vm)
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            window.ShowDialog();

            // update sessions list in menu view model
            MenuCommands.GetMenuViewModel().UpdateFavoriteSessions();
        }

        /// <summary>
        /// Attach a note to a tournament
        /// </summary>
        private static void AttachNote()
        {
            // grab selected tournament
            TournamentRunning tournament = GetDataGridViewModel()?.SelectedTournament;

            if (tournament is null)
                return;

            // create note window, include any note text from previous notes
            var vm = new AttachNoteViewModel(tournament.Note, tournament.TournamentName);
            var window = new AttachNoteView(vm)
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            window.ShowDialog();

            // update running tournaments file
            TournamentHelper.SaveRunningTournaments(new List<TournamentRunning>(GetDataGridViewModel().RunningTournaments));
        }

        /// <summary>
        /// Attach a screenshot to this tournament
        /// </summary>
        private static void AttachScreenshot()
        {
            // grab selected tournament
            TournamentRunning tournament = GetDataGridViewModel()?.SelectedTournament;

            if (tournament is null || ConfirmScreenshotOverwrite(tournament) is false)
                return;

            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png",
            };
            openFileDialog.ShowDialog();

            if (openFileDialog.FileName.Length is 0)
                return;

            // delete a previous screenshot if needed
            if (tournament.IsScreenshotAttached is true && tournament.ScreenshotFilename != "")
            {
                ScreenshotHelper.DeleteScreenshot(tournament, Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault());
                tournament.ScreenshotFilename = "";
            }

            // save screenshot
            ScreenshotHelper.SaveScreenshot(openFileDialog.FileName, tournament, Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault());

            // update running tournaments file
            TournamentHelper.SaveRunningTournaments(new List<TournamentRunning>(GetDataGridViewModel().RunningTournaments));
        }

        /// <summary>
        /// Cancel the selected tournament
        /// </summary>
        private static void CancelTournament()
        {
            DataGridViewModel dataGridViewModel = GetDataGridViewModel();

            // grab selected tournament
            TournamentRunning tournament = dataGridViewModel?.SelectedTournament;

            if (tournament is null || dataGridViewModel is null)
                return;

            var vm = new YesNoViewModel($"Are you sure you want to cancel {tournament.TournamentName}?", "Cancel Tournament");
            var window = new YesNoView(vm)
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            window.ShowDialog();

            if (vm.Saved is false)
                return;

            // remove tournament
            dataGridViewModel.RunningTournaments.Remove(tournament);

            // update running tournaments file
            TournamentHelper.SaveRunningTournaments(new List<TournamentRunning>(dataGridViewModel.RunningTournaments));

            // update running tournaments visibility
            UpdateTournamentVisibility();
        }

        /// <summary>
        /// Cancel tournaments from a list
        /// </summary>
        private static void CancelSelectTournaments()
        {
            DataGridViewModel dataGridViewModel = GetDataGridViewModel();

            if (dataGridViewModel is null || dataGridViewModel.RunningTournaments.Count is 0)
                return;

            // sort running tournaments by tournament name
            dataGridViewModel.RunningTournaments = new ObservableCollection<TournamentRunning>(dataGridViewModel.RunningTournaments.OrderBy(i => i.TournamentName));

            // create window
            var vm = new CancelSelectTournamentsViewModel(dataGridViewModel.RunningTournaments);
            var window = new CancelSelectTournamentsView(vm)
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            window.ShowDialog();

            if (vm.Saved is false)
                return;

            // remove tournaments based on sort order
            if (vm.SortByStartTime is false)
                foreach (var index in vm.Indexes.ToList().OrderByDescending(i => i))
                    dataGridViewModel.RunningTournaments.RemoveAt(index);
            else
            {
                dataGridViewModel.RunningTournaments = new ObservableCollection<TournamentRunning>(dataGridViewModel.RunningTournaments.OrderBy(i => i.StartTime));

                foreach (var index in vm.Indexes.ToList().OrderByDescending(i => i))
                    dataGridViewModel.RunningTournaments.RemoveAt(index);
            }

            // update running tournaments file
            TournamentHelper.SaveRunningTournaments(dataGridViewModel.RunningTournaments.ToList());

            // update tournaments visibility
            UpdateTournamentVisibility();
        }

        /// <summary>
        /// Cancel all running tournaments
        /// </summary>
        private static void CancelAllTournaments()
        {
            DataGridViewModel dataGridViewModel = GetDataGridViewModel();

            if (dataGridViewModel is null || dataGridViewModel.RunningTournaments.Count is 0)
                return;

            var vm = new YesNoViewModel("Are you sure you want to cancel all of the running tournaments?", "Cancel All Tournaments");
            var window = new YesNoView(vm)
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            window.ShowDialog();

            if (vm.Saved is false)
                return;

            // clear running tournaments list
            dataGridViewModel.RunningTournaments = new ObservableCollection<TournamentRunning>();

            // clear visible tournaments list
            dataGridViewModel.VisibleTournaments = new ObservableCollection<TournamentRunning>();

            // update running tournaments file
            TournamentHelper.SaveRunningTournaments(new List<TournamentRunning>(dataGridViewModel.RunningTournaments));
        }

        /// <summary>
        /// Change data grid column visibility
        /// </summary>
        /// <param name="parameter"></param>
        private static void ChangeColumnVisibilityCommand(object parameter)
        {
            // flip column hidden based on parameter
            switch ((string)parameter)
            {
                case "Addon":
                    PreferencesHelper.UpdatePreference("LiveTracker", "DataGridColumnVisibility", "AddonTotalCostColumnVisible", !bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnVisibility", "AddonTotalCostColumnVisible")));
                    DataGridLoaded();
                    break;
                case "AddonCount":
                    PreferencesHelper.UpdatePreference("LiveTracker", "DataGridColumnVisibility", "AddonCountColumnVisible", !bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnVisibility", "AddonCountColumnVisible")));
                    DataGridLoaded();
                    break;
                case "Bounty":
                    PreferencesHelper.UpdatePreference("LiveTracker", "DataGridColumnVisibility", "BountyColumnVisible", !bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnVisibility", "BountyColumnVisible")));
                    DataGridLoaded();
                    break;
                case "Buyin":
                    PreferencesHelper.UpdatePreference("LiveTracker", "DataGridColumnVisibility", "BuyinTotalCostColumnVisible", !bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnVisibility", "BuyinTotalCostColumnVisible")));
                    DataGridLoaded();
                    break;
                case "Entrants":
                    PreferencesHelper.UpdatePreference("LiveTracker", "DataGridColumnVisibility", "EntrantsColumnVisible", !bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnVisibility", "EntrantsColumnVisible")));
                    DataGridLoaded();
                    break;
                case "EntrantsPaid":
                    PreferencesHelper.UpdatePreference("LiveTracker", "DataGridColumnVisibility", "EntrantsPaidColumnVisible", !bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnVisibility", "EntrantsPaidColumnVisible")));
                    DataGridLoaded();
                    break;
                case "FinishPosition":
                    PreferencesHelper.UpdatePreference("LiveTracker", "DataGridColumnVisibility", "FinishPositionColumnVisible", !bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnVisibility", "FinishPositionColumnVisible")));
                    DataGridLoaded();
                    break;
                case "JackpotSpinMultiplier":
                    PreferencesHelper.UpdatePreference("LiveTracker", "DataGridColumnVisibility", "JackpotSpinMultiplierColumnVisible", !bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnVisibility", "JackpotSpinMultiplierColumnVisible")));
                    DataGridLoaded();
                    break;
                case "PrizeWon":
                    PreferencesHelper.UpdatePreference("LiveTracker", "DataGridColumnVisibility", "PrizeWonColumnVisible", !bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnVisibility", "PrizeWonColumnVisible")));
                    DataGridLoaded();
                    break;
                case "RebuyCount":
                    PreferencesHelper.UpdatePreference("LiveTracker", "DataGridColumnVisibility", "RebuyCountColumnVisible", !bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnVisibility", "RebuyCountColumnVisible")));
                    DataGridLoaded();
                    break;
                case "Rebuy":
                    PreferencesHelper.UpdatePreference("LiveTracker", "DataGridColumnVisibility", "RebuyTotalCostColumnVisible", !bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnVisibility", "RebuyTotalCostColumnVisible")));
                    DataGridLoaded();
                    break;
                case "StartTime":
                    PreferencesHelper.UpdatePreference("LiveTracker", "DataGridColumnVisibility", "StartTimeColumnVisible", !bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnVisibility", "StartTimeColumnVisible")));
                    DataGridLoaded();
                    break;
                case "TotalCost":
                    PreferencesHelper.UpdatePreference("LiveTracker", "DataGridColumnVisibility", "TotalCostColumnVisible", !bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnVisibility", "TotalCostColumnVisible")));
                    DataGridLoaded();
                    break;
                case "Venue":
                    PreferencesHelper.UpdatePreference("LiveTracker", "DataGridColumnVisibility", "VenueColumnVisible", !bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnVisibility", "VenueColumnVisible")));
                    DataGridLoaded();
                    break;
            }
        }

        /// <summary>
        /// Change the running tournaments template data
        /// </summary>
        /// <param name="parameter">running tournament</param>
        private static void ChangeTemplateData(object parameter)
        {
            // grab selected tournament
            TournamentRunning tournament = GetDataGridViewModel()?.SelectedTournament;

            if (tournament is null)
                return;

            var vm = new ChangeTemplateDataViewModel(new TournamentRunning(tournament));
            var window = new ChangeTemplateDataView(vm)
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            window.ShowDialog();
        }

        /// <summary>
        /// Check if a screenshot is already attached to the tournament
        /// </summary>
        /// <param name="tournament">tournament to be checked</param>
        /// <returns>true if a screenshot was attached</returns>
        private static bool ConfirmScreenshotOverwrite(TournamentRunning tournament)
        {
            if (tournament.IsScreenshotAttached is false)
                return true;

            var theVm = new YesNoViewModel($"Are you sure you want to overwrite the screenshot attached to {tournament.TournamentName}?", "Overwrite Screenshot");

            var theWindow = new YesNoView(theVm)
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            theWindow.ShowDialog();

            if (theVm.Saved)
                return true;

            return false;
        }

        /// <summary>
        /// Takes command parameter from live tracker data grid view and calls associated methods
        /// </summary>
        /// <param name="parameter">menu item clicked</param>
        private static void ContextMenuItemCommand(object parameter)
        {
            switch (parameter as string)
            {
                case "AddToSession":
                    AddToSession();
                    break;
                case "AttachNote":
                    AttachNote();
                    break;
                case "AttachScreenshot":
                    AttachScreenshot();
                    break;
                case "CancelTournament":
                    CancelTournament();
                    break;
                case "CancelSelectTournaments":
                    CancelSelectTournaments();
                    break;
                case "CancelAllTournaments":
                    CancelAllTournaments();
                    break;
                case "PinToFavorites":
                    PinToFavorites();
                    break;
                case "RemoveFromSession":
                    RemoveFromSession();
                    break;
                case "RemoveScreenshot":
                    RemoveScreenshot();
                    break;
                case "StartDuplicate":
                    StartDuplicate();
                    break;
                case "UpdateColumnOrder":
                    UpdateColumnOrder();
                    break;
                case "UpdateColumnWidth":
                    UpdateColumnWidth();
                    break;
                case "UpdateEndTime":
                    UpdateEndTime();
                    break;
                case "UpdateEndTimeSelected":
                    UpdateEndTimeSelected();
                    break;
                case "UpdateStartTime":
                    UpdateStartTime();
                    break;
                case "UpdateStartTimeSelected":
                    UpdateStartTimeSelected();
                    break;
                case "UpdateTimeStrings":
                    UpdateTimeStrings();
                    break;
                case "ViewScreenshot":
                    ViewScreenshot();
                    break;
                case "WindowLoaded":
                    DataGridLoaded();
                    break;
            }
        }

        /// <summary>
        /// Re-Order datagrid after window is loaded
        /// </summary>
        public static void DataGridLoaded()
        {
            // datagrid columns
            var columns = Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.TournamentsDataGrid.Columns;
            var addonCountColumn = columns?.FirstOrDefault(i => i.MappingName is "AddonCount");
            var addonTotalCostColumn = columns?.FirstOrDefault(i => i.MappingName is "AddonTotalCost");
            var bountyCountColumn = columns?.FirstOrDefault(i => i.MappingName is "BountyCount");
            var buyinTotalCostColumn = columns?.FirstOrDefault(i => i.MappingName is "BuyinTotalCost");
            var entrantsColumn = columns?.FirstOrDefault(i => i.MappingName is "Entrants");
            var entrantsPaidColumn = columns?.FirstOrDefault(i => i.MappingName is "EntrantsPaid");
            var finishPositionColumn = columns?.FirstOrDefault(i => i.MappingName is "FinishPosition");
            var jackpotSpinMultiplierColumn = columns?.FirstOrDefault(i => i.MappingName is "JackpotSpinMultiplier");
            var prizeWonColumn = columns?.FirstOrDefault(i => i.MappingName is "PrizeWon");
            var rebuyCountColumn = columns?.FirstOrDefault(i => i.MappingName is "RebuyCount");
            var rebuyTotalCostColumn = columns?.FirstOrDefault(i => i.MappingName is "RebuyTotalCost");
            var startTimeColumn = columns?.FirstOrDefault(i => i.MappingName is "StartTime");
            var totalCostColumn = columns?.FirstOrDefault(i => i.MappingName is "TotalCost");
            var tournamentNameColumn = columns?.FirstOrDefault(i => i.MappingName is "TournamentName");
            var venueColumn = columns?.FirstOrDefault(i => i.MappingName is "Venue");

            // column width
            addonCountColumn.Width = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnWidth", "AddonCountColumnWidth"));
            addonTotalCostColumn.Width = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnWidth", "AddonTotalCostColumnWidth"));
            bountyCountColumn.Width = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnWidth", "BountyCountColumnWidth"));
            buyinTotalCostColumn.Width = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnWidth", "BuyinTotalCostColumnWidth"));
            entrantsColumn.Width = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnWidth", "EntrantsColumnWidth"));
            entrantsPaidColumn.Width = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnWidth", "EntrantsPaidColumnWidth"));
            finishPositionColumn.Width = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnWidth", "FinishPositionColumnWidth"));
            jackpotSpinMultiplierColumn.Width = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnWidth", "JackpotSpinMultiplierColumnWidth"));
            prizeWonColumn.Width = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnWidth", "PrizeWonColumnWidth"));
            rebuyCountColumn.Width = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnWidth", "RebuyCountColumnWidth"));
            rebuyTotalCostColumn.Width = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnWidth", "RebuyTotalCostColumnWidth"));
            startTimeColumn.Width = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnWidth", "StartTimeColumnWidth"));
            totalCostColumn.Width = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnWidth", "TotalCostColumnWidth"));
            tournamentNameColumn.Width = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnWidth", "TournamentNameColumnWidth"));
            venueColumn.Width = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnWidth", "VenueColumnWidth"));

            // column order from preferences
            var AddonCountColumnOrder = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnOrder", "AddonCountColumnOrder"));
            var AddonTotalCostColumnOrder = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnOrder", "AddonTotalCostColumnOrder"));
            var BountyColumnOrder = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnOrder", "BountyCountColumnOrder"));
            var BuyinTotalCostColumnOrder = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnOrder", "BuyinTotalCostColumnOrder"));
            var EntrantsColumnOrder = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnOrder", "EntrantsColumnOrder"));
            var EntrantsPaidColumnOrder = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnOrder", "EntrantsPaidColumnOrder"));
            var FinishPositionColumnOrder = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnOrder", "FinishPositionColumnOrder"));
            var JackpotSpinMultiplierColumnOrder = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnOrder", "JackpotSpinMultiplierColumnOrder"));
            var PrizeWonColumnOrder = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnOrder", "PrizeWonColumnOrder"));
            var RebuyCountColumnOrder = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnOrder", "RebuyCountColumnOrder"));
            var RebuyTotalCostColumnOrder = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnOrder", "RebuyTotalCostColumnOrder"));
            var StartTimeColumnOrder = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnOrder", "StartTimeColumnOrder"));
            var TotalCostColumnOrder = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnOrder", "TotalCostColumnOrder"));
            var TournamentNameColumnOrder = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnOrder", "TournamentNameColumnOrder"));
            var VenueColumnOrder = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnOrder", "VenueColumnOrder"));

            // re-ordered columns collection
            var newColumns = new Columns();

            // add columns to collection in order user wants
            for (var c = 0; c < 15; c++)
            {
                if (AddonCountColumnOrder == c) newColumns.Add(addonCountColumn);
                if (AddonTotalCostColumnOrder == c) newColumns.Add(addonTotalCostColumn);
                if (BountyColumnOrder == c) newColumns.Add(bountyCountColumn);
                if (BuyinTotalCostColumnOrder == c) newColumns.Add(buyinTotalCostColumn);
                if (EntrantsColumnOrder == c) newColumns.Add(entrantsColumn);
                if (EntrantsPaidColumnOrder == c) newColumns.Add(entrantsPaidColumn);
                if (FinishPositionColumnOrder == c) newColumns.Add(finishPositionColumn);
                if (JackpotSpinMultiplierColumnOrder == c) newColumns.Add(jackpotSpinMultiplierColumn);
                if (PrizeWonColumnOrder == c) newColumns.Add(prizeWonColumn);
                if (RebuyCountColumnOrder == c) newColumns.Add(rebuyCountColumn);
                if (RebuyTotalCostColumnOrder == c) newColumns.Add(rebuyTotalCostColumn);
                if (StartTimeColumnOrder == c) newColumns.Add(startTimeColumn);
                if (TotalCostColumnOrder == c) newColumns.Add(totalCostColumn);
                if (TournamentNameColumnOrder == c) newColumns.Add(tournamentNameColumn);
                if (VenueColumnOrder == c) newColumns.Add(venueColumn);
            }

            // adjust visibility
            if (bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnVisibility", "AddonCountColumnVisible")) is false) addonCountColumn.Width = 0;
            addonTotalCostColumn.Width = 0;
            if (bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnVisibility", "BountyColumnVisible")) is false) bountyCountColumn.Width = 0;
            buyinTotalCostColumn.Width = 0;
            if (bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnVisibility", "EntrantsColumnVisible")) is false) entrantsColumn.Width = 0;
            if (bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnVisibility", "EntrantsPaidColumnVisible")) is false) entrantsPaidColumn.Width = 0;
            if (bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnVisibility", "FinishPositionColumnVisible")) is false) finishPositionColumn.Width = 0;
            if (bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnVisibility", "JackpotSpinMultiplierColumnVisible")) is false) jackpotSpinMultiplierColumn.Width = 0;
            if (bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnVisibility", "PrizeWonColumnVisible")) is false) prizeWonColumn.Width = 0;
            if (bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnVisibility", "RebuyCountColumnVisible")) is false) rebuyCountColumn.Width = 0;
            rebuyTotalCostColumn.Width = 0;
            if (bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnVisibility", "StartTimeColumnVisible")) is false) startTimeColumn.Width = 0;
            if (bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnVisibility", "TotalCostColumnVisible")) is false) totalCostColumn.Width = 0;
            if (bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGridColumnVisibility", "VenueColumnVisible")) is false) venueColumn.Width = 0;

            // add columns to datagrid
            var datagrid = Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.TournamentsDataGrid;

            // clear data grid columns
            datagrid.Columns.Clear();

            // add the adjusted columns
            foreach (var col in newColumns) datagrid.Columns.Add(col);

            // force refresh
            Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.TournamentsDataGrid.Measure(new Size(1, 1));
        }

        /// <summary>
        /// Error check the finished tournament
        /// </summary>
        /// <param name="tournament">data to check</param>
        /// <returns>true if no errors</returns>
        private static bool ErrorCheckFinishedTournament(TournamentRunning tournament)
        {
            if (tournament.Entrants < 0)
                tournament.Entrants = 0;

            if (tournament.EntrantsPaid < 0)
                tournament.EntrantsPaid = 0;

            if (tournament.FinishPosition < 0)
                tournament.FinishPosition = 0;

            if (tournament.PrizeWon < 0)
                tournament.PrizeWon = 0;

            if (tournament.AddonCount < 0)
                tournament.AddonCount = 0;

            if (tournament.RebuyCount < 0)
                tournament.RebuyCount = 0;

            if (tournament.JackpotSpinMultiplier < 0)
                tournament.JackpotSpinMultiplier = 0;

            if (tournament.BountyCount < 0)
                tournament.BountyCount = 0;

            // temp end time for restoring end time if they cancel the finish
            DateTime tempEndTime = tournament.EndTime;

            // if the end time is before or the same as the starting time make the end time the current time (user wants to update time and finish at the same time)
            if (tournament.EndTime <= tournament.StartTime) tournament.EndTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            // if the end time is before the starting time
            if (tournament.EndTime < tournament.StartTime)
            {
                var theVm = new OkViewModel($"{tournament.TournamentName}'s ending time can't be before the starting time", "Time Error");
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.MainWindow,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                tournament.EndTime = tempEndTime;

                return false;
            }

            // if the starting time is after ending time
            if (tournament.StartTime > tournament.EndTime)
            {
                var theVm = new OkViewModel($"{tournament.TournamentName} starting time can't be after the ending time", "Time Error");
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.MainWindow,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                tournament.EndTime = tempEndTime;

                return false;
            }

            return true;
        }

        /// <summary>
        /// Finish a tournament
        /// </summary>
        /// <param name="parameter">tournament to finish</param>
        private static void FinishTournament(object parameter)
        {
            TournamentRunning tournament;

            if (parameter is not null)
                tournament = parameter as TournamentRunning;
            else return;

            if (tournament is null)
                return;

            if (ErrorCheckFinishedTournament(tournament) is false)
                return;

            var vm = new FinishTournamentViewModel(tournament);
            var window = new FinishTournamentView(vm)
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            window.ShowDialog();

            if (vm.Saved is false)
                return;

            // add record to database, quit if error(false returned)
            if (DatabaseHelper.AddRecord(new TournamentFinished(tournament)) is false)
                return;

            // update database id of tournament
            tournament.DatabaseId = DatabaseHelper.NewDatabaseId();

            // remove from running tournaments
            GetDataGridViewModel().RunningTournaments.Remove(tournament);

            // save running tournaments to file
            TournamentHelper.SaveRunningTournaments(GetDataGridViewModel().RunningTournaments.ToList());

            // add tournament to recently finished tournaments
            MenuCommands.GetMenuViewModel()?.AddRecentlyFinished(tournament);

            // update tournament visibility
            UpdateTournamentVisibility();

            // load database
            var tournaments = DatabaseHelper.LoadDatabase();

            // update bankrolls
            MenuCommands.GetMenuViewModel()?.LoadBankrolls(tournaments);

            // see if quick results window is open and update it
            if (Application.Current.Windows.OfType<QuickResultsView>()?.FirstOrDefault()?.DataContext is QuickResultsViewModel quickResultsViewModel)
                quickResultsViewModel.Update(tournaments);

            // see if tournament results window is open and update it
            if (Application.Current.Windows.OfType<TournamentsResultsView>()?.FirstOrDefault()?.DataContext is TournamentsResultsViewModel tournamentsResultsViewModel)
                tournamentsResultsViewModel.Update(tournaments);

            // see if session results window is open and update it
            if (Application.Current.Windows.OfType<SessionResultsView>()?.FirstOrDefault()?.DataContext is SessionResultsViewModel sessionResultsViewModel)
                sessionResultsViewModel.Update(tournaments);
        }

        /// <summary>
        /// returns the datagrid view model
        /// </summary>
        /// <returns>datagrid view model</returns>
        public static DataGridViewModel GetDataGridViewModel()
        {
            return Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.TournamentsDataGrid.DataContext as DataGridViewModel;
        }

        /// <summary>
        /// returns the list of visible tournaments in the running tournaments list
        /// </summary>
        /// <returns>list of visible tournaments</returns>
        private static IEnumerable<TournamentRunning> GetVisibleTournamentsFromRunningTournaments()
        {
            DataGridViewModel dataGridViewModel = GetDataGridViewModel();

            var tournaments = new ObservableCollection<TournamentRunning>();

            if (dataGridViewModel is null || dataGridViewModel.RunningTournaments is null || dataGridViewModel.RunningTournaments.Count is 0)
                return tournaments;

            // sort running tournaments based on visibility status (tournaments handle changing their own status)
            switch (dataGridViewModel.Visibility)
            {
                case TournamentVisibility.ShowAll:
                    tournaments = dataGridViewModel.RunningTournaments;
                    break;
                case TournamentVisibility.ShowStarted:
                    tournaments = new ObservableCollection<TournamentRunning>(dataGridViewModel.RunningTournaments.Where(i =>
                        i.TournamentStartingVisibility is TournamentVisibility.ShowStarted));
                    break;
                case TournamentVisibility.ShowFive:
                    tournaments = new ObservableCollection<TournamentRunning>(dataGridViewModel.RunningTournaments.Where(i =>
                        i.TournamentStartingVisibility is TournamentVisibility.ShowStarted ||
                        i.TournamentStartingVisibility is TournamentVisibility.ShowFive));
                    break;
                case TournamentVisibility.ShowFifteen:
                    tournaments = new ObservableCollection<TournamentRunning>(dataGridViewModel.RunningTournaments.Where(i =>
                        i.TournamentStartingVisibility is TournamentVisibility.ShowStarted ||
                        i.TournamentStartingVisibility is TournamentVisibility.ShowFive ||
                        i.TournamentStartingVisibility is TournamentVisibility.ShowFifteen));
                    break;
                case TournamentVisibility.ShowThirty:
                    tournaments = new ObservableCollection<TournamentRunning>(dataGridViewModel.RunningTournaments.Where(i =>
                        i.TournamentStartingVisibility is TournamentVisibility.ShowStarted ||
                        i.TournamentStartingVisibility is TournamentVisibility.ShowFive ||
                        i.TournamentStartingVisibility is TournamentVisibility.ShowFifteen ||
                        i.TournamentStartingVisibility is TournamentVisibility.ShowThirty));
                    break;
                case TournamentVisibility.ShowSixty:
                    tournaments = new ObservableCollection<TournamentRunning>(dataGridViewModel.RunningTournaments.Where(i =>
                        i.TournamentStartingVisibility is TournamentVisibility.ShowStarted ||
                        i.TournamentStartingVisibility is TournamentVisibility.ShowFive ||
                        i.TournamentStartingVisibility is TournamentVisibility.ShowFifteen ||
                        i.TournamentStartingVisibility is TournamentVisibility.ShowThirty ||
                        i.TournamentStartingVisibility is TournamentVisibility.ShowSixty));
                    break;
                case TournamentVisibility.Queued:
                    tournaments = new ObservableCollection<TournamentRunning>(dataGridViewModel.RunningTournaments.Where(i =>
                        i.TournamentStartingVisibility is TournamentVisibility.Queued));
                    break;
            }

            return tournaments;
        }

        /// <summary>
        /// Check that the tournament template is saved to the file with a valid template id number (positive number)
        /// -1 is used for non template tournaments
        /// </summary>
        /// <param name="tournament">tournament to be checked</param>
        /// <returns>true if saved to file</returns>
        private static bool IsSavedTemplate(TournamentRunning tournament)
        {
            if (tournament is null)
                return false;

            // all saved templates have a positive template id, non-saved templates use a negative value
            if (tournament.TemplateId is >= 0)
                return true;

            var vm = new OkViewModel($"{tournament.TournamentName} is not saved to the tournaments file. \n\nTo save {tournament.TournamentName} use the Update Tournament Data option under \nTournament Data in the right click context menu and check the \nSave as a New Tournament box.", "Tournament Not Saved");
            var theWindow = new OkView(vm)
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            theWindow.ShowDialog();

            return false;
        }

        /// <summary>
        /// add/remove tournament from favorites
        /// </summary>
        private static void PinToFavorites()
        {
            DataGridViewModel dataGridViewModel = GetDataGridViewModel();

            TournamentRunning selectedTournament = dataGridViewModel?.SelectedTournament;

            if (dataGridViewModel is null || selectedTournament is null || IsSavedTemplate(selectedTournament) is false)
                return;

            // update any other running tournaments with this id
            foreach (var tournament in dataGridViewModel.RunningTournaments)
                if (tournament.TemplateId == selectedTournament.TemplateId)
                    tournament.IsFavorite = !tournament.IsFavorite;

            // update favorite property and update template file
            TournamentTemplateHelper.SaveTournamentTemplate(selectedTournament, true);

            // update templates in menu view model
            MenuCommands.GetMenuViewModel()?.UpdateFavoriteTemplates();

            // update tournament template manager if open
            TemplateManagerCommands.GetTemplateManagerViewModel()?.Reload();
        }

        /// <summary>
        /// launch from remove from sessions window
        /// </summary>
        private static void RemoveFromSession()
        {
            MenuViewModel menuViewModel = MenuCommands.GetMenuViewModel();

            TournamentRunning selectedTournament = GetDataGridViewModel()?.SelectedTournament;

            if (menuViewModel is null || menuViewModel.Templates is null || menuViewModel.Templates.Count is 0 || selectedTournament is null || IsSavedTemplate(selectedTournament) is false)
                return;

            if (menuViewModel.Sessions is null || menuViewModel.Sessions.Count is 0)
            {
                var vvm = new OkViewModel("No sessions created, use the Session Creator to make some", "No Sessions Created");
                var wwindow = new OkView(vvm)
                {
                    Owner = Application.Current.MainWindow,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                wwindow.ShowDialog();

                return;
            }

            // load session templates that contain the template
            var sessions = SessionTemplateHelper.LoadSessionTemplates(menuViewModel.Templates);
            var Sessions = new ObservableCollection<SessionTemplate>(sessions.Where(i => i.TemplateIds.Any(i => i == selectedTournament.TemplateId)));

            if (Sessions is null || Sessions.Count is 0)
            {
                var v = new OkViewModel("Tournament is not included in any sessions", "Not Included");
                var windo = new OkView(v)
                {
                    Owner = Application.Current.MainWindow,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                windo.ShowDialog();

                return;
            }

            var vm = new RemoveFromSessionViewModel(selectedTournament,Sessions);
            var window = new RemoveFromSessionView(vm)
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            window.ShowDialog();

            menuViewModel.UpdateFavoriteSessions();
        }

        /// <summary>
        /// remove an attached screenshot from a tournament
        /// </summary>
        private static void RemoveScreenshot()
        {
            TournamentRunning selectedTournament = GetDataGridViewModel()?.SelectedTournament;

            if (selectedTournament is null || string.IsNullOrWhiteSpace(selectedTournament.ScreenshotFilename))
            {
                var theVm = new OkViewModel("No screenshot attached", "None Attached");
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                return;
            }

            var vm = new YesNoViewModel($"Are you sure you want to remove the screenshot attached to {selectedTournament.TournamentName}?", "Remove Screenshot");
            var window = new YesNoView(vm)
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            window.ShowDialog();

            if (vm.Saved is false)
                return;

            ScreenshotHelper.DeleteScreenshot(selectedTournament, Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault());

            selectedTournament.ScreenshotFilename = "";
        }

        /// <summary>
        /// start a duplicate tournament
        /// </summary>
        private static void StartDuplicate()
        {
            TournamentRunning selectedTournament = GetDataGridViewModel()?.SelectedTournament;

            if (selectedTournament is null)
                return;

            // allow only one start duplicate window open at a time
            if (Application.Current.Windows.OfType<CreateDuplicateView>().Any())
            {
                // window
                var win = Application.Current.Windows.OfType<CreateDuplicateView>().FirstOrDefault();

                // restore if minimized
                if (win.WindowState is WindowState.Minimized)
                    win.WindowState = WindowState.Normal;

                // bring window to the front
                win.Activate();
                win.Topmost = true;
                win.Topmost = false;
                win.Focus();

                // leave
                return;
            }

            var vm = new CreateDuplicateViewModel(new TournamentRunning(selectedTournament));
            var window = new CreateDuplicateView(vm);

            window.Left = Application.Current.MainWindow.Left + (Application.Current.MainWindow.Width / 2) - (window.Width / 2);
            window.Top = Application.Current.MainWindow.Top - (window.Height / 2);

            window.Show();
        }

        /// <summary>
        /// update the data grid column orders
        /// </summary>
        private static void UpdateColumnOrder()
        {
            // datagrid columns
            var columns = Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.TournamentsDataGrid.Columns;

            if (columns is null)
                return;

            // find columns
            var addonCountColumn = columns?.FirstOrDefault(i => i.MappingName is "AddonCount");
            var addonTotalCostColumn = columns?.FirstOrDefault(i => i.MappingName is "AddonTotalCost");
            var bountyCountColumn = columns?.FirstOrDefault(i => i.MappingName is "BountyCount");
            var buyinTotalCostColumn = columns?.FirstOrDefault(i => i.MappingName is "BuyinTotalCost");
            var entrantsColumn = columns?.FirstOrDefault(i => i.MappingName is "Entrants");
            var entrantsPaidColumn = columns?.FirstOrDefault(i => i.MappingName is "EntrantsPaid");
            var finishPositionColumn = columns?.FirstOrDefault(i => i.MappingName is "FinishPosition");
            var jackpotSpinMultiplierColumn = columns?.FirstOrDefault(i => i.MappingName is "JackpotSpinMultiplier");
            var prizeWonColumn = columns?.FirstOrDefault(i => i.MappingName is "PrizeWon");
            var rebuyCountColumn = columns?.FirstOrDefault(i => i.MappingName is "RebuyCount");
            var rebuyTotalCostColumn = columns?.FirstOrDefault(i => i.MappingName is "RebuyTotalCost");
            var startTimeColumn = columns?.FirstOrDefault(i => i.MappingName is "StartTime");
            var totalCostColumn = columns?.FirstOrDefault(i => i.MappingName is "TotalCost");
            var tournamentNameColumn = columns?.FirstOrDefault(i => i.MappingName is "TournamentName");
            var venueColumn = columns?.FirstOrDefault(i => i.MappingName is "Venue");

            // save column orders
            if (addonCountColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "DataGridColumnOrder", "AddonCountColumnOrder", columns.IndexOf(addonCountColumn));
            if (addonTotalCostColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "DataGridColumnOrder", "AddonTotalCostColumnOrder", columns.IndexOf(addonTotalCostColumn));
            if (bountyCountColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "DataGridColumnOrder", "BountyCountColumnOrder", columns.IndexOf(bountyCountColumn));
            if (buyinTotalCostColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "DataGridColumnOrder", "BuyinTotalCostColumnOrder", columns.IndexOf(buyinTotalCostColumn));
            if (entrantsColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "DataGridColumnOrder", "EntrantsColumnOrder", columns.IndexOf(entrantsColumn));
            if (entrantsPaidColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "DataGridColumnOrder", "EntrantsPaidColumnOrder", columns.IndexOf(entrantsPaidColumn));
            if (finishPositionColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "DataGridColumnOrder", "FinishPositionColumnOrder", columns.IndexOf(finishPositionColumn));
            if (jackpotSpinMultiplierColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "DataGridColumnOrder", "JackpotSpinMultiplierColumnOrder", columns.IndexOf(jackpotSpinMultiplierColumn));
            if (prizeWonColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "DataGridColumnOrder", "PrizeWonColumnOrder", columns.IndexOf(prizeWonColumn));
            if (rebuyCountColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "DataGridColumnOrder", "RebuyCountColumnOrder", columns.IndexOf(rebuyCountColumn));
            if (rebuyTotalCostColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "DataGridColumnOrder", "RebuyTotalCostColumnOrder", columns.IndexOf(rebuyTotalCostColumn));
            if (startTimeColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "DataGridColumnOrder", "StartTimeColumnOrder", columns.IndexOf(startTimeColumn));
            if (totalCostColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "DataGridColumnOrder", "TotalCostColumnOrder", columns.IndexOf(totalCostColumn));
            if (tournamentNameColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "DataGridColumnOrder", "TournamentNameColumnOrder", columns.IndexOf(tournamentNameColumn));
            if (addonCountColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "DataGridColumnOrder", "VenueColumnOrder", columns.IndexOf(venueColumn));
        }

        /// <summary>
        /// update the data grid column widths
        /// </summary>
        private static void UpdateColumnWidth()
        {
            // datagrid columns
            var columns = Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.TournamentsDataGrid.Columns;

            if(columns is null)
                return;

            // find columns
            var addonCountColumn = columns?.FirstOrDefault(i => i.MappingName is "AddonCount");
            var addonTotalCostColumn = columns?.FirstOrDefault(i => i.MappingName is "AddonTotalCost");
            var bountyCountColumn = columns?.FirstOrDefault(i => i.MappingName is "BountyCount");
            var buyinTotalCostColumn = columns?.FirstOrDefault(i => i.MappingName is "BuyinTotalCost");
            var entrantsColumn = columns?.FirstOrDefault(i => i.MappingName is "Entrants");
            var entrantsPaidColumn = columns?.FirstOrDefault(i => i.MappingName is "EntrantsPaid");
            var finishPositionColumn = columns?.FirstOrDefault(i => i.MappingName is "FinishPosition");
            var jackpotSpinMultiplierColumn = columns?.FirstOrDefault(i => i.MappingName is "JackpotSpinMultiplier");
            var prizeWonColumn = columns?.FirstOrDefault(i => i.MappingName is "PrizeWon");
            var rebuyCountColumn = columns?.FirstOrDefault(i => i.MappingName is "RebuyCount");
            var rebuyTotalCostColumn = columns?.FirstOrDefault(i => i.MappingName is "RebuyTotalCost");
            var startTimeColumn = columns?.FirstOrDefault(i => i.MappingName is "StartTime");
            var totalCostColumn = columns?.FirstOrDefault(i => i.MappingName is "TotalCost");
            var tournamentNameColumn = columns?.FirstOrDefault(i => i.MappingName is "TournamentName");
            var venueColumn = columns?.FirstOrDefault(i => i.MappingName is "Venue");

            // save column widths
            if (addonCountColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "DataGridColumnWidth", "AddonCountColumnWidth", addonCountColumn.ActualWidth);
            if (addonTotalCostColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "DataGridColumnWidth", "AddonTotalCostColumnWidth", addonTotalCostColumn.ActualWidth);
            if (bountyCountColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "DataGridColumnWidth", "BountyCountColumnWidth", bountyCountColumn.ActualWidth);
            if (buyinTotalCostColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "DataGridColumnWidth", "BuyinTotalCostColumnWidth", buyinTotalCostColumn.ActualWidth);
            if (entrantsColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "DataGridColumnWidth", "EntrantsColumnWidth", entrantsColumn.ActualWidth);
            if (entrantsPaidColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "DataGridColumnWidth", "EntrantsPaidColumnWidth", entrantsPaidColumn.ActualWidth);
            if (finishPositionColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "DataGridColumnWidth", "FinishPositionColumnWidth", finishPositionColumn.ActualWidth);
            if (jackpotSpinMultiplierColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "DataGridColumnWidth", "JackpotSpinMultiplierColumnWidth", jackpotSpinMultiplierColumn.ActualWidth);
            if (prizeWonColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "DataGridColumnWidth", "PrizeWonColumnWidth", prizeWonColumn.ActualWidth);
            if (rebuyCountColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "DataGridColumnWidth", "RebuyCountColumnWidth", rebuyCountColumn.ActualWidth);
            if (rebuyTotalCostColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "DataGridColumnWidth", "RebuyTotalCostColumnWidth", rebuyTotalCostColumn.ActualWidth);
            if (startTimeColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "DataGridColumnWidth", "StartTimeColumnWidth", startTimeColumn.ActualWidth);
            if (totalCostColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "DataGridColumnWidth", "TotalCostColumnWidth", totalCostColumn.ActualWidth);
            if (tournamentNameColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "DataGridColumnWidth", "TournamentNameColumnWidth", tournamentNameColumn.ActualWidth);
            if (venueColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "DataGridColumnWidth", "VenueColumnWidth", venueColumn.ActualWidth);
        }

        /// <summary>
        /// update selected tournaments ending time
        /// </summary>
        private static void UpdateEndTime()
        {
            DataGridViewModel dataGridViewModel = GetDataGridViewModel();

            TournamentRunning selectedTournament = dataGridViewModel?.SelectedTournament;

            if (dataGridViewModel is null || selectedTournament is null)
                return;

            var vm = new YesNoViewModel($"Are you sure you want to update the ending time for\n\n{selectedTournament.TournamentName} to {dataGridViewModel.Time.ToShortTimeString()}?", "Update Ending Time");
            if (selectedTournament.StartTime.Date != selectedTournament.EndTime.Date)
                vm = new YesNoViewModel($"Are you sure you want to update the ending time for\n\n{selectedTournament.TournamentName} to {dataGridViewModel.Time.ToShortDateString()} {dataGridViewModel.Time.ToShortTimeString()}?", "Update Ending Time");

            var window = new YesNoView(vm)
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            window.ShowDialog();

            if (vm.Saved is false)
                return;

            selectedTournament.EndTime = dataGridViewModel.Time;
        }

        /// <summary>
        /// update selected tournaments ending time to a specific time
        /// </summary>
        private static void UpdateEndTimeSelected()
        {
            TournamentRunning selectedTournament = GetDataGridViewModel()?.SelectedTournament;

            if (selectedTournament is null)
                return;

            var vm = new UpdateEndTimeSelectedViewModel(selectedTournament);
            var window = new UpdateEndTimeSelectedView(vm)
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            window.ShowDialog();

            if (vm.Saved is false)
                return;

            selectedTournament.EndTime = vm.NewTime;
        }

        /// <summary>
        /// update selected tournaments starting time
        /// </summary>
        private static void UpdateStartTime()
        {
            DataGridViewModel dataGridViewModel = GetDataGridViewModel();

            TournamentRunning selectedTournament = dataGridViewModel?.SelectedTournament;

            if (selectedTournament is null)
                return;

            var vm = new YesNoViewModel($"Are you sure you want to update the starting time for\n\n{selectedTournament.TournamentName} to {dataGridViewModel.Time.ToShortTimeString()}?", "Update Starting Time");
            if (selectedTournament.StartTime.Date.Date != dataGridViewModel.Time.Date.Date)
                vm = new YesNoViewModel($"Are you sure you want to update the starting time for\n{selectedTournament.TournamentName} to {dataGridViewModel.Time.ToShortDateString()} {dataGridViewModel.Time.ToShortTimeString()}?", "Update Starting Time");

            var window = new YesNoView(vm)
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            window.ShowDialog();

            if (vm.Saved is false)
                return;

            selectedTournament.StartTime = dataGridViewModel.Time;

            if (selectedTournament.StartTime > selectedTournament.EndTime)
                selectedTournament.EndTime = selectedTournament.StartTime;

            UpdateTournamentVisibility();
        }

        /// <summary>
        /// update selected tournaments starting time to a specific time
        /// </summary>
        private static void UpdateStartTimeSelected()
        {
            TournamentRunning selectedTournament = GetDataGridViewModel()?.SelectedTournament;

            if (selectedTournament is null)
                return;

            var vm = new UpdateStartTimeSelectedViewModel(selectedTournament);
            var window = new UpdateStartTimeSelectedView(vm)
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            window.ShowDialog();

            if (vm.Saved is false)
                return;

            if (selectedTournament.StartTime == selectedTournament.EndTime)
                selectedTournament.EndTime = vm.NewTime;

            selectedTournament.StartTime = vm.NewTime;

            if (selectedTournament.StartTime > selectedTournament.EndTime)
                selectedTournament.EndTime = selectedTournament.StartTime;

            UpdateTournamentVisibility();
        }

        /// <summary>
        /// update data grid context menu time string when menu is opening
        /// </summary>
        private static void UpdateTimeStrings()
        {
            GetDataGridViewModel()?.UpdateTimeStrings();
        }

        /// <summary>
        /// update visibility status of the running tournaments
        /// </summary>
        private static void UpdateTournamentVisibility()
        {
            DataGridViewModel dataGridViewModel = GetDataGridViewModel();

            if (dataGridViewModel is null)
                return;

            dataGridViewModel.VisibleTournaments = new ObservableCollection<TournamentRunning>(GetVisibleTournamentsFromRunningTournaments().OrderBy(i => i.StartTime));
        }

        /// <summary>
        /// view the currently attached screenshot
        /// </summary>
        private static void ViewScreenshot()
        {
            TournamentRunning selectedTournament = GetDataGridViewModel()?.SelectedTournament;

            if (selectedTournament is null || string.IsNullOrWhiteSpace(selectedTournament.ScreenshotFilename))
            {
                var theVm = new OkViewModel($"No screenshot attached to {selectedTournament.TournamentName}", "No Image Attached");
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.MainWindow,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                return;
            }

            var process = new Process
            {
                StartInfo = new ProcessStartInfo(selectedTournament.ScreenshotFilename)
                {
                    UseShellExecute = true,
                }
            };

            try
            {
                process.Start();
            }
            catch (Win32Exception ex)
            {
                var theVm = new OkViewModel($"No screenshot attached to {selectedTournament.TournamentName}", "No Image Attached");
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.MainWindow,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                selectedTournament.ScreenshotFilename = "";
            }
        }
    }
}