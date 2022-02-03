using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Syncfusion.UI.Xaml.Utility;
using Syncfusion.Windows.Shared;
using Tournament_Life.Helpers;
using Tournament_Life.Models.Tournaments;
using Tournament_Life.Views;
using Tournament_Life.Views.Template_Manager_Views;

namespace Tournament_Life.ViewModels.Template_Manager_ViewModels
{
    public class EditFormatsViewModel : NotificationObject
    {
        private string _selectedItem;
        public ICommand AddCommand => new BaseCommand(Add);
        public ICommand CancelCommand => new BaseCommand(Cancel);
        public ICommand RemoveCommand => new BaseCommand(Remove);
        public ICommand SetDefaultsCommand => new BaseCommand(SetDefaults);

        public EditFormatsViewModel()
        {
            // font size
            FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));

            // visual theme
            Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");

            // load formats from xml file
            Items = TournamentTemplateDataHelper.LoadFormats();

            // create list box items from formats
            CreateListBoxItems();
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
                TournamentTemplateDataHelper.SetDefaultFormats(GetSelectedFormats());
            }
        }
        public string Theme { get; set; }

        /// <summary>
        /// Add a new format to the xml file
        /// </summary>
        /// <param name="parameter"></param>
        private void Add(object parameter)
        {
            // null/zero check
            if(AddText is null || AddText is "") return;

            var addText = AddText.Trim();
            AddText = "";

            // null/blank check
            if (addText is null || addText is "") return;

            // add format, doesn't add if duplicate
            if (!TournamentTemplateDataHelper.AddFormat(addText)) return;

            // load venues from xml file
            Items = TournamentTemplateDataHelper.LoadFormats();

            // create new list box items
            CreateListBoxItems();

            // find template manager view model
            if (Application.Current.Windows.OfType<TemplateManagerView>()?.FirstOrDefault()?.DataContext is not TemplateManagerViewModel templateManagerViewModel) return;

            // add format to template manager formats list
            templateManagerViewModel.Formats.Add(addText);

            // update format list box items
            templateManagerViewModel.CreateFormatListBoxItems();
        }

        /// <summary>
        /// close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Cancel(object parameter)
        {
            // find window
            var window = Application.Current.Windows.OfType<EditFormatsView>().FirstOrDefault();

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

            // loop through formats
            foreach (var format in Items.OrderBy(i => i))
            {
                // create list box item
                var item = new TemplateListBoxItem()
                {
                    DisplayString = format,
                };

                // add list box item to collection
                ItemList.Add(item);
            }
        }

        /// <summary>
        /// get the selected formats from the list box
        /// </summary>
        /// <returns>list of selected formats</returns>
        private List<string> GetSelectedFormats()
        {
            // empty list
            var list = new List<string>();

            // add selected formats
            foreach (var item in ItemList) if (item.IsSelected)  list.Add(item.DisplayString);

            // return list
            return list;
        }

        /// <summary>
        /// remove a format
        /// <param name="parameter"></param>
        private void Remove(object parameter)
        {
            // get selected items
            var selected = new ObservableCollection<TemplateListBoxItem>(ItemList.Where(i => i.IsSelected));

            // null/zero check
            if (selected is null || selected.Count is 0) return;

            // find template manager view model
            if (Application.Current.Windows.OfType<TemplateManagerView>()?.FirstOrDefault()?.DataContext is not TemplateManagerViewModel templateManagerViewModel) return;

            // loop selected items
            foreach (var selection in selected)
            {
                // remove item, can fail
                if (TournamentTemplateDataHelper.RemoveFormat(selection.DisplayString))
                {
                    // load item from xml file
                    Items = TournamentTemplateDataHelper.LoadFormats();

                    // create new list box items
                    CreateListBoxItems();

                    // update selected default item if needed
                    if (SelectedItem == "" && Items.Count > 0) SelectedItem = Items[0];

                    // remove format from template manager item list
                    templateManagerViewModel.Formats.Remove(selection.DisplayString);
                }
            }

            // update format list box items
            templateManagerViewModel.CreateFormatListBoxItems();
        }

        /// <summary>
        /// set selected formats as defaults in xml file
        /// </summary>
        /// <param name="parameter"></param>
        private void SetDefaults(object parameter)
        {
            // update xml file with new default formats
            TournamentTemplateDataHelper.SetDefaultFormats(GetSelectedFormats());

            // ok view model
            var vm = new OkViewModel("Save Successful!", "Saved");

            // create/show ok window
            var window = new OkView(vm)
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            window.ShowDialog();

            // find template manager view model
            if (Application.Current.Windows.OfType<TemplateManagerView>()?.FirstOrDefault()?.DataContext is not TemplateManagerViewModel templateManagerViewModel) return;

            // update formats list and default formats list
            templateManagerViewModel.Formats = TournamentTemplateDataHelper.LoadFormats();
            templateManagerViewModel.DefaultFormats = TournamentTemplateDataHelper.LoadDefaultFormats();
        }
    }
}
