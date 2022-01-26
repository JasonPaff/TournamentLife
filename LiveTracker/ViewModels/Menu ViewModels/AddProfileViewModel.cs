using LiveTracker.Helpers;
using Syncfusion.UI.Xaml.Utility;
using System.Linq;
using System.Windows.Input;
using Syncfusion.Windows.Shared;
using System.Windows;
using LiveTracker.Views.Menu_Views;
using Tournament_Life.ViewModels;
using Tournament_Life.Views;
using System.Collections.Generic;
using System;

namespace LiveTracker.ViewModels.Menu_ViewModels
{
    public class AddProfileViewModel : NotificationObject
    {
        public ICommand CancelCommand => new BaseCommand(Close);
        public ICommand SaveCommand => new BaseCommand(Save);

        public AddProfileViewModel()
        {
            FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));
            Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");

            ImportDefaults = false;
        }

        public int FontSize { get; set; }
        public bool Saved { get; set; }
        public string Text { get; set; }
        public string Theme { get; set; }
        public bool ImportDefaults { get; set; }

        /// <summary>
        /// Cancel and close window
        /// </summary>
        /// <param name="parameter"></param>
        private void Close(object parameter)
       {
            // find window and close it
            Application.Current.Windows.OfType<AddProfileView>().FirstOrDefault()?.Close();
        }

        /// <summary>
        /// Save new profile
        /// </summary>
        /// <param name="parameter"></param>
        private void Save(object parameter)
        {
            // profile name length check
            if (Text is null || Text.Trim().Length is 0)
            {
                // yes/no view model
                var theVm = new OkViewModel("The profile name can't be blank", "Blank Name");

                // create/show yes/no window
                var windo = new OkView(theVm)
                {
                    Owner = Application.Current.Windows.OfType<AddProfileView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                windo.ShowDialog();

                return;
            }

            // yes/no view model
            var theVvm = new YesNoViewModel("Would you like to import the pre-made Global Poker Tournaments File?", "Import Tournaments");

            // create/show yes/no window
            var wwindo = new YesNoView(theVvm)
            {
                Owner = Application.Current.Windows.OfType<AddProfileView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            wwindo.ShowDialog();

            if (theVvm.Saved is true)
                ImportDefaults = true;

            // add new profile to file
            var res = ProfileHelper.AddProfile(Text.Trim());

            // show error message if duplicate
            if (res is false)
            {
                // yes/no view model
                var theVm = new OkViewModel("Profile name already exists", "Duplicate Profile");

                // create/show yes/no window
                var windo = new OkView(theVm)
                {
                    Owner = Application.Current.Windows.OfType<AddProfileView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                windo.ShowDialog();

                return;
            }

            // trim any leading/trailing blank spaces
            Text = Text.Trim();

            // saved
            Saved = true;

            // close window
            Close(null);
        }
    }
}
