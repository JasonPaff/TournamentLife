using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using LiveTracker.Helpers;
using LiveTracker.Models;
using LiveTracker.ViewModels.Menu_ViewModels;
using LiveTracker.ViewModels.Session_Manager_ViewModels;
using LiveTracker.ViewModels.Template_Manager_ViewModels;
using LiveTracker.Views;
using LiveTracker.Views.Datagrid_Views;
using LiveTracker.Views.Menu_Views;
using LiveTracker.Views.Session_Manager_Views;
using LiveTracker.Views.Template_Manager_Views;
using Syncfusion.UI.Xaml.Utility;
using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels;
using Tournament_Life.Views;

namespace LiveTracker.ViewModels.Datagrid_ViewModels
{
    public class ChangeTemplateDataViewModel : NotificationObject
    {
        public ICommand SaveCommand => new BaseCommand(Save);
        public ICommand CancelCommand => new BaseCommand(Cancel);
        public ICommand CheckBoxToggleCommand => new BaseCommand(CheckBoxToggle);

        public ChangeTemplateDataViewModel(TournamentRunning tournament)
        {
            // null check
            if (tournament is null) return;

            TournamentData = tournament;
            Format = tournament.FormatString;
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

        public bool BovadaCheckBox { get; set; }
        public string Format { get; set; }
        public bool Saved { get; set; } = false;
        public bool SaveNewTemplateCheckBox { get; set; }
        public bool SngCheckBox { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime StartTime { get; set; }
        public TournamentRunning TournamentData { get; set; }
        public bool UpdateTemplateCheckBox { get; set; }
        public int FontSize { get; set; }
        public string Theme { get; set; }

        /// <summary>
        /// Cancel any note text and close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Cancel(object parameter)
        {
            // find window
            var window = Application.Current.Windows.OfType<ChangeTemplateDataView>().FirstOrDefault();

            // close window
            window?.Close();
        }

        /// <summary>
        /// Toggle check boxes
        /// </summary>
        /// <param name="parameter">check box to toggle</param>
        private void CheckBoxToggle(object parameter)
        {
            if ((string)parameter is "UpdateTemplate")
            {
                if (UpdateTemplateCheckBox)
                    SaveNewTemplateCheckBox = false;
            }
            else if ((string)parameter is "SaveTemplate")
            {
                if (SaveNewTemplateCheckBox)
                    UpdateTemplateCheckBox = false;
            }
        }

        /// <summary>
        /// Save any note text and close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Save(object parameter)
        {
            // null check
            if (TournamentData is null || TournamentData.TournamentName is null) return;

            // trim tournament name
            TournamentData.TournamentName = TournamentData.TournamentName.Trim();

            // check for blank name
            if (TournamentData.TournamentName.Length is 0)
            {
                // ok view model
                var theVm = new OkViewModel("The tournament name can't be blank", "Blank Name");

                // create/show ok window
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.Windows.OfType<ChangeTemplateDataView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                // set focus to the name text box
                Application.Current.Windows.OfType<ChangeTemplateDataView>().FirstOrDefault().TournamentNameTextBox.Focus();

                // exit
                return;
            }

            // check for length over 50
            if (TournamentData.TournamentName.Length > 50)
            {
                // ok view model
                var theVm = new OkViewModel("The tournament name can't be over 50 characters", "Name Too Long");

                // create/show ok window
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.Windows.OfType<ChangeTemplateDataView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                // set focus to the name text box
                Application.Current.Windows.OfType<ChangeTemplateDataView>().FirstOrDefault().TournamentNameTextBox.Focus();

                // exit
                return;
            }

            // create/show yes/no window
            var vm = new YesNoViewModel("Are you sure you want to save these changes?", "Save Changes");
            var window = new YesNoView(vm)
            {
                Owner = Application.Current.Windows.OfType<ChangeTemplateDataView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            window.ShowDialog();

            // didn't save, end
            if (vm.Saved is false) return;

            // update starting time
            TournamentData.StartTime = new DateTime(StartDate.Year, StartDate.Month, StartDate.Day, StartTime.Hour, StartTime.Minute, StartTime.Second);

            // clear formats
            TournamentData.Formats.Clear();

            // split formats string and add each format
            foreach (var format in (string[])Format.Split(',')) TournamentData.Formats.Add(format);

            // is sng checked
            TournamentData.IsSng = SngCheckBox;

            // is Bovada checked
            TournamentData.IsBovadaBounty = BovadaCheckBox;

            // saving as a new tournament
            if (SaveNewTemplateCheckBox)
            {
                // get a new template id for new tournaments
                TournamentData.TemplateId = TournamentTemplateHelper.GetNewTemplateId();

                // remove favorite status
                TournamentData.IsFavorite = false;

                // save tournament to file
                TournamentTemplateHelper.SaveTournamentTemplate(TournamentData, false);
            }

            // update template file if checked
            if (UpdateTemplateCheckBox && TournamentData.TemplateId is not -1) TournamentTemplateHelper.SaveTournamentTemplate(TournamentData, true);

            // update template file if checked but its a non template file
            if (UpdateTemplateCheckBox && TournamentData.TemplateId is -1)
            {
                // get a new template id for new tournaments
                TournamentData.TemplateId = TournamentTemplateHelper.GetNewTemplateId();

                // remove favorite status
                TournamentData.IsFavorite = false;

                // save tournament to file
                TournamentTemplateHelper.SaveTournamentTemplate(TournamentData);
            }

            // get data grid view model or quit
            if (Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.TournamentsDataGrid.DataContext is not DataGridViewModel dataGridViewModel) return;

            // get menu view model or quit
            if (Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.LiveTrackerMenu.DataContext is not MenuViewModel menuViewModel) return;

            // reload templates
            menuViewModel.UpdateFavoriteTemplates();

            TournamentRunning selectedTournament = dataGridViewModel?.SelectedTournament;

            // update selected tournament in datagrid
            if (selectedTournament is not null)
            {
                // fix the ending time now being before the starting time
                if (selectedTournament.EndTime < TournamentData.StartTime) TournamentData.EndTime = TournamentData.StartTime;

                // update the ending time to the new time if it was never changed originally
                if (selectedTournament.EndTime == selectedTournament.StartTime) TournamentData.EndTime = TournamentData.StartTime;

                // update selected tournament in data grid
                selectedTournament.UpdateData(TournamentData);

                // update running tournaments visibility in case it changed from a time change
                dataGridViewModel.UpdateTournamentVisibility(null);
            }

            // get template manager view model and update templates
            if (Application.Current.Windows.OfType<TemplateManagerView>().FirstOrDefault()?.DataContext is TemplateManagerViewModel templateManagerViewModel) templateManagerViewModel.Reload();

            // get session manager view model and update templates
            if (Application.Current.Windows.OfType<SessionManagerView>().FirstOrDefault()?.DataContext is SessionManagerViewModel sessionManagerViewModel) sessionManagerViewModel.UpdateTemplates();

            // get select tournaments view model and update templates
            if (Application.Current.Windows.OfType<SelectTemplatesView>().FirstOrDefault()?.DataContext is SelectTemplatesViewModel selectTemplatesViewModel) selectTemplatesViewModel.Reload();

            // close window
            Cancel(null);
        }
    }
}
