using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;
using LiveTracker.Models;

namespace LiveTracker.Helpers
{

    public static class TournamentTemplateHelper
    {
        /// <summary>
        /// create list box items for tournaments
        /// </summary>
        /// <param name="tournaments">tournaments</param>
        /// <returns>list box items</returns>
        public static ObservableCollection<ListBoxItem> CreateListBoxItems(ObservableCollection<TournamentRunning> tournaments)
        {
            // list of list box items
            var items = new ObservableCollection<ListBoxItem>();

            // null check
            if (tournaments is null || tournaments.Count is 0)
                return items;

            // loop through the tournaments
            foreach (var tournament in tournaments)
            {
                // create list box item
                var item = new ListBoxItem
                {
                    IsSelected = false,
                    Name = tournament.TournamentName,
                    Id = tournament.TemplateId,
                    Description = tournament.DescriptionWithoutDayMonthYear,
                    Buyin = tournament.BuyinTotalCost,
                    StartTime = tournament.StartTime,
                };

                // add item to list
                items.Add(item);
            }

            // return list of items
            return items;
        }

        /// <summary>
        /// delete tournament template from xml file
        /// </summary>
        /// <param name="template">tournament to remove</param>
        public static void DeleteTournamentTemplate(TournamentTemplate template)
        {
            // null/invalid check
            if (template is null || template.TemplateId is -1)
                return;

            // xml filename
            var fileName = XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.TemplatesFileName;

            // load xml file
            var xmlDocument = XmlHelper.LoadXmlFile(fileName) ?? new XmlDocument();

            // loop tournament xml nodes
            foreach (XmlNode templateNode in xmlDocument.GetElementsByTagName("Template"))
            {
                // check for matching id
                if (int.Parse(templateNode.Attributes?["ID"]?.Value ?? "-1", new CultureInfo("en-US")) == template.TemplateId)
                {
                    // remove matching template
                    templateNode.ParentNode?.RemoveChild(templateNode);

                    // break loop
                    break;
                }
            }

            // save xml file
            xmlDocument.Save(fileName);
        }

        /// <summary>
        /// load template from xml file
        /// </summary>
        /// <param name="theId">id to load</param>
        /// <returns>template</returns>
        public static TournamentTemplate LoadTemplate(int theId)
        {
            // templates file name
            var fileName = XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.TemplatesFileName;

            // blank template file
            var template = new TournamentTemplate();

            // load templates file
            var doc = XmlHelper.LoadXmlFile(fileName);

            // null check
            if (doc?.DocumentElement is null)
                return template;

            // load templates from templates file
            var templateNodeList = doc.DocumentElement.SelectNodes("Template");

            // null check
            if(templateNodeList is null)
                return template;

            // loop templates
            foreach (XmlNode templateNode in templateNodeList)
            {
                // if its not the id we want go the next
                if (int.Parse(templateNode.Attributes?["ID"]?.Value ?? "-1", new CultureInfo("en-US")) != theId)
                    continue;

                // load template data from file into blank template
                template = new TournamentTemplate
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
                    IsFavorite = bool.Parse(templateNode.SelectSingleNode("Favorite")?.InnerText ?? "False"),
                    IsSng = bool.Parse(templateNode.SelectSingleNode("IsSng")?.InnerText ?? "False"),
                    LateReg = int.Parse(templateNode.SelectSingleNode("LateReg")?.InnerText ?? "0", new CultureInfo("en-US")),
                    RebuyBaseCost = decimal.Parse(templateNode.SelectSingleNode("RebuyCost")?.InnerText ?? "0", new CultureInfo("en-US")),
                    RebuyRakeCost = decimal.Parse(templateNode.SelectSingleNode("RebuyRake")?.InnerText ?? "0", new CultureInfo("en-US")),
                    SngPayouts = templateNode.SelectSingleNode("SngPayouts")?.InnerText ?? string.Empty,
                    StackSizeAddon = int.Parse(templateNode.SelectSingleNode("StackSizeAddon")?.InnerText ?? "0", new CultureInfo("en-US")),
                    StackSizeRebuy = int.Parse(templateNode.SelectSingleNode("StackSizeRebuy")?.InnerText ?? "0", new CultureInfo("en-US")),
                    StackSizeStarting = int.Parse(templateNode.SelectSingleNode("StackSizeStarting")?.InnerText ?? "0", new CultureInfo("en-US")),
                    StartTime = DateTime.Parse(templateNode.SelectSingleNode("StartTime")?.InnerText ?? "0", new CultureInfo("en-US")),
                    TableSize = int.Parse(templateNode.SelectSingleNode("TableSize")?.InnerText ?? "9", new CultureInfo("en-US")),
                    TemplateId = int.Parse(templateNode.Attributes?["ID"]?.Value ?? "-1", new CultureInfo("en-US")),
                    TimeZoneName = templateNode.SelectSingleNode("TimeZoneName")?.InnerText ?? TimeZoneInfo.Local.StandardName,
                    TournamentName = templateNode.SelectSingleNode("TournamentName")?.InnerText ?? "",
                    Venue = templateNode.SelectSingleNode("SiteName")?.InnerText ?? "",
                };

                // get format nodes
                var formatNodes = templateNode.SelectSingleNode("Formats")?.ChildNodes;

                // null check
                if (formatNodes is not null)
                    // loop format nodes
                    foreach (XmlNode formatNode in formatNodes)
                        // add formats
                        template.Formats.Add(formatNode.InnerText);

                // make sure start time is 1111/1/1 + the hour and day + 0 seconds
                template.StartTime = new DateTime(1111, 1, 1, template.StartTime.Hour, template.StartTime.Minute, 0);
            }

            // return tournament
            return template;
        }

        /// <summary>
        /// Load templates from xml file
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<TournamentTemplate> LoadTemplates()
        {
            // holds templates
            var templates = new ObservableCollection<TournamentTemplate>();

            // templates xml file
            var fileName = XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.TemplatesFileName;

            // load xml file
            var xmlDocument = XmlHelper.LoadXmlFile(fileName);

            // null check
            if (xmlDocument is null) return templates;

            // null check
            if (xmlDocument.DocumentElement is null) return templates;

            // get template nodes
            var templateNodeList = xmlDocument.DocumentElement.SelectNodes("Template");

            // template nodes null check
            if(templateNodeList is null) return templates;

            // loop template nodes
            foreach (XmlNode templateNode in templateNodeList)
            {
                // create template
                var template = new TournamentTemplate
                {
                    AddonBaseCost = decimal.Parse(templateNode.SelectSingleNode("AddonCost")?.InnerText ?? "0", new CultureInfo("en-US")),
                    AddonRakeCost = decimal.Parse(templateNode.SelectSingleNode("AddonRake")?.InnerText ?? "0", new CultureInfo("en-US")),
                    BlindLevels = int.Parse(templateNode.SelectSingleNode("BlindLevels")?.InnerText ?? "0", new CultureInfo("en-US")),
                    Bounty = decimal.Parse(templateNode.SelectSingleNode("Bounty")?.InnerText ?? "0", new CultureInfo("en-US")),
                    BuyinBaseCost = decimal.Parse(templateNode.SelectSingleNode("BuyinCost")?.InnerText ?? "0", new CultureInfo("en-US")),
                    BuyinRakeCost = decimal.Parse(templateNode.SelectSingleNode("BuyinRake")?.InnerText ?? "0", new CultureInfo("en-US")),
                    Entrants = int.Parse(templateNode.SelectSingleNode("Entrants")?.InnerText ?? "0", new CultureInfo("en-US")),
                    EntrantsPaid = int.Parse(templateNode.SelectSingleNode("EntrantsPaid")?.InnerText ?? "0", new CultureInfo("en-US")),
                    GameType = templateNode.SelectSingleNode("GameType")?.InnerText ?? string.Empty,
                    Guarantee = decimal.Parse(templateNode.SelectSingleNode("Guarantee")?.InnerText ?? "0", new CultureInfo("en-US")),
                    IsBovadaBounty = bool.Parse(templateNode.SelectSingleNode("IsBovadaBounty")?.InnerText ?? "False"),
                    IsFavorite = bool.Parse(templateNode.SelectSingleNode("Favorite")?.InnerText ?? "False"),
                    IsSng = bool.Parse(templateNode.SelectSingleNode("IsSng")?.InnerText ?? "False"),
                    LateReg = int.Parse(templateNode.SelectSingleNode("LateReg")?.InnerText ?? "0", new CultureInfo("en-US")),
                    RebuyBaseCost = decimal.Parse(templateNode.SelectSingleNode("RebuyCost")?.InnerText ?? "0", new CultureInfo("en-US")),
                    RebuyRakeCost = decimal.Parse(templateNode.SelectSingleNode("RebuyRake")?.InnerText ?? "0", new CultureInfo("en-US")),
                    SngPayouts = templateNode.SelectSingleNode("SngPayouts")?.InnerText ?? string.Empty,
                    StackSizeAddon = int.Parse(templateNode.SelectSingleNode("StackSizeAddon")?.InnerText ?? "0", new CultureInfo("en-US")),
                    StackSizeRebuy = int.Parse(templateNode.SelectSingleNode("StackSizeRebuy")?.InnerText ?? "0", new CultureInfo("en-US")),
                    StackSizeStarting = int.Parse(templateNode.SelectSingleNode("StackSizeStarting")?.InnerText ?? "0", new CultureInfo("en-US")),
                    StartTime = DateTime.Parse(templateNode.SelectSingleNode("StartTime")?.InnerText ?? "1/1/1001 12:00:00 AM", new CultureInfo("en-US")),
                    TableSize = int.Parse(templateNode.SelectSingleNode("TableSize")?.InnerText ?? "0", new CultureInfo("en-US")),
                    TemplateId = int.Parse(templateNode.Attributes?["ID"]?.Value ?? "-1", new CultureInfo("en-US")),
                    TimeZoneName = templateNode.SelectSingleNode("TimeZoneName")?.InnerText ?? TimeZoneInfo.Local.StandardName,
                    TournamentName = templateNode.SelectSingleNode("TournamentName")?.InnerText ?? string.Empty,
                    Venue = templateNode.SelectSingleNode("SiteName")?.InnerText ?? string.Empty,
                };

                // get format nodes
                var xmlNodeList = templateNode.SelectSingleNode("Formats")?.ChildNodes;

                // null check and get formats
                if (xmlNodeList is not null)
                    foreach (XmlNode formatNode in xmlNodeList)
                        template.Formats.Add(formatNode.InnerText);

                // make sure start time is 1111/1/1 + the hour and day + 0 seconds
                template.StartTime = new DateTime(1111, 1, 1, template.StartTime.Hour, template.StartTime.Minute, 0);

                // add template to collection
                templates.Add(template);
            }

            // return templates sorted by name
            return new ObservableCollection<TournamentTemplate>(templates.OrderBy(i => i.TournamentName));
        }

        /// <summary>
        /// get a new tournament id
        /// </summary>
        /// <returns>tournament id</returns>
        public static int GetNewTemplateId()
        {
            // xml file name
            var fileName = XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.TemplatesFileName;

            // load xml file
            var xmlDocument = XmlHelper.LoadXmlFile(fileName);

            // null check
            if (xmlDocument is null) return 1;

            // list for template ids
            var templateIds = new ObservableCollection<int>();

            // loop each template
            foreach (XmlNode templateNode in xmlDocument.GetElementsByTagName("Template"))
                // null check
                if (templateNode.Attributes != null)
                    // add id to list
                    templateIds.Add(int.Parse(templateNode.Attributes["ID"]?.Value ?? "0", new CultureInfo("en-US")));

            // zero check template list
            if (templateIds.Any())
                // return new id as highest id + 1
                return templateIds.Max() + 1;
            else
                // return 1 as new id
                return 1;
        }

        /// <summary>
        /// Save tournament template
        /// </summary>
        /// <param name="template">template</param>
        /// <param name="update">update or not</param>
        public static void SaveTournamentTemplate(TournamentTemplate template, bool update = false)
        {
            // null check
            if (template is null) return;

            // template file name
            var fileName = XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.TemplatesFileName;

            // load templates file or create blank
            var xmlDocument = XmlHelper.LoadXmlFile(fileName);
            if (xmlDocument is null)
            {
                // empty xml doc
                xmlDocument = new XmlDocument();

                // create blank
                XmlNode rootNode = xmlDocument.CreateElement("Templates");
                xmlDocument.AppendChild(rootNode);
                xmlDocument.Save(fileName);
                xmlDocument = XmlHelper.LoadXmlFile(fileName);
            }

            // create template nodes
            var templateParentNode = xmlDocument.CreateElement("Template");

            var templateAttribute = xmlDocument.CreateAttribute("ID");
            templateAttribute.Value = template.TemplateId.ToString(new CultureInfo("en-US"));
            templateParentNode.Attributes.Append(templateAttribute);

            var templateChildNode = xmlDocument.CreateElement("Favorite");
            templateChildNode.InnerText = template.IsFavorite.ToString(new CultureInfo("en-US"));
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
                // null check
                if(format is null || format is "") 
                    continue;

                // create format node
                XmlNode formatChildNode = xmlDocument.CreateElement("Format");
                formatChildNode.InnerText = format.Trim();
                templateChildNode.AppendChild(formatChildNode);
            }
            templateParentNode.AppendChild(templateChildNode);

            // check for and remove an existing template with this id number so we can save the updated version
            if (update)
                foreach (XmlNode templateNode in xmlDocument.GetElementsByTagName("Template"))
                    if (templateNode.Attributes != null && int.Parse(templateNode.Attributes["ID"]?.Value ?? "-1", new CultureInfo("en-US")) == template.TemplateId)
                    {
                        templateNode.ParentNode?.RemoveChild(templateNode);
                        break;
                    }

            // add updated tournament node to xml file
            xmlDocument.DocumentElement?.AppendChild(templateParentNode);

            // save xml file
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

        /// <summary>
        /// save tournament template
        /// </summary>
        /// <param name="tournament">tournament</param>
        /// <param name="update">update or not</param>
        public static void SaveTournamentTemplate(TournamentRunning tournament, bool update = false)
        {
            // null check
            if (tournament is null) return;

            // get templates file name
            var fileName = XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.TemplatesFileName;

            // load templates file or create blank
            var xmlDocument = XmlHelper.LoadXmlFile(fileName);
            if (xmlDocument is null)
            {
                xmlDocument = new XmlDocument();

                XmlNode rootNode = xmlDocument.CreateElement("Templates");
                xmlDocument.AppendChild(rootNode);
                xmlDocument.Save(fileName);
                xmlDocument = XmlHelper.LoadXmlFile(fileName);
            }

            // create template node
            var templateParentNode = xmlDocument.CreateElement("Template");

            var templateAttribute = xmlDocument.CreateAttribute("ID");
            templateAttribute.Value = tournament.TemplateId.ToString(new CultureInfo("en-US"));
            templateParentNode.Attributes.Append(templateAttribute);

            var templateChildNode = xmlDocument.CreateElement("TournamentName");
            templateChildNode.InnerText = tournament.TournamentName;
            templateParentNode.AppendChild(templateChildNode);

            if (update)
            {
                templateChildNode = xmlDocument.CreateElement("Favorite");
                templateChildNode.InnerText = tournament.IsFavorite.ToString(new CultureInfo("en-US"));
                templateParentNode.AppendChild(templateChildNode);
            }
            else // new templates are never favorites
            {
                templateChildNode = xmlDocument.CreateElement("Favorite");
                templateChildNode.InnerText = "False";
                templateParentNode.AppendChild(templateChildNode);
            }

            // make sure start time is 1111/1/1 + the hour and day + 0 seconds
            templateChildNode = xmlDocument.CreateElement("StartTime");
            templateChildNode.InnerText = new DateTime(1111, 1, 1, tournament.StartTime.Hour, tournament.StartTime.Minute, 0).ToString(new CultureInfo("en-US"));
            templateParentNode.AppendChild(templateChildNode);

            templateChildNode = xmlDocument.CreateElement("BuyinCost");
            templateChildNode.InnerText = tournament.BuyinBaseCost.ToString(new CultureInfo("en-US"));
            templateParentNode.AppendChild(templateChildNode);

            templateChildNode = xmlDocument.CreateElement("BuyinRake");
            templateChildNode.InnerText = tournament.BuyinRakeCost.ToString(new CultureInfo("en-US"));
            templateParentNode.AppendChild(templateChildNode);

            templateChildNode = xmlDocument.CreateElement("RebuyCost");
            templateChildNode.InnerText = tournament.RebuyBaseCost.ToString(new CultureInfo("en-US"));
            templateParentNode.AppendChild(templateChildNode);

            templateChildNode = xmlDocument.CreateElement("RebuyRake");
            templateChildNode.InnerText = tournament.RebuyRakeCost.ToString(new CultureInfo("en-US"));
            templateParentNode.AppendChild(templateChildNode);

            templateChildNode = xmlDocument.CreateElement("AddonCost");
            templateChildNode.InnerText = tournament.AddonBaseCost.ToString(new CultureInfo("en-US"));
            templateParentNode.AppendChild(templateChildNode);

            templateChildNode = xmlDocument.CreateElement("AddonRake");
            templateChildNode.InnerText = tournament.AddonRakeCost.ToString(new CultureInfo("en-US"));
            templateParentNode.AppendChild(templateChildNode);

            templateChildNode = xmlDocument.CreateElement("TimeZoneName");
            templateChildNode.InnerText = tournament.TimeZoneName;
            templateParentNode.AppendChild(templateChildNode);

            templateChildNode = xmlDocument.CreateElement("StackSizeStarting");
            templateChildNode.InnerText = tournament.StackSizeStarting.ToString(new CultureInfo("en-US"));
            templateParentNode.AppendChild(templateChildNode);

            templateChildNode = xmlDocument.CreateElement("StackSizeRebuy");
            templateChildNode.InnerText = tournament.StackSizeRebuy.ToString(new CultureInfo("en-US"));
            templateParentNode.AppendChild(templateChildNode);

            templateChildNode = xmlDocument.CreateElement("StackSizeAddon");
            templateChildNode.InnerText = tournament.StackSizeAddon.ToString(new CultureInfo("en-US"));
            templateParentNode.AppendChild(templateChildNode);

            templateChildNode = xmlDocument.CreateElement("Guarantee");
            templateChildNode.InnerText = tournament.Guarantee.ToString(new CultureInfo("en-US"));
            templateParentNode.AppendChild(templateChildNode);

            templateChildNode = xmlDocument.CreateElement("SiteName");
            templateChildNode.InnerText = tournament.Venue;
            templateParentNode.AppendChild(templateChildNode);

            templateChildNode = xmlDocument.CreateElement("GameType");
            templateChildNode.InnerText = tournament.GameType;
            templateParentNode.AppendChild(templateChildNode);

            templateChildNode = xmlDocument.CreateElement("IsSng");
            templateChildNode.InnerText = tournament.IsSng.ToString();
            templateParentNode.AppendChild(templateChildNode);

            templateChildNode = xmlDocument.CreateElement("IsBovadaBounty");
            templateChildNode.InnerText = tournament.IsBovadaBounty.ToString();
            templateParentNode.AppendChild(templateChildNode);

            templateChildNode = xmlDocument.CreateElement("Bounty");
            templateChildNode.InnerText = tournament.Bounty.ToString(new CultureInfo("en-US"));
            templateParentNode.AppendChild(templateChildNode);

            templateChildNode = xmlDocument.CreateElement("TableSize");
            templateChildNode.InnerText = tournament.TableSize.ToString(new CultureInfo("en-US"));
            templateParentNode.AppendChild(templateChildNode);

            if(tournament.IsSng)
            {
                templateChildNode = xmlDocument.CreateElement("Entrants");
                templateChildNode.InnerText = tournament.Entrants.ToString(new CultureInfo("en-US"));
                templateParentNode.AppendChild(templateChildNode);

                templateChildNode = xmlDocument.CreateElement("EntrantsPaid");
                templateChildNode.InnerText = tournament.EntrantsPaid.ToString(new CultureInfo("en-US"));
                templateParentNode.AppendChild(templateChildNode);

                templateChildNode = xmlDocument.CreateElement("SngPayouts");
                templateChildNode.InnerText = tournament.SngPayouts;
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
            templateChildNode.InnerText = tournament.BlindLevels.ToString(new CultureInfo("en-US"));
            templateParentNode.AppendChild(templateChildNode);

            templateChildNode = xmlDocument.CreateElement("LateReg");
            templateChildNode.InnerText = tournament.LateReg.ToString(new CultureInfo("en-US"));
            templateParentNode.AppendChild(templateChildNode);

            // get formats
            templateChildNode = xmlDocument.CreateElement("Formats");
            foreach (var format in tournament.Formats)
            {
                XmlNode formatChildNode = xmlDocument.CreateElement("Format");
                formatChildNode.InnerText = format.Trim();
                templateChildNode.AppendChild(formatChildNode);
            }
            templateParentNode.AppendChild(templateChildNode);

            // check for and remove an existing template with this id number so we can save the updated version
            if (update)
                foreach (XmlNode templateNode in xmlDocument.GetElementsByTagName("Template"))
                    if (templateNode.Attributes != null && int.Parse(templateNode.Attributes["ID"]?.Value ?? "-1", new CultureInfo("en-US")) == tournament.TemplateId)
                    {
                        templateNode.ParentNode?.RemoveChild(templateNode);
                        break;
                    }

            xmlDocument.DocumentElement?.AppendChild(templateParentNode);

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

        /// <summary>
        /// save imported tournament templates
        /// </summary>
        /// <param name="templates">tournaments</param>
        /// <param name="checkDupes">check for duplicate templates</param>
        /// <returns>number of imports/dupes</returns>
        public static string SaveImportTournamentTemplates(List<TournamentTemplate> templates, bool checkDupes)
        {
            // null check
            if (templates is null || templates.Count is 0)
                return "Nothing to import";

            // xml file
            var fileName = XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.TemplatesFileName;

            // load xml file
            var xmlDocument = XmlHelper.LoadXmlFile(fileName);

            // null check
            if (xmlDocument is null)
            {
                // blank xml file
                xmlDocument = new XmlDocument();

                // create blank templates file
                XmlNode rootNode = xmlDocument.CreateElement("Templates");
                xmlDocument.AppendChild(rootNode);
                xmlDocument.Save(fileName);
                xmlDocument = XmlHelper.LoadXmlFile(fileName);
            }

            // tracker number of duplicates
            var dupeCounter = 0;
            var importCounter = 0;

            // templates list
            var ourTemplates = new ObservableCollection<TournamentTemplate>();

            // load list if we are checking duplicates
            if (checkDupes) ourTemplates = TournamentTemplateHelper.LoadTemplates();

            // loop templates
            foreach (var template in templates)
            {
                // flag as no dupe
                var isDupe = false;

                // loop templates
                foreach(var temp in ourTemplates)
                {
                    // check for matching templates
                    if (TournamentTemplate.Equals(template, temp))
                    {
                        // increment dupe counter
                        dupeCounter++;

                        // flag as duplicate
                        isDupe = true;

                        // break loop
                        break;
                    }
                }

                // dupe found, go next instead of importing
                if (isDupe) continue;

                // add one to import counter
                importCounter++;

                XmlNode templateParentNode = xmlDocument.CreateElement("Template");

                var templateAttribute = xmlDocument.CreateAttribute("ID");
                templateAttribute.Value = template.TemplateId.ToString(new CultureInfo("en-US"));
                templateParentNode.Attributes?.Append(templateAttribute);

                XmlNode templateChildNode = xmlDocument.CreateElement("Favorite");
                templateChildNode.InnerText = template.IsFavorite.ToString(new CultureInfo("en-US"));
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

                templateChildNode = xmlDocument.CreateElement("IsBovadaBounty");
                templateChildNode.InnerText = template.IsBovadaBounty.ToString();
                templateParentNode.AppendChild(templateChildNode);

                templateChildNode = xmlDocument.CreateElement("IsSng");
                templateChildNode.InnerText = template.IsSng.ToString();
                templateParentNode.AppendChild(templateChildNode);

                templateChildNode = xmlDocument.CreateElement("Bounty");
                templateChildNode.InnerText = template.Bounty.ToString(new CultureInfo("en-US"));
                templateParentNode.AppendChild(templateChildNode);

                templateChildNode = xmlDocument.CreateElement("TimeZoneName");
                templateChildNode.InnerText = template.TimeZoneName;
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

                templateChildNode = xmlDocument.CreateElement("Formats");
                foreach (var format in template.Formats)
                {
                    XmlNode formatChildNode = xmlDocument.CreateElement("Format");
                    formatChildNode.InnerText = format.Trim();
                    templateChildNode.AppendChild(formatChildNode);
                }
                templateParentNode.AppendChild(templateChildNode);

                xmlDocument.DocumentElement?.AppendChild(templateParentNode);
            }

            // save xml file
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

            // success message with duplicates
            if(checkDupes) return $"Import Complete!\n\n{importCounter} tournaments imported \n{dupeCounter} duplicates ignored";

            // success message without duplicates
            return $"Import Complete! \n\n{importCounter} tournaments imported \n";
        }
    }
}