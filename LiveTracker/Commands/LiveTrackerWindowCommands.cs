using System.Linq;
using System.Windows;
using System.Windows.Input;
using Syncfusion.UI.Xaml.Utility;
using Tournament_Life.Helpers;
using Tournament_Life.ViewModels;
using Tournament_Life.ViewModels.Datagrid_ViewModels;
using Tournament_Life.ViewModels.Graph_ViewModels;
using Tournament_Life.ViewModels.Menu_ViewModels;
using Tournament_Life.ViewModels.Results;
using Tournament_Life.ViewModels.Session_Manager_ViewModels;
using Tournament_Life.ViewModels.Template_Manager_ViewModels;
using Tournament_Life.Views;
using Tournament_Life.Views.Graph_Views;
using Tournament_Life.Views.Menu_Views;
using Tournament_Life.Views.Results;
using Tournament_Life.Views.Session_Manager_Views;
using Tournament_Life.Views.Template_Manager_Views;

namespace Tournament_Life.Commands
{
    public static class LiveTrackerWindowCommands
    {
        public static ICommand MenuItem => new BaseCommand(MenuItemCommand);

        /// <summary>
        /// Parse the menu item clicked parameter into method calls
        /// </summary>
        /// <param name="parameter">menu item that was clicked name parameter</param>
        private static void MenuItemCommand(object parameter)
        {
            if(parameter is null)
                return;

            switch (parameter as string)
            {
                case "IsDarkMode":
                    ChangeDarkMode();
                    break;
            }
        }

        /// <summary>
        /// User check/unchecked the dark mode check box in the title bar
        /// </summary>
        private static void ChangeDarkMode()
        {
            // grab live tracker window
            if (Application.Current.Windows.OfType<LiveTrackerWindowView>()?.FirstOrDefault()?.DataContext is not LiveTrackerViewModel liveTrackerViewModel)
                return;

            // grab data grid view model
            if (Application.Current.Windows.OfType<LiveTrackerWindowView>()?.FirstOrDefault()?.TournamentsDataGrid?.DataContext is not DataGridViewModel dataGridViewModel)
                return;

            // grab menu view model
            if (Application.Current.Windows.OfType<LiveTrackerWindowView>()?.FirstOrDefault()?.LiveTrackerMenu?.DataContext is not MenuViewModel menuViewModel)
                return;

            // update is dark mode variable
            liveTrackerViewModel.IsDarkMode = bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "IsDarkMode"));

            // change theme to dark mode if set to dark mode
            if(liveTrackerViewModel.IsDarkMode) liveTrackerViewModel.Theme = "MaterialDark";
            if(liveTrackerViewModel.IsDarkMode is false) liveTrackerViewModel.Theme = "MaterialLight";

            // update theme preference
            PreferencesHelper.UpdatePreference("LiveTracker", "Window", "Theme", liveTrackerViewModel.Theme);

            // update font color preference
            if(liveTrackerViewModel.IsDarkMode) PreferencesHelper.UpdatePreference("LiveTracker", "Window", "FontColor", "White");
            if (liveTrackerViewModel.IsDarkMode is false)
                PreferencesHelper.UpdatePreference("LiveTracker", "Window", "FontColor", "Black");

            // update font colors
            dataGridViewModel.FontColor = "White";
            if (liveTrackerViewModel.IsDarkMode is false)
                dataGridViewModel.FontColor = "Black";

            // update font colors
            menuViewModel.FontColor = PreferencesHelper.FindPreference("LiveTracker", "Window", "FontColor");

            // update font color preference
            PreferencesHelper.UpdatePreference("LiveTracker", "Window", "FontColor", dataGridViewModel.FontColor);

            // update template manager if open
            if(Application.Current.Windows.OfType<TemplateManagerView>().Any() && Application.Current.Windows.OfType<TemplateManagerView>()?.FirstOrDefault()?.DataContext is TemplateManagerViewModel templateManagerViewModel)
                {
                    templateManagerViewModel.Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");
                    templateManagerViewModel.FontColor = PreferencesHelper.FindPreference("LiveTracker", "Window", "FontColor");
                    templateManagerViewModel.FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));
                }

            // update session manager if open
            if (Application.Current.Windows.OfType<SessionManagerView>().Any())
                if (Application.Current.Windows.OfType<SessionManagerView>()?.FirstOrDefault()?.DataContext is SessionManagerViewModel sessionManagerViewModel)
                { sessionManagerViewModel.Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");  sessionManagerViewModel.FontColor = PreferencesHelper.FindPreference("LiveTracker", "Window", "FontColor"); sessionManagerViewModel.FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));}

            // update quick results if open
            if (Application.Current.Windows.OfType<QuickResultsView>().Any())
                if (Application.Current.Windows.OfType<QuickResultsView>()?.FirstOrDefault()?.DataContext is QuickResultsViewModel quickResultsViewModel)
                { quickResultsViewModel.Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme"); quickResultsViewModel.FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize")); }

            // update session results if open
            if (Application.Current.Windows.OfType<SessionResultsView>().Any())
                if (Application.Current.Windows.OfType<SessionResultsView>()?.FirstOrDefault()?.DataContext is SessionResultsViewModel sessionResultsViewModel)
                { sessionResultsViewModel.Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");  sessionResultsViewModel.FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));}

            // update tournaments results if open
            if (Application.Current.Windows.OfType<TournamentsResultsView>().Any())
                if (Application.Current.Windows.OfType<TournamentsResultsView>()?.FirstOrDefault()?.DataContext is TournamentsResultsViewModel tournamentsResultsViewModel)
                { tournamentsResultsViewModel.Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme"); tournamentsResultsViewModel.FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));}

            // update select tournaments if open
            if (Application.Current.Windows.OfType<SelectTemplatesView>().Any())
                if (Application.Current.Windows.OfType<SelectTemplatesView>()?.FirstOrDefault()?.DataContext is SelectTemplatesViewModel selectTemplatesViewModel)
                { selectTemplatesViewModel.Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme"); selectTemplatesViewModel.FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize")); }

            var theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");
            var fontColor = PreferencesHelper.FindPreference("LiveTracker", "Window", "FontColor");
            var fontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));

            // update any tournament profit graphs that are open
            foreach (var graph in Application.Current.Windows.OfType<TournamentProfitGraphView>())
            {
                if (graph.DataContext is not TournamentProfitGraphViewModel vm) continue;

                vm.Theme = theme;
                vm.FontColor = fontColor;
                vm.FontSize = fontSize;
            }

            // update any no label tournament profit graphs that are open
            foreach (var graph in Application.Current.Windows.OfType<TournamentProfitGraphNoLabelView>())
            {
                if (graph.DataContext is not TournamentProfitGraphNoLabelViewModel vm) continue;

                vm.Theme = theme;
                vm.FontColor = fontColor;
                vm.FontSize = fontSize;
            }

            // update any finish position graphs that are open
            foreach (var graph in Application.Current.Windows.OfType<FinishPositionGraphView>())
            {
                if (graph.DataContext is not FinishPositionGraphViewModel vm) continue;

                vm.Theme = theme;
                vm.FontColor = fontColor;
                vm.FontSize = fontSize;
            }

            // update any session profit graphs that are open
            foreach (var graph in Application.Current.Windows.OfType<SessionProfitGraphView>())
            {
                if (graph.DataContext is not SessionProfitGraphViewModel vm) continue;

                vm.Theme = theme;
                vm.FontColor = fontColor;
                vm.FontSize = fontSize;
            }

            // update any no label session profit graphs that are open
            foreach (var graph in Application.Current.Windows.OfType<SessionProfitGraphNoLabelView>())
            {
                if (graph.DataContext is not SessionProfitGraphNoLabelViewModel vm) continue;

                vm.Theme = theme;
                vm.FontColor = fontColor;
                vm.FontSize = fontSize;
            }

            // update any tournament buy-in roi graphs that are open
            foreach (var graph in Application.Current.Windows.OfType<TournamentBuyinRoiHistogramView>())
            {
                if (graph.DataContext is not TournamentBuyinRoiHistogramViewModel vm) continue;

                vm.Theme = theme;
                vm.FontColor = fontColor;
                vm.FontSize = fontSize;
            }

            // update any tournament format roi graphs that are open
            foreach (var graph in Application.Current.Windows.OfType<TournamentFormatRoiChartView>())
            {
                if (graph.DataContext is not TournamentFormatRoiChartViewModel vm) continue;

                vm.Theme = theme;
                vm.FontColor = fontColor;
                vm.FontSize = fontSize;
            }

            // update any tournament game type roi graphs that are open
            foreach (var graph in Application.Current.Windows.OfType<TournamentGameTypeRoiChartView>())
            {
                if (graph.DataContext is not TournamentGameTypeRoiChartViewModel vm) continue;

                vm.Theme = theme;
                vm.FontColor = fontColor;
                vm.FontSize = fontSize;
            }

            // update any tournament venue roi graphs that are open
            foreach (var graph in Application.Current.Windows.OfType<TournamentVenueRoiChartView>())
            {
                if (graph.DataContext is not TournamentVenueRoiChartViewModel vm) continue;

                vm.Theme = theme;
                vm.FontColor = fontColor;
                vm.FontSize = fontSize;
            }

            // update any tournaments views that are open
            foreach (var window in Application.Current.Windows.OfType<TournamentsView>())
            {
                if (window.DataContext is not TournamentsViewModel vm) continue;

                vm.Theme = theme;
                vm.FontColor = fontColor;
                vm.FontSize = fontSize;
            }
        }
    }
}
