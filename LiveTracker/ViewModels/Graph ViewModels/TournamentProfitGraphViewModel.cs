using System.Collections.ObjectModel;
using System.Linq;
using LiveTracker.Helpers;
using LiveTracker.Models;
using LiveTracker.Models.Graph;
using Syncfusion.UI.Xaml.Charts;
using Syncfusion.Windows.Shared;
using Syncfusion.UI.Xaml.Utility;
using System.Windows;
using System.Windows.Input;
using LiveTracker.Views.Graph_Views;
using Microsoft.Win32;
using Tournament_Life.ViewModels;
using Tournament_Life.Views;

namespace LiveTracker.ViewModels.Graph_ViewModels
{
    public class TournamentProfitGraphViewModel : NotificationObject
    {
        private string _filter;
        private SessionModel _session;
        private ObservableCollection<TournamentFinished> _tournaments;
        public ICommand CloseCommand => new BaseCommand(Cancel);
        public ICommand SaveImageCommand => new BaseCommand(SaveImage);
        public ICommand GraphInfoCommand => new BaseCommand(ShowGraphInfo);

        public TournamentProfitGraphViewModel(SessionModel session)
        {
            // font color
            FontColor = PreferencesHelper.FindPreference("LiveTracker", "Window", "FontColor");

            // font size
            FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));

            // visual theme
            Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");

            // session we are graphing data from
            _session = session;

            // set filter
            _filter = "Session from " + _session.StartTime.ToString() + " to " + _session.EndTime.ToString();

            // set label currency symbol
            if (bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "ShowSweeps"))) YAxisLabelFormat = "SC ##.00"; else YAxisLabelFormat = "$##.00";

            // set title
            Title = "Session Profit";

            // create chart
            CreateSessionChart();
        }

        public TournamentProfitGraphViewModel(ObservableCollection<TournamentFinished> tournaments, string filter)
        {
            // font color
            FontColor = PreferencesHelper.FindPreference("LiveTracker", "Window", "FontColor");

            // font size
            FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));

            // visual theme
            Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");

            // tournaments we are graphing data from
            _tournaments = tournaments;

            // filter used to create the graph
            _filter = filter;

            // correct for no filter default filter
            if (_filter == "No filter in use" || _filter.Trim().Length == 0) _filter = "All Results";

            // set label currency symbol
            if (bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "ShowSweeps"))) YAxisLabelFormat = "SC ##.00"; else YAxisLabelFormat = "$##.00";

            // set title
            Title = "Tournament Profit";

            // create chart
            CreateTournamentChart();
        }

        public string FontColor { get; set; }
        public int FontSize { get; set; }
        public string Palette { get; set; }
        public bool ShowMarkers { get; set; }
        public string Theme { get; set; }
        public bool ThisChart { get; set; }
        public string Title { get; set; }
        public string YAxisLabelFormat { get; set; }
        public ObservableCollection<TournamentProfitGraphModel> Tournaments { get; set; }

        /// <summary>
        /// Close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Cancel(object parameter)
        {
            // get window and close it
            GetWindow()?.Close();
        }

        /// <summary>
        /// Create the chart
        /// </summary>
        private void CreateSessionChart()
        {
            Tournaments = new ObservableCollection<TournamentProfitGraphModel>();

            //filler tournament to start at 0
            var filler = new TournamentFinished();
            filler.BuyinBaseCost = 10;
            filler.BuyinRakeCost = 1;
            filler.PrizeWon = 11;
            filler.StartTime = _session.Tournaments[0].StartTime;
            _session.Tournaments.Insert(0, filler);

            var c = 0;
            var profitTotal = 0.0;

            foreach (var tournament in _session.Tournaments)
            {
                var item = new TournamentProfitGraphModel()
                {
                    Count = c++,
                    Description = tournament.GraphDescription,
                    Profit = profitTotal + (double)tournament.Profit
                };

                Tournaments.Add(item);

                profitTotal += (double)tournament.Profit;
            }

            // set filler description
            Tournaments[0].Description = "Filler tournament for starting the graph at 0";

            // remove filler after creating graph lines
            _session.Tournaments.RemoveAt(0);

            // graph line colors based on profit
            if (_session.TotalProfit > 0 ) Palette="GreenChrome";
            if (_session.TotalProfit < 0) Palette="RedChrome";
            if (_session.TotalProfit is 0) Palette="Metro";
        }

        /// <summary>
        /// Create the chart
        /// </summary>
        private void CreateTournamentChart()
        {
            Tournaments = new ObservableCollection<TournamentProfitGraphModel>();

            //filler tournament to start at 0
            var filler = new TournamentFinished();
            filler.BuyinBaseCost = 10;
            filler.BuyinRakeCost = 1;
            filler.PrizeWon = 11;
            filler.StartTime = _tournaments[0].StartTime;
            _tournaments.Insert(0, filler);

            var c = 0;
            var profitTotal = 0.0;

            foreach (var tournament in _tournaments)
            {
                var item = new TournamentProfitGraphModel()
                {
                    Count = c++,
                    Description = tournament.GraphDescription,
                    Profit = profitTotal + (double)tournament.Profit
                };

                Tournaments.Add(item);

                profitTotal += (double)tournament.Profit;
            }

            //set filler description
            Tournaments[0].Description = "Filler tournament for starting the graph at 0";

            // remove filler tournament after creating graph lines
            _tournaments.RemoveAt(0);

            // graph line colors based on profit
            if (_tournaments.Sum(i => i.Profit) > 0) Palette = "GreenChrome";
            if (_tournaments.Sum(i => i.Profit) < 0) Palette = "RedChrome";
            if (_tournaments.Sum(i => i.Profit) is 0) Palette = "Metro";
        }

        /// <summary>
        /// gets this graphs window view
        /// </summary>
        private TournamentProfitGraphView GetWindow()
        {
            // holds our window
            TournamentProfitGraphView view = null;

            // get all tournament profit graphs
            var windows = Application.Current.Windows.OfType<TournamentProfitGraphView>();


            // loop through all graphs
            foreach (var window in windows)
            {
                // get view model of window
                var vm = window.DataContext as TournamentProfitGraphViewModel;

                // flag this chart in view model
                ThisChart = true;

                // store window with flag and remove flag from this chart
                if (vm.ThisChart) { view = window; ThisChart = false; break; }
            }

            // return this window
            return view;
        }

        /// <summary>
        /// Save the graph as an image
        /// </summary>
        /// <param name="parameter"></param>
        private void SaveImage(object parameter)
        {
            // hold our view model when we find it
            var theVM = GetWindow();

            // no window
            if (theVM is null) return;

            // get file name
            var sfd = new SaveFileDialog(){ Filter = "Bitmap(*.bmp)|*.bmp|JPEG(*.jpg,*.jpeg)|*.jpg;*.jpeg|Gif (*.gif)|*.gif|PNG(*.png)|*.png|TIFF(*.tif,*.tiff)|*.tif|All files (*.*)|*.*" };
            sfd.ShowDialog();

            if(sfd.FileName.Length is 0) return;

            // save image
            theVM.chart.Save(sfd.FileName);

            // ok view model
            var theVm = new OkViewModel(sfd.FileName + " was saved successfully", "Saved Successfully");

            // create/show ok window
            var theWindow = new OkView(theVm)
            {
                Owner = theVM,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            theWindow.ShowDialog();
        }

        /// <summary>
        /// show the graphs info
        /// </summary>
        /// <param name="parameter"></param>
        private void ShowGraphInfo(object parameter)
        {
            // filter in use
            var message = _filter;

            // ok view model
            var theVm = new OkViewModel(message, "Graph Filter");

            // create/show ok window
            var theWindow = new OkView(theVm)
            {
                Owner = GetWindow(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            theWindow.ShowDialog();
        }
    }
}
