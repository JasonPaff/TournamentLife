using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Syncfusion.UI.Xaml.Utility;
using Syncfusion.Windows.Shared;
using Tournament_Life.Helpers;
using Tournament_Life.Views;
using Tournament_Life.Views.Menu_Views;

namespace Tournament_Life.ViewModels.Menu_ViewModels
{
    public class RegisterViewModel : NotificationObject
    {
        public ICommand CancelCommand => new BaseCommand(Cancel);
        public ICommand SaveCommand => new BaseCommand(Save);

        public RegisterViewModel()
        {
            // font size
            FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));

            // visual theme
            Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");

            // register label text
            RegisterText = "Email the following Product ID to:\n Admin@Tournaments.Life\nWhen you receive your Product Key,\nenter it into the Product Key section below";

            // load product id
            ProductID = RegistrationHelper.GetSerialNumber();

            // load product key if product is registered
            if(RegistrationHelper.IsRegistered())
            {
                ProductKey = Tournament_Life.Properties.Settings.Default.ProductKey;
                RegisterText = "Product is successfully registered";
                SaveEnabled = false;
            }
            else
            {
                SaveEnabled = true;
            }
        }

        public int FontSize { get; set; }
        public UInt32 ProductID { get; set; }
        public UInt32 ProductKey { get; set; }
        public string RegisterText { get; set; }
        public bool SaveEnabled { get; set; }
        public string Theme { get; set; }

        /// <summary>
        /// Cancel  and close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Cancel(object parameter)
        {
            // find window
            var window = Application.Current.Windows.OfType<RegisterView>().FirstOrDefault();

            // close window
            window?.Close();
        }

        /// <summary>
        /// tag as saved and close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Save(object parameter)
        {
            // register failed
            if(!RegistrationHelper.Register(RegistrationHelper.Encrypt(RegistrationHelper.programID, ProductID)))
            {
                // ok view model
                var theVm = new OkViewModel("You have entered an invalid Product Key", "Invalid Key");

                // create/show ok window
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.Windows.OfType<RegisterView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                // exit
                return;
            }

            // ok view model
            var vm = new OkViewModel("Registration Successful", "Success");

            // create/show ok window
            var window = new OkView(vm)
            {
                Owner = Application.Current.Windows.OfType<RegisterView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            window.ShowDialog();

            // find window and close it
            Application.Current.Windows.OfType<RegisterView>().FirstOrDefault()?.Close();
        }
    }
}
