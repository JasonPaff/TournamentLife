using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
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
    public class TemplateExportViewModel : NotificationObject
    {
        public ICommand CancelCommand => new BaseCommand(Cancel);
        public ICommand ExportCommand => new BaseCommand(Export);

        public TemplateExportViewModel()
        {
            // font size
            FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));

            // visual theme
            Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");

            // find template manager view model
            if (Application.Current.Windows.OfType<TemplateManagerView>()?.FirstOrDefault()?.DataContext is not TemplateManagerViewModel templateManagerViewModel) 
                return;

            // grab templates from template manager
            Templates = new List<TournamentTemplate>(templateManagerViewModel.Templates);

            // create list box items from templates
            CreateListBoxItems();
        }

        public int FontSize { get; set; }
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
            Application.Current.Windows.OfType<TemplateExportView>().FirstOrDefault()?.Close();
        }

        /// <summary>
        ///  create template list box items from templates
        /// </summary>
        private void CreateListBoxItems()
        {
            // templates list box item list
            TemplatesList = new ObservableCollection<TemplateListBoxItem>();

            // loop through templates
            foreach(TournamentTemplate template in Templates)
            {
                var item = new TemplateListBoxItem()
                {
                    Description = template.DescriptionWithoutDayMonthYear,
                    DisplayString = $"{template.Venue} - {template.BuyinTotalCost.ToString("C2", new CultureInfo("en-US"))} - {template.TournamentName}",
                    IsSelected = false,
                    TemplateId = template.TemplateId
                };

                TemplatesList.Add(item);
            }
        }

        /// <summary>
        /// Export selected templates
        /// </summary>
        /// <param name="parameter"></param>
        private void Export(object parameter)
        {
            // zero check
            if (TemplatesList is null || TemplatesList.Any(i => i.IsSelected) is false)
                return;

            // open file dialog to name backup and select save location
            Microsoft.Win32.SaveFileDialog openFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "Xml file (*.xml)|*.xml"
            };
            openFileDialog.ShowDialog(Application.Current.Windows.OfType<TemplateExportView>()?.FirstOrDefault());

            // no file saved
            if (openFileDialog.FileName.Length is 0) 
                return;

            // get selected list box items
            var selected = TemplatesList.Where(i => i.IsSelected);

            // get templates for selected list box items
            var selectedTemplates = new List<TournamentTemplate>();
            foreach(var item in selected) 
                selectedTemplates.Add(Templates.FirstOrDefault(i => i.TemplateId == item.TemplateId));

            // create xml file from selected templates
            CreateExportFile(selectedTemplates, openFileDialog.FileName);

            // deselect all templates
            foreach(var template in TemplatesList) 
                template.IsSelected = false;

            // ok view model
            var theVm = new OkViewModel($"Export Completed - {selectedTemplates.Count} tournaments exported", "Tournaments Exported");

            // create/show ok window
            var theWindow = new OkView(theVm)
            {
                Owner = Application.Current.Windows.OfType<TemplateExportView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            theWindow.ShowDialog();
        }

        /// <summary>
        /// create a templates xml file
        /// </summary>
        /// <param name="templates">templates to save</param>
        /// <param name="fileName">file to save to</param>
        private void CreateExportFile(List<TournamentTemplate> templates, string fileName)
        {
            // null check
            if (templates is null || templates.Count is 0) return;

            // xml document
            var xmlDocument = new XmlDocument();

            // root node
            XmlNode rootNode = xmlDocument.CreateElement("Templates");

            foreach(TournamentTemplate template in templates)
            {
            // create template nodes
            XmlNode templateParentNode = xmlDocument.CreateElement("Template");

            XmlAttribute templateAttribute = xmlDocument.CreateAttribute("ID");
            templateAttribute.Value = template.TemplateId.ToString(new CultureInfo("en-US"));
            templateParentNode.Attributes.Append(templateAttribute);

            XmlNode templateChildNode = xmlDocument.CreateElement("Favorite");
            templateChildNode.InnerText = "false";
            templateParentNode.AppendChild(templateChildNode);

            templateChildNode = xmlDocument.CreateElement("TournamentName");
            templateChildNode.InnerText = template.TournamentName;
            templateParentNode.AppendChild(templateChildNode);

            // make sure start time is 1111/1/1 + the hour and day + 0 seconds
            templateChildNode = xmlDocument.CreateElement("StartTime");
            templateChildNode.InnerText = new DateTime(1111, 1, 1, template.StartTime.Hour, template.StartTime.Minute, 0).ToString(new CultureInfo("en-US"));
            templateParentNode.AppendChild(templateChildNode);

            templateChildNode = xmlDocument.CreateElement("BuyinCost");
            templateChildNode.InnerText = template.BuyinBaseCost.ToString(new CultureInfo("en-US"));
            templateParentNode.AppendChild(templateChildNode);

            templateChildNode = xmlDocument.CreateElement("BuyinRake");
            templateChildNode.InnerText = template.BuyinRakeCost.ToString(new CultureInfo("en-US"));
            templateParentNode.AppendChild(templateChildNode);

            templateChildNode = xmlDocument.CreateElement("RebuyCost");
            templateChildNode.InnerText = template.RebuyBaseCost.ToString(new CultureInfo("en-US"));
            templateParentNode.AppendChild(templateChildNode);

            templateChildNode = xmlDocument.CreateElement("RebuyRake");
            templateChildNode.InnerText = template.RebuyRakeCost.ToString(new CultureInfo("en-US"));
            templateParentNode.AppendChild(templateChildNode);

            templateChildNode = xmlDocument.CreateElement("AddonCost");
            templateChildNode.InnerText = template.AddonBaseCost.ToString(new CultureInfo("en-US"));
            templateParentNode.AppendChild(templateChildNode);

            templateChildNode = xmlDocument.CreateElement("AddonRake");
            templateChildNode.InnerText = template.AddonRakeCost.ToString(new CultureInfo("en-US"));
            templateParentNode.AppendChild(templateChildNode);

            templateChildNode = xmlDocument.CreateElement("TimeZoneName");
            templateChildNode.InnerText = template.TimeZoneName;
            templateParentNode.AppendChild(templateChildNode);

            templateChildNode = xmlDocument.CreateElement("StackSizeStarting");
            templateChildNode.InnerText = template.StackSizeStarting.ToString(new CultureInfo("en-US"));
            templateParentNode.AppendChild(templateChildNode);

            templateChildNode = xmlDocument.CreateElement("StackSizeRebuy");
            templateChildNode.InnerText = template.StackSizeRebuy.ToString(new CultureInfo("en-US"));
            templateParentNode.AppendChild(templateChildNode);

            templateChildNode = xmlDocument.CreateElement("StackSizeAddon");
            templateChildNode.InnerText = template.StackSizeAddon.ToString(new CultureInfo("en-US"));
            templateParentNode.AppendChild(templateChildNode);

            templateChildNode = xmlDocument.CreateElement("Guarantee");
            templateChildNode.InnerText = template.Guarantee.ToString(new CultureInfo("en-US"));
            templateParentNode.AppendChild(templateChildNode);

            templateChildNode = xmlDocument.CreateElement("SiteName");
            templateChildNode.InnerText = template.Venue;
            templateParentNode.AppendChild(templateChildNode);

            templateChildNode = xmlDocument.CreateElement("GameType");
            templateChildNode.InnerText = template.GameType;
            templateParentNode.AppendChild(templateChildNode);

            templateChildNode = xmlDocument.CreateElement("IsSng");
            templateChildNode.InnerText = template.IsSng.ToString();
            templateParentNode.AppendChild(templateChildNode);

            templateChildNode = xmlDocument.CreateElement("IsBovadaBounty");
            templateChildNode.InnerText = template.IsBovadaBounty.ToString();
            templateParentNode.AppendChild(templateChildNode);

            templateChildNode = xmlDocument.CreateElement("Bounty");
            templateChildNode.InnerText = template.Bounty.ToString(new CultureInfo("en-US"));
            templateParentNode.AppendChild(templateChildNode);

            templateChildNode = xmlDocument.CreateElement("TableSize");
            templateChildNode.InnerText = template.TableSize.ToString(new CultureInfo("en-US"));
            templateParentNode.AppendChild(templateChildNode);

            if (template.IsSng)
            {
                templateChildNode = xmlDocument.CreateElement("Entrants");
                templateChildNode.InnerText = template.Entrants.ToString(new CultureInfo("en-US"));
                templateParentNode.AppendChild(templateChildNode);

                templateChildNode = xmlDocument.CreateElement("EntrantsPaid");
                templateChildNode.InnerText = template.EntrantsPaid.ToString(new CultureInfo("en-US"));
                templateParentNode.AppendChild(templateChildNode);

                templateChildNode = xmlDocument.CreateElement("SngPayouts");
                templateChildNode.InnerText = template.SngPayouts;
                templateParentNode.AppendChild(templateChildNode);
            }
            else // only sngs get the entrants and entrants paid saved to something other than 0
            {
                templateChildNode = xmlDocument.CreateElement("Entrants");
                templateChildNode.InnerText = "0";
                templateParentNode.AppendChild(templateChildNode);

                templateChildNode = xmlDocument.CreateElement("EntrantsPaid");
                templateChildNode.InnerText = "0";
                templateParentNode.AppendChild(templateChildNode);

                templateChildNode = xmlDocument.CreateElement("SngPayouts");
                templateChildNode.InnerText = "";
                templateParentNode.AppendChild(templateChildNode);
            }

            templateChildNode = xmlDocument.CreateElement("BlindLevels");
            templateChildNode.InnerText = template.BlindLevels.ToString(new CultureInfo("en-US"));
            templateParentNode.AppendChild(templateChildNode);

            templateChildNode = xmlDocument.CreateElement("LateReg");
            templateChildNode.InnerText = template.LateReg.ToString(new CultureInfo("en-US"));
            templateParentNode.AppendChild(templateChildNode);

            // get formats
            templateChildNode = xmlDocument.CreateElement("Formats");
            foreach (var format in template.Formats)
            {
                XmlNode formatChildNode = xmlDocument.CreateElement("Format");
                formatChildNode.InnerText = format.Trim();
                templateChildNode.AppendChild(formatChildNode);
            }
            templateParentNode.AppendChild(templateChildNode);

            // add template node to templates node
            rootNode.AppendChild(templateParentNode);
            }

            // add templates node to xml file
            xmlDocument.AppendChild(rootNode);

            try
            {
                xmlDocument.Save(fileName);
            }
            catch (IOException ioException)
            {
                Console.WriteLine(ioException.Message);
            }
            catch (XmlException xmlException)
            {
                Console.WriteLine(xmlException.Message);
            }
        }
    }
}
