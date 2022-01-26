using System.Collections.ObjectModel;
using System.Linq;
using LiveTracker.Helpers;
using LiveTracker.Models;
using Syncfusion.Windows.Shared;
using System.Windows.Input;
using Syncfusion.UI.Xaml.Utility;
using System.Windows;
using Tournament_Life.Views.Results;
using Syncfusion.UI.Xaml.Grid;
using Tournament_Life.Helpers;
using System.IO;
using System.Diagnostics;
using LiveTracker.Views.Results;
using LiveTracker.ViewModels.Results;
using LiveTracker.Views;
using LiveTracker.ViewModels.Menu_ViewModels;
using Tournament_Life.Views;
using LiveTracker.ViewModels.Datagrid_ViewModels;
using LiveTracker.Views.Datagrid_Views;
using System.ComponentModel;

namespace Tournament_Life.ViewModels.Results
{
    public class TournamentsViewModel : NotificationObject
    {
        public ICommand AddScreenshotCommand => new BaseCommand(AddScreenshot);
        public ICommand DeleteRecordCommand => new BaseCommand(DeleteRecord);
        public ICommand EditRecordCommand => new BaseCommand(EditRecord);
        public ICommand ExcelCommand => new BaseCommand(Excel);
        public ICommand ExitCommand => new BaseCommand(ExitWindow);
        public ICommand RemoveScreenshotCommand => new BaseCommand(RemoveScreenshot);
        public ICommand UpdateColumnOrderCommand => new BaseCommand(UpdateColumnOrder);
        public ICommand ViewNotesCommand => new BaseCommand(ViewNotes);
        public ICommand ViewScreenshotCommand => new BaseCommand(ViewScreenshot);
        public ICommand WindowLoadedCommand => new BaseCommand(WindowLoaded);

        public TournamentsViewModel(ObservableCollection<TournamentFinished> tournaments)
        {
            // default window prefs
            FontColor = PreferencesHelper.FindPreference("LiveTracker", "Window", "FontColor");
            FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));
            Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");
            IsSession = false;
            SessionId = -1;
            ShowRowHeader = true;
            ThisWindow = false;

            // set title
            Title = "Tournaments - " + ProfileHelper.GetCurrentProfile();

            // load tournaments
            Tournaments = new ObservableCollection<TournamentFinished>(tournaments.OrderByDescending(i => i.StartTime));
        }
        public TournamentsViewModel(SessionModel session)
        {
            // default window prefs
            FontColor = PreferencesHelper.FindPreference("LiveTracker", "Window", "FontColor");
            FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));
            Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");
            IsSession = true;
            SessionId = session.ID;
            ShowRowHeader = true;
            ThisWindow = false;

            // set title
            Title = "Tournaments - " + ProfileHelper.GetCurrentProfile();

            // load tournaments
            Tournaments = new ObservableCollection<TournamentFinished>(session.Tournaments.OrderByDescending(i => i.StartTime));
        }

        public string FontColor { get; set; }
        public int FontSize { get; set; }
        public bool IsSession { get; set; }
        public TournamentFinished SelectedTournament { get; set; }
        public int SessionId { get; set; }
        public bool ShowRowHeader { get; set; }
        public ObservableCollection<TournamentFinished> Tournaments { get; set; }
        public string Theme { get; set; }
        public bool ThisWindow { get; set; }
        public string Title { get; set; }

        /// <summary>
        /// Add a screenshot
        /// </summary>
        /// <param name="parameter"></param>
        private void AddScreenshot(object parameter)
        {
            // no selected or no screenshot, quit
            if (SelectedTournament is null) return;

            // confirm overwrite, if needed
            if (File.Exists(SelectedTournament.ScreenshotFilename))
            {
                // yes/no view model
                var vm = new YesNoViewModel("A screenshot already exists, are you sure you want to overwrite?", "Overwrite Screenshot");

                // create/show yes/no window
                var window = new YesNoView(vm)
                {
                    Owner = FindWindow(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                window.ShowDialog();

                // not saved
                if (vm.Saved is false) return;
            }

            // file dialog for image files
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png",
            };

            // show open file dialog
            openFileDialog.ShowDialog();

            // no file selected
            if (openFileDialog.FileName.Length is 0) return;

            // remove old screenshot
            if (SelectedTournament.IsScreenshotAttached is true && SelectedTournament.ScreenshotFilename != "") { ScreenshotHelper.DeleteScreenshot(SelectedTournament,FindWindow()); SelectedTournament.ScreenshotFilename = ""; }

            // save screenshot
            ScreenshotHelper.SaveScreenshot(openFileDialog.FileName, SelectedTournament, FindWindow());

            // update record
            DatabaseHelper.EditRecord(SelectedTournament, SelectedTournament.DatabaseId);
        }

        /// <summary>
        /// Delete a record
        /// </summary>
        /// <param name="parameter"></param>
        private void DeleteRecord(object parameter)
        {
            // no selected or no screenshot, quit
            if (SelectedTournament is null) return;

            // yes/no view model
            var vm = new YesNoViewModel("Are you sure you want to permanently delete this record?", "Delete Record");

            // create/show yes/no window
            var window = new YesNoView(vm)
            {
                Owner = FindWindow(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            window.ShowDialog();

            // not saved
            if (vm.Saved is false) return;

            // delete from database
            DatabaseHelper.DeleteRecord(SelectedTournament.DatabaseId);

            // flag our window
            ThisWindow = true;

            // scan over any open tournaments windows for this tournaments database id and remove them too
            foreach (var win in Application.Current.Windows.OfType<TournamentsView>().ToList())
            {
                // get the windows view model
                var vmm = win?.DataContext as TournamentsViewModel;

                // null check
                if (vmm is null) continue;

                // don't touch our window
                if (vmm.ThisWindow) continue;

                // check for a matching database id
                var tournaments = win?.TournamentsDataGrid?.ItemsSource as ObservableCollection<TournamentFinished>;

                // empty go next
                if (tournaments is null || tournaments.Count is 0) continue;

                // remove the deleted tournament
                foreach (var tournament in tournaments.ToList()) if(tournament.DatabaseId == SelectedTournament.DatabaseId) tournaments.Remove(tournament);

                // close window if it was the last tournament
                if(tournaments.Count is 0) win?.Close();
            }

            // get menu view model
            var menuViewModel = Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.LiveTrackerMenu.DataContext as MenuViewModel;

            // find matching database id on a tournament in the recently finished tournaments list
            var removeTournament = menuViewModel.RecentlyFinishedTournaments.Where(i => i.DatabaseId == SelectedTournament.DatabaseId)?.FirstOrDefault();

            // null check, if null don't remove anything
            if (removeTournament is not null)
            {
                // remove tournament from recently finished tournaments list
                menuViewModel.RecentlyFinishedTournaments.Remove(removeTournament);

                // save updated list
                menuViewModel.SaveRecentlyFinishedTournaments();
            }

            // remove from this collection
            Tournaments.Remove(SelectedTournament);

            // remove flag for our window
            ThisWindow = false;

            // load database
            var databaseTournaments = DatabaseHelper.LoadDatabase();

            // update bankrolls
            menuViewModel.LoadBankrolls(databaseTournaments);

            // see if quick results window is open and update it
            if (Application.Current.Windows.OfType<QuickResultsView>()?.FirstOrDefault()?.DataContext is QuickResultsViewModel quickResultsViewModel) quickResultsViewModel.Update(databaseTournaments);

            // see if tournament results window is open and update it
            if (Application.Current.Windows.OfType<TournamentsResultsView>()?.FirstOrDefault()?.DataContext is TournamentsResultsViewModel tournamentsResultsViewModel) tournamentsResultsViewModel.Update(databaseTournaments);

            // see if session results window is open and update it
            if (Application.Current.Windows.OfType<SessionResultsView>()?.FirstOrDefault()?.DataContext is SessionResultsViewModel sessionResultsViewModel) sessionResultsViewModel.Update(databaseTournaments);

            // create/show success message
            var okayVM1 = new OkViewModel("Record was deleted successfully", "Delete Record");
            var okayWindow1 = new OkView(okayVM1) { Owner = FindWindow(), WindowStartupLocation = WindowStartupLocation.CenterOwner, }; okayWindow1.ShowDialog();

            // if no records exist anymore close the window
            if (Tournaments.Count is 0) ExitWindow(null);
        }

        /// <summary>
        /// Edit record
        /// </summary>
        /// <param name="parameter"></param>
        private void EditRecord(object parameter)
        {
            // no selected or no screenshot, quit
            if (SelectedTournament is null) return;

            // create/show edit window
            var vm = new EditRecordViewModel(SelectedTournament);
            var window = new EditRecordView(vm)
            {
                Owner = FindWindow(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            window.ShowDialog();

            // didn't save
            if (!vm.Saved) return;

            // edit database record
            DatabaseHelper.EditRecord(vm.TournamentData, vm.TournamentData.DatabaseId);

            // update record in collection
            SelectedTournament.UpdateData(vm.TournamentData);

            // get menu view model
            var menuViewModel = Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.LiveTrackerMenu.DataContext as MenuViewModel;

            // load database
            var databaseTournaments = DatabaseHelper.LoadDatabase();

            // update bankrolls
            menuViewModel.LoadBankrolls(databaseTournaments);

            // see if quick results window is open and update it
            if (Application.Current.Windows.OfType<QuickResultsView>()?.FirstOrDefault()?.DataContext is QuickResultsViewModel quickResultsViewModel) quickResultsViewModel.Update(databaseTournaments);

            // see if tournament results window is open and update it
            if (Application.Current.Windows.OfType<TournamentsResultsView>()?.FirstOrDefault()?.DataContext is TournamentsResultsViewModel tournamentsResultsViewModel) tournamentsResultsViewModel.Update(databaseTournaments);

            // see if session results window is open and update it
            if (Application.Current.Windows.OfType<SessionResultsView>()?.FirstOrDefault()?.DataContext is SessionResultsViewModel sessionResultsViewModel) sessionResultsViewModel.Update(databaseTournaments);

            // create/show success message
            var okayVM1 = new OkViewModel("Record was edited successfully", "Edit Record");
            var okayWindow1 = new OkView(okayVM1) { Owner = FindWindow(), WindowStartupLocation = WindowStartupLocation.CenterOwner, }; okayWindow1.ShowDialog();
        }

        /// <summary>
        /// Export to excel
        /// </summary>
        /// <param name="parameter"></param>
        private void Excel(object parameter)
        {
            // flag this window
            ThisWindow = true;

            // save excel file
            ExcelHelper.ExportToExcel(Tournaments);

            // find this window
            var windows = Application.Current.Windows.OfType<TournamentsView>();

            // loop through all tournaments windows
            foreach (var window in windows)
            {
                // get view model
                var vm = window.DataContext as TournamentsViewModel;

                // show message if its the one
                if (vm is not null && vm.ThisWindow)
                {
                    // ok view model
                    var theVm = new OkViewModel("Excel file saved successfully!", "Saved Successfully");

                    // create/show ok window
                    var theWindow = new OkView(theVm)
                    {
                        Owner = FindWindow(),
                        WindowStartupLocation = WindowStartupLocation.CenterOwner,
                    };
                    theWindow.ShowDialog();

                    // exit loop
                    break;
                }
            }

            // remove flag
            ThisWindow = false;
        }

        /// <summary>
        /// close the tournaments window
        /// </summary>
        /// <param name="parameter"></param>
        private void ExitWindow(object parameter)
        {
            // flag to find this window
            ThisWindow = true;

            // find this window
            var windows = Application.Current.Windows.OfType<TournamentsView>();

            // loop through all tournaments windows
            foreach (var window in windows)
            {
                // get view model
                var vm = window.DataContext as TournamentsViewModel;

                // close if its this one
                if (vm is not null) if (vm.ThisWindow) { window.Close(); break; }
            }
        }

        /// <summary>
        /// find the reference to this specific window
        /// </summary>
        private Window FindWindow()
        {
            // flag to find this window
            ThisWindow = true;

            // find this window
            var windows = Application.Current.Windows.OfType<TournamentsView>();

            // loop through all tournaments windows
            foreach (var window in windows)
            {
                // get view model
                var vm = window.DataContext as TournamentsViewModel;

                // close if its this one
                if (vm is not null) if (vm.ThisWindow) { return window; }
            }

            // no window found
            return null;
        }

        /// <summary>
        /// Remove a screenshot
        /// </summary>
        /// <param name="parameter"></param>
        private void RemoveScreenshot(object parameter)
        {
            // no selected or no screenshot, quit
            if (SelectedTournament is null || SelectedTournament.ScreenshotFilename == "None" || SelectedTournament.ScreenshotFilename.Length <= 0)
            {
                // ok view model
                var theVm = new OkViewModel("No saved screenshot", "Not Saved");

                // create/show ok window
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.MainWindow,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                // leave
                return;
            }

            // yes/no view model
            var vm = new YesNoViewModel("Are you sure you want to delete this screenshot?", "Delete Screenshot");

            // create/show yes/no window
            var window = new YesNoView(vm)
            {
                Owner = FindWindow(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            window.ShowDialog();

            // not saved
            if (vm.Saved is false) return;

            // delete screenshot
            ScreenshotHelper.DeleteScreenshot(SelectedTournament, FindWindow());

            // clear screenshot property
            SelectedTournament.ScreenshotFilename = "";

            // update record
            DatabaseHelper.EditRecord(SelectedTournament, SelectedTournament.DatabaseId);
        }

        /// <summary>
        /// Show the notes
        /// </summary>
        /// <param name="parameter"></param>
        private void ViewNotes(object parameter)
        {
            if (SelectedTournament is null || SelectedTournament.Note is null) return;

            // create note window, include any note text from previous notes
            var vm = new AttachNoteViewModel(SelectedTournament.Note, SelectedTournament.TournamentName);
            var window = new AttachNoteView(vm)
            {
                Owner = FindWindow(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };

            // show note window, wait for close
            window.ShowDialog();

            // edit database record
            DatabaseHelper.EditRecord(SelectedTournament, SelectedTournament.DatabaseId);
        }

        /// <summary>
        /// Show screenshot attached to record
        /// </summary>
        /// <param name="parameter"></param>
        private void ViewScreenshot(object parameter)
        {
            // no selected or no screenshot, quit
            if (SelectedTournament is null || SelectedTournament.ScreenshotFilename == "None" || SelectedTournament.ScreenshotFilename.Length is 0)
            {
                // ok view model
                var theVm = new OkViewModel("No saved screenshot", "No Screenshot");

                // create/show ok window
                var theWindow = new OkView(theVm)
                {
                    Owner = FindWindow(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                // exit
                return;
            }

            // show screenshot
            var process = new Process();
            process.StartInfo = new ProcessStartInfo(SelectedTournament.ScreenshotFilename)
            {
                UseShellExecute = true,
            };

            try
            {
                process.Start();
            }
            catch (Win32Exception ex)
            {
                var theVm = new OkViewModel($"No screenshot attached to {SelectedTournament.TournamentName}", "No Image Attached");
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.MainWindow,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                SelectedTournament.ScreenshotFilename = "";
            }
        }

        /// <summary>
        /// called to adjust columns after window is loaded
        /// </summary>
        private void WindowLoaded(object parameter)
        {
            // flag our window
            ThisWindow = true;

            // hold our tournaments view
            var tournamentsView = new TournamentsView(null);

            // hold our tournaments view columns
            var columns = new Columns();

            // scan over tournaments windows for this tournaments database id and remove them too
            foreach (var window in Application.Current.Windows.OfType<TournamentsView>().ToList())
            {
                // get the windows view model
                var vm = window?.DataContext as TournamentsViewModel;

                // null check
                if (vm is null) continue;

                // grab columns and leave loop when we find our window
                if (vm.ThisWindow) { columns = window.TournamentsDataGrid.Columns; tournamentsView = window; break; }
            }

            // remove flag
            ThisWindow = false;

            // null check
            if (columns is null) return;

            // find columns
            var addonBaseCostColumn = columns?.FirstOrDefault(i => i.MappingName is "AddonBaseCost");
            var addonCountColumn = columns?.FirstOrDefault(i => i.MappingName is "AddonCount");
            var addonRakeCostColumn = columns?.FirstOrDefault(i => i.MappingName is "AddonRakeCost");
            var addonTotalCostColumn = columns?.FirstOrDefault(i => i.MappingName is "AddonTotalCost");
            var blindLevelsColumn = columns?.FirstOrDefault(i => i.MappingName is "BlindLevels");
            var bountyColumn = columns?.FirstOrDefault(i => i.MappingName is "Bounty");
            var buyinBaseCostColumn = columns?.FirstOrDefault(i => i.MappingName is "BuyinBaseCost");
            var buyinRakeCostColumn = columns?.FirstOrDefault(i => i.MappingName is "BuyinRakeCost");
            var buyinTotalCostColumn = columns?.FirstOrDefault(i => i.MappingName is "BuyinTotalCost");
            var endDateColumn = columns?.FirstOrDefault(i => i.MappingName is "EndDate");
            var endTimeColumn = columns?.FirstOrDefault(i => i.MappingName is "EndTime");
            var entrantsColumn = columns?.FirstOrDefault(i => i.MappingName is "Entrants");
            var entrantsPaidColumn = columns?.FirstOrDefault(i => i.MappingName is "EntrantsPaid");
            var finishPositionColumn = columns?.FirstOrDefault(i => i.MappingName is "FinishPosition");
            var isBovadaBountyColumn = columns?.FirstOrDefault(i => i.MappingName is "IsBovadaBounty");
            var isSngColumn = columns?.FirstOrDefault(i => i.MappingName is "IsSng");
            var formatStringColumn = columns?.FirstOrDefault(i => i.MappingName is "FormatString");
            var gameTypeColumn = columns?.FirstOrDefault(i => i.MappingName is "GameType");
            var guaranteeColumn = columns?.FirstOrDefault(i => i.MappingName is "Guarantee");
            var jackpotSpinMultiplierColumn = columns?.FirstOrDefault(i => i.MappingName is "JackpotSpinMultiplier");
            var lenghtColumn = columns?.FirstOrDefault(i => i.MappingName is "Length");
            var lateRegColumn = columns?.FirstOrDefault(i => i.MappingName is "LateReg");
            var noteColumn = columns?.FirstOrDefault(i => i.MappingName is "Note");
            var prizeWonColumn = columns?.FirstOrDefault(i => i.MappingName is "PrizeWon");
            var profitColumn = columns?.FirstOrDefault(i => i.MappingName is "Profit");
            var rebuyBaseCostColumn = columns?.FirstOrDefault(i => i.MappingName is "RebuyBaseCost");
            var rebuyCountColumn = columns?.FirstOrDefault(i => i.MappingName is "RebuyCount");
            var rebuyRakeCostColumn = columns?.FirstOrDefault(i => i.MappingName is "RebuyRakeCost");
            var rebuyTotalCostColumn = columns?.FirstOrDefault(i => i.MappingName is "RebuyTotalCost");
            var screenshotColumn = columns?.FirstOrDefault(i => i.MappingName is "ScreenshotFilename");
            var stackSizeStartingColumn = columns?.FirstOrDefault(i => i.MappingName is "StackSizeStarting");
            var stackSizeAddonColumn = columns?.FirstOrDefault(i => i.MappingName is "StackSizeAddon");
            var stackSizeRebuyColumn = columns?.FirstOrDefault(i => i.MappingName is "StackSizeRebuy");
            var startDateColumn = columns?.FirstOrDefault(i => i.MappingName is "StartDate");
            var startTimeColumn = columns?.FirstOrDefault(i => i.MappingName is "StartTime");
            var tableSizeColumn = columns?.FirstOrDefault(i => i.MappingName is "TableSize");
            var tournamentNameColumn = columns?.FirstOrDefault(i => i.MappingName is "TournamentName");
            var totalCostColumn = columns?.FirstOrDefault(i => i.MappingName is "TotalCost");
            var venueColumn = columns?.FirstOrDefault(i => i.MappingName is "Venue");

            // get column orders
            var addonBaseCostOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TournamentsViewColumnOrder", "AddonBaseCostColumnOrder"));
            var addonCountOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TournamentsViewColumnOrder", "AddonCountColumnOrder"));
            var addonRakeCostOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TournamentsViewColumnOrder", "AddonRakeCostColumnOrder"));
            var addonTotalCostOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TournamentsViewColumnOrder", "AddonTotalCostColumnOrder"));
            var blindLevelsOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TournamentsViewColumnOrder", "BlindLevelsColumnOrder"));
            var bountyOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TournamentsViewColumnOrder", "BovadaColumnOrder"));
            var isBovadaBountyOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TournamentsViewColumnOrder", "BovadaBountyColumnOrder"));
            var buyinRakeCostOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TournamentsViewColumnOrder", "BuyinRakeCostColumnOrder"));
            var buyinBaseCostOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TournamentsViewColumnOrder", "BuyinBaseCostColumnOrder"));
            var buyinTotalCostOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TournamentsViewColumnOrder", "BuyinTotalCostColumnOrder"));
            var endDateOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TournamentsViewColumnOrder", "EndDateColumnOrder"));
            var endTimeOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TournamentsViewColumnOrder", "EndTimeColumnOrder"));
            var entrantsOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TournamentsViewColumnOrder", "EntrantsColumnOrder"));
            var entrantsPaidOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TournamentsViewColumnOrder", "EntrantsPaidColumnOrder"));
            var finishPositionOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TournamentsViewColumnOrder", "FinishPositionColumnOrder"));
            var formatStringOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TournamentsViewColumnOrder", "FormatsColumnOrder"));
            var gameTypeOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TournamentsViewColumnOrder", "GameTypeColumnOrder"));
            var guaranteeOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TournamentsViewColumnOrder", "GuaranteeColumnOrder"));
            var jackpotSpinMultiplierOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TournamentsViewColumnOrder", "JackpotSpinMultiplierColumnOrder"));
            var isSngOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TournamentsViewColumnOrder", "SngColumnOrder"));
            var lengthOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TournamentsViewColumnOrder", "LengthColumnOrder"));
            var lateRegOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TournamentsViewColumnOrder", "LateRegColumnOrder"));
            var noteOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TournamentsViewColumnOrder", "NoteColumnOrder"));
            var prizeWonOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TournamentsViewColumnOrder", "PrizeWonColumnOrder"));
            var profitOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TournamentsViewColumnOrder", "ProfitColumnOrder"));
            var rebuyBaseCostOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TournamentsViewColumnOrder", "RebuyBaseCostColumnOrder"));
            var rebuyCountOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TournamentsViewColumnOrder", "RebuyBaseCostColumnOrder"));
            var rebuyRakeCostOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TournamentsViewColumnOrder", "RebuyRakeCostColumnOrder"));
            var rebuyTotalCostOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TournamentsViewColumnOrder", "RebuyTotalCostColumnOrder"));
            var screenshotOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TournamentsViewColumnOrder", "ScreenshotColumnOrder"));
            var stackSizeStartingOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TournamentsViewColumnOrder", "StackSizeStartingColumnOrder"));
            var stackSizeAddonOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TournamentsViewColumnOrder", "StackSizeAddonColumnOrder"));
            var stackSizeRebuyOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TournamentsViewColumnOrder", "StackSizeRebuyColumnOrder"));
            var startDateOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TournamentsViewColumnOrder", "StartDateColumnOrder"));
            var startTimeOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TournamentsViewColumnOrder", "StartTimeColumnOrder"));
            var tableSizeOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TournamentsViewColumnOrder", "TableSizeColumnOrder"));
            var totalCostOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TournamentsViewColumnOrder", "TotalCostColumnOrder"));
            var tournamentNameOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TournamentsViewColumnOrder", "TournamentNameColumnOrder"));
            var venueOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TournamentsViewColumnOrder", "VenueColumnOrder"));

            // re-ordered columns collection
            var newColumns = new Columns();

            // add the columns in the desired order
            for (var c = 0; c < 39; c++)
            {
                if (addonBaseCostOrderColumn == c) newColumns.Add(addonBaseCostColumn);
                if (addonCountOrderColumn == c) newColumns.Add(addonCountColumn);
                if (addonRakeCostOrderColumn == c) newColumns.Add(addonRakeCostColumn);
                if (addonTotalCostOrderColumn == c) newColumns.Add(addonTotalCostColumn);
                if (blindLevelsOrderColumn == c) newColumns.Add(blindLevelsColumn);
                if (bountyOrderColumn == c) newColumns.Add(bountyColumn);
                if (buyinBaseCostOrderColumn == c) newColumns.Add(buyinBaseCostColumn);
                if (buyinRakeCostOrderColumn == c) newColumns.Add(buyinRakeCostColumn);
                if (buyinTotalCostOrderColumn == c) newColumns.Add(buyinTotalCostColumn);
                if (endDateOrderColumn == c) newColumns.Add(endDateColumn);
                if (endTimeOrderColumn == c) newColumns.Add(endTimeColumn);
                if (entrantsOrderColumn == c) newColumns.Add(entrantsColumn);
                if (entrantsPaidOrderColumn == c) newColumns.Add(entrantsPaidColumn);
                if (finishPositionOrderColumn == c) newColumns.Add(finishPositionColumn);
                if (isBovadaBountyOrderColumn == c) newColumns.Add(isBovadaBountyColumn);
                if (isSngOrderColumn == c) newColumns.Add(isSngColumn);
                if (formatStringOrderColumn == c) newColumns.Add(formatStringColumn);
                if (gameTypeOrderColumn == c) newColumns.Add(gameTypeColumn);
                if (guaranteeOrderColumn == c) newColumns.Add(guaranteeColumn);
                if (jackpotSpinMultiplierOrderColumn == c) newColumns.Add(jackpotSpinMultiplierColumn);
                if (lengthOrderColumn == c) newColumns.Add(lenghtColumn);
                if (lateRegOrderColumn == c) newColumns.Add(lateRegColumn);
                if (noteOrderColumn == c) newColumns.Add(noteColumn);
                if (prizeWonOrderColumn == c) newColumns.Add(prizeWonColumn);
                if (profitOrderColumn == c) newColumns.Add(profitColumn);
                if (rebuyBaseCostOrderColumn == c) newColumns.Add(rebuyBaseCostColumn);
                if (rebuyCountOrderColumn == c) newColumns.Add(rebuyCountColumn);
                if (rebuyRakeCostOrderColumn == c) newColumns.Add(rebuyRakeCostColumn);
                if (rebuyTotalCostOrderColumn == c) newColumns.Add(rebuyTotalCostColumn);
                if (screenshotOrderColumn == c) newColumns.Add(screenshotColumn);
                if (stackSizeStartingOrderColumn == c) newColumns.Add(stackSizeStartingColumn);
                if (stackSizeAddonOrderColumn == c) newColumns.Add(stackSizeAddonColumn);
                if (stackSizeRebuyOrderColumn == c) newColumns.Add(stackSizeRebuyColumn);
                if (startDateOrderColumn == c) newColumns.Add(startDateColumn);
                if (startTimeOrderColumn == c) newColumns.Add(startTimeColumn);
                if (tableSizeOrderColumn == c) newColumns.Add(tableSizeColumn);
                if (totalCostOrderColumn == c) newColumns.Add(totalCostColumn);
                if (tournamentNameOrderColumn == c) newColumns.Add(tournamentNameColumn);
                if (venueOrderColumn == c) newColumns.Add(venueColumn);

                // clear columns
                tournamentsView.TournamentsDataGrid.Columns.Clear();

                // add the adjusted columns
                foreach (var column in newColumns) tournamentsView.TournamentsDataGrid.Columns.Add(column);

                // refresh the grid
                tournamentsView.TournamentsDataGrid.Measure(new Size(1, 1));
            }
        }

        /// <summary>
        /// update tournaments list
        /// </summary>
        public void UpdateSession(SessionModel session)
        {
            // not a session, leave
            if (!IsSession) return;

            // null check
            if (session is null) { ExitWindow(null); return; }

            // not the matching session
            if (session.ID != SessionId) return;

            // reload tournaments
            Tournaments = new ObservableCollection<TournamentFinished>(session.Tournaments.OrderByDescending(i => i.StartTime));

            // 0 tournaments left in window
            if (Tournaments.Count is 0) ExitWindow(null);
        }

        /// <summary>
        /// update the data grid column orders
        /// </summary>
        private void UpdateColumnOrder(object parameter)
        {
            // flag our window
            ThisWindow = true;

             var columns = new Columns();

            // scan over tournaments windows for this tournaments database id and remove them too
            foreach (var window in Application.Current.Windows.OfType<TournamentsView>().ToList())
            {
                // get the windows view model
                var vm = window?.DataContext as TournamentsViewModel;

                // null check
                if (vm is null || vm.ThisWindow is false) continue;

                // grab columns and leave loop when we find our window
                if (vm.ThisWindow) { columns = window.TournamentsDataGrid.Columns; break; }
            }

            // remove flag
            ThisWindow = false;

            // null check
            if (columns is null) return;

            // find columns
            var addonBaseCostColumn = columns?.FirstOrDefault(i => i.MappingName is "AddonBaseCost");
            var addonCountColumn = columns?.FirstOrDefault(i => i.MappingName is "AddonCount");
            var addonRakeCostColumn = columns?.FirstOrDefault(i => i.MappingName is "AddonRakeCost");
            var addonTotalCostColumn = columns?.FirstOrDefault(i => i.MappingName is "AddonTotalCost");
            var blindLevelsColumn = columns?.FirstOrDefault(i => i.MappingName is "BlindLevels");
            var bountyColumn = columns?.FirstOrDefault(i => i.MappingName is "Bounty");
            var buyinBaseCostColumn = columns?.FirstOrDefault(i => i.MappingName is "BuyinBaseCost");
            var buyinRakeCostColumn = columns?.FirstOrDefault(i => i.MappingName is "BuyinRakeCost");
            var buyinTotalCostColumn = columns?.FirstOrDefault(i => i.MappingName is "BuyinTotalCost");
            var endDateColumn = columns?.FirstOrDefault(i => i.MappingName is "EndDate");
            var endTimeColumn = columns?.FirstOrDefault(i => i.MappingName is "EndTime");
            var entrantsColumn = columns?.FirstOrDefault(i => i.MappingName is "Entrants");
            var entrantsPaidColumn = columns?.FirstOrDefault(i => i.MappingName is "EntrantsPaid");
            var finishPositionColumn = columns?.FirstOrDefault(i => i.MappingName is "FinishPosition");
            var isBovadaBountyColumn = columns?.FirstOrDefault(i => i.MappingName is "IsBovadaBounty");
            var isSngColumn = columns?.FirstOrDefault(i => i.MappingName is "IsSng");
            var formatStringColumn = columns?.FirstOrDefault(i => i.MappingName is "FormatString");
            var gameTypeColumn = columns?.FirstOrDefault(i => i.MappingName is "GameType");
            var guaranteeColumn = columns?.FirstOrDefault(i => i.MappingName is "Guarantee");
            var jackpotSpinMultiplierColumn = columns?.FirstOrDefault(i => i.MappingName is "JackpotSpinMultiplier");
            var lengthColumn = columns?.FirstOrDefault(i => i.MappingName is "Length");
            var lateRegColumn = columns?.FirstOrDefault(i => i.MappingName is "LateReg");
            var noteColumn = columns?.FirstOrDefault(i => i.MappingName is "Note");
            var prizeWonColumn = columns?.FirstOrDefault(i => i.MappingName is "PrizeWon");
            var profitColumn = columns?.FirstOrDefault(i => i.MappingName is "Profit");
            var rebuyBaseCostColumn = columns?.FirstOrDefault(i => i.MappingName is "RebuyBaseCost");
            var rebuyCountColumn = columns?.FirstOrDefault(i => i.MappingName is "RebuyCount");
            var rebuyRakeCostColumn = columns?.FirstOrDefault(i => i.MappingName is "RebuyRakeCost");
            var rebuyTotalCostColumn = columns?.FirstOrDefault(i => i.MappingName is "RebuyTotalCost");
            var screenshotColumn = columns?.FirstOrDefault(i => i.MappingName is "ScreenshotFilename");
            var stackSizeStartingColumn = columns?.FirstOrDefault(i => i.MappingName is "StackSizeStarting");
            var stackSizeAddonColumn = columns?.FirstOrDefault(i => i.MappingName is "StackSizeAddon");
            var stackSizeRebuyColumn = columns?.FirstOrDefault(i => i.MappingName is "StackSizeRebuy");
            var startDateColumn = columns?.FirstOrDefault(i => i.MappingName is "StartDate");
            var startTimeColumn = columns?.FirstOrDefault(i => i.MappingName is "StartTime");
            var tableSizeColumn = columns?.FirstOrDefault(i => i.MappingName is "TableSize");
            var tournamentNameColumn = columns?.FirstOrDefault(i => i.MappingName is "TournamentName");
            var totalCostColumn = columns?.FirstOrDefault(i => i.MappingName is "TotalCost");
            var venueColumn = columns?.FirstOrDefault(i => i.MappingName is "Venue");

            // save column orders
            if (addonBaseCostColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TournamentsViewColumnOrder", "AddonBaseCostColumnOrder", columns.IndexOf(addonBaseCostColumn));
            if (addonCountColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TournamentsViewColumnOrder", "AddonCountColumnOrder", columns.IndexOf(addonCountColumn));
            if (addonRakeCostColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TournamentsViewColumnOrder", "AddonRakeCostColumnOrder", columns.IndexOf(addonRakeCostColumn));
            if (addonTotalCostColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TournamentsViewColumnOrder", "AddonTotalCostColumnOrder", columns.IndexOf(addonTotalCostColumn));
            if (blindLevelsColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TournamentsViewColumnOrder", "BlindLevelsColumnOrder", columns.IndexOf(blindLevelsColumn));
            if (bountyColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TournamentsViewColumnOrder", "BovadaColumnOrder", columns.IndexOf(bountyColumn));
            if (isBovadaBountyColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TournamentsViewColumnOrder", "BovadaBountyColumnOrder", columns.IndexOf(isBovadaBountyColumn));
            if (buyinRakeCostColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TournamentsViewColumnOrder", "BuyinRakeCostColumnOrder", columns.IndexOf(buyinRakeCostColumn));
            if (buyinBaseCostColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TournamentsViewColumnOrder", "BuyinBaseCostColumnOrder", columns.IndexOf(buyinBaseCostColumn));
            if (buyinTotalCostColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TournamentsViewColumnOrder", "BuyinTotalCostColumnOrder", columns.IndexOf(buyinTotalCostColumn));
            if (endDateColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TournamentsViewColumnOrder", "EndDateColumnOrder", columns.IndexOf(endDateColumn));
            if (endTimeColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TournamentsViewColumnOrder", "EndTimeColumnOrder", columns.IndexOf(endTimeColumn));
            if (entrantsColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TournamentsViewColumnOrder", "EntrantsColumnOrder", columns.IndexOf(entrantsColumn));
            if (entrantsPaidColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TournamentsViewColumnOrder", "EntrantsPaidColumnOrder", columns.IndexOf(entrantsPaidColumn));
            if (finishPositionColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TournamentsViewColumnOrder", "FinishPositionColumnOrder", columns.IndexOf(finishPositionColumn));
            if (formatStringColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TournamentsViewColumnOrder", "FormatStringColumnOrder", columns.IndexOf(formatStringColumn));
            if (gameTypeColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TournamentsViewColumnOrder", "GameTypeColumnOrder", columns.IndexOf(gameTypeColumn));
            if (guaranteeColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TournamentsViewColumnOrder", "GuaranteeColumnOrder", columns.IndexOf(guaranteeColumn));
            if (jackpotSpinMultiplierColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TournamentsViewColumnOrder", "JackpotSpinMultiplierColumnOrder", columns.IndexOf(jackpotSpinMultiplierColumn));
            if (lengthColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TournamentsViewColumnOrder", "LengthColumnOrder", columns.IndexOf(lengthColumn));
            if (isSngColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TournamentsViewColumnOrder", "SngColumnOrder", columns.IndexOf(isSngColumn));
            if (lateRegColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TournamentsViewColumnOrder", "LateRegColumnOrder", columns.IndexOf(lateRegColumn));
            if (noteColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TournamentsViewColumnOrder", "NoteColumnOrder", columns.IndexOf(noteColumn));
            if (prizeWonColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TournamentsViewColumnOrder", "PrizeWonColumnOrder", columns.IndexOf(prizeWonColumn));
            if (profitColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TournamentsViewColumnOrder", "ProfitColumnOrder", columns.IndexOf(profitColumn));
            if (rebuyBaseCostColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TournamentsViewColumnOrder", "RebuyBaseCostColumnOrder", columns.IndexOf(rebuyBaseCostColumn));
            if (rebuyCountColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TournamentsViewColumnOrder", "RebuyBaseCostColumnOrder", columns.IndexOf(rebuyCountColumn));
            if (rebuyRakeCostColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TournamentsViewColumnOrder", "RebuyRakeCostColumnOrder", columns.IndexOf(rebuyRakeCostColumn));
            if (rebuyTotalCostColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TournamentsViewColumnOrder", "RebuyTotalCostColumnOrder", columns.IndexOf(rebuyTotalCostColumn));
            if (screenshotColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TournamentsViewColumnOrder", "ScreenshotColumnOrder", columns.IndexOf(screenshotColumn));
            if (stackSizeStartingColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TournamentsViewColumnOrder", "StackSizeStartingColumnOrder", columns.IndexOf(stackSizeStartingColumn));
            if (stackSizeAddonColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TournamentsViewColumnOrder", "StackSizeAddonColumnOrder", columns.IndexOf(stackSizeAddonColumn));
            if (stackSizeRebuyColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TournamentsViewColumnOrder", "StackSizeRebuyColumnOrder", columns.IndexOf(stackSizeRebuyColumn));
            if (startDateColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TournamentsViewColumnOrder", "StartDateColumnOrder", columns.IndexOf(startDateColumn));
            if (startTimeColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TournamentsViewColumnOrder", "StartTimeColumnOrder", columns.IndexOf(startTimeColumn));
            if (tableSizeColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TournamentsViewColumnOrder", "TableSizeColumnOrder", columns.IndexOf(tableSizeColumn));
            if (tournamentNameColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TournamentsViewColumnOrder", "TournamentNameColumnOrder", columns.IndexOf(tournamentNameColumn));
            if (totalCostColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TournamentsViewColumnOrder", "TotalCostColumnOrder", columns.IndexOf(totalCostColumn));
            if (venueColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TournamentsViewColumnOrder", "VenueColumnOrder", columns.IndexOf(venueColumn));
        }
    }
}