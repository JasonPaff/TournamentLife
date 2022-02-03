using Syncfusion.UI.Xaml.Utility;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Tournament_Life.Helpers;
using Tournament_Life.Views;

namespace Tournament_Life.ViewModels
{
    public class YesNoViewModel
    {
        public YesNoViewModel(string text, string title)
        {
            // font size
            FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));

            // visual theme
            Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");

            // set title
            Title = title;

            // set text
            Text = text;
        }

        public int FontSize { get; set; }
        public bool Saved { get; set; }
        public string Text { get; set; }
        public string Theme { get; set; }
        public string Title { get; set; }
        public ICommand SaveCommand => new BaseCommand(Save);
        public ICommand CancelCommand => new BaseCommand(Cancel);

        /// <summary>
        /// Cancel any note text and close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Cancel(object parameter)
        {
            // find window
            var window = Application.Current.Windows.OfType<YesNoView>().FirstOrDefault();

            // close window
            window?.Close();
        }

        /// <summary>
        /// Save and close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Save(object parameter)
        {
            // saved
            Saved = true;

            // close window
            Cancel(null);
        }
    }
}
