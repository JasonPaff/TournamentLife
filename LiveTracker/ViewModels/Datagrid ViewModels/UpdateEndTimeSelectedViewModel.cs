using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using LiveTracker.Helpers;
using LiveTracker.Models;
using LiveTracker.Views.Datagrid_Views;
using Syncfusion.UI.Xaml.Utility;
using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels;
using Tournament_Life.Views;

namespace LiveTracker.ViewModels.Datagrid_ViewModels
{
    public class UpdateEndTimeSelectedViewModel : NotificationObject
    {
        private readonly TournamentRunning _tournament;
        public ICommand SaveCommand => new BaseCommand(Save);
        public ICommand CancelCommand => new BaseCommand(Cancel);

        public UpdateEndTimeSelectedViewModel(TournamentRunning tournament)
        {
            // null check
            if(tournament is null) return;

            // copy tournament
            _tournament = tournament;

            // starting end time
            EndTime = DateTime.Now;
            EndDate = DateTime.Now;

            // font size
            FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));

            // visual theme
            Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");
        }

        public DateTime EndTime { get; set; }
        public DateTime EndDate { get; set; }
        public int FontSize { get; set;}
        public DateTime NewTime { get; set; }
        public bool Saved { get; set; }
        public string Theme { get; set;}

        /// <summary>
        /// Cancel and close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Cancel(object parameter)
        {
            // reset end time
            if (!Saved) EndTime = _tournament.EndTime;

            // find window
            var window = Application.Current.Windows.OfType<UpdateEndTimeSelectedView>().FirstOrDefault();

            // close window
            window?.Close();
        }

        /// <summary>
        /// Save and close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Save(object parameter)
        {
            // create new datetime from time and date, remove seconds
            NewTime = new DateTime(EndDate.Year, EndDate.Month, EndDate.Day, EndTime.Hour, EndTime.Minute, EndTime.Second);

            //  same date message
            var vm = new YesNoViewModel("Are you sure you want to update the ending time for\n" + _tournament.TournamentName + " to " + NewTime.ToShortTimeString() + "?", "Update Ending Time");

            // different date message
            if (_tournament.EndTime.Date != _tournament.StartTime.Date) vm = new YesNoViewModel("Are you sure you want to update the ending time for\n" + _tournament.TournamentName + " to " + NewTime.ToShortDateString() + " " + NewTime.ToShortTimeString() + "?", "Update Ending Time");

            var window = new YesNoView(vm)
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            window.ShowDialog();

            // didn't save end time
            if (vm.Saved is false) return;

            // saved
            Saved = true;

            // find window
            var win = Application.Current.Windows.OfType<UpdateEndTimeSelectedView>().FirstOrDefault();

            // close window
            win?.Close();
        }
    }
}
