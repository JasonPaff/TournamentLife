using LiveTracker.Helpers;
using LiveTracker.Models;
using LiveTracker.Models.Tournaments;
using LiveTracker.ViewModels.Datagrid_ViewModels;
using LiveTracker.ViewModels.Results;
using LiveTracker.Views;
using LiveTracker.Views.Menu_Views;
using LiveTracker.Views.Results;
using Syncfusion.UI.Xaml.Utility;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Tournament_Life.ViewModels.Results;
using Tournament_Life.Views.Results;
using Tournament_Life.ViewModels;
using Tournament_Life.Views;

namespace LiveTracker.ViewModels.Menu_ViewModels
{
    public class DatabaseViewModel : NotificationObject
    {
        public ICommand BackupCommand => new BaseCommand(BackupDatabase);
        public ICommand CancelCommand => new BaseCommand(Cancel);
        public ICommand DeleteCommand => new BaseCommand(DeleteDatabase);
        public ICommand ImportCommand => new BaseCommand(ImportDatabase);
        public ICommand ImportRecordsCommand => new BaseCommand(ImportRecords);

        public DatabaseViewModel()
        {
            // font size
            FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));

            // visual theme
            Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");

            ImportSelectedItems = new ObservableCollection<TemplateListBoxItem>();
            ImportList = new ObservableCollection<TemplateListBoxItem>();
            Imports = new ObservableCollection<TournamentFinished>();

            // enable window
            Enabled = true;
        }

        public bool Enabled { get; set; }
        public int FontSize { get; set; }
        public ObservableCollection<TemplateListBoxItem> ImportList { get; set; }
        public bool ImportRecordsButton { get; set; }
        public ObservableCollection<TournamentFinished> Imports { get; set; }
        public ObservableCollection<TemplateListBoxItem> ImportSelectedItems { get; set; }
        public bool Saved { get; set; }
        public string Theme { get; set; }

        /// <summary>
        /// Creates a backup copy of your database
        /// </summary>
        /// <param name="parameter"></param>
        private void BackupDatabase(object parameter)
        {
            // open file dialog to name backup and select save location
            Microsoft.Win32.SaveFileDialog openFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "Sdf file (*.sdf)|*.sdf"
            };
            openFileDialog.ShowDialog(Application.Current.MainWindow);

            // no file saved
            if (openFileDialog.FileName.Length is 0) return;

            // copy file
            File.Copy(XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + DatabaseHelper.DatabaseName, openFileDialog.FileName, true);

            // success
            var vm = new OkViewModel("Database copied successfully!", "Copy Success");
            var window = new OkView(vm)
            {
                Owner = Application.Current.Windows.OfType<DatabaseView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            window.ShowDialog();
        }

        /// <summary>
        /// close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Cancel(object parameter)
        {
            // making sure
            Saved = false;

            // find window
            var window = Application.Current.Windows.OfType<DatabaseView>().FirstOrDefault();

            // close if not null
            if (window is not null) window?.Close();
        }

        /// <summary>
        /// Create list box items from the imported database
        /// </summary>
        private List<TemplateListBoxItem> CreateListBoxItems(string databaseFilename)
        {
            var sqlCommand = new SqlCeCommand();
            var sqlConnection = new SqlCeConnection();

            var tempList = new List<TemplateListBoxItem>();

            try
            {
                sqlConnection = DatabaseHelper.ConnectDatabase(databaseFilename);

                sqlCommand = new SqlCeCommand(DatabaseHelper.TableName, sqlConnection)
                {
                    CommandType = CommandType.TableDirect
                };

                var sqlResultSet = sqlCommand.ExecuteResultSet(ResultSetOptions.Scrollable);

                // loop table rows (each row is one tournament) and load into list and create list box items
                if (sqlResultSet.HasRows)
                    while (sqlResultSet.Read())
                    {
                        try
                        {
                            // create/load tournament record
                            var tournamentRecord = new TournamentFinished
                            {
                                AddonBaseCost = (decimal)sqlResultSet.GetSqlMoney(sqlResultSet.GetOrdinal("AddonCost")),
                                AddonCount = (int)sqlResultSet.GetSqlInt32(sqlResultSet.GetOrdinal("AddonCount")),
                                AddonRakeCost = (decimal)sqlResultSet.GetSqlMoney(sqlResultSet.GetOrdinal("AddonRake")),
                                BlindLevels = (int)sqlResultSet.GetSqlInt32(sqlResultSet.GetOrdinal("BlindLevels")),
                                BuyinBaseCost = (decimal)sqlResultSet.GetSqlMoney(sqlResultSet.GetOrdinal("BuyinCost")),
                                BuyinRakeCost = (decimal)sqlResultSet.GetSqlMoney(sqlResultSet.GetOrdinal("BuyinRake")),
                                DatabaseId = (int)sqlResultSet.GetInt32(sqlResultSet.GetOrdinal("Id")),
                                EndTime = (DateTime)sqlResultSet.GetSqlDateTime(sqlResultSet.GetOrdinal("EndTime")),
                                Entrants = (int)sqlResultSet.GetSqlInt32(sqlResultSet.GetOrdinal("Entrants")),
                                EntrantsPaid = (int)sqlResultSet.GetSqlInt32(sqlResultSet.GetOrdinal("EntrantsPaid")),
                                FinishPosition = (int)sqlResultSet.GetSqlInt32(sqlResultSet.GetOrdinal("FinishPosition")),
                                GameType = sqlResultSet.GetString(sqlResultSet.GetOrdinal("GameType")),
                                Guarantee = (decimal)sqlResultSet.GetSqlMoney(sqlResultSet.GetOrdinal("Guarantee")),
                                LateReg = (int)sqlResultSet.GetSqlInt32(sqlResultSet.GetOrdinal("LateReg")),
                                PrizeWon = (decimal)sqlResultSet.GetSqlMoney(sqlResultSet.GetOrdinal("PrizeWon")),
                                RebuyBaseCost = (decimal)sqlResultSet.GetSqlMoney(sqlResultSet.GetOrdinal("RebuyCost")),
                                RebuyCount = (int)sqlResultSet.GetSqlInt32(sqlResultSet.GetOrdinal("RebuyCount")),
                                RebuyRakeCost = (decimal)sqlResultSet.GetSqlMoney(sqlResultSet.GetOrdinal("RebuyRake")),
                                ScreenshotFilename = sqlResultSet.GetString(sqlResultSet.GetOrdinal("Screenshot")),
                                StackSizeStarting = (int)sqlResultSet.GetSqlInt32(sqlResultSet.GetOrdinal("StackSizeStarting")),
                                StackSizeRebuy = (int)sqlResultSet.GetSqlInt32(sqlResultSet.GetOrdinal("StackSizeRebuy")),
                                StackSizeAddon = (int)sqlResultSet.GetSqlInt32(sqlResultSet.GetOrdinal("StackSizeAddon")),
                                StartTime = (DateTime)(sqlResultSet.GetSqlDateTime(sqlResultSet.GetOrdinal("StartTime"))),
                                TableSize = (int)sqlResultSet.GetSqlInt32(sqlResultSet.GetOrdinal("TableSize")),
                                TournamentName = sqlResultSet.GetString(sqlResultSet.GetOrdinal("Name")),
                                Venue = sqlResultSet.GetString(sqlResultSet.GetOrdinal("SiteName")),
                            };

                            if (sqlResultSet.FieldCount <= 27)
                            {
                                tournamentRecord.IsSng = false;
                                tournamentRecord.IsBovadaBounty = false;
                                tournamentRecord.Note = "";
                                tournamentRecord.Bounty = 0;
                                tournamentRecord.BountyCount = 0;
                                tournamentRecord.JackpotSpinMultiplier = 0;
                            }
                            else
                            {
                                if (!sqlResultSet.IsDBNull(sqlResultSet.GetOrdinal("IsSng"))) tournamentRecord.IsSng = bool.Parse(sqlResultSet.GetString(sqlResultSet.GetOrdinal("IsSng")));

                                if (!sqlResultSet.IsDBNull(sqlResultSet.GetOrdinal("IsBovadaBounty"))) tournamentRecord.IsBovadaBounty = bool.Parse(sqlResultSet.GetString(sqlResultSet.GetOrdinal("IsBovadaBounty")));

                                if (!sqlResultSet.IsDBNull(sqlResultSet.GetOrdinal("Note"))) tournamentRecord.Note = sqlResultSet.GetString(sqlResultSet.GetOrdinal("Note"));

                                if (!sqlResultSet.IsDBNull(sqlResultSet.GetOrdinal("Bounty"))) tournamentRecord.Bounty = (decimal)sqlResultSet.GetSqlMoney(sqlResultSet.GetOrdinal("Bounty"));

                                if (!sqlResultSet.IsDBNull(sqlResultSet.GetOrdinal("BountyCount"))) tournamentRecord.BountyCount = (int)sqlResultSet.GetSqlInt32(sqlResultSet.GetOrdinal("BountyCount"));

                                if (!sqlResultSet.IsDBNull(sqlResultSet.GetOrdinal("JackpotSpinMultiplier"))) tournamentRecord.JackpotSpinMultiplier = (int)sqlResultSet.GetSqlInt32(sqlResultSet.GetOrdinal("JackpotSpinMultiplier"));
                            }

                            //load formats
                            tournamentRecord.LoadFormats(sqlResultSet.GetString(sqlResultSet.GetOrdinal("Formats")));

                            // fix times
                            tournamentRecord.StartTime = new DateTime(tournamentRecord.StartTime.Year, tournamentRecord.StartTime.Month, tournamentRecord.StartTime.Day, tournamentRecord.StartTime.Hour, tournamentRecord.StartTime.Minute, 0);
                            tournamentRecord.EndTime = new DateTime(tournamentRecord.EndTime.Year, tournamentRecord.EndTime.Month, tournamentRecord.EndTime.Day, tournamentRecord.EndTime.Hour, tournamentRecord.EndTime.Minute, 0);

                            // add to collection
                            Imports.Add(tournamentRecord);

                            // create list box item
                            var listBoxItem = new TemplateListBoxItem()
                            {
                                Description = tournamentRecord.DatabaseDescription,
                                DisplayString = tournamentRecord.StartTime.ToShortDateString() + " " + tournamentRecord.StartTime.ToShortTimeString() + " - " + tournamentRecord.Venue + " - " + tournamentRecord.TournamentName,
                                IsSelected = false,
                                Name = tournamentRecord.TournamentName,
                                StartTime = tournamentRecord.StartTime

                            };
                            tempList.Add(listBoxItem);
                        }
                        catch (Exception exception)
                        {
                            // ok view model
                            var theVm = new OkViewModel(exception.Message, "Error");

                            // create/show ok window
                            var theWindow = new OkView(theVm)
                            {
                                Owner = Application.Current.MainWindow,
                                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                            };
                            theWindow.ShowDialog();

                            throw;
                        }
                    }
            }
            catch (SqlCeException sqlException)
            {
                // ok view model
                var theVm = new OkViewModel(sqlException.Message, "Error");

                // create/show ok window
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.MainWindow,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                throw;
            }
            finally
            {
                sqlCommand.Dispose();

                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            // return list box items
            return tempList;
        }

        /// <summary>
        /// delete your database for a fresh one
        /// </summary>
        /// <param name="parameter"></param>
        private void DeleteDatabase(object parameter)
        {
            // yes/no view model
            var vm = new YesNoViewModel("Are you sure you want to delete your database? \n\n-This will completely erase your database!\n-This can't be undone!\n-Recommended that you backup your database first!)", "Delete Database");

            // create/show yes/no window
            var window = new YesNoView(vm)
            {
                Owner = Application.Current.Windows.OfType<DatabaseView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            window.ShowDialog();

            // didn't save
            if(vm.Saved is false) return;

            // find menu view model
            if (Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.LiveTrackerMenu?.DataContext is not MenuViewModel menuViewModel) return;

            // database file name
            var databaseFileName = XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + DatabaseHelper.DatabaseName;

            // leave if file doesn't exist
            if(!File.Exists(databaseFileName)) return;

            // delete database
            File.Delete(databaseFileName);

            // create new database
            DatabaseHelper.CreateDatabase(ProfileHelper.GetCurrentProfile());

            // clear recently finished tournaments list and xml file
            menuViewModel.RecentlyFinishedTournaments = new ObservableCollection<TournamentRunning>();
            menuViewModel.SaveRecentlyFinishedTournaments();

            // load database
            var tournaments = DatabaseHelper.LoadDatabase();

            // update bankrolls
            menuViewModel.LoadBankrolls(tournaments);

            // see if quick results window is open and update it
            if (Application.Current.Windows.OfType<QuickResultsView>()?.FirstOrDefault()?.DataContext is QuickResultsViewModel quickResultsViewModel) quickResultsViewModel.Update(tournaments);

            // see if tournament results window is open and update it
            if (Application.Current.Windows.OfType<TournamentsResultsView>()?.FirstOrDefault()?.DataContext is TournamentsResultsViewModel tournamentsResultsViewModel) tournamentsResultsViewModel.Update(tournaments);

            // see if session results window is open and update it
            if (Application.Current.Windows.OfType<SessionResultsView>()?.FirstOrDefault()?.DataContext is SessionResultsViewModel sessionResultsViewModel) sessionResultsViewModel.Update(tournaments);

            // success
            var theVm = new OkViewModel("Database deleted successfully", "Delete Success");
            var theWindow = new OkView(theVm)
            {
                Owner = Application.Current.Windows.OfType<DatabaseView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            theWindow.ShowDialog();
        }

        /// <summary>
        /// import a database file
        /// </summary>
        /// <param name="parameter"></param>
        private void ImportDatabase(object parameter)
        {
            // create/show file selection window
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Sdf files (*.sdf)|*.sdf"
            };
            openFileDialog.ShowDialog(Application.Current.Windows.OfType<DatabaseView>()?.FirstOrDefault());

            // didn't import
            if (openFileDialog.FileName.Length is 0) return;

            // import tournaments into collection and create list box items
            ImportList = new ObservableCollection<TemplateListBoxItem>(CreateListBoxItems(openFileDialog.FileName));

            // show import records button
            ImportRecordsButton = true;
        }

        /// <summary>
        /// Import the selected records
        /// </summary>
        /// <param name="parameter"></param>
        private void ImportRecords(object parameter)
        {
            // null/zero check
            if (ImportList is null || ImportList.Any(i => i.IsSelected) is false)
            {
                // create/show error message
                var okayVM1 = new OkViewModel("No records selected", "None Selected");
                var okayWindow1 = new OkView(okayVM1) { Owner = Application.Current.Windows.OfType<DatabaseView>().FirstOrDefault(), WindowStartupLocation = WindowStartupLocation.CenterOwner, }; okayWindow1.ShowDialog();

                // exit
                return;
            }

            // lock window
            Enabled = false;

            // list of tournament records to import
            var imports = new List<TournamentFinished>();

            // add selected to imports list
            for (int c=0;c < ImportList.Count; c++) if(ImportList[c].IsSelected) imports.Add(Imports[c]);

            // import records
            DatabaseHelper.AddRecords(imports);

            // remove imported records
            ImportList = new ObservableCollection<TemplateListBoxItem>(ImportList.Where(i => i.IsSelected is false));

            // unlock window
            Enabled = true;

            // menu view model
            var menuViewModel = Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.LiveTrackerMenu.DataContext as MenuViewModel;

            // load database
            var tournaments = DatabaseHelper.LoadDatabase();

            // update bankrolls
            menuViewModel.LoadBankrolls(tournaments);

            // see if quick results window is open and update it
            if (Application.Current.Windows.OfType<QuickResultsView>()?.FirstOrDefault()?.DataContext is QuickResultsViewModel quickResultsViewModel) quickResultsViewModel.Update(tournaments);

            // see if tournament results window is open and update it
            if (Application.Current.Windows.OfType<TournamentsResultsView>()?.FirstOrDefault()?.DataContext is TournamentsResultsViewModel tournamentsResultsViewModel) tournamentsResultsViewModel.Update(tournaments);

            // see if session results window is open and update it
            if (Application.Current.Windows.OfType<SessionResultsView>()?.FirstOrDefault()?.DataContext is SessionResultsViewModel sessionResultsViewModel) sessionResultsViewModel.Update(tournaments);

            // success
            var vm = new OkViewModel("Records imported successfully!", "Import Success");
            var window = new OkView(vm)
            {
                Owner = Application.Current.Windows.OfType<DatabaseView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            window.ShowDialog();
        }
    }
}