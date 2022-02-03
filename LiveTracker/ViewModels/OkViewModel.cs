using Syncfusion.UI.Xaml.Utility;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Syncfusion.Windows.Shared;
using Tournament_Life.Helpers;
using Tournament_Life.Views;

namespace Tournament_Life.ViewModels
{
    public class OkViewModel : NotificationObject
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
        public int FontSize { get; }
        public string Text { get; }
        public string Theme { get; }
        public string Title { get; }
        public ICommand CancelCommand => new BaseCommand(Cancel);

        /// <summary>
        /// Cancel and close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Cancel(object parameter)
        {
             Application.Current.Windows.OfType<OkView>().FirstOrDefault()?.Close();
        }
    }
}