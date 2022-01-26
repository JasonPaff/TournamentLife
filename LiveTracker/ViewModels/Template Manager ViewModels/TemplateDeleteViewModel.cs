using System;
using System.Collections.Generic;
using System.Linq;
using LiveTracker.Helpers;
using Syncfusion.Windows.Shared;
using System.Windows.Input;
using Syncfusion.UI.Xaml.Utility;
using System.Windows;
using LiveTracker.Views.Template_Manager_Views;
using System.Collections.ObjectModel;
using LiveTracker.Models;
using LiveTracker.Models.Tournaments;
using System.Globalization;
using LiveTracker.Views;
using LiveTracker.ViewModels.Menu_ViewModels;
using LiveTracker.ViewModels.Datagrid_ViewModels;
using LiveTracker.Views.Session_Manager_Views;
using LiveTracker.ViewModels.Session_Manager_ViewModels;
using Tournament_Life.ViewModels;
using Tournament_Life.Views;

namespace LiveTracker.ViewModels.Template_Manager_ViewModels
{
    public class TemplateDeleteViewModel : NotificationObject
    {
        public ICommand CancelCommand => new BaseCommand(Cancel);
        public ICommand ExportCommand => new BaseCommand(Delete);

        public TemplateDeleteViewModel()
        {
            // font size
            FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));

            // visual theme
            Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");

            // loading label
            LoadingLabel = false;

            // find template manager view model
            if (Application.Current.Windows.OfType<TemplateManagerView>()?.FirstOrDefault()?.DataContext is not TemplateManagerViewModel templateManagerViewModel) return;

            // grab templates from template manager
            Templates = new List<TournamentTemplate>(templateManagerViewModel.Templates);

            // create list box items from templates
            CreateListBoxItems();
        }

        public int FontSize { get; set; }
        public bool LoadingLabel { get; set; }
        public bool Saved { get; set; }
        public List<TournamentTemplate> Templates { get; set; }
        public ObservableCollection<TemplateListBoxItem> TemplatesList { get; set; }
        public string Theme { get; set; }

        /// <summary>
        /// close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Cancel(object parameter)
        {
            // find window
            var window = Application.Current.Windows.OfType<TemplateDeleteView>().FirstOrDefault();

            // close window
            window?.Close();
        }

        /// <summary>
        /// Delete the selected tournaments
        /// </summary>
        /// <param name="parameter"></param>
        private void Delete(object parameter)
        {
            // get selected list box items
            var selected = TemplatesList.Where(i => i.IsSelected);

            // null/zero check
            if (selected is null || selected.Count() is 0) return;

            // yes/no view model
            var vm = new YesNoViewModel("Are you sure you want to delete these tournaments?", "Delete Tournaments");

            // create/show yes/no window
            var window = new YesNoView(vm)
            {
                Owner = Application.Current.Windows.OfType<TemplateImportView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            window.ShowDialog();

            // not saved, exit
            if (vm.Saved is false) return;

            // show loading label
            LoadingLabel = true;

            // loop through selected tournaments
            foreach (var tournament in selected.ToList())
            {
                // remove tournament from any sessions
                SessionTemplateHelper.RemoveTemplateFromSessions(Templates.FirstOrDefault(i => i.TemplateId == tournament.TemplateId));

                // delete tournament from xml file
                TournamentTemplateHelper.DeleteTournamentTemplate(Templates.FirstOrDefault(i => i.TemplateId == tournament.TemplateId));

                // remove tournament from list box collection
                TemplatesList.Remove(tournament);
            }

            // find template manager view model
            if (Application.Current.Windows.OfType<TemplateManagerView>()?.FirstOrDefault()?.DataContext is TemplateManagerViewModel templateManagerViewModel)
            {
                // reload template manager templates
                templateManagerViewModel.LoadTemplates();

                // update template manager title
                templateManagerViewModel.SetTitle();
            }

            // find menu view model
            if (Application.Current.Windows.OfType<LiveTrackerWindowView>()?.FirstOrDefault()?.LiveTrackerMenu?.DataContext is MenuViewModel menuViewModel)
            {
                // reload menu view model templates
                menuViewModel.UpdateFavoriteTemplates();

                // reload view model sessions
                menuViewModel.UpdateFavoriteSessions();
            }

            // data grid view model
            if (Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.TournamentsDataGrid?.DataContext is DataGridViewModel dataGridViewModel) dataGridViewModel.UpdateRunningTournaments();

            // get session manager view model
            if (Application.Current.Windows.OfType<SessionManagerView>().FirstOrDefault()?.DataContext is SessionManagerViewModel sessionManagerViewModel) sessionManagerViewModel.UpdateTemplates();

            // remove loading label
            LoadingLabel = false;
        }

        /// <summary>
        ///  create template list box items from templates
        /// </summary>
        private void CreateListBoxItems()
        {
            // templates list box item list
            TemplatesList = new ObservableCollection<TemplateListBoxItem>();

            // loop through templates
            foreach (var template in Templates)
            {
                var item = new TemplateListBoxItem()
                {
                    Description = template.DescriptionWithoutDayMonthYear,
                    DisplayString = template.Venue + " - " + template.BuyinTotalCost.ToString("C2", new CultureInfo("en-US")) + " - " + template.TournamentName,
                    IsSelected = false,
                    TemplateId = template.TemplateId
                };

                TemplatesList.Add(item);
            }
        }
    }
}
