using LiveTracker.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using LiveTracker.Enums;
using LiveTracker.Models;
using Syncfusion.Windows.Shared;
using System.Collections.ObjectModel;
using LiveTracker.Models.Tournaments;

namespace LiveTracker.ViewModels.Template_Manager_ViewModels
{
    public class TemplateManagerViewModel : NotificationObject
    {
        public bool BovadaCheckBox { get; set; }
        public List<string> DefaultFormats { get; set; }
        public string DefaultGameType { get; set; }
        public string DefaultVenue { get; set; }
        public string FontColor { get; set; }
        public int FontSize { get; set; }
        public List<string> Formats { get; set; }
        public ObservableCollection<TemplateListBoxItem> FormatsList { get; set; }
        public ObservableCollection<string> GameTypes { get; set; }
        public TemplateManagerMode Mode { get; set; }
        public bool Saved { get; set; }
        public TemplateListBoxItem SelectedFormat { get; set; }
        public string SelectedGameType { get; set; }
        public TournamentTemplate SelectedTemplate { get; set; }
        public string SelectedVenue { get; set; }
        public bool ShowRowHeader { get; set; }
        public bool SngCheckBox { get; set; }
        public TournamentTemplate Template { get; set; }
        public ObservableCollection<TournamentTemplate> Templates { get; set; }
        public string Theme { get; set; }
        public string Title { get; set; }
        public ObservableCollection<string> Venues { get; set; }

        public TemplateManagerViewModel()
        {
            // default window prefs
            FontColor = PreferencesHelper.FindPreference("LiveTracker", "Window", "FontColor");
            FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));
            ShowRowHeader = true;
            Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");
            Mode = TemplateManagerMode.New;

            // load default values from xml file
            LoadDefaults();

            // create formats list box items
            CreateFormatListBoxItems();

            // load templates from file
            LoadTemplates();

            // update title bar
            SetTitle();
        }


        /// <summary>
        /// Create list box items out of formats list
        /// </summary>
        public void CreateFormatListBoxItems()
        {
            // new formats list box item collection
            FormatsList = new ObservableCollection<TemplateListBoxItem>();

            // null/zero check
            if(Formats is null || Formats.Count is 0) return;

            // loop through formats
            foreach (var format in Formats)
            {
                if (format.Length is 0) continue;

                // create list box item
                var formatListBoxItem = new TemplateListBoxItem()
                {
                    IsSelected = false,
                    DisplayString = format
                };

                // set default formats to selected
                if(DefaultFormats.Any(i => i == formatListBoxItem.DisplayString)) formatListBoxItem.IsSelected = true;

                // add list box to collection
                if(formatListBoxItem.IsSelected) FormatsList.Insert(0, formatListBoxItem);
                else FormatsList.Add(formatListBoxItem);
            }

            // order so selected is at the top sorted alphabetically and the unselected are alphabetically sorted below
            FormatsList = new ObservableCollection<TemplateListBoxItem>(FormatsList.OrderByDescending(i => i.IsSelected).ThenBy(i => i.DisplayString));
        }

        /// <summary>
        /// set all formats list box items selected to false
        /// </summary>
        public void DeselectFormats()
        {
            // return is selected to false for all formats
            foreach (var item in FormatsList) item.IsSelected = false;
        }

        /// <summary>
        /// Load default values from xml file
        /// </summary>
        public void LoadDefaults()
        {
            Template = TournamentTemplateDataHelper.LoadDefaultValues();

            GameTypes = new ObservableCollection<string>(TournamentTemplateDataHelper.LoadGameTypes());
            Venues = new ObservableCollection<string>(TournamentTemplateDataHelper.LoadVenues());
            Formats = TournamentTemplateDataHelper.LoadFormats();

            DefaultVenue = TournamentTemplateDataHelper.LoadDefaultVenue();
            DefaultGameType = TournamentTemplateDataHelper.LoadDefaultGameType();
            DefaultFormats = TournamentTemplateDataHelper.LoadDefaultFormats();

            // unselect check boxes
            SngCheckBox = false;
            BovadaCheckBox = false;

            // set default venue
            foreach (var venue in Venues) if (venue.Equals(DefaultVenue, StringComparison.OrdinalIgnoreCase)) SelectedVenue = DefaultVenue;

            // set default game type
            foreach (var gameType in GameTypes) if (gameType.Equals(DefaultGameType, StringComparison.OrdinalIgnoreCase)) SelectedGameType = DefaultGameType;
        }

        /// <summary>
        /// load templates from xml file
        /// </summary>
        public void LoadTemplates()
        {
            // load templates from file
            Templates = TournamentTemplateHelper.LoadTemplates();
        }

        /// <summary>
        /// reload the template manager after a profile change
        /// </summary>
        public void Reload()
        {
            // default window prefs
            FontColor = PreferencesHelper.FindPreference("LiveTracker", "Window", "FontColor");
            FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));
            ShowRowHeader = true;
            Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");
            Mode = TemplateManagerMode.New;

            // load default values from xml file
            LoadDefaults();

            // create formats list box items
            CreateFormatListBoxItems();

            // load templates from file
            LoadTemplates();

            // update title bar
            SetTitle();
        }

        /// <summary>
        ///  resets the format list box items to the default selected items
        /// </summary>
        public void ResetToDefaults()
        {
            // reload
            GameTypes = new ObservableCollection<string>(TournamentTemplateDataHelper.LoadGameTypes());
            Venues = new ObservableCollection<string>(TournamentTemplateDataHelper.LoadVenues());
            Formats = TournamentTemplateDataHelper.LoadFormats();

            // set defaults
            DefaultVenue = TournamentTemplateDataHelper.LoadDefaultVenue();
            DefaultGameType = TournamentTemplateDataHelper.LoadDefaultGameType();
            DefaultFormats = TournamentTemplateDataHelper.LoadDefaultFormats();

            // set default venue
            foreach (var venue in Venues) if (venue.Equals(DefaultVenue, StringComparison.OrdinalIgnoreCase)) SelectedVenue = DefaultVenue;

            // set default game type
            foreach (var gameType in GameTypes) if (gameType.Equals(DefaultGameType, StringComparison.OrdinalIgnoreCase)) SelectedGameType = DefaultGameType;

            // unselect any selected formats
            foreach (var item in FormatsList) item.IsSelected = false;

            // set default formats
            foreach (var item in FormatsList) foreach (var format in DefaultFormats) if (item.DisplayString.Equals(format, StringComparison.OrdinalIgnoreCase)) item.IsSelected = true;

            // load default values into template
            Template = TournamentTemplateDataHelper.LoadDefaultValues();

            // set mode to new template
            Mode = TemplateManagerMode.New;

            //reset check boxes
            SngCheckBox = false;
            BovadaCheckBox = false;
        }

        /// <summary>
        /// set window title
        /// </summary>
        public void SetTitle()
        {
            // window title
            Title = "Tournament Manager - " + ProfileHelper.GetCurrentProfile() + " - " + Templates.Count + " Tournaments";
        }

        /// <summary>
        /// update the tournament managers default values, templates and title bar
        /// </summary>
        public void Update()
        {
            // load default values from xml file
            LoadDefaults();

            // load templates from file
            LoadTemplates();

            // update title bar
            SetTitle();
        }

        /// <summary>
        /// update formats list box items
        /// </summary>
        /// <param name="list">items to add</param>
        public void UpdateFormatListBoxItems()
        {
            //// template manager view
            //if (Application.Current.Windows.OfType<TemplateManagerView>()?.FirstOrDefault() is not TemplateManagerView templateManagerView) return;

            //// get selected formats
            //var selected = templateManagerView.Grid.SelectedItems;

            //// mark items as selected
            //foreach (var item in FormatsList.ToList()) foreach (TemplateListBoxItem selectedItem in selected) if(selectedItem.DisplayString == item.DisplayString) item.IsSelected = true;

            //// move selected items to the top of the list box
            //foreach (var f in FormatsList.ToList()) if (f.IsSelected) FormatsList.Move(FormatsList.IndexOf(f), 0);

            //// alphabetize
            //FormatsList.OrderByDescending(i => i.IsSelected).ThenBy(i => i.DisplayString);
        }
    }
}
