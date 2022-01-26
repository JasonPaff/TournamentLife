using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using LiveTracker.Helpers;
using LiveTracker.Models;
using Syncfusion.UI.Xaml.Utility;
using Syncfusion.Windows.Shared;
using Tournament_Life.Views;
using Tournament_Life.Views.Results;

namespace Tournament_Life.ViewModels.Results
{
    public class EditRecordViewModel : NotificationObject
    {
        public EditRecordViewModel(TournamentFinished tournament)
        {
            // null check
            if (tournament is null) return;

            // copy data
            TournamentData = tournament;
            Format = tournament.FormatString;
            EndDate = tournament.EndTime;
            EndTime = tournament.EndTime;
            StartDate = tournament.StartTime;
            StartTime = tournament.StartTime;

            // Bovada check box
            BovadaCheckBox = tournament.IsBovadaBounty;

            // sng check box
            SngCheckBox = tournament.IsSng;

            // font size
            FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));

            // visual theme
            Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");
        }

        public ICommand SaveCommand => new BaseCommand(Save);
        public ICommand CancelCommand => new BaseCommand(Cancel);
        public ICommand CheckBoxToggleCommand => new BaseCommand(CheckBoxToggle);

        public bool BovadaCheckBox { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime EndTime { get; set; }
        public int FontSize { get; set; }
        public string Format { get; set; }
        public bool Saved { get; set; } = false;
        public bool SaveNewTemplateCheckBox { get; set; }
        public bool SngCheckBox { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime StartTime { get; set; }
        public string Theme { get; set; }
        public TournamentFinished TournamentData { get; set; }
        public bool UpdateTemplateCheckBox { get; set; }

        /// <summary>
    /// Cancel any note text and close the window
    /// </summary>
    /// <param name="parameter"></param>
        private void Cancel(object parameter)
        {
            // find window
            var window = Application.Current.Windows.OfType<EditRecordView>().FirstOrDefault();

            // close window
            window?.Close();
        }

        /// <summary>
    /// Toggle check boxes
    /// </summary>
    /// <param name="parameter">check box to toggle</param>
        private void CheckBoxToggle(object parameter)
        {
            if ((string)parameter is "UpdateTemplate") if (UpdateTemplateCheckBox) SaveNewTemplateCheckBox = false;
            else if ((string)parameter is "SaveTemplate") if (SaveNewTemplateCheckBox) UpdateTemplateCheckBox = false;
        }

        /// <summary>
    /// Save any note text and close the window
    /// </summary>
    /// <param name="parameter"></param>
        private void Save(object parameter)
        {
            // yes/no view model
            var vm = new YesNoViewModel("Are you sure you want to save these changes?", "Save Changes");

            // create/show yes/no window
            var window = new YesNoView(vm)
            {
                Owner = Application.Current.Windows.OfType<EditRecordView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            window.ShowDialog();

            // not saved
            if (vm.Saved is false) return;

            // update starting time
            TournamentData.StartTime = new DateTime(StartDate.Year, StartDate.Month, StartDate.Day, StartTime.Hour, StartTime.Minute, 0);

            // update ending time
            TournamentData.EndTime = new DateTime(EndDate.Year, EndDate.Month, EndDate.Day, EndTime.Hour, EndTime.Minute, 0);

            // clear formats
            TournamentData.Formats.Clear();

            // split formats string and add each format
            foreach (var format in (string[])Format.Split(',')) TournamentData.Formats.Add(format);

            // is sng checked
            TournamentData.IsSng = SngCheckBox;

            // is Bovada checked
            TournamentData.IsBovadaBounty = BovadaCheckBox;

            // saved
            Saved = true;

            // close window
            Cancel(null);
        }
    }
}
