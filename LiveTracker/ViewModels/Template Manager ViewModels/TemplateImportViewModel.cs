using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using Syncfusion.UI.Xaml.Utility;
using Syncfusion.Windows.Shared;
using Tournament_Life.Helpers;
using Tournament_Life.Models.Tournaments;
using Tournament_Life.Views;
using Tournament_Life.Views.Template_Manager_Views;

namespace Tournament_Life.ViewModels.Template_Manager_ViewModels
{
    public class TemplateImportViewModel : NotificationObject
    {
        public ICommand CancelCommand => new BaseCommand(Cancel);
        public ICommand ImportCommand => new BaseCommand(ImportTemplate);
        public ICommand ImportFileCommand => new BaseCommand(Import);

        public TemplateImportViewModel()
        {
            // font size
            FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));

            // visual theme
            Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");

            CheckForDuplicates = true;
            LoadingLabel = false;
        }

        public bool CheckForDuplicates { get; set; }
        public int FontSize { get; set; }
        public bool LoadingLabel { get; set; }
        public bool Saved { get; set; }
        public List<TournamentTemplate> Templates { get; set; }
        public ObservableCollection<TemplateListBoxItem> TemplatesList { get; set; }
        public string Theme { get; set; }
        public bool WindowLock { get; set; }

        /// <summary>
        /// close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Cancel(object parameter)
        {
            Application.Current.Windows.OfType<TemplateImportView>().FirstOrDefault()?.Close();
        }

        /// <summary>
        /// Export selected templates
        /// </summary>
        /// <param name="parameter"></param>
        private void Import(object parameter)
        {
            // open file dialog to name backup and select save location
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Xml file (*.xml)|*.xml"
            };
            openFileDialog.ShowDialog(Application.Current.Windows.OfType<TemplateExportView>()?.FirstOrDefault());

            // no file saved
            if (openFileDialog.FileName.Length is 0)
                return;

            // clear list
            TemplatesList = new ObservableCollection<TemplateListBoxItem>();

            // show loading label
            LoadingLabel = true;

            // import templates from file
            ImportTemplates(openFileDialog.FileName);
        }

        /// <summary>
        /// import templates
        /// </summary>
        /// <param name="parameter"></param>
        private void ImportTemplate(object parameter)
        {
            // null/zero check
            if (Templates is null || Templates.Count is 0 || TemplatesList is null || TemplatesList.Count is 0)
                return;

            // get selected list box items
            var selected = new ObservableCollection<TemplateListBoxItem>(TemplatesList.Where(i => i.IsSelected));

            // nothing selected
            if (selected is null || selected.Count is 0)
                return;

            // yes/no view model
            var vm = new YesNoViewModel("Are you sure you want to import the selected tournaments?", "Import Tournament");

            // create/show yes/no window
            var window = new YesNoView(vm)
            {
                Owner = Application.Current.Windows.OfType<TemplateImportView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            window.ShowDialog();

            // not saved, exit
            if (vm.Saved is false)
                return;

            // flag templates imported
            Saved = true;

            // show loading label
            LoadingLabel = true;

            // import templates
            SaveTemplates();
        }

        /// <summary>
        /// Import templates from file
        /// </summary>
        /// <param name="fileName">templates file</param>
        private void ImportTemplates(string fileName)
        {
            // templates collection
            Templates = new List<TournamentTemplate>();

            // templates list box item collection
            var templatesList = new ObservableCollection<TemplateListBoxItem>();

            // load xml file
            var doc = XmlHelper.LoadXmlFile(fileName);
            if(doc is null)
                return;

            // get template nodes
            var templates = doc?.DocumentElement?.SelectNodes("Template");
            if (templates is null || templates.Count is 0)
                return;

            // get template id that we can increment from
            var id = TournamentTemplateHelper.GetNewTemplateId();

            // loop templates
            foreach (XmlNode templateNode in templates)
            {
                // load template data from file into blank template
                var template = new TournamentTemplate
                {
                    AddonBaseCost = decimal.Parse(templateNode?.SelectSingleNode("AddonCost")?.InnerText ?? "0", new CultureInfo("en-US")),
                    AddonRakeCost = decimal.Parse(templateNode.SelectSingleNode("AddonRake")?.InnerText ?? "0", new CultureInfo("en-US")),
                    BlindLevels = int.Parse(templateNode.SelectSingleNode("BlindLevels")?.InnerText ?? "0", new CultureInfo("en-US")),
                    Bounty = decimal.Parse(templateNode.SelectSingleNode("Bounty")?.InnerText ?? "0", new CultureInfo("en-US")),
                    BuyinBaseCost = decimal.Parse(templateNode.SelectSingleNode("BuyinCost")?.InnerText ?? "0", new CultureInfo("en-US")),
                    BuyinRakeCost = decimal.Parse(templateNode.SelectSingleNode("BuyinRake")?.InnerText ?? "0", new CultureInfo("en-US")),
                    Entrants = int.Parse(templateNode.SelectSingleNode("Entrants")?.InnerText ?? "0", new CultureInfo("en-US")),
                    EntrantsPaid = int.Parse(templateNode.SelectSingleNode("EntrantsPaid")?.InnerText ?? "0", new CultureInfo("en-US")),
                    GameType = templateNode.SelectSingleNode("GameType")?.InnerText ?? "",
                    Guarantee = decimal.Parse(templateNode.SelectSingleNode("Guarantee")?.InnerText ?? "0", new CultureInfo("en-US")),
                    IsBovadaBounty = bool.Parse(templateNode.SelectSingleNode("IsBovadaBounty")?.InnerText ?? "False"),
                    IsFavorite = false,
                    IsSng = bool.Parse(templateNode.SelectSingleNode("IsSng")?.InnerText ?? "False"),
                    LateReg = int.Parse(templateNode.SelectSingleNode("LateReg")?.InnerText ?? "0", new CultureInfo("en-US")),
                    RebuyBaseCost = decimal.Parse(templateNode.SelectSingleNode("RebuyCost")?.InnerText ?? "0", new CultureInfo("en-US")),
                    RebuyRakeCost = decimal.Parse(templateNode.SelectSingleNode("RebuyRake")?.InnerText ?? "0", new CultureInfo("en-US")),
                    SngPayouts = templateNode.SelectSingleNode("SngPayouts")?.InnerText ?? "",
                    StackSizeAddon = int.Parse(templateNode.SelectSingleNode("StackSizeAddon")?.InnerText ?? "0", new CultureInfo("en-US")),
                    StackSizeRebuy = int.Parse(templateNode.SelectSingleNode("StackSizeRebuy")?.InnerText ?? "0", new CultureInfo("en-US")),
                    StackSizeStarting = int.Parse(templateNode.SelectSingleNode("StackSizeStarting")?.InnerText ?? "0", new CultureInfo("en-US")),
                    StartTime = DateTime.Parse(templateNode.SelectSingleNode("StartTime")?.InnerText ?? "1/1/1111 12:00 AM", new CultureInfo("en-US")),
                    TableSize = int.Parse(templateNode.SelectSingleNode("TableSize")?.InnerText ?? "9", new CultureInfo("en-US")),
                    TimeZoneName = templateNode.SelectSingleNode("TimeZoneName")?.InnerText ?? TimeZoneInfo.Local.StandardName,
                    TournamentName = templateNode.SelectSingleNode("TournamentName")?.InnerText ?? "",
                    Venue = templateNode.SelectSingleNode("SiteName")?.InnerText ?? "",
                };

                // add formats
                var formatNodes = templateNode.SelectSingleNode("Formats")?.ChildNodes;
                if (formatNodes is not null)
                    foreach (XmlNode formatNode in formatNodes)
                        template.Formats.Add(formatNode.InnerText);

                // give template new id number
                template.TemplateId = id++;

                // update template start times for time zone changes
                if (template.TimeZoneName.Length is 0)
                    template.TimeZoneName = TimeZoneInfo.Local.StandardName;

                template.StartTime = TimeZoneInfo.ConvertTime(template.StartTime, TimeZoneInfo.FindSystemTimeZoneById(template.TimeZoneName), TimeZoneInfo.FindSystemTimeZoneById(TimeZoneInfo.Local.StandardName));
                template.TimeZoneName = TimeZoneInfo.Local.StandardName;

                // make sure start time is 1111/1/1 + the hour and day + 0 seconds
                template.StartTime = new DateTime(1111, 1, 1, template.StartTime.Hour, template.StartTime.Minute, 0);

                // add template to collection
                Templates.Add(template);

                // create list box item from template
                var item = new TemplateListBoxItem()
                {
                    Description = template.DescriptionWithoutDayMonthYear,
                    DisplayString = $"{template.Venue} - {template.BuyinTotalCost.ToString("C2", new CultureInfo("en-US"))} - {template.TournamentName}",
                    IsSelected = false,
                    TemplateId = template.TemplateId
                };

                // add list box to collection
                templatesList.Add(item);

                // add new game types to template data
                TournamentTemplateDataHelper.AddGameType(template.GameType);

                // add new venues to template data
                TournamentTemplateDataHelper.AddVenue(template.Venue);

                // add new formats to template data
                foreach (var format in template.Formats)
                    TournamentTemplateDataHelper.AddFormat(format);
            }

            // remove loading label
            LoadingLabel = false;

            // update list
            TemplatesList = templatesList;
        }

        /// <summary>
        /// Save templates to file
        /// </summary>
        private void SaveTemplates()
        {
            // get selected list box items
            var selected = new ObservableCollection<TemplateListBoxItem>(TemplatesList.Where(i => i.IsSelected));

            // nothing selected
            if (selected is null || selected.Count is 0)
                return;

            // hold our selected templates
            var selectedTemplates = new List<TournamentTemplate>();

            // get template for each selected list box item
            foreach (var item in selected) 
                selectedTemplates.Add(Templates.FirstOrDefault(i => i.TemplateId == item.TemplateId));

            // save templates to file
            var res = TournamentTemplateHelper.SaveImportTournamentTemplates(selectedTemplates, CheckForDuplicates);

            // remove the template list box items
            foreach (var item in selected)
                TemplatesList.Remove(item);

            // get template manager view model
            if (Application.Current.Windows.OfType<TemplateManagerView>().FirstOrDefault()?.DataContext is TemplateManagerViewModel templateManagerViewModel)
                templateManagerViewModel.Reload();

            // remove loading message
            LoadingLabel = false;

            // ok view model
            var theVm = new OkViewModel(res, "Import Complete");

            // create/show ok window
            var theWindow = new OkView(theVm)
            {
                Owner = Application.Current.Windows.OfType<TemplateImportView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            theWindow.ShowDialog();
        }
    }
}