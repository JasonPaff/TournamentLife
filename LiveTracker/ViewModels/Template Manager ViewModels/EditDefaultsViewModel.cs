using LiveTracker.Helpers;
using LiveTracker.Models;
using LiveTracker.Views.Template_Manager_Views;
using Syncfusion.UI.Xaml.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Syncfusion.Windows.Shared;
using System.Collections.ObjectModel;
using LiveTracker.Models.Tournaments;
using Tournament_Life.ViewModels;
using Tournament_Life.Views;

namespace LiveTracker.ViewModels.Template_Manager_ViewModels
{
    public class EditDefaultsViewModel : NotificationObject
    {
        public ICommand SaveCommand => new BaseCommand(Save);
        public ICommand CancelCommand => new BaseCommand(Cancel);

        public EditDefaultsViewModel()
        {
            // font size
            FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));

            // visual theme
            Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");

            // load starting data using default template
            TournamentData = new TournamentRunning(TournamentTemplateDataHelper.LoadDefaultValues());

            // load starting time
            StartTime = TournamentData.StartTime;

            // load venues
            Venues = new ObservableCollection<string>(TournamentTemplateDataHelper.LoadVenues());

            // set default venue
            var defaultVenue = TournamentTemplateDataHelper.LoadDefaultVenue();
            foreach (var venue in Venues) if (venue.Equals(defaultVenue, StringComparison.OrdinalIgnoreCase)) SelectedVenue = defaultVenue;

            // load game types
            GameTypes = new ObservableCollection<string>(TournamentTemplateDataHelper.LoadGameTypes());

            // set default game type
            var defaultGameType = TournamentTemplateDataHelper.LoadDefaultGameType();
            foreach (var gameType in GameTypes) if (gameType.Equals(defaultGameType, StringComparison.OrdinalIgnoreCase)) SelectedGameType = defaultGameType;

            // load formats
            Formats = TournamentTemplateDataHelper.LoadFormats();
            var defaultFormats = TournamentTemplateDataHelper.LoadDefaultFormats();

            // create formats list box items
            FormatsList = new ObservableCollection<TemplateListBoxItem>();
            foreach (var format in Formats)
            {
                // create format list box item
                var item = new TemplateListBoxItem
                {
                    IsSelected = false,
                    DisplayString = format
                };

                // set default formats to selected
                if (defaultFormats.Any(i => i == item.DisplayString)) item.IsSelected = true;

                // add list box to collection
                if (item.IsSelected) FormatsList.Insert(0, item);
                else FormatsList.Add(item);
            }

            // order so selected is at the top sorted alphabetically and the unselected are alphabetically sorted below
            FormatsList = new ObservableCollection<TemplateListBoxItem>(FormatsList.OrderByDescending(i => i.IsSelected).ThenBy(i => i.DisplayString));
        }

        public int FontSize { get; set; }
        public List<string> Formats { get; set; }
        public ObservableCollection<TemplateListBoxItem> FormatsList { get; set; }
        public ObservableCollection<string> GameTypes { get; set; }
        public TemplateListBoxItem SelectedFormat { get; set; }
        public string SelectedGameType { get; set; }
        public string SelectedVenue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime StartTime { get; set; }
        public string Theme { get; set; }
        public TournamentRunning TournamentData { get; set; }
        public ObservableCollection<string> Venues { get; set; }

        /// <summary>
        /// Cancel any note text and close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Cancel(object parameter)
        {
            // find window
            var window = Application.Current.Windows.OfType<EditDefaultsView>().FirstOrDefault();

            // close window
            window?.Close();
        }

        /// <summary>
        /// Save any note text and close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Save(object parameter)
        {
            // yes/no view model
            var vm = new YesNoViewModel("Are you sure you want to save these defaults?", "Save Defaults");

            // create/show yes/no window
            var window = new YesNoView(vm)
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            window.ShowDialog();

            // not saved, exit
            if (vm.Saved is false) return;

            // update venue
            TournamentData.Venue = SelectedVenue;
            TournamentTemplateDataHelper.SetDefaultVenue(SelectedVenue);

            // update game type
            TournamentData.GameType = SelectedGameType;
            TournamentTemplateDataHelper.SetDefaultGameType(SelectedGameType);

            // update formats
            foreach (var item in FormatsList) if(item.IsSelected) TournamentData.Formats.Add(item.DisplayString);
            TournamentTemplateDataHelper.SetDefaultFormats(TournamentData.Formats);

            // update default values in xml file
            TournamentTemplateDataHelper.SetDefaultValues(TournamentData);

            // close window
            Application.Current.Windows.OfType<EditDefaultsView>().FirstOrDefault()?.Close();
        }
    }
}
