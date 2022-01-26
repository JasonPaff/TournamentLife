using LiveTracker.Models;
using Syncfusion.UI.Xaml.Utility;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows;
using Tournament_Life.Views.Session_Manager_Views;
using LiveTracker.Factories;
using LiveTracker.Helpers;
using LiveTracker.Models.Tournaments;
using System.Collections.Generic;
using System;
using Tournament_Life.Views;
using Syncfusion.Windows.Shared;
using LiveTracker.Commands;

namespace Tournament_Life.ViewModels.Session_Manager_ViewModels
{
    public class SessionStartViewModel : NotificationObject
    {
        SessionTemplate _session;
        bool _isAnyRemoved;

        public ICommand CloseCommand => new BaseCommand(CloseWindow);
        public ICommand RemoveCommand => new BaseCommand(Remove);
        public ICommand SaveCommand => new BaseCommand(Save);
        public ICommand ConfirmAlphaCommand => new BaseCommand(ConfirmAlpha);
        public ICommand ConfirmStartCommand => new BaseCommand(ConfirmStart);
        public ICommand SelectToggleCommand => new BaseCommand(SelectToggle);
        public ICommand RemoveToggleCommand => new BaseCommand(RemoveToggle);

        public SessionStartViewModel(SessionTemplate session)
        {
            // copy session
            _session = session;

            // default window preferences
            FontColor = PreferencesHelper.FindPreference("LiveTracker", "Window", "FontColor");
            FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));
            Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");

            // session start options
            ConfirmAlphaToggle = bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "SessionStartConfirmAlpha"));
            ConfirmStartToggle = bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "SessionStartConfirmStart"));
            MultiSelectionToggle = bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "SessionStartMultiToggle"));
            ConfirmRemoveToggle = bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "SessionStartRemoveToggle"));

            if (MultiSelectionToggle is true)
                SelectionMode = "Multiple";
            else
                SelectionMode = "Single";

            // set title
            Title = _session.SessionName;

            // create tournament list box items
            CreateListBoxItems();

            // sort the tournaments
            SortTournaments();
        }

        public bool ConfirmAlphaToggle { get; set; }
        public bool ConfirmRemoveToggle { get; set; }
        public bool ConfirmStartToggle { get; set; }
        public string FontColor { get; set; }
        public int FontSize { get; set; }
        public bool MultiSelectionToggle { get; set; }
        public bool Saved { get; set; }
        public string SelectionMode { get; set; }
        public string Theme { get; set; }
        public string Title { get; set; }
        public ObservableCollection<TemplateListBoxItem> TournamentList { get; set; }

        /// <summary>
        /// close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void CloseWindow(object parameter)
        {
            Application.Current.Windows.OfType<SessionStartView>().FirstOrDefault().Close();
        }

        /// <summary>
        /// change confirmation preference in menu
        /// </summary>
        /// <param name="parameter"></param>
        private void ConfirmAlpha(object parameter)
        {
            // get opposite of saved preference
            var confirm = !bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "SessionStartConfirmAlpha"));

            // update saved preference
            PreferencesHelper.UpdatePreference("LiveTracker", "Window", "SessionStartConfirmAlpha", confirm);

            // sort the tournaments
            SortTournaments();
        }

        /// <summary>
        /// Change confirmation preference in the menu
        /// </summary>
        private void ConfirmStart(object parameter)
        {
            // get opposite of saved preference
            var confirm = !bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "SessionStartConfirmStart"));

            // update saved preference
            PreferencesHelper.UpdatePreference("LiveTracker", "Window", "SessionStartConfirmStart", confirm);
        }

        /// <summary>
        /// Create list box items from the tournaments in the session
        /// </summary>
        private void CreateListBoxItems()
        {
            // get templates from xml file
            var templates = new List<TournamentTemplate>(TournamentTemplateHelper.LoadTemplates().OrderBy(i => i.TournamentName));

            // null/zero check
            if (templates is null || templates.Count is 0)
                return;

            // holds our list box items
            TournamentList = new ObservableCollection<TemplateListBoxItem>();

            foreach(var tournamentId in _session.TemplateIds)
            {
                // get template
                var template = templates.FirstOrDefault(i => i.TemplateId == tournamentId);

                // null, go next
                if (template is null)
                        continue;

                // create list box item for template/tournament
                var item = new TemplateListBoxItem()
                {
                    Description = template.DescriptionWithoutDayMonthYear,
                    DisplayString = $"{template.TournamentName} - {template.Venue}",
                    IsSelected = false,
                    Name = template.TournamentName,
                    StartTime = new DateTime(1111, 1, 1, template.StartTime.Hour, template.StartTime.Minute, 0),
                    TemplateId = template.TemplateId,
                    Venue = template.Venue
                };

                // add list box item to list
                TournamentList.Add(item);
            }
        }

        /// <summary>
        /// remove tournaments from the list
        /// </summary>
        /// <param name="parameter"></param>
        private void Remove(object parameter)
        {
            // nothing selected, leave
            if (TournamentList is null || TournamentList.Any(i => i.IsSelected) is false)
            {
                // create/show error message
                var okayVM1 = new OkViewModel("No tournaments selected", "Nothing Selected");
                var okayWindow1 = new OkView(okayVM1)
                {
                    Owner = Application.Current.Windows.OfType<SessionStartView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                okayWindow1.ShowDialog();

                // leave
                return;
            }

            // view model for remove tournaments message
            var vm1 = new YesNoViewModel("Are you sure you want to remove the selected tournaments?", "Remove Tournaments");

            // change message if only 1 tournament vs multiple being removed
            if (TournamentList.Where(i => i.IsSelected).Count() is 1)
                vm1 = new YesNoViewModel("Are you sure you want to remove the selected tournament?", "Remove Tournament");

            // show message, if flagged
            if (bool.Parse(PreferencesHelper.FindPreference("LiveTracker","Window","SessionStartRemoveToggle")))
            {
                var window1 = new YesNoView(vm1)
                {
                    Owner = Application.Current.Windows.OfType<SessionStartView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                window1.ShowDialog();

                if (vm1.Saved is false)
                    return;
            }

            // get selected tournaments
            var selected = TournamentList.Where(i => i.IsSelected);

            // remove selected tournaments from session
            foreach (var item in TournamentList.Where(i => i.IsSelected).ToList())
            {
                TournamentList.Remove(item);
                _session.TemplateIds?.Remove(item.TemplateId);
            }

            _isAnyRemoved = true;
        }

        /// <summary>
        /// Change confirmation preference in the menu
        /// </summary>
        private void RemoveToggle(object parameter)
        {
            // get opposite of saved preference
            var confirm = !bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "SessionStartRemoveToggle"));

            // update saved preference
            PreferencesHelper.UpdatePreference("LiveTracker", "Window", "SessionStartRemoveToggle", confirm);
        }

        /// <summary>
        /// save and close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Save(object parameter)
        {
          // flag session list as having been saved
          Saved = true;

          // confirm message, if flagged for it
          if (bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "SessionStartConfirmStart")))
          {
            // confirm message
            var vm1 = new YesNoViewModel("Are you sure you want to start the session?", "Confirm Start");
            var window1 = new YesNoView(vm1)
            {
                Owner = Application.Current.Windows.OfType<SessionStartView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            window1.ShowDialog();

            // said no, exit
            if (vm1.Saved is false)
                    return;
          }

          // start session tournaments
          TournamentFactory.StartTournaments(_session);

          if (_isAnyRemoved)
            MenuCommands.GetMenuViewModel()?.UpdateFavoriteSessions();

          // close window
          CloseWindow(null);
        }

        /// <summary>
        /// Change selection mode preference
        /// </summary>
        private void SelectToggle(object parameter)
        {
            // get opposite of saved preference
            var selection = !bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "SessionStartMultiToggle"));

            // update saved preference
            PreferencesHelper.UpdatePreference("LiveTracker", "Window", "SessionStartMultiToggle", selection);

            // update list box selection mode
            if (selection)
                Application.Current.Windows.OfType<SessionStartView>().FirstOrDefault().TournamentList.SelectionMode = System.Windows.Controls.SelectionMode.Multiple;
            else
                Application.Current.Windows.OfType<SessionStartView>().FirstOrDefault().TournamentList.SelectionMode = System.Windows.Controls.SelectionMode.Single;
        }

        /// <summary>
        /// sort the tournament list alphabetically
        /// </summary>
        private void SortTournaments()
        {
            // null check
            if (TournamentList is null)
                return;

            // get sort value
            var alphaSort = bool.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "SessionStartConfirmAlpha"));

            // sort by start time or name
            if (alphaSort)
                TournamentList = new ObservableCollection<TemplateListBoxItem>(TournamentList.OrderBy(i => i.Name));
            else
                TournamentList = new ObservableCollection<TemplateListBoxItem>(TournamentList.OrderBy(i => i.StartTime));
        }
    }
}