using LiveTracker.Helpers;
using Syncfusion.UI.Xaml.Utility;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Tournament_Life.Views;

namespace Tournament_Life.ViewModels
{
    public class OkViewModel
    {
        public OkViewModel(string text, string title)
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
        public string Text { get; set; }
        public string Theme { get; set; }
        public string Title { get; set; }
        public ICommand CancelCommand => new BaseCommand(Cancel);

        /// <summary>
        /// Cancel and close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Cancel(object parameter)
        {
            Application.Current.Windows.OfType<OkView>().FirstOrDefault().Close();
        }
    }
}
