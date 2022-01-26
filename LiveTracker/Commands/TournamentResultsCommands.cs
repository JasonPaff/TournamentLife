using LiveTracker.Models;
using LiveTracker.ViewModels.Graph_ViewModels;
using LiveTracker.Views.Graph_Views;
using Syncfusion.UI.Xaml.Utility;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Tournament_Life.ViewModels.Results;
using Tournament_Life.ViewModels.Graph_ViewModels;
using Tournament_Life.Views.Graph_Views;
using Tournament_Life.Views.Results;
using LiveTracker.Helpers;
using Tournament_Life.ViewModels;
using Tournament_Life.Views;
using LiveTracker.Views;

namespace LiveTracker.Commands
{
    public static class TournamentResultsCommands
    {
        public static ICommand MenuItemCommand => new BaseCommand(MenuItem);

        /// <summary>
        /// parse commands from tournaments results view
        /// </summary>
        /// <param name="parameter"></param>
        public static void MenuItem(object parameter)
        {
            switch (parameter as string)
            {
                case "BuyinRoiHistogram":
                    BuyinRoiHistogram();
                    break;
                case "ClearFilter":
                    ClearFilter();
                    break;
                case "CreateFilter":
                    CreateFilter();
                    break;
                case "DeleteFilter":
                    DeleteFilter();
                    break;
                case "Exit":
                    Exit();
                    break;
                case "FinishPositionGraph":
                    FinishPositionGraph();
                    break;
                case "FormatRoiChart":
                    FormatRoiChart();
                    break;
                case "GameTypeRoiChart":
                    GameTypeRoiChart();
                    break;
                case "Last50":
                    QuickFilterLast(50);
                    break;
                case "Last100":
                    QuickFilterLast(100);
                    break;
                case "Last250":
                    QuickFilterLast(250);
                    break;
                case "Last500":
                    QuickFilterLast(500);
                    break;
                case "Last1000":
                    QuickFilterLast(1000);
                    break;
                case "90Days":
                    NinetyDays();
                    break;
                case "12Hours":
                    TwelveHours();
                    break;
                case "24Hours":
                    TwentyFourHours();
                    break;
                case "7Days":
                    SevenDays();
                    break;
                case "30Days":
                    ThirtyDays();
                    break;
                case "LoadFilter":
                    LoadFilter();
                    break;
                case "MonthToDate":
                    MonthToDate();
                    break;
                case "Today":
                    Today();
                    break;
                case "YearToDate":
                    YearToDate();
                    break;
                case "ProfitGraph":
                    ProfitGraph();
                    break;
                case "ShowCurrentFilter":
                    ShowCurrentFilter();
                    break;
                case "VenueRoiChart":
                    VenueRoiChart();
                    break;
                case "ViewTournaments":
                    ViewTournaments();
                    break;
                case "WeekToDate":
                    WeekToDate();
                    break;
                case "Yesterday":
                    Yesterday();
                    break;
            }
        }

        /// <summary>
        /// show the buyin/roi historgram
        /// </summary>
        private static void BuyinRoiHistogram()
        {
            // find tournament results window
            if (Application.Current.Windows.OfType<TournamentsResultsView>()?.FirstOrDefault()?.DataContext is not TournamentsResultsViewModel tournamentsResultsViewModel)
                return;

            // no tournaments
            if (tournamentsResultsViewModel.Tournaments is null || tournamentsResultsViewModel.Tournaments.Count is 0)
                return;

            // create/show finish position graph
            var vm = new TournamentBuyinRoiHistogramViewModel(tournamentsResultsViewModel.Tournaments);
            var window = new TournamentBuyinRoiHistogramView(vm)
            {
                Owner = Application.Current.Windows.OfType<TournamentsResultsView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            window.Show();
        }

        /// <summary>
        /// reset the tournaments
        /// </summary>
        private static void ClearFilter()
        {
            // find tournament results window view model
            if (Application.Current.Windows.OfType<TournamentsResultsView>()?.FirstOrDefault()?.DataContext is not TournamentsResultsViewModel tournamentsResultsViewModel)
                return;

            // update view to refresh to a clean view
            tournamentsResultsViewModel.Update();

            // reset current filter
            tournamentsResultsViewModel.CurrentFilter = "All Results";

            // reset filter text
            tournamentsResultsViewModel.FilterName = "";

            // update title
            tournamentsResultsViewModel.SetTitle("All Results");
        }

        /// <summary>
        /// launch create tournament filter window
        /// </summary>
        private static void CreateFilter()
        {
            // only open one at a time
            if (Application.Current.Windows.OfType<CreateTournamentFilterView>().Any()) return;

            // find tournament results window
            if (Application.Current.Windows.OfType<TournamentsResultsView>()?.FirstOrDefault()?.DataContext is not TournamentsResultsViewModel tournamentsResultsViewModel)
                return;

            // create/show create filter window
            var vm = new CreateTournamentFilterViewModel(tournamentsResultsViewModel.UnfilteredTournaments);
            var window = new CreateTournamentFilterView(vm)
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            window.Show(); window.Activate();
        }

        /// <summary>
        /// create/launch delete tournament filter window
        /// </summary>
        private static void DeleteFilter()
        {
            // filter file name
            var filename = XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.FiltersFileName;

            // load filters file
            var doc = XmlHelper.LoadXmlFile(filename);

            // no file
            if (doc is null)
            {
                // ok view model
                var theVm = new OkViewModel("No filters to delete", "No Filters");

                // create/show ok window
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.MainWindow,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                // exit
                return;
            }

            // get all the saved tournament filters
            var tournamentFilterNodes = doc.DocumentElement.SelectSingleNode("TournamentFilters").SelectNodes("Filter");

            // no filters in the file
            if (tournamentFilterNodes.Count is 0)
            {
                // ok view model
                var theVm = new OkViewModel("No filters to delete", "No Filters");

                // create/show ok window
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.MainWindow,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                // exit
                return;
            }

            // create/show delete tournament window
            var vm = new DeleteTournamentFilterViewModel();
            var window = new DeleteTournamentFilterView(vm)
            {
                Owner = Application.Current.Windows.OfType<TournamentsResultsView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            window.ShowDialog();
        }

        /// <summary>
        /// close tournaments results window
        /// </summary>
        private static void Exit()
        {
            Application.Current.Windows.OfType<TournamentsResultsView>()?.FirstOrDefault()?.Close();
        }

        /// <summary>
        /// Launch finish position graph
        /// </summary>
        private static void FinishPositionGraph()
        {
            // find tournament results window
            if (Application.Current.Windows.OfType<TournamentsResultsView>()?.FirstOrDefault()?.DataContext is not TournamentsResultsViewModel tournamentsResultsViewModel)
                return;

            // no tournaments
            if (tournamentsResultsViewModel.Tournaments is null || tournamentsResultsViewModel.Tournaments.Count is 0)
                return;

            // create/show finish position graph
            var vm = new FinishPositionGraphViewModel(tournamentsResultsViewModel.Tournaments);
            var window = new FinishPositionGraphView(vm)
            {
                Owner = Application.Current.Windows.OfType<TournamentsResultsView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            window.Show();
        }

        /// <summary>
        /// show the format/roi chart
        /// </summary>
        private static void FormatRoiChart()
        {
            // find tournament results window
            if (Application.Current.Windows.OfType<TournamentsResultsView>()?.FirstOrDefault()?.DataContext is not TournamentsResultsViewModel tournamentsResultsViewModel)
                return;

            // no tournaments
            if (tournamentsResultsViewModel.Tournaments is null || tournamentsResultsViewModel.Tournaments.Count is 0)
                return;

            // create/show finish position graph
            var vm = new TournamentFormatRoiChartViewModel(tournamentsResultsViewModel.Tournaments);
            var window = new TournamentFormatRoiChartView(vm)
            {
                Owner = Application.Current.Windows.OfType<TournamentsResultsView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            window.Show();
        }

        /// <summary>
        /// show the game type/roi chart
        /// </summary>
        private static void GameTypeRoiChart()
        {
            // find tournament results window
            if (Application.Current.Windows.OfType<TournamentsResultsView>()?.FirstOrDefault()?.DataContext is not TournamentsResultsViewModel tournamentsResultsViewModel)
                return;

            // no tournaments
            if (tournamentsResultsViewModel.Tournaments is null || tournamentsResultsViewModel.Tournaments.Count is 0)
                return;

            // create/show finish position graph
            var vm = new TournamentGameTypeRoiChartViewModel(tournamentsResultsViewModel.Tournaments);
            var window = new TournamentGameTypeRoiChartView(vm)
            {
                Owner = Application.Current.Windows.OfType<TournamentsResultsView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            window.Show();
        }

        /// <summary>
        /// launch the load filter window
        /// </summary>
        private static void LoadFilter()
        {
            // filter file name
            var filename = XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.FiltersFileName;

            // load filters file
            var doc = XmlHelper.LoadXmlFile(filename);

            // no file
            if (doc is null)
            {
                // ok view model
                var theVm = new OkViewModel("No filters to load", "No Filters");

                // create/show ok window
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.MainWindow,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                // exit
                return;
            }

            // get all the saved tournament filters
            var tournamentFilterNodes = doc.DocumentElement.SelectSingleNode("TournamentFilters").SelectNodes("Filter");

            // no filters in the file
            if (tournamentFilterNodes.Count is 0)
            {
                // ok view model
                var theVm = new OkViewModel("No filters to load", "No Filters");

                // create/show ok window
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.MainWindow,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                // exit
                return;
            }

            // create/show load filter window
            var vm = new LoadTournamentFilterViewModel();
            var window = new LoadTournamentFilterView(vm)
            {
                Owner = Application.Current.Windows.OfType<TournamentsResultsView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            window.ShowDialog();
        }

        /// <summary>
        /// filter tournaments to current month
        /// </summary>
        private static void MonthToDate()
        {
            // find tournament results window
            if (Application.Current.Windows.OfType<TournamentsResultsView>()?.FirstOrDefault()?.DataContext is not TournamentsResultsViewModel tournamentsResultsViewModel)
                return;

            // no tournaments
            if (tournamentsResultsViewModel.UnfilteredTournaments is null || tournamentsResultsViewModel.UnfilteredTournaments.Count is 0)
            {
                // ok view model
                var theVm = new OkViewModel($"No tournaments played during the current month ({DateTime.Now.Month})", "Current Month");

                // create/show ok window
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.Windows.OfType<TournamentsResultsView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                // exit
                return;
            }

            // get tournaments from the current month
            var temp = new ObservableCollection<TournamentFinished>(tournamentsResultsViewModel.UnfilteredTournaments.Where(i => i.StartTime.Month == DateTime.Now.Month && i.StartTime.Year == DateTime.Now.Year));

            // no data message
            if (temp is null || temp.Count is 0)
            {
                // ok view model
                var theVm = new OkViewModel($"No tournaments played during the current month ({DateTime.Now.Month})", "Current Month");

                // create/show ok window
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.Windows.OfType<TournamentsResultsView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                // exit
                return;
            }

            // update tournaments
            tournamentsResultsViewModel.Tournaments = new ObservableCollection<TournamentFinished>(tournamentsResultsViewModel.UnfilteredTournaments.Where(i => i.StartTime.Month == DateTime.Now.Month && i.StartTime.Year == DateTime.Now.Year));

            // filter
            tournamentsResultsViewModel.Filter();

            // holds current month
            var month = "";

            // find current month
            switch(DateTime.Now.Month)
            {
                case 1:
                    month = "January";
                    break;
                case 2:
                    month = "February";
                    break;
                case 3:
                    month = "March";
                    break;
                case 4:
                    month = "April";
                    break;
                case 5:
                    month = "May";
                    break;
                case 6:
                    month = "June";
                    break;
                case 7:
                    month = "July";
                    break;
                case 8:
                    month = "August";
                    break;
                case 9:
                    month = "September";
                    break;
                case 10:
                    month = "October";
                    break;
                case 11:
                    month = "November";
                    break;
                case 12:
                    month = "December";
                    break;
            }

            // update current filter message
            tournamentsResultsViewModel.CurrentFilter = $"Current Month ({month})";

            // update title
            tournamentsResultsViewModel.FilterName = $"Current Month ({month})";
        }

        /// <summary>
        /// filter tournaments to the last 90 days
        /// </summary>
        private static void NinetyDays()
        {
            // find session results window
            if (Application.Current.Windows.OfType<TournamentsResultsView>()?.FirstOrDefault()?.DataContext is not TournamentsResultsViewModel tournamentsResultsViewModel)
                return;

            // no tournaments
            if (tournamentsResultsViewModel.UnfilteredTournaments is null || tournamentsResultsViewModel.UnfilteredTournaments.Count is 0)
            {
                // ok view model
                var theVm = new OkViewModel("No tournaments played in the last 90 Days", "90 Days");

                // create/show ok window
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.Windows.OfType<TournamentsResultsView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                // exit
                return;
            }

            // get tournaments from the last 90 days
            var temp = new ObservableCollection<TournamentFinished>(tournamentsResultsViewModel.UnfilteredTournaments.Where(i => i.StartTime >= DateTime.Now.AddDays(-90)));

            // no data message
            if (temp is null || temp.Count is 0)
            {
                // ok view model
                var theVm = new OkViewModel("No tournaments played in the last 90 Days", "90 Days");

                // create/show ok window
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.Windows.OfType<TournamentsResultsView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                // exit
                return;
            }

            // update tournaments
            tournamentsResultsViewModel.Tournaments = new ObservableCollection<TournamentFinished>(tournamentsResultsViewModel.UnfilteredTournaments.Where(i => i.StartTime >= DateTime.Now.AddDays(-90)));

            // filter
            tournamentsResultsViewModel.Filter();

            // update current filter message
            tournamentsResultsViewModel.CurrentFilter = "Last 90 Days";
            // update title
            tournamentsResultsViewModel.FilterName = "Last 90 Days";
        }

        /// <summary>
        /// Launch tournaments profit graph
        /// </summary>
        private static void ProfitGraph()
        {
            // find session results window
            if (Application.Current.Windows.OfType<TournamentsResultsView>()?.FirstOrDefault()?.DataContext is not TournamentsResultsViewModel tournamentsResultsViewModel)
                return;

            // no tournaments
            if (tournamentsResultsViewModel.Tournaments is null || tournamentsResultsViewModel.Tournaments.Count is 0)
                return;

            // under 1000 data points, show labels
            if(tournamentsResultsViewModel.Tournaments.Count <= 600)
            {
                // create/show tournaments profit graph
                var vm = new TournamentProfitGraphViewModel(tournamentsResultsViewModel.Tournaments, tournamentsResultsViewModel.CurrentFilter);
                var window = new TournamentProfitGraphView(vm)
                {
                    Owner = Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner
                };
               window.Show();

               return;
            }

            // create/show tournaments profit graph with no labels
            var vmm = new TournamentProfitGraphNoLabelViewModel(tournamentsResultsViewModel.Tournaments, tournamentsResultsViewModel.CurrentFilter);
            var windoww = new TournamentProfitGraphNoLabelView(vmm)
            {
                Owner = Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            windoww.Show();
        }

        /// <summary>
        /// filter tournaments by last amount played
        /// </summary>
        /// <param name="amount"></param>
        private static void QuickFilterLast(int amount)
        {
            // find tournament results window
            if (Application.Current.Windows.OfType<TournamentsResultsView>()?.FirstOrDefault()?.DataContext is not TournamentsResultsViewModel tournamentsResultsViewModel)
                return;

            // no tournaments
            if (tournamentsResultsViewModel.UnfilteredTournaments is null || tournamentsResultsViewModel.UnfilteredTournaments.Count is 0)
            {
                // ok view model
                var theVm = new OkViewModel("No tournaments in the database", "Empty Database");

                // create/show ok window
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.Windows.OfType<TournamentsResultsView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                // exit
                return;
            }

            // get tournaments from last amount
            var temp = new ObservableCollection<TournamentFinished>(tournamentsResultsViewModel.UnfilteredTournaments.OrderByDescending(i => i.StartDate).Take(amount));

            // no data message
            if (temp is null || temp.Count is 0)
            {
                // ok view model
                var theVm = new OkViewModel("No tournaments in the database", "Empty Database");

                // create/show ok window
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.Windows.OfType<TournamentsResultsView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                // exit
                return;
            }

            // update tournaments
            tournamentsResultsViewModel.Tournaments = new ObservableCollection<TournamentFinished>(temp.OrderBy(i => i.StartDate));

            // show message if not able to grab full amount
            if (tournamentsResultsViewModel.Tournaments.Count < amount)
            {
                // yes/no view model
                var vm = new YesNoViewModel($"only able to find {tournamentsResultsViewModel.Tournaments.Count} tournament(s) in your database", "Limited Database");

                // create/show yes/no window
                var window = new YesNoView(vm)
                {
                    Owner = Application.Current.MainWindow,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                window.ShowDialog();

                amount = tournamentsResultsViewModel.Tournaments.Count;
            }

            // filter
            tournamentsResultsViewModel.Filter();

            // update current filter message
            tournamentsResultsViewModel.CurrentFilter = $"{amount} most recently played tournaments";

            // update title
            tournamentsResultsViewModel.FilterName = $"{amount} most recently played tournaments";
        }

        /// <summary>
        /// filter tournaments to the last 12 hours
        /// </summary>
        private static void TwelveHours()
        {
            // find session results window
            if (Application.Current.Windows.OfType<TournamentsResultsView>()?.FirstOrDefault()?.DataContext is not TournamentsResultsViewModel tournamentsResultsViewModel)
                return;

            // no tournaments
            if (tournamentsResultsViewModel.UnfilteredTournaments is null || tournamentsResultsViewModel.UnfilteredTournaments.Count is 0)
            {
                // ok view model
                var theVm = new OkViewModel("No tournaments played in the last 12 hours", "12 Hours");

                // create/show ok window
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.Windows.OfType<TournamentsResultsView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                // exit
                return;
            }

            // get tournaments from the last 24 hours
            var temp = new ObservableCollection<TournamentFinished>(tournamentsResultsViewModel.UnfilteredTournaments.Where(i => i.StartTime >= DateTime.Now.AddHours(-12)));

            // no data message
            if (temp is null || temp.Count is 0)
            {
                // ok view model
                var theVm = new OkViewModel("No tournaments played in the last 12 hours", "12 Hours");

                // create/show ok window
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.Windows.OfType<TournamentsResultsView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                // exit
                return;
            }

            // update tournaments
            tournamentsResultsViewModel.Tournaments = new ObservableCollection<TournamentFinished>(tournamentsResultsViewModel.UnfilteredTournaments.Where(i => i.StartTime >= DateTime.Now.AddHours(-12)));

            // filter
            tournamentsResultsViewModel.Filter();

            // update current filter message
            tournamentsResultsViewModel.CurrentFilter = "Last 12 Hours";

            // update title
            tournamentsResultsViewModel.FilterName = "Last 12 Hours";
        }

        /// <summary>
        /// filter tournaments to the last 24 hours
        /// </summary>
        private static void TwentyFourHours()
        {
            // find tournament results window
            if (Application.Current.Windows.OfType<TournamentsResultsView>()?.FirstOrDefault()?.DataContext is not TournamentsResultsViewModel tournamentsResultsViewModel)
                return;

            // no tournaments
            if (tournamentsResultsViewModel.UnfilteredTournaments is null || tournamentsResultsViewModel.UnfilteredTournaments.Count is 0)
            {
                // ok view model
                var theVm = new OkViewModel("No tournaments played in the last 24 hours", "24 Hours");

                // create/show ok window
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.Windows.OfType<TournamentsResultsView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                // exit
                return;
            }

            // get tournaments from the last 24 hours
            var temp = new ObservableCollection<TournamentFinished>(tournamentsResultsViewModel.UnfilteredTournaments.Where(i => i.StartTime >= DateTime.Now.AddHours(-24)));

            // no data message
            if (temp is null || temp.Count is 0)
            {
                // ok view model
                var theVm = new OkViewModel("No tournaments played in the last 24 hours", "24 Hours");

                // create/show ok window
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.Windows.OfType<TournamentsResultsView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                // exit
                return;
            }

            // update tournaments
            tournamentsResultsViewModel.Tournaments = new ObservableCollection<TournamentFinished>(tournamentsResultsViewModel.UnfilteredTournaments.Where(i => i.StartTime >= DateTime.Now.AddHours(-24)));

            // filter
            tournamentsResultsViewModel.Filter();

            // update current filter message
            tournamentsResultsViewModel.CurrentFilter = "Last 24 Hours";

            // update title
            tournamentsResultsViewModel.FilterName = "Last 24 Hours";
        }

        /// <summary>
        /// filter tournaments to the last 7 days
        /// </summary>
        private static void SevenDays()
        {
            // find session results window
            if (Application.Current.Windows.OfType<TournamentsResultsView>()?.FirstOrDefault()?.DataContext is not TournamentsResultsViewModel tournamentsResultsViewModel)
                return;

            // no tournaments
            if (tournamentsResultsViewModel.UnfilteredTournaments is null || tournamentsResultsViewModel.UnfilteredTournaments.Count is 0)
            {
                // ok view model
                var theVm = new OkViewModel("No tournaments played in the last 7 days", "7 Days");

                // create/show ok window
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.Windows.OfType<TournamentsResultsView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                // exit
                return;
            }

            // get tournaments from the last 24 hours
            var temp = new ObservableCollection<TournamentFinished>(tournamentsResultsViewModel.UnfilteredTournaments.Where(i => i.StartTime >= DateTime.Now.AddDays(-7)));

            // no data message
            if (temp is null || temp.Count is 0)
            {
                // ok view model
                var theVm = new OkViewModel("No tournaments played in the last 7 days", "7 Days");

                // create/show ok window
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.Windows.OfType<TournamentsResultsView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                // exit
                return;
            }

            // update tournaments
            tournamentsResultsViewModel.Tournaments = new ObservableCollection<TournamentFinished>(tournamentsResultsViewModel.UnfilteredTournaments.Where(i => i.StartTime >= DateTime.Now.AddDays(-7)));

            // filter
            tournamentsResultsViewModel.Filter();

            // update current filter message
            tournamentsResultsViewModel.CurrentFilter = "Last 7 Days";

            // update title
            tournamentsResultsViewModel.FilterName = "Last 7 Days";
        }

        /// <summary>
        /// Show the current filter for the results
        /// </summary>
        private static void ShowCurrentFilter()
        {
            // find tournament results window
            if (Application.Current.Windows.OfType<TournamentsResultsView>()?.FirstOrDefault()?.DataContext is not TournamentsResultsViewModel tournamentsResultsViewModel)
                return;

            // default message if none exists
            if (tournamentsResultsViewModel.CurrentFilter.Length is 0)
                tournamentsResultsViewModel.CurrentFilter = "All Results";

            // ok view model
            var theVm = new OkViewModel(tournamentsResultsViewModel.CurrentFilter, "Current Filter");

            // create/show ok window
            var theWindow = new OkView(theVm)
            {
                Owner = Application.Current.Windows.OfType<TournamentsResultsView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            theWindow.ShowDialog();
        }

        /// <summary>
        /// filter tournaments to the last 30 days
        /// </summary>
        private static void ThirtyDays()
        {
            // find session results window
            if (Application.Current.Windows.OfType<TournamentsResultsView>()?.FirstOrDefault()?.DataContext is not TournamentsResultsViewModel tournamentsResultsViewModel)
                return;

            // no tournaments
            if (tournamentsResultsViewModel.UnfilteredTournaments is null || tournamentsResultsViewModel.UnfilteredTournaments.Count is 0)
            {
                // ok view model
                var theVm = new OkViewModel("No tournaments played in the last 30 days", "30 Days");

                // create/show ok window
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.Windows.OfType<TournamentsResultsView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                // exit
                return;
            }

            // get tournaments from the last thirty days
            var temp = new ObservableCollection<TournamentFinished>(tournamentsResultsViewModel.UnfilteredTournaments.Where(i => i.StartTime >= DateTime.Now.AddDays(-30)));

            // no data message
            if (temp is null || temp.Count is 0)
            {
                // ok view model
                var theVm = new OkViewModel("No tournaments played in the last 30 days", "30 Days");

                // create/show ok window
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.Windows.OfType<TournamentsResultsView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                // exit
                return;
            }

            // update tournaments
            tournamentsResultsViewModel.Tournaments = new ObservableCollection<TournamentFinished>(tournamentsResultsViewModel.UnfilteredTournaments.Where(i => i.StartTime >= DateTime.Now.AddDays(-30)));

            // filter
            tournamentsResultsViewModel.Filter();

            // update current filter message
            tournamentsResultsViewModel.CurrentFilter = "Last 30 Days";

            // update title
            tournamentsResultsViewModel.FilterName = "Last 30 Days";
        }

        /// <summary>
        /// tournaments for the current day
        /// </summary>
        private static void Today()
        {
            // find tournament results window
            if (Application.Current.Windows.OfType<TournamentsResultsView>()?.FirstOrDefault()?.DataContext is not TournamentsResultsViewModel tournamentsResultsViewModel)
                return;

            // no tournaments
            if (tournamentsResultsViewModel.UnfilteredTournaments is null || tournamentsResultsViewModel.UnfilteredTournaments.Count is 0)
            {
                // ok view model
                var theVm = new OkViewModel($"No tournaments were played during the current day ({DateTime.Now.Date.ToShortDateString()})", "Today");

                // create/show ok window
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.Windows.OfType<TournamentsResultsView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                // exit
                return;
            }

            // get tournaments from the current date
            var temp = new ObservableCollection<TournamentFinished>(tournamentsResultsViewModel.UnfilteredTournaments.Where(i => i.StartTime.Date == DateTime.Now.Date));

            // no data message
            if (temp is null || temp.Count is 0)
            {
                // ok view model
                var theVm = new OkViewModel($"No tournaments were played during the current day ({DateTime.Now.Date.ToShortDateString()})", "Today");

                // create/show ok window
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.Windows.OfType<TournamentsResultsView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                // exit
                return;
            }

            // update tournaments
            tournamentsResultsViewModel.Tournaments = new ObservableCollection<TournamentFinished>(temp);

            // filter
            tournamentsResultsViewModel.Filter();

            // update current filter message
            tournamentsResultsViewModel.CurrentFilter = $"Today - ({DateTime.Now.Date.ToShortDateString()})";

            // update title
            tournamentsResultsViewModel.FilterName = $"Today - ({DateTime.Now.Date.ToShortDateString()})";
        }

        /// <summary>
        /// show the format/roi chart
        /// </summary>
        private static void VenueRoiChart()
        {
            // find tournament results window
            if (Application.Current.Windows.OfType<TournamentsResultsView>()?.FirstOrDefault()?.DataContext is not TournamentsResultsViewModel tournamentsResultsViewModel)
                return;

            // no tournaments
            if (tournamentsResultsViewModel.Tournaments is null || tournamentsResultsViewModel.Tournaments.Count is 0)
                return;

            // create/show finish position graph
            var vm = new TournamentVenueRoiChartViewModel(tournamentsResultsViewModel.Tournaments);
            var window = new TournamentVenueRoiChartView(vm)
            {
                Owner = Application.Current.Windows.OfType<TournamentsResultsView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            window.Show();
        }

        /// <summary>
        /// launch view tournaments window
        /// </summary>
        private static void ViewTournaments()
        {
            // find session results window
            if (Application.Current.Windows.OfType<TournamentsResultsView>()?.FirstOrDefault()?.DataContext is not TournamentsResultsViewModel tournamentsResultsViewModel)
                return;

            // no tournaments
            if(tournamentsResultsViewModel.Tournaments is null || tournamentsResultsViewModel.Tournaments.Count is 0)
                return;

            // create/show tournaments view
            var vm = new TournamentsViewModel(tournamentsResultsViewModel.Tournaments);
            var window = new TournamentsView(vm)
            {
                Owner = Application.Current.Windows.OfType<TournamentsResultsView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            window.Show();
        }

        /// <summary>
        /// filter tournaments to the current week
        /// </summary>
        private static void WeekToDate()
        {
            // find tournament results window
            if (Application.Current.Windows.OfType<TournamentsResultsView>()?.FirstOrDefault()?.DataContext is not TournamentsResultsViewModel tournamentsResultsViewModel)
                return;

            // current Date
            var currentDate = DateTime.Now;

            // day of the week as an int (Sunday is 0)
            var dayOfWeekInt = (int)currentDate.DayOfWeek;

            // date for previous Sunday
            var sundayDate = currentDate.AddDays(-dayOfWeekInt).Date;

            // set h/m/s of previous Sunday to 12:00:00 AM
            sundayDate = new DateTime(sundayDate.Year,sundayDate.Month,sundayDate.Day, 0,0,0);

            // no tournaments error message
            if (tournamentsResultsViewModel.UnfilteredTournaments is null || tournamentsResultsViewModel.UnfilteredTournaments.Count is 0)
            {
                // ok view model
                var theVm = new OkViewModel($"No tournaments played during the current week ({sundayDate.Date.ToShortDateString()} to {sundayDate.AddDays(7).Date.ToShortDateString()})", "Current Week");

                // create/show ok window
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.Windows.OfType<TournamentsResultsView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                // exit
                return;
            }

            // pull tournaments that took place on or after the Sunday date
            var temp = new ObservableCollection<TournamentFinished>(tournamentsResultsViewModel.UnfilteredTournaments.Where(i => i.StartTime.Date >= sundayDate.Date));

            // no tournaments error message
            if (temp is null || temp.Count is 0)
            {
                // ok view model
                var theVm = new OkViewModel($"No tournaments played during the current week - ({sundayDate.Date.ToShortDateString()} to {sundayDate.AddDays(7).Date.ToShortDateString()}", "Current Week");

                // create/show ok window
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.Windows.OfType<TournamentsResultsView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                // exit
                return;
            }

            // update tournaments
            tournamentsResultsViewModel.Tournaments = new ObservableCollection<TournamentFinished>(tournamentsResultsViewModel.UnfilteredTournaments.Where(i => i.StartTime.Month == DateTime.Now.Month && i.StartTime.Year == DateTime.Now.Year));

            // filter
            tournamentsResultsViewModel.Filter();

            // update current filter message
            tournamentsResultsViewModel.CurrentFilter = $"Current Week - ({sundayDate.Date.ToShortDateString()} to {sundayDate.AddDays(7).Date.ToShortDateString()})";

            // update title text
            tournamentsResultsViewModel.FilterName = $"Current Week - ({sundayDate.Date.ToShortDateString()} to {sundayDate.AddDays(7).Date.ToShortDateString()})";
        }

        /// <summary>
        /// filter tournaments to the current year
        /// </summary>
        private static void YearToDate()
        {
            // find session results window
            if (Application.Current.Windows.OfType<TournamentsResultsView>()?.FirstOrDefault()?.DataContext is not TournamentsResultsViewModel tournamentsResultsViewModel) return;

            // no tournaments
            if (tournamentsResultsViewModel.UnfilteredTournaments is null || tournamentsResultsViewModel.UnfilteredTournaments.Count is 0)
            {
                // ok view model
                var theVm = new OkViewModel($"No tournaments played during the current year ({DateTime.Now.Year})", "Current Year");

                // create/show ok window
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.Windows.OfType<TournamentsResultsView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                // exit
                return;
            }

            // get tournaments from the last 24 hours
            var temp = new ObservableCollection<TournamentFinished>(tournamentsResultsViewModel.UnfilteredTournaments.Where(i => i.StartTime.Year == DateTime.Now.Year));

            // no data message
            if (temp is null || temp.Count is 0)
            {
                // ok view model
                var theVm = new OkViewModel($"No tournaments played during the current year - ({DateTime.Now.Year})", "Current Year");

                // create/show ok window
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.Windows.OfType<TournamentsResultsView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                // exit
                return;
            }

            // update tournaments
            tournamentsResultsViewModel.Tournaments = new ObservableCollection<TournamentFinished>(tournamentsResultsViewModel.UnfilteredTournaments.Where(i => i.StartTime.Year == DateTime.Now.Year));

            // filter
            tournamentsResultsViewModel.Filter();

            // update current filter message
            tournamentsResultsViewModel.CurrentFilter = $"Current Year - ({DateTime.Now.Year})";

            // update title text
            tournamentsResultsViewModel.FilterName = $"Current Year - ({DateTime.Now.Year})";
        }

        /// <summary>
        /// tournaments for the yesterday
        /// </summary>
        private static void Yesterday()
        {
            // find tournament results window
            if (Application.Current.Windows.OfType<TournamentsResultsView>()?.FirstOrDefault()?.DataContext is not TournamentsResultsViewModel tournamentsResultsViewModel)
                return;

            // no tournaments
            if (tournamentsResultsViewModel.UnfilteredTournaments is null || tournamentsResultsViewModel.UnfilteredTournaments.Count is 0)
            {
                // ok view model
                var theVm = new OkViewModel($"No tournaments were played yesterday - ({DateTime.Now.Date.AddDays(-1).ToShortDateString()})", "Yesterday");

                // create/show ok window
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.Windows.OfType<TournamentsResultsView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                // exit
                return;
            }

            // get tournaments from yesterday
            var temp = new ObservableCollection<TournamentFinished>(tournamentsResultsViewModel.UnfilteredTournaments.Where(i => i.StartTime.Date == DateTime.Now.Date.AddDays(-1)));

            // no data message
            if (temp is null || temp.Count is 0)
            {
                // ok view model
                var theVm = new OkViewModel($"No tournaments were played yesterday - ({DateTime.Now.Date.AddDays(-1).ToShortDateString()})", "Yesterday");

                // create/show ok window
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.Windows.OfType<TournamentsResultsView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                // exit
                return;
            }

            // update tournaments
            tournamentsResultsViewModel.Tournaments = new ObservableCollection<TournamentFinished>(temp);

            // filter
            tournamentsResultsViewModel.Filter();

            // update current filter message
            tournamentsResultsViewModel.CurrentFilter = $"Yesterday - ({DateTime.Now.Date.AddDays(-1).ToShortDateString()}";

            // update title text
            tournamentsResultsViewModel.FilterName = $"Yesterday - ({DateTime.Now.Date.AddDays(-1).ToShortDateString()})";
        }
    }
}