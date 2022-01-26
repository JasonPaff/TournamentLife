using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using LiveTracker.Helpers;
using LiveTracker.Models;
using LiveTracker.Views.Datagrid_Views;
using Syncfusion.UI.Xaml.Utility;
using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels;
using Tournament_Life.Views;

namespace LiveTracker.ViewModels.Datagrid_ViewModels
{
    public class CancelSelectTournamentsViewModel : NotificationObject
    {
        // hold tournaments that can be canceled
        ObservableCollection<TournamentRunning> _tournaments;

        public CancelSelectTournamentsViewModel(ObservableCollection<TournamentRunning> tournaments)
        {
            // sort option
            SortByStartTime = bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "SortCancelTournamentsByStartTime"));

            // create list box items for tournaments
            if (SortByStartTime) Tournaments = CreateListBoxItems(new ObservableCollection<TournamentRunning>(tournaments.OrderBy(i => i.StartTime)));
            else Tournaments = CreateListBoxItems(new ObservableCollection<TournamentRunning>(tournaments.OrderBy(i => i.TournamentName)));

            // tournament indexes
            Indexes = new ObservableCollection<int>();

            // font size
            FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));

            // visual theme
            Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");

            // copy tournaments
            _tournaments = tournaments;
        }

        public int FontSize { get; set; }
        public bool Saved { get; set; } = false;
        public string Theme { get; set;}
        public ObservableCollection<int> Indexes { get; set; }
        public bool SortByStartTime { get; set; }
        public ObservableCollection<ListBoxItem> Tournaments { get; set; }
        public ICommand CancelCommand => new BaseCommand(Cancel);
        public ICommand SaveCommand => new BaseCommand(Save);
        public ICommand SortCommand => new BaseCommand(Sort);

        /// <summary>
        /// Cancel any note text and close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Cancel(object parameter)
        {
            // find window
            var window = Application.Current.Windows.OfType<CancelSelectTournamentsView>().FirstOrDefault();

            // close window
            window?.Close();
        }

        /// <summary>
        /// show a cancel message
        /// </summary>
        private bool CancelMessage()
        {

            // create name list for confirm message
            var nameString = new StringBuilder();

            // get selected tournaments and build name string
            foreach (var tournament in Tournaments) if (tournament.IsSelected) nameString.AppendLine(tournament.Name);

            // default cancel message
            var vm = new YesNoViewModel("Are you sure you want to cancel these tournaments?\n\n" + nameString, "Cancel Tournaments");

            // cancel message for multiple tournaments
            if (Tournaments.Count(i => i.IsSelected) > 1) vm = new YesNoViewModel("Are you sure you want to cancel these tournaments?\n\n" + nameString, "Cancel Tournaments");

            // cancel message for single tournaments
            if (Tournaments.Count(i => i.IsSelected) is 1) vm = new YesNoViewModel("Are you sure you want to cancel this tournament?\n\n" + nameString, "Cancel Tournament");

            // create/show yes or no window
            var window = new YesNoView(vm)
            {
                Owner = Application.Current.Windows.OfType<CancelSelectTournamentsView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            window.ShowDialog();

            // didn't confirm yes for cancel
            if (vm.Saved is false) return false;

            // confirmed
            return true;
        }

        /// <summary>
        /// create list box items for tournaments
        /// </summary>
        /// <param name="tournaments">tournaments</param>
        /// <returns>list box items</returns>
        private ObservableCollection<ListBoxItem> CreateListBoxItems(ObservableCollection<TournamentRunning> tournaments)
        {
            // list of list box items
            var items = new ObservableCollection<ListBoxItem>();

            // null check
            if (tournaments is null || tournaments.Count is 0) return items;

            // loop through the tournaments
            foreach (var tournament in tournaments)
            {
                // create list box item
                var item = new ListBoxItem
                {
                    IsSelected = false,
                    Name = tournament.TournamentName,
                    Id = tournament.TemplateId,
                    Description = tournament.Description,
                    Buyin = tournament.BuyinTotalCost,
                    StartTime = tournament.StartTime,
                };

                // add item to list
                items.Add(item);
            }

            // return list of items
            return items;
        }

        /// <summary>
        /// Save any note text and close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Save(object parameter)
        {
            // can't save if nothing selected
            if (!Tournaments.Any(i => i.IsSelected)) return;

            // show the cancel confirmation message
            if (CancelMessage() is false) return;

            // get selected items
            var temp = new ObservableCollection<ListBoxItem>(Tournaments.Where(i => i.IsSelected));

            // collection indexes for canceling
            foreach (var tournament in temp) { Indexes.Add(Tournaments.IndexOf(tournament)); Debug.WriteLine(Tournaments.IndexOf(tournament)); }

            // flag that we saved
            Saved = true;

            // close and exit
            Cancel(null);
        }

        /// <summary>
        /// Sort tournaments by menu choice
        /// </summary>
        /// <param name="parameter"></param>
        private void Sort(object parameter)
        {
            // get current sort value
            var sort = !bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "SortCancelTournamentsByStartTime"));

            // save opposite sort value
            PreferencesHelper.UpdatePreference("LiveTracker", "Window", "SortCancelTournamentsByStartTime", sort);

            // sort by start time if flagged
            if (sort) Tournaments = TournamentTemplateHelper.CreateListBoxItems(new ObservableCollection<TournamentRunning>(_tournaments.OrderBy(i => i.StartTime)));
            else Tournaments = TournamentTemplateHelper.CreateListBoxItems(new ObservableCollection<TournamentRunning>(_tournaments.OrderBy(i => i.TournamentName)));
        }
    }
}