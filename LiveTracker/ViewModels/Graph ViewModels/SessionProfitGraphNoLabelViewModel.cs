using Microsoft.Win32;
using Syncfusion.UI.Xaml.Utility;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Tournament_Life.Models.Graph;
using Tournament_Life.Views.Graph_Views;
using Syncfusion.Windows.Shared;
using Tournament_Life.Helpers;
using Tournament_Life.Models.Sessions;
using Tournament_Life.Models.Tournaments;
using Tournament_Life.Views;

namespace Tournament_Life.ViewModels.Graph_ViewModels
{
    public class SessionProfitGraphNoLabelViewModel : NotificationObject
    {
        public SessionProfitGraphNoLabelViewModel(ObservableCollection<SessionModel> sessions)
        {
            // font color
            FontColor = PreferencesHelper.FindPreference("LiveTracker", "Window", "FontColor");

            // font size
            FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));

            // visual theme
            Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");

            // copy sessions
            Sessions = new ObservableCollection<SessionModel>(sessions.OrderBy(i => i.StartTime));

            // set label currency symbol
            if (bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "ShowSweeps"))) YAxisLabelFormat = "SC ##.00"; else YAxisLabelFormat = "$##.00";

            // create chart
            CreateChart();
        }

        public ICommand CloseCommand => new BaseCommand(Cancel);
        public ICommand SaveImageCommand => new BaseCommand(SaveImage);
        public ObservableCollection<SessionModel> Sessions { get; set; }
        public ObservableCollection<SessionProfitGraphModel> SessionList { get; set; }
        public string ChartHeader { get; set; }
        public string FontColor { get; set; }
        public int FontSize { get; set; }
        public string InnerCircle { get; set; }
        public string OuterCircle { get; set; }
        public string Palette { get; set; }
        public string Theme { get; set; }
        public bool ThisChart { get; set; }
        public string YAxisLabelFormat { get; set; }

        /// <summary>
        /// Close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Cancel(object parameter)
        {
            // flag this window
            ThisChart = true;

            // get all session profit graphs
            var windows = Application.Current.Windows.OfType<SessionProfitGraphView>();

            // loop through all graphs
            foreach (var window in windows)
            {
                // get view model
                var vm = window.DataContext as SessionProfitGraphViewModel;

                // close if its this one
                if (vm.ThisChart) { window.Close(); break; }
            }
        }

        /// <summary>
        /// Create the chart
        /// </summary>
        private void CreateChart()
        {
            SessionList = new ObservableCollection<SessionProfitGraphModel>();

            var filler = new SessionModel();
            var fillerMtt = new TournamentFinished();

            fillerMtt.BuyinBaseCost = 10;
            fillerMtt.BuyinRakeCost = 1;
            fillerMtt.PrizeWon = 11;
            fillerMtt.StartTime = Sessions[0].StartTime;
            filler.AddTournament(fillerMtt);
            Sessions.Insert(0, filler);

            var c = 0;
            var profitTotal = 0.0;

            foreach (var session in Sessions)
            {
                var item = new SessionProfitGraphModel()
                {
                    Count = c++,
                    Description = session.GraphDescription,
                    Profit = profitTotal + (double)session.TotalProfit
                };

                SessionList.Add(item);

                profitTotal += (double)session.TotalProfit;
            }

            // add filler description
            SessionList[0].Description = "Filler session to start the graph at 0";

            // remove filler
            Sessions.RemoveAt(0);

            // chart header
            ChartHeader = "Sessions Profit Graph - " + Sessions.Count + " sessions played from " + Sessions.Min(i => i.StartTime) + " to " + Sessions.Max(i => i.StartTime);

            // line colors based on profit
            if (Sessions.Sum(i => i.TotalProfit) > 0)
            {
                Palette = "GreenChrome";
                InnerCircle = "White";
                OuterCircle = "Black";
            }
            if (Sessions.Sum(i => i.TotalProfit) < 0)
            {
                Palette = "RedChrome";
                InnerCircle = "White";
                OuterCircle = "Black";
            }
            if (Sessions.Sum(i => i.TotalProfit) is 0)
            {
                Palette = "Metro";
                InnerCircle = "White";
                OuterCircle = "Black";
            }
        }

        /// <summary>
        /// Save the graph as an image
        /// </summary>
        /// <param name="parameter"></param>
        private void SaveImage(object parameter)
        {
            // get all tournament profit graphs
            var windows = Application.Current.Windows.OfType<SessionProfitGraphView>();

            // hold our view model when we find it
            SessionProfitGraphView theVM = null;

            // loop through all graphs
            foreach (var window in windows)
            {
                // get view model
                var vm = window.DataContext as SessionProfitGraphViewModel;

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
            var theVm = new OkViewModel(sfd.FileName + " was saved successfully", "Saved Successfully");

            // create/show ok window
            var theWindow = new OkView(theVm)
            {
                Owner = Application.Current.Windows.OfType<SessionProfitGraphNoLabelView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            theWindow.ShowDialog();
        }
    }
}