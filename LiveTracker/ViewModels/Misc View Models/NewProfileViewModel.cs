using System.Collections.Generic;
using System.Linq;
using Syncfusion.Windows.Shared;
using System.Windows.Input;
using Syncfusion.UI.Xaml.Utility;
using System.Windows;
using LiveTracker.Helpers;
using LiveTracker.Views.Misc_Views;
using System.IO;

namespace LiveTracker.ViewModels.Misc_View_Models
{
    public class NewProfileViewModel : NotificationObject
    {
        public ICommand CancelCommand => new BaseCommand(Cancel);
        public ICommand SaveCommand => new BaseCommand(Save);

        public string ProfileName { get; set; }

        /// <summary>
        /// Close the program
        /// </summary>
        /// <param name="parameter"></param>
        private void Cancel(object parameter)
        {
            // close the program
            Application.Current.MainWindow.Close();
        }

        /// <summary>
        /// Save the new profile
        /// </summary>
        /// <param name="parameter"></param>
        private void Save(object parameter)
        {
            // no name
            if (ProfileName.Length is 0) return;

            // programs directory
            var dir = System.AppDomain.CurrentDomain.BaseDirectory;

            // create screenshot folder
            if (!Directory.Exists(dir + "Screenshots")) Directory.CreateDirectory(dir + "Screenshots");

            // create folder
            if (!Directory.Exists(dir + "Preferences")) Directory.CreateDirectory(dir + "Preferences");

            // create new profile, creates database and default preferences file with it
            ProfileHelper.AddProfile(ProfileName.Trim());

            // set to current profile
            ProfileHelper.SetCurrentProfile(ProfileName.Trim());

            // close the window
            Application.Current.Windows.OfType<NewProfileView>().FirstOrDefault().Close();
        }
    }
}
