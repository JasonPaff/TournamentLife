using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlServerCe;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using Tournament_Life.Models.Tournaments;
using Tournament_Life.ViewModels;
using Tournament_Life.Views;

namespace Tournament_Life.Helpers
{

    public static class DatabaseHelper
    {
        public const string DatabaseName = "Tournaments.sdf"; // database file name
        public const string Password = "password"; // database password
        public const string TableName = "Tournaments"; // database table name

        /// <summary>
        /// Checks to see if trial limit has been exceeded
        /// </summary>
        /// <returns>true if exceeded</returns>
        private static bool TrialExceeded()
        {
            // program is registered
            if (RegistrationHelper.IsRegistered())
                return false;

            //haven't reached the trial limit
            if (LoadDatabase().Count <= 100)
                return false;

            // trial exceeded messaged
            var theVm = new OkViewModel("Thank your for trying out Tournament Life!\n\n Unfortunately you have reached the 100 tournament trial limit\n\n Please head over to http://www.Tournaments.Life to purchase", "Trial Exceeded");

            // create/show ok window
            var theWindow = new OkView(theVm)
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            theWindow.ShowDialog();

            // return trial exceeded
            return true;
        }

        /// <summary>
        /// Adds a record to the database
        /// </summary>
        /// <param name="tournament">record to add</param>
        /// <param name="overrideDatabaseName">optional database name</param>
        /// <returns>false if failed</returns>
        public static bool AddRecord(TournamentFinished tournament, string overrideDatabaseName = null)
        {
            // null check
            if (tournament is null)
                return false;

            // free trial over check
            if (TrialExceeded())
                return false;

            var sqlCommand = new SqlCeCommand();
            var sqlConnection = new SqlCeConnection();

            var sqlCommandString = "insert into Tournaments "
                                   + "(IsSng, IsBovadaBounty, Screenshot, Note, Name, Bounty, BountyCount, JackpotSpinMultiplier, BuyinCost, BuyinRake, RebuyCost, RebuyRake, AddonCost, AddonRake, StartTime, EndTime, StackSizeStarting, StackSizeRebuy, StackSizeAddon, Guarantee, " +
                                   "SiteName, GameType, Formats, TableSize, BlindLevels, LateReg, Entrants, AddonCount, RebuyCount, PrizeWon, FinishPosition, EntrantsPaid ) "
                                   + "values (@IsSng, @IsBovadaBounty, @Screenshot, @Note, @Name, @Bounty, @BountyCount, @JackpotSpinMultiplier, @BuyinCost, @BuyinRake, @RebuyCost, @RebuyRake, @AddonCost, @AddonRake, @StartTime, @EndTime, @StackSizeStarting, @StackSizeRebuy, @StackSizeAddon, " +
                                   "@Guarantee, @SiteName, @GameType, @Formats, @TableSize, @BlindLevels, @LateReg, @Entrants, @AddonCount, @RebuyCount, @PrizeWon, @FinishPosition, @EntrantsPaid )";

            try
            {
                if (overrideDatabaseName is null)
                    sqlConnection = ConnectDatabase();
                else
                    sqlConnection = ConnectDatabase(overrideDatabaseName);

                sqlCommand = new SqlCeCommand(sqlCommandString, sqlConnection);

                sqlCommand.Parameters.AddWithValue("@IsSng", tournament.IsSng);
                sqlCommand.Parameters.AddWithValue("@IsBovadaBounty", tournament.IsBovadaBounty);
                sqlCommand.Parameters.AddWithValue("@Screenshot", tournament.ScreenshotFilename);
                sqlCommand.Parameters.AddWithValue("@Note", tournament.Note);
                sqlCommand.Parameters.AddWithValue("@Name", tournament.TournamentName);
                sqlCommand.Parameters.AddWithValue("@Bounty", tournament.Bounty);
                sqlCommand.Parameters.AddWithValue("@BountyCount", tournament.BountyCount);
                sqlCommand.Parameters.AddWithValue("@JackpotSpinMultiplier", tournament.JackpotSpinMultiplier);
                sqlCommand.Parameters.AddWithValue("@BuyinCost", tournament.BuyinBaseCost);
                sqlCommand.Parameters.AddWithValue("@BuyinRake", tournament.BuyinRakeCost);
                sqlCommand.Parameters.AddWithValue("@RebuyCost", tournament.RebuyBaseCost);
                sqlCommand.Parameters.AddWithValue("@RebuyRake", tournament.RebuyRakeCost);
                sqlCommand.Parameters.AddWithValue("@AddonCost", tournament.AddonBaseCost);
                sqlCommand.Parameters.AddWithValue("@AddonRake", tournament.AddonRakeCost);
                sqlCommand.Parameters.AddWithValue("@StartTime", tournament.StartTime);
                sqlCommand.Parameters.AddWithValue("@EndTime", tournament.EndTime);
                sqlCommand.Parameters.AddWithValue("@StackSizeStarting", tournament.StackSizeStarting);
                sqlCommand.Parameters.AddWithValue("@StackSizeRebuy", tournament.StackSizeRebuy);
                sqlCommand.Parameters.AddWithValue("@StackSizeAddon", tournament.StackSizeAddon);
                sqlCommand.Parameters.AddWithValue("@Guarantee", tournament.Guarantee);
                sqlCommand.Parameters.AddWithValue("@SiteName", tournament.Venue);
                sqlCommand.Parameters.AddWithValue("@GameType", tournament.GameType);
                sqlCommand.Parameters.AddWithValue("@Formats", tournament.FormatString);
                sqlCommand.Parameters.AddWithValue("@TableSize", tournament.TableSize);
                sqlCommand.Parameters.AddWithValue("@BlindLevels", tournament.BlindLevels);
                sqlCommand.Parameters.AddWithValue("@LateReg", tournament.LateReg);
                sqlCommand.Parameters.AddWithValue("@Entrants", tournament.Entrants);
                sqlCommand.Parameters.AddWithValue("@AddonCount", tournament.AddonCount);
                sqlCommand.Parameters.AddWithValue("@RebuyCount", tournament.RebuyCount);
                sqlCommand.Parameters.AddWithValue("@PrizeWon", tournament.PrizeWon);
                sqlCommand.Parameters.AddWithValue("@FinishPosition", tournament.FinishPosition);
                sqlCommand.Parameters.AddWithValue("@EntrantsPaid", tournament.EntrantsPaid);

                if (sqlCommand.ExecuteNonQuery() != 1)
                {
                    // error message
                    var theVm = new OkViewModel("Error saving record", "Error");

                    // create/show ok window
                    var theWindow = new OkView(theVm)
                    {
                        Owner = Application.Current.MainWindow,
                        WindowStartupLocation = WindowStartupLocation.CenterOwner,
                    };
                    theWindow.ShowDialog();
                }
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

                // toss error up
                throw;
            }
            finally
            {
                sqlCommand.Dispose();

                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            // success
            return true;
        }

        /// <summary>
        /// Adds a record to the database
        /// </summary>
        /// <param name="tournament">record to add</param>
        /// <returns>false if failed</returns>
        public static bool AddRecords(List<TournamentFinished> tournaments, string overrideDatabaseName = null)
        {
            if (tournaments is null || tournaments.Count is 0)
                return false;

            if (TrialExceeded())
                return false;

            var sqlCommand = new SqlCeCommand();
            var sqlConnection = new SqlCeConnection();

            var sqlCommandString = "insert into Tournaments "
                                   + "(IsSng, IsBovadaBounty, Screenshot, Note, Name, Bounty, BountyCount, JackpotSpinMultiplier, BuyinCost, BuyinRake, RebuyCost, RebuyRake, AddonCost, AddonRake, StartTime, EndTime, StackSizeStarting, StackSizeRebuy, StackSizeAddon, Guarantee, " +
                                   "SiteName, GameType, Formats, TableSize, BlindLevels, LateReg, Entrants, AddonCount, RebuyCount, PrizeWon, FinishPosition, EntrantsPaid ) "
                                   + "values (@IsSng, @IsBovadaBounty, @Screenshot, @Note, @Name, @Bounty, @BountyCount, @JackpotSpinMultiplier, @BuyinCost, @BuyinRake, @RebuyCost, @RebuyRake, @AddonCost, @AddonRake, @StartTime, @EndTime, @StackSizeStarting, @StackSizeRebuy, @StackSizeAddon, " +
                                   "@Guarantee, @SiteName, @GameType, @Formats, @TableSize, @BlindLevels, @LateReg, @Entrants, @AddonCount, @RebuyCount, @PrizeWon, @FinishPosition, @EntrantsPaid )";

            try
            {
                if (overrideDatabaseName is null)
                    sqlConnection = ConnectDatabase();
                else
                    sqlConnection = ConnectDatabase(overrideDatabaseName);


                foreach (var tournament in tournaments)
                {
                    sqlCommand = new SqlCeCommand(sqlCommandString, sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@IsSng", tournament.IsSng);
                    sqlCommand.Parameters.AddWithValue("@IsBovadaBounty", tournament.IsBovadaBounty);
                    sqlCommand.Parameters.AddWithValue("@Screenshot", tournament.ScreenshotFilename);
                    sqlCommand.Parameters.AddWithValue("@Note", tournament.Note);
                    sqlCommand.Parameters.AddWithValue("@Name", tournament.TournamentName);
                    sqlCommand.Parameters.AddWithValue("@Bounty", tournament.Bounty);
                    sqlCommand.Parameters.AddWithValue("@BountyCount", tournament.BountyCount);
                    sqlCommand.Parameters.AddWithValue("@JackpotSpinMultiplier", tournament.JackpotSpinMultiplier);
                    sqlCommand.Parameters.AddWithValue("@BuyinCost", tournament.BuyinBaseCost);
                    sqlCommand.Parameters.AddWithValue("@BuyinRake", tournament.BuyinRakeCost);
                    sqlCommand.Parameters.AddWithValue("@RebuyCost", tournament.RebuyBaseCost);
                    sqlCommand.Parameters.AddWithValue("@RebuyRake", tournament.RebuyRakeCost);
                    sqlCommand.Parameters.AddWithValue("@AddonCost", tournament.AddonBaseCost);
                    sqlCommand.Parameters.AddWithValue("@AddonRake", tournament.AddonRakeCost);
                    sqlCommand.Parameters.AddWithValue("@StartTime", tournament.StartTime);
                    sqlCommand.Parameters.AddWithValue("@EndTime", tournament.EndTime);
                    sqlCommand.Parameters.AddWithValue("@StackSizeStarting", tournament.StackSizeStarting);
                    sqlCommand.Parameters.AddWithValue("@StackSizeRebuy", tournament.StackSizeRebuy);
                    sqlCommand.Parameters.AddWithValue("@StackSizeAddon", tournament.StackSizeAddon);
                    sqlCommand.Parameters.AddWithValue("@Guarantee", tournament.Guarantee);
                    sqlCommand.Parameters.AddWithValue("@SiteName", tournament.Venue);
                    sqlCommand.Parameters.AddWithValue("@GameType", tournament.GameType);
                    sqlCommand.Parameters.AddWithValue("@Formats", tournament.FormatString);
                    sqlCommand.Parameters.AddWithValue("@TableSize", tournament.TableSize);
                    sqlCommand.Parameters.AddWithValue("@BlindLevels", tournament.BlindLevels);
                    sqlCommand.Parameters.AddWithValue("@LateReg", tournament.LateReg);
                    sqlCommand.Parameters.AddWithValue("@Entrants", tournament.Entrants);
                    sqlCommand.Parameters.AddWithValue("@AddonCount", tournament.AddonCount);
                    sqlCommand.Parameters.AddWithValue("@RebuyCount", tournament.RebuyCount);
                    sqlCommand.Parameters.AddWithValue("@PrizeWon", tournament.PrizeWon);
                    sqlCommand.Parameters.AddWithValue("@FinishPosition", tournament.FinishPosition);
                    sqlCommand.Parameters.AddWithValue("@EntrantsPaid", tournament.EntrantsPaid);

                    if (sqlCommand.ExecuteNonQuery() != 1)
                    {
                        // error message
                        var theVm = new OkViewModel("Error saving record", "Error");

                        // create/show ok window
                        var theWindow = new OkView(theVm)
                        {
                            Owner = Application.Current.MainWindow,
                            WindowStartupLocation = WindowStartupLocation.CenterOwner,
                        };
                        theWindow.ShowDialog();
                    }
                }
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

                // toss error up
                throw;
            }
            finally
            {
                sqlCommand.Dispose();

                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            return true;
        }

        /// <summary>
        /// Connects to the current profiles database
        /// </summary>
        /// <param name="overrideDatabaseName">database name to use</param>
        /// <returns>database connection</returns>
        public static SqlCeConnection ConnectDatabase(string overrideDatabaseName = null)
        {
            // current database name
            var databaseName = XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + DatabaseName;

            // create connection
            var sqlConnection = overrideDatabaseName is null
                ? new SqlCeConnection(string.Format(new CultureInfo("en-US", false),
                    "DataSource=\"{0}\"; Password='{1}'", databaseName, Password))
                : new SqlCeConnection(string.Format(new CultureInfo("en-US", false),
                    "DataSource=\"{0}\"; Password='{1}'", overrideDatabaseName, Password));

            // open connection
            sqlConnection.Open();

            // return connection
            return sqlConnection;
        }

        /// <summary>
        /// Creates a database for the current profile
        /// </summary>
        public static void CreateDatabase(string profile, string overrideDatabaseName = null)
        {
            // current database name
            var databaseName = XmlHelper.PreferencesFolderName + profile + DatabaseName;

            // check for database
            if (File.Exists(databaseName))
            {
                // ok view model
                var theVm = new OkViewModel("Database with that name already exists", "Duplicate Name");

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

            // override database name
            if(overrideDatabaseName is not null)
                databaseName = overrideDatabaseName;

            var sqlCommand = new SqlCeCommand();
            var sqlConnection = new SqlCeConnection();

            // create database
            try
            {
                var engine = new SqlCeEngine(string.Format(new CultureInfo("en-US", false), "DataSource=\"{0}\"; Password='{1}'", databaseName, Password));
                engine.CreateDatabase();
                engine.Dispose();

                sqlConnection = ConnectDatabase(databaseName);

                var sqlCommandString = "create table Tournaments ("
                                       + "Id int IDENTITY(1,1) PRIMARY KEY, "
                                       + "Name nvarchar (50), "
                                       + "Screenshot nvarchar (200), "
                                       + "Note nvarchar (500), "
                                       + "IsBovadaBounty nvarchar (10), "
                                       + "IsSng nvarchar (10), "
                                       + "Bounty money, "
                                       + "BountyCount int, "
                                       + "BuyinCost money, "
                                       + "BuyinRake money, "
                                       + "RebuyCost money, "
                                       + "RebuyRake money, "
                                       + "AddonCost money, "
                                       + "AddonRake money, "
                                       + "StartTime datetime, "
                                       + "EndTime datetime, "
                                       + "StackSizeStarting int, "
                                       + "StackSizeRebuy int, "
                                       + "StackSizeAddon int, "
                                       + "Guarantee money, "
                                       + "SiteName nvarchar (50), "
                                       + "GameType nvarchar (50), "
                                       + "Formats nvarchar (100), "
                                       + "TableSize int, "
                                       + "BlindLevels int, "
                                       + "LateReg int, "
                                       + "Entrants int, "
                                       + "AddonCount int, "
                                       + "RebuyCount int, "
                                       + "JackpotSpinMultiplier int, "
                                       + "PrizeWon money, "
                                       + "FinishPosition int, "
                                       + "EntrantsPaid int )";

                sqlCommand = new SqlCeCommand(sqlCommandString, sqlConnection);
                sqlCommand.ExecuteNonQuery();
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

                // toss it out
                throw;
            }
            finally
            {
                sqlCommand.Dispose();

                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }

        /// <summary>
        /// Deletes a record from the current profiles database
        /// </summary>
        /// <param name="id">record to remove</param>
        public static void DeleteRecord(int id)
        {
            var sqlCommand = new SqlCeCommand();
            var sqlConnection = new SqlCeConnection();

            try
            {
                sqlConnection = ConnectDatabase();

                var sqlCommandString = "DELETE FROM Tournaments WHERE Id = (@id)";
                sqlCommand = new SqlCeCommand(sqlCommandString, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@id", id);

                sqlCommand.ExecuteNonQuery();
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
            finally
            {
                sqlCommand.Dispose();

                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }

        /// <summary>
        /// Edit a record from the current profiles database
        /// </summary>
        /// <param name="tournament">changes to save</param>
        /// <param name="id">record to change</param>
        public static void EditRecord(TournamentFinished tournament, int id)
        {
            if (tournament is null)
                return;

            var sqlCommand = new SqlCeCommand();
            var sqlConnection = new SqlCeConnection();

            try
            {
                sqlConnection = ConnectDatabase();

                var sqlCommandString = "UPDATE Tournaments "
                                       + "SET Screenshot = @Screenshot,IsSng = @IsSng, IsBovadaBounty = @IsBovadaBounty, Note = @Note, Name = @Name, Bounty = @Bounty, BountyCount = @BountyCount, JackpotSpinMultiplier = @JackpotSpinMultiplier, BuyinCost = @BuyinCost, BuyinRake = @BuyinRake, RebuyCost = @RebuyCost, RebuyRake = @RebuyRake," +
                                       " AddonCost = @AddonCost, AddonRake = @AddonRake, StartTime = @StartTime, EndTime = @EndTime, StackSizeStarting = @StackSizeStarting, StackSizeRebuy = @StackSizeRebuy" +
                                       ", StackSizeAddon = @StackSizeAddon, Guarantee = @Guarantee, SiteName = @SiteName, GameType = @GameType, Formats = @Formats, TableSize = @TableSize, BlindLevels = @BlindLevels, LateReg = @LateReg, Entrants = @Entrants, AddonCount = @AddonCount" +
                                       ", RebuyCount = @RebuyCount, PrizeWon = @PrizeWon, FinishPosition = @FinishPosition, EntrantsPaid = @EntrantsPaid WHERE Id = (@id)";

                sqlCommand = new SqlCeCommand(sqlCommandString, sqlConnection);

                sqlCommand.Parameters.AddWithValue("@Screenshot", tournament.ScreenshotFilename);
                sqlCommand.Parameters.AddWithValue("@IsSng", tournament.IsSng);
                sqlCommand.Parameters.AddWithValue("@IsBovadaBounty", tournament.IsBovadaBounty);
                sqlCommand.Parameters.AddWithValue("@Note", tournament.Note);
                sqlCommand.Parameters.AddWithValue("@Name", tournament.TournamentName);
                sqlCommand.Parameters.AddWithValue("@Bounty", tournament.Bounty);
                sqlCommand.Parameters.AddWithValue("@BountyCount", tournament.BountyCount);
                sqlCommand.Parameters.AddWithValue("@JackpotSpinMultiplier", tournament.JackpotSpinMultiplier);
                sqlCommand.Parameters.AddWithValue("@BuyinCost", tournament.BuyinBaseCost);
                sqlCommand.Parameters.AddWithValue("@BuyinRake", tournament.BuyinRakeCost);
                sqlCommand.Parameters.AddWithValue("@RebuyCost", tournament.RebuyBaseCost);
                sqlCommand.Parameters.AddWithValue("@RebuyRake", tournament.RebuyRakeCost);
                sqlCommand.Parameters.AddWithValue("@AddonCost", tournament.AddonBaseCost);
                sqlCommand.Parameters.AddWithValue("@AddonRake", tournament.AddonRakeCost);
                sqlCommand.Parameters.AddWithValue("@StartTime", tournament.StartTime);
                sqlCommand.Parameters.AddWithValue("@EndTime", tournament.EndTime);
                sqlCommand.Parameters.AddWithValue("@StackSizeStarting", tournament.StackSizeStarting);
                sqlCommand.Parameters.AddWithValue("@StackSizeRebuy", tournament.StackSizeRebuy);
                sqlCommand.Parameters.AddWithValue("@StackSizeAddon", tournament.StackSizeAddon);
                sqlCommand.Parameters.AddWithValue("@Guarantee", tournament.Guarantee);
                sqlCommand.Parameters.AddWithValue("@SiteName", tournament.Venue);
                sqlCommand.Parameters.AddWithValue("@GameType", tournament.GameType);
                sqlCommand.Parameters.AddWithValue("@Formats", tournament.FormatString);
                sqlCommand.Parameters.AddWithValue("@TableSize", tournament.TableSize);
                sqlCommand.Parameters.AddWithValue("@BlindLevels", tournament.BlindLevels);
                sqlCommand.Parameters.AddWithValue("@LateReg", tournament.LateReg);
                sqlCommand.Parameters.AddWithValue("@Entrants", tournament.Entrants);
                sqlCommand.Parameters.AddWithValue("@AddonCount", tournament.AddonCount);
                sqlCommand.Parameters.AddWithValue("@RebuyCount", tournament.RebuyCount);
                sqlCommand.Parameters.AddWithValue("@PrizeWon", tournament.PrizeWon);
                sqlCommand.Parameters.AddWithValue("@FinishPosition", tournament.FinishPosition);
                sqlCommand.Parameters.AddWithValue("@EntrantsPaid", tournament.EntrantsPaid);
                sqlCommand.Parameters.AddWithValue("@id", id);

                if (sqlCommand.ExecuteNonQuery() != 1)
                {
                    // error message
                    var theVm = new OkViewModel("Error editing record", "Error");

                    // create/show ok window
                    var theWindow = new OkView(theVm)
                    {
                        Owner = Application.Current.MainWindow,
                        WindowStartupLocation = WindowStartupLocation.CenterOwner,
                    };
                    theWindow.ShowDialog();
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
        }

        /// <summary>
        /// Load the current profiles database
        /// </summary>
        /// <returns>collection of tournaments from the database</returns>
        public static ObservableCollection<TournamentFinished> LoadDatabase(string databaseName = null)
        {
            var sqlCommand = new SqlCeCommand();
            var sqlConnection = new SqlCeConnection();

            var databaseTournaments = new ObservableCollection<TournamentFinished>();

            try
            {
                if (databaseName is null)
                    sqlConnection = ConnectDatabase();
                else
                    sqlConnection = ConnectDatabase(databaseName);

                sqlCommand = new SqlCeCommand(TableName, sqlConnection)
                {
                    CommandType = CommandType.TableDirect
                };

                var sqlResultSet = sqlCommand.ExecuteResultSet(ResultSetOptions.Scrollable);

                // loop table rows (each row is one tournament) and load into list
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
                                DatabaseId = sqlResultSet.GetInt32(sqlResultSet.GetOrdinal("Id")),
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
                                StartTime = (DateTime)sqlResultSet.GetSqlDateTime(sqlResultSet.GetOrdinal("StartTime")),
                                TableSize = (int)sqlResultSet.GetSqlInt32(sqlResultSet.GetOrdinal("TableSize")),
                                TournamentName = sqlResultSet.GetString(sqlResultSet.GetOrdinal("Name")),
                                Venue = sqlResultSet.GetString(sqlResultSet.GetOrdinal("SiteName")),
                            };

                            // handle is sng column that might not exist
                            if (!sqlResultSet.IsDBNull(sqlResultSet.GetOrdinal("IsSng")))
                                tournamentRecord.IsSng = bool.Parse(sqlResultSet.GetString(sqlResultSet.GetOrdinal("IsSng")));

                            // handle is Bovada bounty column that might not exist
                            if (!sqlResultSet.IsDBNull(sqlResultSet.GetOrdinal("IsBovadaBounty")))
                                tournamentRecord.IsBovadaBounty = bool.Parse(sqlResultSet.GetString(sqlResultSet.GetOrdinal("IsBovadaBounty")));

                            // handle note column that might not exist
                            if (!sqlResultSet.IsDBNull(sqlResultSet.GetOrdinal("Note")))
                                tournamentRecord.Note = sqlResultSet.GetString(sqlResultSet.GetOrdinal("Note"));

                            // handle bounty column that might not exist
                            if (!sqlResultSet.IsDBNull(sqlResultSet.GetOrdinal("Bounty")))
                                tournamentRecord.Bounty = (decimal)sqlResultSet.GetSqlMoney(sqlResultSet.GetOrdinal("Bounty"));

                            // handle bounty count column that might not exist
                            if (!sqlResultSet.IsDBNull(sqlResultSet.GetOrdinal("BountyCount")))
                                tournamentRecord.BountyCount = (int)sqlResultSet.GetSqlInt32(sqlResultSet.GetOrdinal("BountyCount"));

                            // handle jackpot spin multiplier column that might not exist
                            if (!sqlResultSet.IsDBNull(sqlResultSet.GetOrdinal("JackpotSpinMultiplier")))
                                tournamentRecord.JackpotSpinMultiplier = (int)sqlResultSet.GetSqlInt32(sqlResultSet.GetOrdinal("JackpotSpinMultiplier"));

                            //load formats
                            tournamentRecord.LoadFormats(sqlResultSet.GetString(sqlResultSet.GetOrdinal("Formats")));

                            // add to collection
                            databaseTournaments.Add(tournamentRecord);
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

            return new ObservableCollection<TournamentFinished>(databaseTournaments.OrderBy(i => i.StartTime));
        }

        /// <summary>
        /// Gets the next unique database id number
        /// </summary>
        /// <returns>Database id number</returns>
        public static int NewDatabaseId()
        {
            var sqlCommand = new SqlCeCommand();
            var sqlConnection = new SqlCeConnection();

            var newId = 0;

            try
            {
                sqlConnection = ConnectDatabase();

                sqlCommand = new SqlCeCommand(TableName, sqlConnection)
                {
                    CommandType = CommandType.TableDirect
                };

                var sqlResultSet = sqlCommand.ExecuteResultSet(ResultSetOptions.Scrollable);

                // loop table rows once, get row id and compare to newId, if higher store row id in nextId
                if (sqlResultSet.HasRows)
                {
                    while (sqlResultSet.Read())
                        if (sqlResultSet.GetInt32(sqlResultSet.GetOrdinal("Id")) > newId)
                            newId = sqlResultSet.GetInt32(sqlResultSet.GetOrdinal("Id"));
                }
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
            finally
            {
                sqlCommand.Dispose();

                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            return newId;
        }
    }
}
