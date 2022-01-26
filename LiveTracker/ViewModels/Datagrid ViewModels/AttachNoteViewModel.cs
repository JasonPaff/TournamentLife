using System.Linq;
using System.Windows;
using System.Windows.Input;
using LiveTracker.Helpers;
using LiveTracker.Views.Datagrid_Views;
using Syncfusion.UI.Xaml.Utility;
using Syncfusion.Windows.Shared;
using LiveTracker.Commands;
using LiveTracker.Models;

namespace LiveTracker.ViewModels.Datagrid_ViewModels
{
    public class AttachNoteViewModel : NotificationObject
    {
        public AttachNoteViewModel(string note, string name)
        {
            LoadPreferences();

            NoteText = note;

            Title = name + " - Notes";
        }

        public int FontSize { get; set; }
        public string NoteText { get; set; }
        public string Theme { get; set; }
        public string Title { get; set; }
        public ICommand SaveCommand => new BaseCommand(Save);
        public ICommand CloseCommand => new BaseCommand(CloseWindow);

        /// <summary>
        /// Cancel any note text and close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void CloseWindow(object parameter)
        {
            Application.Current.Windows.OfType<AttachNoteView>().FirstOrDefault()?.Close();
        }

        /// <summary>
        /// load initial preferences
        /// </summary>
        private void LoadPreferences()
        {
            // font size
            FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));

            // visual theme
            Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");
        }

        /// <summary>
        /// Save any note text and close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Save(object parameter)
        {
            TournamentRunning selectedTournament = DataGridCommands.GetDataGridViewModel().SelectedTournament;

            if (selectedTournament is null) return;

            selectedTournament.Note = NoteText;

            CloseWindow(null);
        }
    }
}
