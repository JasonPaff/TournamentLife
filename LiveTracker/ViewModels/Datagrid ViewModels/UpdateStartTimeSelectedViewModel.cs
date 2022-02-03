using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Syncfusion.UI.Xaml.Utility;
using Syncfusion.Windows.Shared;
using Tournament_Life.Helpers;
using Tournament_Life.Models.Tournaments;
using Tournament_Life.Views;
using Tournament_Life.Views.Datagrid_Views;

namespace Tournament_Life.ViewModels.Datagrid_ViewModels
{
    public class UpdateStartTimeSelectedViewModel : NotificationObject
    {
        private readonly TournamentRunning _tournament;

        public ICommand SaveCommand => new BaseCommand(Save);
        public ICommand CancelCommand => new BaseCommand(Cancel);

        public UpdateStartTimeSelectedViewModel(TournamentRunning tournament)
        {
            // null check
            if(tournament is null) return;

            // copy tournament
            _tournament = tournament;

            // starting start time
            StartTime = DateTime.Now;
            StartDate = DateTime.Now;

            // font size
            FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));

            // visual theme
            Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");
        }

        public bool Saved { get; set; } = false;
        public int FontSize { get; set; }
        public string Theme { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime NewTime { get; set;}

        /// <summary>
        /// Cancel and close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Cancel(object parameter)
        {
            // reset start time
            if (!Saved) StartTime = _tournament.StartTime;

            // find window
            var window = Application.Current.Windows.OfType<UpdateStartTimeSelectedView>().FirstOrDefault();

            // close window
            window?.Close();
        }

        /// <summary>
        /// Save and close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Save(object parameter)
        {
            // create new datetime from date and time, remove seconds
            NewTime = new DateTime(StartDate.Year, StartDate.Month, StartDate.Day, StartTime.Hour, StartTime.Minute, StartTime.Second);

            //  same date message
            var vm = new YesNoViewModel("Are you sure you want to update the starting time for\n\n" + _tournament.TournamentName + " to " + NewTime.ToShortTimeString() + "?", "Update Starting Time");

            // different date message
            if (_tournament.StartTime.Date != NewTime.Date) vm = new YesNoViewModel("Are you sure you want to update the starting time for\n"+ _tournament.TournamentName + " to " + NewTime.ToShortDateString() + " " + NewTime.ToShortTimeString() + "?", "Update Starting Time");

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

            // close window
            Cancel(null);
        }
    }
}
