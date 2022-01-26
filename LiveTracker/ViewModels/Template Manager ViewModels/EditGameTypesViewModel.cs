using LiveTracker.Helpers;
using LiveTracker.Models.Tournaments;
using LiveTracker.Views.Template_Manager_Views;
using Syncfusion.UI.Xaml.Utility;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Syncfusion.Windows.Shared;

namespace LiveTracker.ViewModels.Template_Manager_ViewModels
{
    public class EditGameTypesViewModel : NotificationObject
    {
        private string _selectedItem;
        public ICommand AddCommand => new BaseCommand(Add);
        public ICommand CancelCommand => new BaseCommand(Cancel);
        public ICommand RemoveCommand => new BaseCommand(Remove);

        public EditGameTypesViewModel()
        {
            // font size
            FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));

            // visual theme
            Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");

            // load venues from xml file
            Items = TournamentTemplateDataHelper.LoadGameTypes();

            // create list box items from venues
            CreateListBoxItems();

            // get default venue
            var defaultGameType = TournamentTemplateDataHelper.LoadDefaultGameType();
            if (Items.Contains(defaultGameType)) SelectedItem = defaultGameType;
        }

        public string AddText { get; set; }
        public int FontSize { get; set; }
        public List<string> Items { get; set; }
        public ObservableCollection<TemplateListBoxItem> ItemList { get; set; }
        public string SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (_selectedItem == value) return;

                _selectedItem = value;
                RaisePropertyChanged(nameof(SelectedItem));
                TournamentTemplateDataHelper.SetDefaultGameType(SelectedItem);
            }
        }
        public string Theme { get; set; }

        /// <summary>
        /// Add a new game type to the xml file
        /// </summary>
        /// <param name="parameter"></param>
        private void Add(object parameter)
        {
            // null/blank check
            if (AddText is null || AddText.Trim() is "") return;

            var addText = AddText.Trim();
            AddText = "";

            // add game type, doesn't add if duplicate
            if (!TournamentTemplateDataHelper.AddGameType(addText)) return;

            // load venues from xml file
            Items = TournamentTemplateDataHelper.LoadGameTypes();

            // create new list box items
            CreateListBoxItems();

            // find template manager view model
            if (Application.Current.Windows.OfType<TemplateManagerView>()?.FirstOrDefault()?.DataContext is not TemplateManagerViewModel templateManagerViewModel) return;

            // add game type to template manager game types list
            templateManagerViewModel.GameTypes.Add(addText.Trim());

            // reload game types
            templateManagerViewModel.GameTypes = new ObservableCollection<string>(TournamentTemplateDataHelper.LoadGameTypes());
        }

        /// <summary>
        /// close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Cancel(object parameter)
        {
            // find window
            var window = Application.Current.Windows.OfType<EditGameTypesView>().FirstOrDefault();

            // close window
            window?.Close();
        }

        /// <summary>
        /// Create list box items from game types
        /// </summary>
        private void CreateListBoxItems()
        {
            // holds our list box items
            ItemList = new ObservableCollection<TemplateListBoxItem>();

            // null/zero check
            if (Items is null || Items.Count is 0) return;

            // loop through venues
            foreach (var gameType in Items.OrderBy(i => i))
            {
                // create list box item
                var item = new TemplateListBoxItem()
                {
                    DisplayString = gameType,
                };

                // add list box item to collection
                ItemList.Add(item);
            }
        }

        /// <summary>
        /// remove a game type
        /// </summary>
        /// <param name="parameter"></param>
        private void Remove(object parameter)
        {
            // get selected items
            var selected = new ObservableCollection<TemplateListBoxItem>(ItemList.Where(i => i.IsSelected));

            // null/zero check
            if (selected is null || selected.Count is 0) return;

            // loop selected items
            foreach (var selection in selected)
            {
                // remove item, can fail
                if (TournamentTemplateDataHelper.RemoveGameType(selection.DisplayString))
                {
                    // load item from xml file
                    Items = TournamentTemplateDataHelper.LoadGameTypes();

                    // create new list box items
                    CreateListBoxItems();

                    // update selected default item if needed
                    if (SelectedItem == "" && Items.Count > 0) SelectedItem = Items[0];

                    // find template manager view model
                    if (Application.Current.Windows.OfType<TemplateManagerView>()?.FirstOrDefault()?.DataContext is not TemplateManagerViewModel templateManagerViewModel) return;

                    // add venue to template manager item list
                    templateManagerViewModel.GameTypes.Remove(selection.DisplayString);
                }
            }
        }
    }
}
