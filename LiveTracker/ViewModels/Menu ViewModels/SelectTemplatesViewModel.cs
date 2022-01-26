using Syncfusion.UI.Xaml.Utility;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using LiveTracker.Views.Menu_Views;
using System.Windows;
using LiveTracker.Views;
using LiveTracker.Models.Tournaments;
using LiveTracker.Helpers;
using LiveTracker.Factories;
using Tournament_Life.ViewModels;
using Tournament_Life.Views;

namespace LiveTracker.ViewModels.Menu_ViewModels
{
    public class SelectTemplatesViewModel : NotificationObject
    {
        public ICommand CancelCommand => new BaseCommand(Cancel);
        public ICommand CloseOnStartCommand => new BaseCommand(CloseOnStart);
        public ICommand ConfirmStartCommand => new BaseCommand(ConfirmStart);
        public ICommand ExitCommand => new BaseCommand(ExitWindow);
        public ICommand ResetCommand => new BaseCommand(Reset);
        public ICommand SaveCommand => new BaseCommand(Save);
        public ICommand SelectToggleCommand => new BaseCommand(SelectToggle);
        public ICommand TextChangedCommand => new BaseCommand(TextChanged);
        public ICommand WindowLoadedCommand => new BaseCommand(WindowLoaded);

        public SelectTemplatesViewModel()
        {
            // get menu view model or quit
            if (Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.LiveTrackerMenu.DataContext is not MenuViewModel menuViewModel) return;

            // font size
            FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));

            // visual theme
            Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");

            // load venues combo box initial values
            LoadVenues();

            // list box item list for the templates
            TemplateList = CreateListBoxItems();

            // new temporary list of list box items
            var tempList = new ObservableCollection<TemplateListBoxItem>(TemplateList);

            // filter by venue
            if (SelectedVenue is not "All Venues") tempList = new ObservableCollection<TemplateListBoxItem>(tempList.Where(i => i.Venue.Equals(SelectedVenue, StringComparison.OrdinalIgnoreCase)));

            // filter by name
            tempList = new ObservableCollection<TemplateListBoxItem>(tempList.Where(i => i.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase)));

            // set temporary list to the main list, if it changed
            if (tempList.Count != TemplateList.Count) TemplateList = tempList;

            // set close on start toggle
            CloseOnStartToggle = bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "Menu", "SelectTournamentsCloseOnStart"));

            // set confirm tournament start toggle
            ConfirmStartToggle = bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "Menu", "SelectTournamentsConfirmStart"));

            // set multi selection mode
            MultiSelectionToggle = bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "Menu", "SelectTournamentsMultiToggle"));
        }

        public bool CloseOnStartToggle { get; set; }
        public bool ConfirmStartToggle { get; set; }
        public int FontSize { get; set; }
        public bool MultiSelectionToggle { get; set; }
        public bool Saved { get; set; }
        public string SearchText { get; set; } = "";
        public string SelectedVenue { get; set; }
        public ObservableCollection<TemplateListBoxItem> TemplateList { get; set; }
        public TemplateListBoxItem TemplateSelectedItem
        {
            get => null;
            set
            {
                var selected = TemplateList.Count(x => x.IsSelected);
                RaisePropertyChanged(nameof(TemplateSelectedItem));
            }
        }
        public string Theme { get; set; }
        public ObservableCollection<string> Venues { get; set; }

        /// <summary>
        /// Cancel any note text and close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Cancel(object parameter)
        {
            // find window
            var window = Application.Current.Windows.OfType<SelectTemplatesView>().FirstOrDefault();

            // close window
            window?.Close();
        }

        /// <summary>
        /// Change close window on tournament start preference
        /// </summary>
        private void CloseOnStart(object parameter)
        {
            // get opposite of saved preference
            var close = !bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "Menu", "SelectTournamentsCloseOnStart"));

            // update saved preference
            PreferencesHelper.UpdatePreference("LiveTracker", "Menu", "SelectTournamentsCloseOnStart", close);
        }

        /// <summary>
        /// Change confirmation preference in the menu
        /// </summary>
        private void ConfirmStart(object parameter)
        {
            // get opposite of saved preference
            var confirm = !bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "Menu", "SelectTournamentsConfirmStart"));

            // update saved preference
            PreferencesHelper.UpdatePreference("LiveTracker", "Menu", "SelectTournamentsConfirmStart", confirm);
        }

        /// <summary>
        /// creates list box items from templates
        /// </summary>
        private ObservableCollection<TemplateListBoxItem> CreateListBoxItems()
        {
            var list = new ObservableCollection<TemplateListBoxItem>();

            // get menu view model or quit
            if (!(Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.LiveTrackerMenu.DataContext is MenuViewModel menuViewModel)) return list;

            // create the list box items from the templates list from the menu
            foreach (var template in menuViewModel.Templates)
            {
                var listBoxItem = new TemplateListBoxItem()
                {
                    Buyin = (double)template.BuyinTotalCost,
                    Description = template.DescriptionWithoutDayMonthYear,
                    DisplayString = template.TournamentName,
                    IsSelected = false,
                    Name = template.TournamentName,
                    TemplateId = template.TemplateId,
                    Venue = template.Venue
                };

                list.Add(listBoxItem);
            }

            // return the list of list box items
            return list;
        }

        /// <summary>
        /// close window
        /// </summary>
        private void ExitWindow(object parameter)
        {
            // find and close window
            if(Application.Current.Windows.OfType<SelectTemplatesView>().FirstOrDefault() is SelectTemplatesView window) window.Close();
        }

        /// <summary>
        /// Load the venues combo box
        /// </summary>
        private void LoadVenues()
        {
            // get venues from tournament template data file
            Venues = new ObservableCollection<string>(TournamentTemplateDataHelper.LoadVenues());

            // insert all venues option
            Venues.Insert(0, "All Venues");

            // set select venue to first venue
            SelectedVenue = PreferencesHelper.FindPreference("LiveTracker", "Menu", "SelectTournamentsDefaultVenue");
        }

        /// <summary>
        /// Reload select tournaments list box item
        /// </summary>
        public void Reload()
        {
            // reload venues from file
            LoadVenues();

            // temporary search text
            var tempSearchText = SearchText;

            // reset list box
            Reset(null);

            // reset search text
            SearchText = tempSearchText;
        }

        /// <summary>
        /// called when the user clicks the reset button
        /// clears text box
        /// unselects all tournaments
        /// </summary>
        /// <param name="parameter"></param>
        private void Reset(object parameter)
        {
            // reset text box
            SearchText = "";

            // reset templates list
            TemplateList = CreateListBoxItems();

            // new temporary list of list box items
            var tempList = new ObservableCollection<TemplateListBoxItem>(TemplateList);

            // filter by venue
            if (SelectedVenue is not "All Venues") tempList = new ObservableCollection<TemplateListBoxItem>(tempList.Where(i => i.Venue.Equals(SelectedVenue, StringComparison.OrdinalIgnoreCase)));

            // filter by name
            tempList = new ObservableCollection<TemplateListBoxItem>(tempList.Where(i => i.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase)));

            // set temporary list to the main list, if it changed
            if (tempList.Count != TemplateList.Count) TemplateList = tempList;
        }

        /// <summary>
        /// Save any note text and close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Save(object parameter)
        {
            // tournament names for confirm message
            var nameString = "\n";

            // template ids list for tournaments we want to start
            var templateIds = new List<int>();

            // loop through select templates and create name string and template id collection
            foreach(var template in TemplateList)
            {
                if(template.IsSelected)
                {
                    nameString += "\n" + template.Name;
                    templateIds.Add(template.TemplateId);
                }
            }

            // none selected leave
            if(templateIds.Count is 0) return;

            // get confirmation preference
            var confirm = bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "Menu", "SelectTournamentsConfirmStart"));

            // confirm
            if (confirm & templateIds.Count is 1)
            {
                // yes/no view model
                var vm = new YesNoViewModel("Are you sure you want to start this tournament?" + nameString, "Start Tournament");

                // create/show yes/no window
                var window = new YesNoView(vm)
                {
                    Owner = Application.Current.Windows.OfType<SelectTemplatesView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                window.ShowDialog();

                // not saved
                if (vm.Saved is false) return;
            }

            // confirm
            if (confirm & templateIds.Count > 1)
            {
                // yes/no view model
                var vm = new YesNoViewModel("Are you sure you want to start these tournaments?" + nameString, "Start Tournaments");

                // create/show yes/no window
                var window = new YesNoView(vm)
                {
                    Owner = Application.Current.Windows.OfType<SelectTemplatesView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                window.ShowDialog();

                // not saved
                if (vm.Saved is false) return;
            }

            // saved
            Saved = true;

            // start tournaments
            TournamentFactory.StartTournaments(templateIds);

            // reset
            Reset(null);

            // exit window if close on start preference is set
            if (bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "Menu", "SelectTournamentsCloseOnStart"))) ExitWindow(null);
        }

        /// <summary>
        /// Change selection mode preference
        /// </summary>
        private void SelectToggle(object parameter)
        {
            // get opposite of saved preference
            var selection = !bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "Menu", "SelectTournamentsMultiToggle"));

            // update saved preference
            PreferencesHelper.UpdatePreference("LiveTracker", "Menu", "SelectTournamentsMultiToggle", selection);

            // update list box selection mode
            if (selection) Application.Current.Windows.OfType<SelectTemplatesView>().FirstOrDefault().TournamentList.SelectionMode = System.Windows.Controls.SelectionMode.Multiple;
            if (!selection) Application.Current.Windows.OfType<SelectTemplatesView>().FirstOrDefault().TournamentList.SelectionMode = System.Windows.Controls.SelectionMode.Single;
        }

        /// <summary>
        /// called when the user type in the text box
        /// filters the tournaments list
        /// </summary>
        /// <param name="parameter"></param>
        private void TextChanged(object parameter)
        {
            // get menu view model or quit
            if (Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.LiveTrackerMenu.DataContext is not MenuViewModel menuViewModel)  return;

            // new temporary list of list box items
            var tempList = new ObservableCollection<TemplateListBoxItem>(CreateListBoxItems());

            // filter by venue
            if(SelectedVenue is not "All Venues") tempList = new ObservableCollection<TemplateListBoxItem>(tempList.Where(i => i.Venue.Equals(SelectedVenue, StringComparison.OrdinalIgnoreCase)));

            // filter by name
            tempList = new ObservableCollection<TemplateListBoxItem>(tempList.Where(i => i.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase)));

            // set temporary list to the main list, if it changed
            if(tempList.Count != TemplateList.Count) TemplateList = tempList;

            // save select venue as default venue
            PreferencesHelper.UpdatePreference("LiveTracker", "Menu", "SelectTournamentsDefaultVenue", SelectedVenue);
        }

        /// <summary>
        /// Called after window is loaded for the first time
        /// </summary>
        private void WindowLoaded(object parameter)
        {
            // update list box selection mode
            if (MultiSelectionToggle) Application.Current.Windows.OfType<SelectTemplatesView>().FirstOrDefault().TournamentList.SelectionMode = System.Windows.Controls.SelectionMode.Multiple;
            if (!MultiSelectionToggle) Application.Current.Windows.OfType<SelectTemplatesView>().FirstOrDefault().TournamentList.SelectionMode = System.Windows.Controls.SelectionMode.Single;
        }
    }
}
