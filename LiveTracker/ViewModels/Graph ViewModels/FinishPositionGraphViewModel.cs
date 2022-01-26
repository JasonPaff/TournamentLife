using System.Collections.ObjectModel;
using System.Linq;
using LiveTracker.Helpers;
using LiveTracker.Models;
using LiveTracker.Models.Graph;
using Syncfusion.UI.Xaml.Charts;
using Syncfusion.Windows.Shared;
using System.Windows.Input;
using Syncfusion.UI.Xaml.Utility;
using System.Windows;
using LiveTracker.Views.Graph_Views;
using Microsoft.Win32;
using Tournament_Life.Views;
using Tournament_Life.ViewModels;

namespace LiveTracker.ViewModels.Graph_ViewModels
{
    public class FinishPositionGraphViewModel : NotificationObject
    {
        private bool _sessionFlag;
        private SessionModel _session;
        private ObservableCollection<TournamentFinished> _tournaments;
        public ICommand CloseCommand => new BaseCommand(Cancel);
        public ICommand FilterCommand => new BaseCommand(Filter);
        public ICommand SaveImageCommand => new BaseCommand(SaveImage);

        public FinishPositionGraphViewModel(SessionModel session)
        {
            // font color
            FontColor = PreferencesHelper.FindPreference("LiveTracker", "Window", "FontColor");

            // font size
            FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));

            // visual theme
            Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");

            // session we are graphing data from
            _session = session;

            _sessionFlag = true;

            // set max
            Max = "27";

            // create chart
            CreateSessionChart();
        }

        public FinishPositionGraphViewModel(ObservableCollection<TournamentFinished> tournaments)
        {
            // font color
            FontColor = PreferencesHelper.FindPreference("LiveTracker", "Window", "FontColor");

            // font size
            FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));

            // visual theme
            Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");

            // session we are graphing data from
            _tournaments = tournaments;

            // set max
            Max = "27";

            // create chart
            CreateTournamentChart();
        }

        public string ChartHeader { get; set; }
        public string FontColor { get; set; }
        public int FontSize { get; set; }
        public string Max { get; set; }
        public string Palette { get; set; }
        public string Theme { get; set; }
        public bool ThisChart { get; set; }
        public ObservableCollection<FinishPositionGraphModel> Positions { get; set; }

        /// <summary>
        /// Close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Cancel(object parameter)
        {
            // flag this window
            ThisChart = true;

            // get all tournament profit graphs
            var windows = Application.Current.Windows.OfType<FinishPositionGraphView>();

            // loop through all graphs
            foreach (var window in windows)
            {
                // get view model
                var vm = window.DataContext as FinishPositionGraphViewModel;

                // close if its this one
                if (vm.ThisChart) { window.Close(); break;}
            }
        }

        /// <summary>
        /// Create the chart
        /// </summary>
        private void CreateSessionChart()
        {
            Positions = new ObservableCollection<FinishPositionGraphModel>();

            var finishPositionGroups = _session.Tournaments.GroupBy(i => i.FinishPosition);

            foreach (var group in finishPositionGroups)
            {
                var item = new FinishPositionGraphModel()
                {
                    Description = "Position: " + group.Key + "\n" + "Times Finished: " + group.Count(),
                    FinishPosition = group.Key,
                    FinishPositionCount = group.Count()
                };

                if(item.FinishPosition <= int.Parse(Max)) Positions.Add(item);
            }

            ChartHeader = "Tournament Finish Position Graph for Tournaments from " + _session.StartTime.ToShortDateString() + " " + _session.StartTime.ToShortTimeString() + " to " + _session.EndTime.ToShortDateString() + " " + _session.EndTime.ToShortTimeString();
        }

        /// <summary>
        /// Create the chart
        /// </summary>
        private void CreateTournamentChart()
        {
            Positions = new ObservableCollection<FinishPositionGraphModel>();

            var finishPositionGroups = _tournaments.GroupBy(i => i.FinishPosition);

            foreach (var group in finishPositionGroups)
            {
                var item = new FinishPositionGraphModel()
                {
                    Description = "Position: " + group.Key + "\n" + "Times Finished: " + group.Count(),
                    FinishPosition = group.Key,
                    FinishPositionCount = group.Count()
                };

                if(item.FinishPosition <= int.Parse(Max)) Positions.Add(item);
            }

            ChartHeader = "Tournament Finish Position Graph for Tournaments from " + _tournaments.Min(i => i.StartTime).ToShortDateString() + " " + _tournaments.Min(i => i.StartTime).ToShortTimeString() + " to " + _tournaments.Max(i => i.StartTime).ToShortDateString() + " " + _tournaments.Max(i => i.StartTime).ToShortTimeString();

        }

        /// <summary>
        /// filter tournaments graph
        /// </summary>
        /// <param name="parameter"></param>
        private void Filter(object parameter)
        {
            if(_sessionFlag) CreateSessionChart(); else CreateTournamentChart();
        }

        /// <summary>
        /// Save the graph as an image
        /// </summary>
        /// <param name="parameter"></param>
        private void SaveImage(object parameter)
        {
            // get all tournament profit graphs
            var windows = Application.Current.Windows.OfType<FinishPositionGraphView>();

            // hold our view model when we find it
            FinishPositionGraphView theVM = null;

            // loop through all graphs
            foreach (var window in windows)
            {
                // get view model
                var vm = window.DataContext as FinishPositionGraphViewModel;

                // flag this chart
                ThisChart = true;

                // store window and remove flag from this chart
                if (vm.ThisChart) { theVM = window; ThisChart = false; break;}
            }

            // get file name
            var sfd = new SaveFileDialog(){ Filter = "Bitmap(*.bmp)|*.bmp|JPEG(*.jpg,*.jpeg)|*.jpg;*.jpeg|Gif (*.gif)|*.gif|PNG(*.png)|*.png|TIFF(*.tif,*.tiff)|*.tif|All files (*.*)|*.*" };
            sfd.ShowDialog();

            if (sfd.FileName.Length is 0) return;

            // save image
            theVM.chart.Save(sfd.FileName);

            // ok view model
            var theVm = new OkViewModel("Image saved successfully", "Saved Successfully");

            // create/show ok window
            var theWindow = new OkView(theVm)
            {
                Owner = Application.Current.Windows.OfType<FinishPositionGraphView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            theWindow.ShowDialog();
        }
    }
}
