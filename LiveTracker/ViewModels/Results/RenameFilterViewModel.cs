using Syncfusion.UI.Xaml.Utility;
using Syncfusion.Windows.Shared;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Tournament_Life.Helpers;
using Tournament_Life.Models.Filters;
using Tournament_Life.Views;
using Tournament_Life.Views.Results;

namespace Tournament_Life.ViewModels.Results
{
    public class RenameFilterViewModel : NotificationObject
    {
        public ICommand CancelCommand => new BaseCommand(Cancel);
        public ICommand RenameCommand => new BaseCommand(Rename);

        public RenameFilterViewModel(FilterListBoxItem filter, ObservableCollection<FilterListBoxItem> filterList)
        {
            FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));
            Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");

            // copy over filter and filter list
            Filter = filter;
            FilterList = filterList;

            // filter starting text
            Text = "";

        }
        public FilterListBoxItem Filter { get; set; }
        public ObservableCollection<FilterListBoxItem> FilterList { get; set; }
        public int FontSize { get; set; }
        public bool Saved { get; set; }
        public string Text { get; set; }
        public string Theme { get; set; }

        /// <summary>
        /// Cancel and close window
        /// </summary>
        /// <param name="parameter"></param>
        private void Cancel(object parameter)
        {
            // find window
            var window = Application.Current.Windows.OfType<RenameFilterView>().FirstOrDefault();

            // close window
            window?.Close();
        }

        /// <summary>
        /// Save new filter
        /// </summary>
        /// <param name="parameter"></param>
        private void Rename(object parameter)
        {
            // blank name check
            if (Text.Length is 0 || Text.Trim().Length is 0)
            {
                // ok view model
                var theVm = new OkViewModel("Filter name can't be blank", "Blank Name");

                // create/show ok window
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.Windows.OfType<RenameFilterView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                // exit
                return;
            }

            // duplicate name check
            foreach(var fil in FilterList) if(fil.Name == Text)
            {
                // ok view model
                var theVm = new OkViewModel("Filter name already exists", "Name Already Exists");

                // create/show ok window
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.Windows.OfType<RenameFilterView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                // exit
                return;
            }

            // yes/no view model
            var vm = new YesNoViewModel("Are you sure you want to rename this filter?\n\n" + Filter.Name + "\n\nto\n\n" + Text, "Rename Filter");

            // create/show yes/no window
            var window = new YesNoView(vm)
            {
                Owner = Application.Current.Windows.OfType<RenameFilterView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            window.ShowDialog();

            // didn't save, exit
            if (vm.Saved is false) return;

            // flag as saved
            Saved = true;

            // close window
            Cancel(null);
        }
    }
}
