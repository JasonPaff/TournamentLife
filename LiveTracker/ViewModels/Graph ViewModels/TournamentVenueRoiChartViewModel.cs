using Syncfusion.UI.Xaml.Utility;
using System.Linq;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Windows;
using Tournament_Life.Views.Graph_Views;
using Tournament_Life.Models.Graph;
using Microsoft.Win32;
using Syncfusion.Windows.Shared;
using System;
using Tournament_Life.Helpers;
using Tournament_Life.Models.Tournaments;
using Tournament_Life.Views;

namespace Tournament_Life.ViewModels.Graph_ViewModels
{
    public class TournamentVenueRoiChartViewModel : NotificationObject
    {
        private ObservableCollection<TournamentFinished> _tournaments;

        public TournamentVenueRoiChartViewModel(ObservableCollection<TournamentFinished> tournaments)
        {
            // font color
            FontColor = PreferencesHelper.FindPreference("LiveTracker", "Window", "FontColor");

            // font size
            FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));

            // visual theme
            Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");

            // copy tournaments
            _tournaments = new ObservableCollection<TournamentFinished>(tournaments);

            // set Y axis label format
            YAxisLabelFormat = ".##%";

            // set minimum tournaments for display to "1"
            MinimumTournaments = "1";

            // create the histogram
            CreateGraph();
        }

        public ICommand CloseCommand => new BaseCommand(Cancel);
        public ICommand FilterCommand => new BaseCommand(Filter);
        public ICommand SaveImageCommand => new BaseCommand(SaveImage);
        public string ChartHeader { get; set; }
        public string FontColor { get; set; }
        public int FontSize { get; set; }
        public ObservableCollection<string> Venues { get; set; }
        public string MinimumTournaments { get; set; }
        public string Theme { get; set; }
        public bool ThisChart { get; set; }
        public ObservableCollection<TournamentVenueRoiChartModel> Tournaments { get; set; }
        public string YAxisLabelFormat { get; set; }
        public string XAxisLabelFormat { get; set; }

        /// <summary>
        /// close window
        /// </summary>
        /// <param name="parameter"></param>
        private void Cancel(object parameter)
        {
            // flag this window
            ThisChart = true;

            // get all session profit graphs
            var windows = Application.Current.Windows.OfType<TournamentVenueRoiChartView>();

            // loop through all graphs
            foreach (var window in windows)
            {
                // get view model
                var vm = window.DataContext as TournamentVenueRoiChartViewModel;

                // null check
                if (vm is null) continue;

                // close if its this one
                if (vm.ThisChart) { window.Close(); break; }
            }
        }

        /// <summary>
        /// create the venue/roi histogram
        /// </summary>
        private void CreateGraph()
        {
            Tournaments = new ObservableCollection<TournamentVenueRoiChartModel>();
            Venues = new ObservableCollection<string>();

            // get distinct venues
            var venues = _tournaments.Select(i => i.Venue).Distinct().OrderBy(i => i);

            // loop through each venue
            foreach (var venue in venues)
            {
                // skip blank entries
                if (venue.Trim() == "") continue;

                // chart data point
                var item = new TournamentVenueRoiChartModel();

                // set venue
                item.Venue = venue;

                // profit at this venue
                var profit = _tournaments.Where(i => i.Venue == venue).Sum(i => i.Profit);

                // total cost at this venue
                var totalCost = _tournaments.Where(i => i.Venue == venue).Sum(i => i.TotalCost);

                // total tournaments at this venue
                item.Count = _tournaments.Count(i => i.Venue == venue);

                // zero check and calculate roi
                if (profit is 0 || totalCost is 0) item.Roi = 0;
                else item.Roi = (profit / totalCost);

                // get in the money % for buyin
                item.Itm = _tournaments.Any(i => i.Venue == venue && i.FinishPosition <= i.EntrantsPaid && i.PrizeWon > 0) ? _tournaments.Count(i => i.Venue == venue && i.FinishPosition <= i.EntrantsPaid && i.PrizeWon > 0) / (decimal)_tournaments.Count(i => i.Venue == venue) : 0;

                // get $ per tournament for buyin
                if (profit is 0 || item.Count is 0) item.ProfitPerTournament = 0; else item.ProfitPerTournament = profit / item.Count;

                // get total wins for buyin
                item.TotalWins = _tournaments.Any(i => i.Venue == venue && i.FinishPosition is 1) ? _tournaments.Count(i => i.Venue == venue && i.FinishPosition is 1) : 0;

                // get total final tables for buyin
                item.FinalTables = _tournaments.Any(i => i.Venue == venue && i.FinishPosition > 0 && i.FinishPosition <= i.TableSize) ? _tournaments.Count(i => i.Venue == venue && i.FinishPosition > 0 && i.FinishPosition <= i.TableSize) : 0;

                // get tournament winning percentage for buyin
                if (item.TotalWins is 0 || item.Count is 0) item.WinPercent = 0; else item.WinPercent = item.TotalWins / (decimal)item.Count;

                // get final table win percentage for buyin
                if (item.FinalTables is 0 || item.TotalWins is 0) item.FinalTableWinPercent = 0; else item.FinalTableWinPercent = item.TotalWins / (decimal)item.FinalTables;

                // get make final table percentage for buyin
                if (item.FinalTables is 0 || item.Count is 0) item.FinalTablePercent = 0; else item.FinalTablePercent = item.FinalTables / (decimal)item.Count;

                // get most entrants for buyin
                item.MostEntrants = _tournaments.Any(i => i.Venue == venue && i.Entrants > 0) ? _tournaments.Where(i => i.Venue == venue && i.FinishPosition > 0).Max(i => i.Entrants) : 0;

                // get least entrants for buyin
                item.LeastEntrants = _tournaments.Any(i => i.Venue == venue && i.Entrants > 0) ? _tournaments.Where(i => i.Venue == venue && i.Entrants > 0).Min(i => i.Entrants) : 0;

                // get average entrants for buyin
                item.AverageEntrants = _tournaments.Any(i => i.Venue == venue && i.Entrants > 0) ? (int)_tournaments.Where(i => i.Venue == venue && i.Entrants > 0).Average(i => i.Entrants) : 0;

                // get best finish position for buyin
                item.BestFinish = _tournaments.Any(i => i.Venue == venue && i.FinishPosition > 0) ? _tournaments.Where(i => i.Venue == venue && i.FinishPosition > 0).Min(i => i.FinishPosition) : 0;

                // get best finish position for buyin
                item.WorstFinish = _tournaments.Any(i => i.Venue == venue && i.FinishPosition > 0) ? _tournaments.Where(i => i.Venue == venue && i.FinishPosition > 0).Max(i => i.FinishPosition) : 0;

                // get average finish position for buyin
                item.AverageFinish = _tournaments.Any(i => i.Venue == venue && i.FinishPosition > 0) ? (int)_tournaments.Where(i => i.Venue == venue && i.FinishPosition > 0).Average(i => i.FinishPosition) : 0;

                // get stone bubbles for buyin
                var stoneBubble = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "StoneBubble"));
                item.StoneBubbles = _tournaments.Any(i => i.Venue == venue && i.FinishPosition == i.EntrantsPaid + stoneBubble) ? _tournaments.Count(i => i.Venue == venue && i.FinishPosition == i.EntrantsPaid + stoneBubble) : 0;

                // get final table bubbles for buyin
                item.FinalTableBubbles = _tournaments.Any(i => i.Venue == venue && i.FinishPosition == (i.TableSize + 1) && i.FinishPosition > 0) ? _tournaments.Count(i => i.Venue == venue && i.FinishPosition == (i.TableSize + 1) && i.FinishPosition > 0) : 0;

                // get average % beaten for buyin
                item.AverageBeaten = _tournaments.Any(i => i.Venue == venue) ? (_tournaments.Where(i => i.Venue == venue).Sum(i => i.PercentFieldBeaten * 100) / item.Count) / 100 : 0;

                // get late finish % for buyin
                item.LateFinish = _tournaments.Any(i => i.PercentFieldBeaten * 100 >= 90 && i.PercentFieldBeaten * 100 < 100 && i.Venue == venue) && item.Count > 0 ? _tournaments.Count(i => i.PercentFieldBeaten * 100 >= 90 && i.PercentFieldBeaten * 100 < 100 && i.Venue == venue) / (decimal)item.Count : 0;

                // get mid/late finish % for buyin
                item.MidLateFinish = _tournaments.Any(i => i.PercentFieldBeaten * 100 >= 70 && i.PercentFieldBeaten * 100 < 90 &&  i.Venue == venue) && item.Count > 0 ? _tournaments.Count(i => i.PercentFieldBeaten * 100 >= 70 && i.PercentFieldBeaten * 100 < 90 &&  i.Venue == venue) / (decimal)item.Count : 0;

                // get mid finish % for buyin
                item.MidFinish = _tournaments.Any(i => i.PercentFieldBeaten * 100 >= 30 && i.PercentFieldBeaten * 100 < 70 &&  i.Venue == venue) && item.Count > 0 ? _tournaments.Count(i => i.PercentFieldBeaten * 100 >= 30 && i.PercentFieldBeaten * 100 < 70 &&  i.Venue == venue) / (decimal)item.Count : 0;

                // get early/late finish % for buyin
                item.EarlyMidFinish = _tournaments.Any(i => i.PercentFieldBeaten * 100 >= 10 && i.PercentFieldBeaten * 100 < 30 &&  i.Venue == venue) && item.Count > 0 ? _tournaments.Count(i => i.PercentFieldBeaten * 100 >= 10 && i.PercentFieldBeaten * 100 < 30 && i.Venue == venue) / (decimal)item.Count : 0;

                // get early finish % for buyin
                item.EarlyFinish = _tournaments.Any(i => i.PercentFieldBeaten * 100 >= 0 && i.PercentFieldBeaten * 100 < 10 &&  i.Venue == venue) && item.Count > 0 ? _tournaments.Count(i => i.PercentFieldBeaten * 100 >= 0 && i.PercentFieldBeaten * 100 < 10 &&  i.Venue == venue) / (decimal)item.Count : 0;

                // get longest tournament for buyin
                item.LongestTournament = _tournaments.Any(i => i.Venue == venue) ? _tournaments.Where(i => i.Venue == venue).Max(i => i.Length) : TimeSpan.Zero;

                // get shortest tournament for buyin
                item.ShortestTournament = _tournaments.Any(i => i.Venue == venue) ? _tournaments.Where(i => i.Venue == venue).Min(i => i.Length) : TimeSpan.Zero;

                // get average tournament for buyin
                item.AverageTournament = _tournaments.Any(i => i.Venue == venue) ? TimeSpan.FromMinutes(_tournaments.Where(i => i.Venue == venue).Select(i => i.Length).Average(timespan => timespan.TotalMinutes)) : TimeSpan.Zero;

                // remove seconds from average tournament
                item.AverageTournament = new TimeSpan(item.AverageTournament.Hours, item.AverageTournament.Minutes, 0);

                // get latest date for buyin
                item.LastDate = _tournaments.Any(i => i.Venue == venue) ? _tournaments.Where(i => i.Venue == venue).Max(i => i.StartTime) : DateTime.MaxValue;

                // get earliest date for buyin
                item.FirstDate = _tournaments.Any(i => i.Venue == venue) ? _tournaments.Where(i => i.Venue == venue).Min(i => i.StartTime) : DateTime.MinValue;

                // create plot point description
                item.Description = "Venue: " + item.Venue + "\nPlayed: " + item.Count + "\n\nRoi: " + item.Roi.ToString("P2") + "\nItm: " + item.Itm.ToString("P2") + "\n$/Tournament: " + item.ProfitPerTournament.ToString("C2")
                                 + "\n\nTotal Cost: " + totalCost.ToString("C2") + "\nTotal Prizes: " + item.TotalPrizes.ToString("C2") + "\nTotal Profit: " + profit.ToString("C2") + "\n\nTotal Wins: " + item.TotalWins + "\nWin Tournament: " + item.WinPercent.ToString("P2") + "\nStone Bubbles: " + item.StoneBubbles
                                 + "\n\nFinal Tables: " + item.FinalTables + "\nMake Final Table: " + item.FinalTablePercent.ToString("P2") + "\nWin Final Table: " + item.FinalTableWinPercent.ToString("P2") + "\nFinal Table Bubbles: " + item.FinalTableBubbles + "\n\nMost Entrants: " + item.MostEntrants + "\nLeast Entrants: "
                                 + item.LeastEntrants + "\nAverage Entrants: " + item.AverageEntrants + "\n\nBest Finish: " + item.BestFinish + "\nWorst Finish: " + item.WorstFinish + "\nAverage Finish: " + item.AverageFinish + "\n\nAverage % Beaten: " + item.AverageBeaten.ToString("P2") + "\nEarly Finish: " + item.EarlyFinish.ToString("P2")
                                 + "\nEarly/Mid Finish: " + item.EarlyMidFinish.ToString("P2") + "\nMid Finish: " + item.MidFinish.ToString("P2") + "\nMid/Late Finish: " + item.MidLateFinish.ToString("P2") + "\nLate Finish: " + item.LateFinish.ToString("P2") + "\n\nLongest Tournament: " + item.LongestTournament
                                 + "\nShortest Tournament: " + item.ShortestTournament + "\nAverage Tournament: " + item.AverageTournament + "\n\nFirst Game: " + item.FirstDate + "\nLast Game: " + item.LastDate;

                // add chart data point to chart data point list
                if (item.Count >= int.Parse(MinimumTournaments)) Tournaments.Add(item);
            }
        }

        /// <summary>
        /// filter tournaments graph
        /// </summary>
        /// <param name="parameter"></param>
        private void Filter(object parameter)
        {
            CreateGraph();
        }

        /// <summary>
        /// Save image of graph
        /// </summary>
        /// <param name="parameter"></param>
        private void SaveImage(object parameter)
        {
            // get all tournament profit graphs
            var windows = Application.Current.Windows.OfType<TournamentVenueRoiChartView>();

            // hold our view model when we find it
            TournamentVenueRoiChartView theVM = null;

            // loop through all graphs
            foreach (var window in windows)
            {
                // get view model
                var vm = window.DataContext as TournamentVenueRoiChartViewModel;

                // flag this chart
                ThisChart = true;

                // store window and remove flag from this chart
                if (vm.ThisChart) { theVM = window; ThisChart = false; break; }
            }

            // get file name
            var sfd = new SaveFileDialog() { Filter = "Bitmap(*.bmp)|*.bmp|JPEG(*.jpg,*.jpeg)|*.jpg;*.jpeg|Gif (*.gif)|*.gif|PNG(*.png)|*.png|TIFF(*.tif,*.tiff)|*.tif|All files (*.*)|*.*" };
            sfd.ShowDialog();

            if (sfd.FileName.Length is 0) return;

            // save image
            theVM.chart.Save(sfd.FileName);

            // ok view model
            var theVm = new OkViewModel("Image saved successfully", "Saved Successfully");

            // create/show ok window
            var theWindow = new OkView(theVm)
            {
                Owner = Application.Current.Windows.OfType<TournamentVenueRoiChartView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            theWindow.ShowDialog();
        }
    }
}