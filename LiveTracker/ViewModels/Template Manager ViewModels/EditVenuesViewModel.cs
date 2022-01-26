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
    public class EditVenuesViewModel : NotificationObject
    {
        private string _selectedItem;
        public ICommand AddCommand => new BaseCommand(Add);
        public ICommand CancelCommand => new BaseCommand(Cancel);
        public ICommand RemoveCommand => new BaseCommand(Remove);

        public EditVenuesViewModel()
        {
            // font size
            FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));

            // visual theme
            Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");

            // load venues from xml file
            Items = TournamentTemplateDataHelper.LoadVenues();

            // create list box items from venues
            CreateListBoxItems();

            // get default venue
            var defaultVenue = TournamentTemplateDataHelper.LoadDefaultVenue();
            if(Items.Contains(defaultVenue)) SelectedItem = defaultVenue;
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
                if(_selectedItem == value) return;

                _selectedItem = value;
                RaisePropertyChanged(nameof(SelectedItem));
                TournamentTemplateDataHelper.SetDefaultVenue(_selectedItem);
            }
        }
        public string Theme { get; set; }

        /// <summary>
        /// Add a new venue to the xml file
        /// </summary>
        /// <param name="parameter"></param>
        private void Add(object parameter)
        {
            // null/blank check
            if(AddText is null || AddText.Trim() is "") return;

            var addText = AddText.Trim();
            AddText = "";

            // add venue, doesn't add if duplicate
            if (!TournamentTemplateDataHelper.AddVenue(addText)) return;

            // load venues from xml file
            Items = TournamentTemplateDataHelper.LoadVenues();

            // create new list box items
            CreateListBoxItems();

            // find template manager view model
            if (Application.Current.Windows.OfType<TemplateManagerView>()?.FirstOrDefault()?.DataContext is not TemplateManagerViewModel templateManagerViewModel) return;

            // add venue to template manager venues list
            templateManagerViewModel.Venues.Add(addText);
        }

        /// <summary>
        /// close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Cancel(object parameter)
        {
            // find window
            var window = Application.Current.Windows.OfType<EditVenuesView>().FirstOrDefault();

            // close window
            window?.Close();
        }

        /// <summary>
        /// Create list box items from venues
        /// </summary>
        private void CreateListBoxItems()
        {
            // holds our list box items
            ItemList = new ObservableCollection<TemplateListBoxItem>();

            // null/zero check
            if(Items is null || Items.Count is 0) return;

            // loop through venues
            foreach(var venue in Items.OrderBy(i => i))
            {
                // create list box item
                var item = new TemplateListBoxItem()
                {
                    DisplayString = venue,
                };

                // add list box item to collection
                ItemList.Add(item);
            }
        }

        /// <summary>
        /// remove a venue
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
                // remove venue, can fail
                if(TournamentTemplateDataHelper.RemoveVenue(selection.DisplayString))
                {
                    // load venues from xml file
                    Items = TournamentTemplateDataHelper.LoadVenues();

                    // create new list box items
                    CreateListBoxItems();

                    // update selected default venue if needed
                    if(SelectedItem == "" && Items.Count > 0) SelectedItem = Items[0];

                    // find template manager view model
                    if (Application.Current.Windows.OfType<TemplateManagerView>()?.FirstOrDefault()?.DataContext is not TemplateManagerViewModel templateManagerViewModel) return;

                    // add venue to template manager venues list
                    templateManagerViewModel.Venues.Remove(selection.DisplayString);
                }
            }
        }
    }
}
