using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using LiveTracker.Helpers;
using LiveTracker.Models.Bankroll_Model;
using LiveTracker.Views.Bankroll_Views;
using Syncfusion.UI.Xaml.Utility;
using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels;
using Tournament_Life.Views;

namespace LiveTracker.ViewModels.Bankroll_ViewModels
{
    public class RemoveVenueViewModel : NotificationObject
    {
        private readonly BankrollListBoxItem _selectedVenue;
        public bool Saved { get; set; }
        public string Theme { get; set; }
        public int FontSize { get; set; }
        public BankrollListBoxItem SelectedVenues
        {
            get => _selectedVenue;
            set
            {
                var selected = Venues.Count(i => i.IsSelected);
                RaisePropertyChanged(nameof(SelectedVenues));
            }
        }
        public ObservableCollection<BankrollListBoxItem> Venues { get; set; }
        public ObservableCollection<string> VenuesToDelete { get; set; }
        public ICommand SaveCommand => new BaseCommand(Save);
        public ICommand CancelCommand => new BaseCommand(Cancel);

        public RemoveVenueViewModel()
        {
            FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));
            Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");

            // create list box items
            Venues = new ObservableCollection<BankrollListBoxItem> (BankrollHelper.CreateListBoxItems(TournamentTemplateDataHelper.LoadVenues()));
        }

        /// <summary>
        /// Cancel and close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Cancel(object parameter)
        {
            Application.Current.Windows.OfType<RemoveVenueView>().FirstOrDefault()?.Close();
        }

        /// <summary>
        /// Save and close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Save(object parameter)
        {
            // did save
            Saved = true;

            // collection of venues to remove
            VenuesToDelete = new ObservableCollection<string>();

            // string for name of venues to be removed
            var removedString = new StringBuilder();

            // add each selected venue to the collection
            foreach (var listBoxItem in Venues)
            {
                if (listBoxItem.IsSelected is false)
                    continue;

                removedString.AppendLine(listBoxItem.Name);
                VenuesToDelete.Add(listBoxItem.Name);
            }

            // add blank line
            removedString.AppendLine();

            // confirm for multiple bankrolls
            if (VenuesToDelete.Count > 1)
            {
                // yes/no view model
                var vm = new YesNoViewModel($"Are you sure you want to remove these venues?\n\n{removedString.ToString().TrimEnd()}", "Remove Venues");

                // create/show yes/no window
                var window = new YesNoView(vm)
                {
                    Owner = Application.Current.MainWindow,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                window.ShowDialog();

                // not saved
                if (vm.Saved is false)
                    return;
            }

            // confirm for single bankrolls
            if (VenuesToDelete.Count is 1)
            {
                // yes/no view model
                var vm = new YesNoViewModel($"Are you sure you want to remove this venue?\n\n{removedString.ToString().TrimEnd()}", "Remove Venue");

                // create/show yes/no window
                var window = new YesNoView(vm)
                {
                    Owner = Application.Current.MainWindow,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                window.ShowDialog();

                // not saved
                if (vm.Saved is false)
                    return;
            }

            // close window
            Cancel(null);
        }
    }
}
