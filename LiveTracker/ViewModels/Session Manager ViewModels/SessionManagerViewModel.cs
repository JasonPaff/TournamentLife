using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Syncfusion.UI.Xaml.Utility;
using Syncfusion.Windows.Shared;
using Tournament_Life.Enums;
using Tournament_Life.Helpers;
using Tournament_Life.Models.Sessions;
using Tournament_Life.Models.Tournaments;

namespace Tournament_Life.ViewModels.Session_Manager_ViewModels
{
    public class SessionManagerViewModel : NotificationObject
    {
        private int _sessionId;
        public ICommand TextChangedCommand => new BaseCommand(TextChanged);

        public SessionManagerViewModel(ObservableCollection<int> templateList = null)
        {
            // default window prefs
            FontColor = PreferencesHelper.FindPreference("LiveTracker", "Window", "FontColor");
            FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));
            Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");

            // set mode to new session
            Mode = SessionManagerMode.New;

            // session name to blank
            SessionName = "";

            // search box to blank
            SearchText = "";

            // session list collection
            SessionList = new ObservableCollection<SessionListBoxItem>();

            // get templates from xml file
            Templates = new List<TournamentTemplate>(TournamentTemplateHelper.LoadTemplates().OrderBy(i => i.TournamentName));

            // create template list box items
            TemplateList = CreateTemplateListBoxItems();

            // load venues combo box initial values
            LoadVenues();

            // update title
            SetTitle();

            // if not creating from running, quit
            if (templateList is null) return;

            // create session list box items
            CreateSessionListBoxItems(templateList);
        }

        public string FontColor { get; set; }
        public int FontSize { get; set; }
        public SessionManagerMode Mode { get; set; }
        public string SearchText { get; set; }
        public string SelectedVenue { get; set; }
        public ObservableCollection<SessionListBoxItem> SessionList { get; set; }
        public string SessionName { get; set; }
        public List<TournamentTemplate> Templates { get; set; }
        public ObservableCollection<TemplateListBoxItem> TemplateList { get; set; }
        public string Theme { get; set; }
        public string Title { get; set; }
        public ObservableCollection<string> Venues { get; set; }

        /// <summary>
        /// Clear the selected tournaments in the tournaments list
        /// </summary>
        public void ClearSelectedTournaments()
        {
            SearchText = "";

            foreach (var item in TemplateList)
                if (item.IsSelected)
                    item.IsSelected = false;

            var temp = new ObservableCollection<SessionListBoxItem>();
            foreach (var item in SessionList)
            {
                item.IsSelected = false;
                temp.Add(item);
            }

            SessionList = temp;
        }

        /// <summary>
        /// create session list box items for passed in tournaments
        /// </summary>
        public void CreateSessionListBoxItems(ObservableCollection<int> templateList)
        {
            // loop through selected tournaments in the template list
            foreach (var id in templateList)
            {
                // load tournament
                var tournament = TournamentTemplateHelper.LoadTemplate(id);

                // create session list box item from tournament
                var item = new SessionListBoxItem()
                {
                    Description = tournament.Description,
                    DisplayString = tournament.TournamentName + " - " + tournament.Venue,
                    IsSelected = false,
                    Name = tournament.TournamentName,
                    StartTime = tournament.StartTime,
                    TemplateId = tournament.TemplateId
                };

                // add tournament to session list
                SessionList.Add(item);
            }

            // sort session list by starting time
            SessionList = SessionTemplateHelper.SortListBoxItemsByStartTime(SessionList);
        }

        /// <summary>
        /// create a session template from the data
        /// </summary>
        /// <returns>session template</returns>
        public SessionTemplate CreateSessionTemplate()
        {
            // create session with name
            var session = new SessionTemplate()
            {
                SessionName = this.SessionName
            };

            // new id for non-edits
            if (Mode is SessionManagerMode.New || Mode is SessionManagerMode.Copy) session.SessionId = SessionTemplateHelper.NewSessionId();

            // saved id for edits
            if (Mode is SessionManagerMode.Edit) session.SessionId = _sessionId;

            // add templates to session
            foreach (var item in SessionList) session.AddTemplateToSession(item.TemplateId);

            // update template description
            session.UpdateTemplatesDescription(new ObservableCollection<TournamentTemplate>(Templates));

            // return session with tournaments sorted by start time
            return SessionTemplateHelper.SortTournamentsByStartTime(new ObservableCollection<TournamentTemplate>(Templates), session);
        }

        /// <summary>
        /// create the template list box items from the templates
        /// </summary>
        private ObservableCollection<TemplateListBoxItem> CreateTemplateListBoxItems()
        {
            // create collection
            var list = new ObservableCollection<TemplateListBoxItem>();

            // null/zero check
            if (Templates is null || Templates.Count is 0) return list;

            // loop templates
            foreach (var template in Templates)
            {
                // create list box item
                var item = new TemplateListBoxItem()
                {
                    Description = template.DescriptionWithoutDayMonthYear,
                    DisplayString = template.TournamentName + " - " + template.Venue,
                    IsSelected = false,
                    Name = template.TournamentName,
                    StartTime = new DateTime(1111, 1, 1, template.StartTime.Hour, template.StartTime.Minute, 0),
                    TemplateId = template.TemplateId,
                    Venue = template.Venue
                };

                // add item to collection
                list.Add(item);
            }

            // return template list box item list
            return list;
        }

        /// <summary>
        /// load a session template into the view
        /// </summary>
        public void LoadSession(int sessionId)
        {
            _sessionId = sessionId;

            SessionList.Clear();

            var session = SessionTemplateHelper.LoadSessionTemplate(sessionId, new ObservableCollection<TournamentTemplate>(Templates));

            foreach (var id in session.TemplateIds)
            {
                var tournament = TournamentTemplateHelper.LoadTemplate(id);

                var item = new SessionListBoxItem()
                {
                    Description = tournament.Description,
                    DisplayString = tournament.TournamentName + " - " + tournament.Venue,
                    IsSelected = false,
                    Name = tournament.TournamentName,
                    StartTime = tournament.StartTime,
                    TemplateId = tournament.TemplateId
                };

                SessionList.Add(item);
            }

            SessionList = SessionTemplateHelper.SortListBoxItemsByStartTime(SessionList);
        }

        /// <summary>
        /// Load the venues combo box
        /// </summary>
        private void LoadVenues()
        {
            // get venues from tournament template data file
            Venues = new ObservableCollection<string>(TournamentTemplateDataHelper.LoadVenues());

            // insert all venues option
            Venues.Insert(0, "All Venues");

            // set select venue to all venues
            SelectedVenue = "All Venues";
        }

        /// <summary>
        /// Reload the session manager after a profile change
        /// </summary>
        public void Reload()
        {
            // default window prefs
            FontColor = PreferencesHelper.FindPreference("LiveTracker", "Window", "FontColor");
            FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));
            Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");

            // set mode to new session
            Mode = SessionManagerMode.New;

            // session name to blank
            SessionName = "";

            // search text to blank
            SearchText = "";

            // selected venue to all venues
            SelectedVenue = "All Venues";

            // session list collection
            SessionList = new ObservableCollection<SessionListBoxItem>();

            // get templates from xml file
            Templates = new List<TournamentTemplate>(TournamentTemplateHelper.LoadTemplates().OrderBy(i => i.TournamentName));

            // create template list box items
            TemplateList = CreateTemplateListBoxItems();

            // update title
            SetTitle();
        }

        /// <summary>
        /// set window title
        /// </summary>
        public void SetTitle()
        {
            Title = "Session Manager - " + ProfileHelper.GetCurrentProfile();
        }

        /// <summary>
        /// called when the user type in the text box
        /// filters the tournaments list
        /// </summary>
        /// <param name="parameter"></param>
        private void TextChanged(object parameter)
        {
            // new temporary list of list box items
            var tempList = CreateTemplateListBoxItems();

            // filter by venue
            if (SelectedVenue is not "All Venues") tempList = new ObservableCollection<TemplateListBoxItem>(tempList.Where(i => i.Venue.Equals(SelectedVenue, StringComparison.OrdinalIgnoreCase)));

            // filter by name
            tempList = new ObservableCollection<TemplateListBoxItem>(tempList.Where(i => i.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase)));

            // set temporary list to the main list, if it changed
            if (tempList.Count != TemplateList.Count) TemplateList = tempList;
        }

        /// <summary>
        /// update templates and list box items
        /// </summary>
        public void UpdateTemplates()
        {
            // reload templates from file
            Templates = new List<TournamentTemplate>(TournamentTemplateHelper.LoadTemplates());

            // update list box items
            TemplateList = CreateTemplateListBoxItems();

            // check for deleted templates already in the session list and remove them
            foreach (var item in SessionList.ToList()) if (Templates.Any(i => i.TemplateId == item.TemplateId) is false) SessionList.Remove(item);
        }
    }
}