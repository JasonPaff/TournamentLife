using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Syncfusion.UI.Xaml.Utility;
using Syncfusion.Windows.Shared;
using Tournament_Life.Helpers;
using Tournament_Life.ViewModels.Datagrid_ViewModels;
using Tournament_Life.ViewModels.Menu_ViewModels;
using Tournament_Life.ViewModels.Results;
using Tournament_Life.ViewModels.Session_Manager_ViewModels;
using Tournament_Life.ViewModels.Template_Manager_ViewModels;
using Tournament_Life.Views;
using Tournament_Life.Views.Options;
using Tournament_Life.Views.Results;
using Tournament_Life.Views.Session_Manager_Views;
using Tournament_Life.Views.Template_Manager_Views;

namespace Tournament_Life.ViewModels.Options
{
    public class OptionsViewModel : NotificationObject
    {
        private int _selectedFontSize;
        private int _selectedMaxShowInViewSize;
        private bool _showRowHeader;
        private bool _singleRowMode;

        public OptionsViewModel()
        {
            Theme = PreferencesHelper.FindPreference("LiveTracker","Window","Theme");

            // show tournament counter in first row
            ShowRowHeader = bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGrid", "ShowRowHeaders"));

            CreateFontSizes();

            CreateMaxShowInViewSizes();
        }
        public ICommand CancelCommand => new BaseCommand(Cancel);
        public ICommand SaveCommand => new BaseCommand(Save);
        public ICommand MaxShowInViewChangedCommand => new BaseCommand(Save);
        public int FontSize { get; set; }
        public ObservableCollection<int> FontSizes { get; set; }
        public ObservableCollection<int> MaxShowInViewSizes { get; set; }
        public string Theme { get; set; }
        public int SelectedFontSize
        {
            get => _selectedFontSize;
            set
            {
                // update property
                _selectedFontSize = value;
                RaisePropertyChanged(nameof(SelectedFontSize));

                // update preference xml file
                PreferencesHelper.UpdatePreference("LiveTracker", "Window", "FontSize", _selectedFontSize);

                // get some view models
                var dataGridViewModel = Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.TournamentsDataGrid.DataContext as DataGridViewModel;
                var windowViewModel = Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.DataContext as LiveTrackerViewModel;
                var menuViewModel = Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.LiveTrackerMenu.DataContext as MenuViewModel;

                // update property on view models
                dataGridViewModel.FontSize = _selectedFontSize;
                menuViewModel.FontSize = _selectedFontSize;
                windowViewModel.FontSize = _selectedFontSize;

                // check for open template manager and update
                if (Application.Current.Windows.OfType<TemplateManagerView>().FirstOrDefault()?.DataContext is TemplateManagerViewModel templateManagerViewModel) templateManagerViewModel.FontSize = _selectedFontSize;

                // check for open session manager and update
                if (Application.Current.Windows.OfType<SessionManagerView>().FirstOrDefault()?.DataContext is SessionManagerViewModel sessionManagerViewModel) sessionManagerViewModel.FontSize = _selectedFontSize;

                // check for open quick results and update
                if (Application.Current.Windows.OfType<QuickResultsView>().FirstOrDefault()?.DataContext is QuickResultsViewModel quickResultsViewModel) quickResultsViewModel.FontSize = _selectedFontSize;
            }
        }
        public int SelectedMaxSizeInView
        {
            get => _selectedMaxShowInViewSize;
            set
            {
                // update property
                _selectedMaxShowInViewSize = value;
                RaisePropertyChanged(nameof(SelectedMaxSizeInView));

                // update preference in xml file
                PreferencesHelper.UpdatePreference("LiveTracker", "DataGrid", "MaxShowInView", _selectedMaxShowInViewSize);

                // get datagrid view model
                if (Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.TournamentsDataGrid.DataContext is not DataGridViewModel dataGridViewModel) return;

                // update tournament visibility to set new max
                dataGridViewModel.UpdateTournamentVisibility(null);
            }
        }
        public bool SingleRowMode
        {
            get => _singleRowMode;
            set
            {
                // update property
                _singleRowMode = value;
                RaisePropertyChanged(nameof(SingleRowMode));

                // update preference xml file
                PreferencesHelper.UpdatePreference("LiveTracker", "DataGrid", "SingleRowMode", _singleRowMode);

                // data grid view model
                var dataGridViewModel = Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.TournamentsDataGrid.DataContext as DataGridViewModel;

                // update property on view model
                dataGridViewModel.SingleRowMode = _singleRowMode;
            }
        }
        public bool ShowRowHeader
        {
            get => _showRowHeader;
            set
            {
                // update property
                _showRowHeader = value;
                RaisePropertyChanged(nameof(ShowRowHeader));

                // update preference xml file
                PreferencesHelper.UpdatePreference("LiveTracker", "DataGrid", "ShowRowHeaders", _showRowHeader);

                // data grid view model
                var dataGridViewModel = Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.TournamentsDataGrid.DataContext as DataGridViewModel;

                // update property on view model
                dataGridViewModel.ShowRowHeader = _showRowHeader;
            }
        }

        /// <summary>
        /// Cancel any note text and close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Cancel(object parameter)
        {
            // find window
            var window = Application.Current.Windows.OfType<OptionsView>().FirstOrDefault();

            // close window
            window?.Close();
        }

        /// <summary>
        /// create font size options
        /// </summary>
        private void CreateFontSizes()
        {
            FontSizes = new ObservableCollection<int>
            {
                8, 10, 12, 14, 16, 18, 20, 22, 24
            };

            SelectedFontSize =  int.Parse(PreferencesHelper.FindPreference("LiveTracker","Window","FontSize"));
        }

        /// <summary>
        /// create max show in view sizes
        /// </summary>
        private void CreateMaxShowInViewSizes()
        {
            MaxShowInViewSizes = new ObservableCollection<int>
            {
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 75, 100
            };

            SelectedMaxSizeInView = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "DataGrid", "MaxShowInView"));
        }

        /// <summary>
        /// Save any note text and close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Save(object parameter)
        {
            // find window
            var window = Application.Current.Windows.OfType<OptionsView>().FirstOrDefault();

            // close window
            window?.Close();
        }

    }
}
