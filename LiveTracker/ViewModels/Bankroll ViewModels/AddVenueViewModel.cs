using System.Linq;
using System.Windows;
using System.Windows.Input;
using LiveTracker.Helpers;
using LiveTracker.Views.Bankroll_Views;
using Syncfusion.UI.Xaml.Utility;
using Tournament_Life.ViewModels;
using Tournament_Life.Views;

namespace LiveTracker.ViewModels.Bankroll_ViewModels
{
    public class AddVenueViewModel
    {
        public bool Saved { get; set; }
        public string Venue { get; set; }
        public ICommand SaveCommand => new BaseCommand(Save);
        public ICommand CancelCommand => new BaseCommand(Cancel);
        public string Theme { get; set; }
        public int FontSize { get; set; }

        public AddVenueViewModel()
        {
            FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));
            Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");
        }

        /// <summary>
        /// Cancel and close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Cancel(object parameter)
        {
            Application.Current.Windows.OfType<AddVenueView>().FirstOrDefault()?.Close();
        }

        /// <summary>
        /// Save and close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Save(object parameter)
        {
            // blank check
            if (Venue is null || Venue.Trim() == string.Empty)
                return;

            // yes/no view model
            var vm = new YesNoViewModel($"Are you sure you want to add {Venue}?", "Add Venue");

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

            // did save
            Saved = true;

            // close window
            Cancel(null);
        }
    }
}
