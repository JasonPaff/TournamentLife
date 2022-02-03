using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;
using Tournament_Life.Models.Tournaments;

namespace Tournament_Life.Helpers
{
    public static class TournamentTemplateDataHelper
    {
        /// <summary>
        /// Add a new format to the template data file
        /// </summary>
        /// <param name="format">new format</param>
        public static bool AddFormat(string format)
        {
            // null check
            if (format is null)
                return false;

            // trim blank space
            format = format.Trim();

            // no string or blank string
            if (format is "")
                return false;

            // load saved formats for duplicate check
            var formats = LoadFormats();

            // leave if its a duplicate
            if (formats.Any(v => format.Equals(v, StringComparison.OrdinalIgnoreCase)))
                return false;

            // add format
            formats.Add(format);

            // save formats to file
            SaveFormats(formats);

            // success
            return true;
        }

        /// <summary>
        /// Add a new game type to the template data file
        /// </summary>
        /// <param name="gameType">new game type</param>
        public static bool AddGameType(string gameType)
        {
            // null check
            if (gameType is null)
                return false;

            // trim blank space
            gameType = gameType.Trim();

            // no string or blank string
            if (gameType is "")
                return false;

            // load saved game types for duplicate check
            var gameTypes = LoadGameTypes();

            // leave if its a duplicate
            if (gameTypes.Any(v => gameType.Equals(v, StringComparison.OrdinalIgnoreCase)))
                return false;

            // add game type
            gameTypes.Add(gameType);

            // save game types to file
            SaveGameTypes(gameTypes);

            // success
            return true;
        }

        /// <summary>
        /// Add a new venue to the template data file
        /// </summary>
        /// <param name="venue">new venue</param>
        public static bool AddVenue(string venue)
        {
            // null check
            if (venue is null)
                return false;

            // trim blank space
            venue = venue.Trim();

            // no string or blank string
            if (venue is "")
                return false;

            // load saved venues for duplicate check
            var venues = LoadVenues();

            // leave if its a duplicate
            if (venues.Any(v => venue.Equals(v, StringComparison.OrdinalIgnoreCase)))
                return false;

            // add venue
            venues.Add(venue);

            // save venues to file
            SaveVenues(venues);

            // success
            return true;
        }

        /// <summary>
        /// gets the defaults values from the xml file
        /// </summary>
        /// <returns>tournament template set to default values</returns>
        public static TournamentTemplate LoadDefaultValues()
        {
            // template for default values
            var template = new TournamentTemplate();

            // template data filename
            var filename = XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.TemplateDataFileName;

            // load template data file, leave if it doesn't exist
            var xmlDocument = XmlHelper.LoadXmlFile(filename);
            if (xmlDocument is null)
                return template;

            // get default values nodes
            var defaultNode = xmlDocument.DocumentElement?.SelectSingleNode("DefaultValues");

            // load default values from xml file
            template.AddonBaseCost = decimal.Parse(defaultNode?.SelectSingleNode(nameof(template.AddonBaseCost))?.InnerText ?? "0", new CultureInfo("en-US"));
            template.AddonRakeCost = decimal.Parse(defaultNode?.SelectSingleNode(nameof(template.AddonRakeCost))?.InnerText ?? "0", new CultureInfo("en-US"));
            template.BlindLevels = int.Parse(defaultNode?.SelectSingleNode(nameof(template.BlindLevels))?.InnerText ?? "10", new CultureInfo("en-US"));
            template.BuyinBaseCost = decimal.Parse(defaultNode?.SelectSingleNode(nameof(template.BuyinBaseCost))?.InnerText ?? "10", new CultureInfo("en-US"));
            template.BuyinRakeCost = decimal.Parse(defaultNode?.SelectSingleNode(nameof(template.BuyinRakeCost))?.InnerText ?? "1", new CultureInfo("en-US"));
            template.Formats = LoadDefaultFormats();
            template.GameType = LoadDefaultGameType();
            template.Guarantee = decimal.Parse(defaultNode?.SelectSingleNode(nameof(template.Guarantee))?.InnerText ?? "0", new CultureInfo("en-US"));
            template.LateReg = int.Parse(defaultNode?.SelectSingleNode(nameof(template.LateReg))?.InnerText ?? "60", new CultureInfo("en-US"));
            template.RebuyBaseCost = decimal.Parse(defaultNode?.SelectSingleNode(nameof(template.RebuyBaseCost))?.InnerText ?? "0", new CultureInfo("en-US"));
            template.RebuyRakeCost = decimal.Parse(defaultNode?.SelectSingleNode(nameof(template.RebuyRakeCost))?.InnerText ?? "0", new CultureInfo("en-US"));
            template.StartTime = DateTime.Parse(defaultNode?.SelectSingleNode(nameof(template.StartTime))?.InnerText ?? "1/1/1111 5:00:00 PM", new CultureInfo("en-US"));
            template.StackSizeAddon = int.Parse(defaultNode?.SelectSingleNode(nameof(template.StackSizeAddon))?.InnerText ?? "0", new CultureInfo("en-US"));
            template.StackSizeRebuy = int.Parse(defaultNode?.SelectSingleNode(nameof(template.StackSizeRebuy))?.InnerText ?? "0", new CultureInfo("en-US"));
            template.StackSizeStarting = int.Parse(defaultNode?.SelectSingleNode(nameof(template.StackSizeStarting))?.InnerText ?? "5000", new CultureInfo("en-US"));
            template.TableSize = int.Parse(defaultNode?.SelectSingleNode(nameof(template.TableSize))?.InnerText ?? "9", new CultureInfo("en-US"));
            template.TournamentName = "";
            template.Venue = LoadDefaultVenue();

            // return template
            return template;
        }

        /// <summary>
        /// gets the default game type from the xml file
        /// </summary>
        /// <returns>default game type string</returns>
        public static List<string> LoadDefaultFormats()
        {
            // template data filename
            var filename = XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.TemplateDataFileName;

            // formats collection
            var formats = new List<string>();

            // load template data file, leave if it doesn't exist
            var xmlDocument = XmlHelper.LoadXmlFile(filename);
            if (xmlDocument is null)
                return formats;

            // return the default value from xml file
            var formatNodes = xmlDocument.DocumentElement?.SelectSingleNode("Formats")?.SelectNodes("DefaultFormat");

            // null check
            if(formatNodes is null)
                return formats;

            // load formats
            foreach(XmlNode formatNode in formatNodes)
                formats.Add(formatNode.InnerText);

            // return formats
            return formats;
        }

        /// <summary>
        /// gets the default game type from the xml file
        /// </summary>
        /// <returns>default game type string</returns>
        public static string LoadDefaultGameType()
        {
            // template data filename
            var filename = XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.TemplateDataFileName;

            // load template data file, leave if it doesn't exist
            var xmlDocument = XmlHelper.LoadXmlFile(filename);
            if (xmlDocument is null)
                return "";

            // return the default value from xml file
            return xmlDocument.DocumentElement?.SelectSingleNode("GameTypes")?.SelectSingleNode("DefaultGameType")?.InnerText ?? "";
        }

        /// <summary>
        /// gets the default venue from the xml file
        /// </summary>
        /// <returns>default venue string</returns>
        public static string LoadDefaultVenue()
        {
            // template data filename
            var filename = XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.TemplateDataFileName;

            // load template data file, leave if it doesn't exist
            var xmlDocument = XmlHelper.LoadXmlFile(filename);
            if (xmlDocument is null)
                return "";

            // return the default value from xml file
            return xmlDocument.DocumentElement?.SelectSingleNode("Venues")?.SelectSingleNode("DefaultVenue")?.InnerText ?? "";
        }

        /// <summary>
        /// Load the formats from the template data file
        /// </summary>
        /// <returns>formats collection</returns>
        public static List<string> LoadFormats()
        {
            // template data filename
            var filename = XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.TemplateDataFileName;

            // venues collection
            var formats = new List<string>();

            // load template data file, leave if it doesn't exist
            var xmlDocument = XmlHelper.LoadXmlFile(filename);
            if (xmlDocument is null)
                return formats;

            // load files root node, leave if it doesn't exist
            var rootNode = xmlDocument.DocumentElement;
            if (rootNode is null)
                return formats;

            // load files format nodes list, leave if it don't exist or is empty
            var formatNodes = rootNode.SelectSingleNode("Formats")?.SelectNodes("Format");
            if (formatNodes is null || formatNodes.Count is 0)
                return formats;

            // add each format to the collection
            foreach (XmlNode node in formatNodes)
                if(node.InnerText.Length > 0) formats.Add(node.InnerText);

            // return collection sorted alphabetically
            return new List<string>(formats.OrderBy(i => i));
        }

        /// <summary>
        /// Load the game types from the template data file
        /// </summary>
        /// <returns>game types collection</returns>
        public static List<string> LoadGameTypes()
        {
            // template data filename
            var filename = XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.TemplateDataFileName;

            // game types collection
            var gameTypes = new List<string>();

            // load template data file, leave if it doesn't exist
            var xmlDocument = XmlHelper.LoadXmlFile(filename);
            if (xmlDocument is null)
                return gameTypes;

            // load files root node, leave if it doesn't exist
            var rootNode = xmlDocument.DocumentElement;
            if (rootNode is null)
                return gameTypes;

            // load files game types nodes list, leave if it don't exist or is empty
            var gameTypeNodes = rootNode.SelectSingleNode("GameTypes")?.SelectNodes("GameType");
            if (gameTypeNodes is null || gameTypeNodes.Count is 0)
                return gameTypes;

            // add each game type to the collection
            foreach (XmlNode node in gameTypeNodes)
                if (node.InnerText.Length > 0) gameTypes.Add(node.InnerText);

            // return collection sorted alphabetically
            return new List<string>(gameTypes.OrderBy(i => i));
        }

        /// <summary>
        /// Load the venues from the template data file
        /// </summary>
        /// <returns>venues collection</returns>
        public static List<string> LoadVenues()
        {
            // template data filename
            var filename = XmlHelper.PreferencesFolderName  + ProfileHelper.GetCurrentProfile() + XmlHelper.TemplateDataFileName;

            // venues collection
            var venues = new List<string>();

            // load template data file, leave if it doesn't exist
            var xmlDocument = XmlHelper.LoadXmlFile(filename);
            if (xmlDocument is null)
                return venues;

            // load files root node, leave if it doesn't exist
            var rootNode = xmlDocument.DocumentElement;
            if (rootNode is null)
                return venues;

            // load files venue nodes list, leave if it don't exist or is empty
            var venueNodes = rootNode.SelectSingleNode("Venues")?.SelectNodes("Venue");
            if (venueNodes is null || venueNodes.Count is 0)
                return venues;

            // add each venue to the collection
            foreach (XmlNode node in venueNodes) if(node.InnerText.Length > 0) venues.Add(node.InnerText);

            // return collection sorted alphabetically
            return new List<string>(venues.OrderBy(i => i));
        }

        /// <summary>
        /// Removes a format from the xml file
        /// </summary>
        /// <param name="format">format to remove</param>
        /// <returns>true is successful</returns>
        public static bool RemoveFormat(string format)
        {
            // null check
            if (format is null)
                return false;

            // trim blank space
            format = format.Trim();

            // no string or blank string
            if (format is "")
                return false;

            // load saved formats for duplicate check
            var formats = LoadFormats();

            // leave if its not on the list
            if (!formats.Any(v => format.Equals(v, StringComparison.OrdinalIgnoreCase)))
                return false;

            // remove format
            formats.Remove(format);

            // save formats to file
            SaveFormats(formats);

            // get default formats
            var defaultFormats = LoadDefaultFormats();

            // check for removed format in default formats, leave if not found
            if (defaultFormats.Any(i => i == format) is false)
                return true;

            // remove default format that matches removed format
            foreach (var defaultFormat in defaultFormats)
            {
                if(defaultFormat == format)
                {
                    defaultFormats.Remove(defaultFormat);
                    break;
                }
            }

            // update default formats
            SetDefaultFormats(defaultFormats);

            // success
            return true;
        }

        /// <summary>
        /// Removes a game type from the xml file
        /// </summary>
        /// <param name="gameType">game type to remove</param>
        /// <returns>true is successful</returns>
        public static bool RemoveGameType(string gameType)
        {
            // null check
            if (gameType is null)
                return false;

            // trim blank space
            gameType = gameType.Trim();

            // no string or blank string
            if (gameType is "")
                return false;

            // load saved game types for duplicate check
            var gameTypes = LoadGameTypes();

            // leave if its not on the list
            if (!gameTypes.Any(v => gameType.Equals(v, StringComparison.OrdinalIgnoreCase)))
                return false;

            // remove game type
            gameTypes.Remove(gameType);

            // save game types to file
            SaveGameTypes(gameTypes);

            // get default game type
            var defaultGameType = LoadDefaultGameType();

            // leave it if doesn't match
            if (defaultGameType.Equals(gameType, StringComparison.OrdinalIgnoreCase) is false)
                return true;

            // load game types
            var games = LoadGameTypes();

            // reset default to top of venues or blank if no other venues
            if (games.Count > 0)
                SetDefaultGameType(games[0]);
            else
                SetDefaultGameType("");

            // success
            return true;
        }

        /// <summary>
        /// Removes a venue from the xml file
        /// </summary>
        /// <param name="venue">venue to remove</param>
        /// <returns>true is successful</returns>
        public static bool RemoveVenue(string venue)
        {
            // null check
            if (venue is null)
                return false;

            // trim blank space
            venue = venue.Trim();

            // no string or blank string
            if (venue is "")
                return false;

            // load saved venues for duplicate check
            var venues = LoadVenues();

            // leave if its not on the list
            if (!venues.Any(v => venue.Equals(v, StringComparison.OrdinalIgnoreCase)))
                return false;

            // remove venue
            venues.Remove(venue);

            // save venues to file
            SaveVenues(venues);

            // get default venue
            var defaultVenue = LoadDefaultVenue();

            // leave it if doesn't match
            if (defaultVenue.Equals(venue, StringComparison.OrdinalIgnoreCase) is false)
                return true;

            // load venues
            var vens = LoadVenues();

            // reset default to top of venues or blank if no other venues
            if(vens.Count > 0)
                SetDefaultVenue(vens[0]);
            else
                SetDefaultVenue("");

            // success
            return true;
        }

        /// <summary>
        /// Save the game types to the template data xml file
        /// </summary>
        /// <param name="gameTypes">new game types to save</param>
        public static void SaveGameTypes(List<string> gameTypes)
        {
            // template data filename
            var filename = XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.TemplateDataFileName;

            // load template data xml file
            var doc = XmlHelper.LoadXmlFile(filename);

            // create template data file if it didn't exist
            if (doc is null)
            {
                CreateDefaultTemplateDataFile();
                doc = XmlHelper.LoadXmlFile(filename);
            }

            // error if still null
            if (doc is null)
                return;

            // game types root node
            var gameTypesRootNode = doc.CreateElement("GameTypes");

            // add game types child nodes to the game types root node
            foreach (var gameType in gameTypes)
            {
                XmlNode gameTypeNode = doc.CreateElement("GameType");
                gameTypeNode.InnerText = gameType.Trim();
                gameTypesRootNode.AppendChild(gameTypeNode);
            }

            // default game types node
            XmlNode defaultGameTypesNode = doc.CreateElement("DefaultGameType");
            defaultGameTypesNode.InnerText = LoadDefaultGameType();
            gameTypesRootNode.AppendChild(defaultGameTypesNode);

            // old game types node
            var oldGameTypesNode = doc.DocumentElement?.SelectSingleNode("GameTypes");

            // add new game types node to xml file
            if (oldGameTypesNode is null)
                doc.DocumentElement?.AppendChild(gameTypesRootNode);
            else
                doc.DocumentElement.ReplaceChild(gameTypesRootNode, oldGameTypesNode);

            // save file
            doc.Save(filename);
        }

        /// <summary>
        /// Save the formats to the template data xml file
        /// </summary>
        /// <param name="formats">new formats to save</param>
        public static void SaveFormats(List<string> formats)
        {
            // template data filename
            var filename = XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.TemplateDataFileName;

            // load template data xml file
            var doc = XmlHelper.LoadXmlFile(filename);

            // create template data file if it didn't exist
            if (doc is null)
            {
                CreateDefaultTemplateDataFile();
                doc = XmlHelper.LoadXmlFile(filename);
            }

            // error if still null
            if (doc is null)
                return;

            // format root node
            var formatsNode = doc.CreateElement("Formats");

            // add format child nodes to the formats root node
            foreach (var format in formats)
            {
                XmlNode formatNode = doc.CreateElement("Format");
                formatNode.InnerText = format.Trim();
                formatsNode.AppendChild(formatNode);
            }

            // default formats node(s)
            foreach(var format in LoadDefaultFormats())
            {
                XmlNode formatNode = doc.CreateElement("DefaultFormat");
                formatNode.InnerText = format;
                formatsNode.AppendChild(formatNode);
            }

            // old formats node
            var oldFormatsNode = doc.DocumentElement?.SelectSingleNode("Formats");

            // add new venues node to xml file
            if (oldFormatsNode is null)
                doc.DocumentElement?.AppendChild(formatsNode);
            else
                doc.DocumentElement.ReplaceChild(formatsNode, oldFormatsNode);

            // save file
            doc.Save(filename);
        }

        /// <summary>
        /// Save the venues to the template data xml file
        /// </summary>
        /// <param name="venues">new venues to save</param>
        public static void SaveVenues(List<string> venues)
        {
            // template data filename
            var filename = XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.TemplateDataFileName;

            // load template data xml file
            var doc = XmlHelper.LoadXmlFile(filename);

            // create template data file if it didn't exist
            if(doc is null)
            {
                CreateDefaultTemplateDataFile();
                doc = XmlHelper.LoadXmlFile(filename);
            }

            // error if still null
            if (doc is null)
                return;

            // venues root node
            var venuesRootNode = doc.CreateElement("Venues");

            // add venue child nodes to the venues root node
            foreach (var venue in venues)
            {
                XmlNode venueNode = doc.CreateElement("Venue");
                venueNode.InnerText = venue.Trim();
                venuesRootNode.AppendChild(venueNode);
            }

            // default venues node
            XmlNode defaultVenueNode = doc.CreateElement("DefaultVenue");
            defaultVenueNode.InnerText = LoadDefaultVenue();
            venuesRootNode.AppendChild(defaultVenueNode);

            // old venues node
            var oldVenuesNode = doc.DocumentElement?.SelectSingleNode("Venues");

            // add new venues node to xml file
            if (oldVenuesNode is null)
                doc.DocumentElement?.AppendChild(venuesRootNode);
            else
                doc.DocumentElement.ReplaceChild(venuesRootNode, oldVenuesNode);

            // save file
            doc.Save(filename);
        }

        /// <summary>
        /// Sets the default formats in the xml file
        /// </summary>
        /// <param name="newFormats">default formats</param>
        public static void SetDefaultFormats(List<string> newFormats)
        {
            // null check
            if(newFormats is null)
                return;

            // add formats in case we don't have any of them
            foreach (var format in newFormats) AddFormat(format);

            // template data filename
            var filename = XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.TemplateDataFileName;

            // load template data file, leave if it doesn't exist
            var xmlDocument = XmlHelper.LoadXmlFile(filename);
            if (xmlDocument is null)
                return;

            // load format from file
            var formats = LoadFormats();

            // create new format nodes
            XmlNode formatsNode = xmlDocument.CreateElement("Formats");
            foreach (var format in formats)
            {
                XmlNode formatNode = xmlDocument.CreateElement("Format");
                formatNode.InnerText = format;
                formatsNode.AppendChild(formatNode);
            }

            // create new default format nodes
            foreach (var format in newFormats)
            {
                XmlNode defaultNode = xmlDocument.CreateElement("DefaultFormat");
                defaultNode.InnerText = format;
                formatsNode.AppendChild(defaultNode);
            }

            // find old formats node
            XmlNode oldNode = xmlDocument.DocumentElement?.SelectSingleNode("Formats");

            // replace old node with new, or append if not found
            if (oldNode is not null)
                xmlDocument.DocumentElement?.ReplaceChild(formatsNode, oldNode);
            else
                xmlDocument.DocumentElement?.AppendChild(formatsNode);

            // save xml file
            xmlDocument.Save(filename);
        }

        /// <summary>
        /// Sets the default game type in the xml file
        /// </summary>
        /// <param name="gameType">default game type</param>
        public static void SetDefaultGameType(string gameType)
        {
            // null/zero check
            if(gameType is null || gameType is "")
                return;

            // add game type in case we don't have it
            AddGameType(gameType);

            // template data filename
            var filename = XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.TemplateDataFileName;

            // load template data file, leave if it doesn't exist
            var xmlDocument = XmlHelper.LoadXmlFile(filename);
            if (xmlDocument is null)
                return;

            // create new default game type node
            XmlNode newNode = xmlDocument.CreateElement("DefaultGameType");
            newNode.InnerText = gameType;

            // find old default game type node
            XmlNode oldNode = xmlDocument.DocumentElement?.SelectSingleNode("GameTypes")?.SelectSingleNode("DefaultGameType");

            // replace old node with new, or append if not found
            if (oldNode is not null)
                xmlDocument.DocumentElement?.SelectSingleNode("GameTypes")?.ReplaceChild(newNode, oldNode);
            else
                xmlDocument.DocumentElement?.SelectSingleNode("GameTypes")?.AppendChild(newNode);

            // save xml file
            xmlDocument.Save(filename);
        }

        /// <summary>
        /// sets the template default values in the xml file
        /// </summary>
        /// <param name="defaults">default values</param>
        public static void SetDefaultValues(TournamentTemplate defaults)
        {
            // null check
            if(defaults is null)
                return;

            // template data filename
            var filename = XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.TemplateDataFileName;

            // load template data file, leave if it doesn't exist
            var doc = XmlHelper.LoadXmlFile(filename);
            if (doc is null)
                return;

            // default values node
            var defaultValueNode = doc.CreateElement("DefaultValues");
            var valueNode = doc.CreateElement(nameof(TournamentTemplate.TournamentName));
            valueNode.InnerText = defaults.TournamentName;
            defaultValueNode.AppendChild(valueNode);
            valueNode = doc.CreateElement(nameof(TournamentTemplate.AddonBaseCost));
            valueNode.InnerText = defaults.AddonBaseCost.ToString();
            defaultValueNode.AppendChild(valueNode);
            valueNode = doc.CreateElement(nameof(TournamentTemplate.AddonRakeCost));
            valueNode.InnerText = defaults.AddonRakeCost.ToString();
            defaultValueNode.AppendChild(valueNode);
            valueNode = doc.CreateElement(nameof(TournamentTemplate.BlindLevels));
            valueNode.InnerText = defaults.BlindLevels.ToString();
            defaultValueNode.AppendChild(valueNode);
            valueNode = doc.CreateElement(nameof(TournamentTemplate.BuyinBaseCost));
            valueNode.InnerText = defaults.BuyinBaseCost.ToString();
            defaultValueNode.AppendChild(valueNode);
            valueNode = doc.CreateElement(nameof(TournamentTemplate.BuyinRakeCost));
            valueNode.InnerText = defaults.BuyinRakeCost.ToString();
            defaultValueNode.AppendChild(valueNode);
            valueNode = doc.CreateElement(nameof(TournamentTemplate.Guarantee));
            valueNode.InnerText = defaults.Guarantee.ToString();
            defaultValueNode.AppendChild(valueNode);
            valueNode = doc.CreateElement(nameof(TournamentTemplate.LateReg));
            valueNode.InnerText = defaults.LateReg.ToString();
            defaultValueNode.AppendChild(valueNode);
            valueNode = doc.CreateElement(nameof(TournamentTemplate.RebuyBaseCost));
            valueNode.InnerText = defaults.RebuyBaseCost.ToString();
            defaultValueNode.AppendChild(valueNode);
            valueNode = doc.CreateElement(nameof(TournamentTemplate.RebuyRakeCost));
            valueNode.InnerText = defaults.RebuyRakeCost.ToString();
            defaultValueNode.AppendChild(valueNode);
            valueNode = doc.CreateElement(nameof(TournamentTemplate.StartTime));
            valueNode.InnerText = defaults.StartTime.ToString();
            defaultValueNode.AppendChild(valueNode);
            valueNode = doc.CreateElement(nameof(TournamentTemplate.StackSizeAddon));
            valueNode.InnerText = defaults.StackSizeAddon.ToString();
            defaultValueNode.AppendChild(valueNode);
            valueNode = doc.CreateElement(nameof(TournamentTemplate.StackSizeRebuy));
            valueNode.InnerText = defaults.StackSizeRebuy.ToString();
            defaultValueNode.AppendChild(valueNode);
            valueNode = doc.CreateElement(nameof(TournamentTemplate.StackSizeStarting));
            valueNode.InnerText = defaults.StackSizeStarting.ToString();
            defaultValueNode.AppendChild(valueNode);
            valueNode = doc.CreateElement(nameof(TournamentTemplate.TableSize));
            valueNode.InnerText =  defaults.TableSize.ToString();
            defaultValueNode.AppendChild(valueNode);

            // find old default values node
            XmlNode oldNode = doc.DocumentElement?.SelectSingleNode("DefaultValues");

            // replace old node with new, or append if not found
            if (oldNode is not null)
                doc.DocumentElement?.ReplaceChild(defaultValueNode, oldNode);
            else
                doc.DocumentElement?.AppendChild(defaultValueNode);

            // save xml file
            doc.Save(filename);
        }

        /// <summary>
        /// Sets the default venue in the xml file
        /// </summary>
        /// <param name="gameType">default venue</param>
        public static void SetDefaultVenue(string venue)
        {
            // null/zero check
            if(venue is null || venue is "")
                return;

            // add venue in case we don't have it
            AddVenue(venue);

            // template data filename
            var filename = XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.TemplateDataFileName;

            // load template data file, leave if it doesn't exist
            var xmlDocument = XmlHelper.LoadXmlFile(filename);
            if (xmlDocument is null)
                return;

            // create new default venue node
            XmlNode newNode = xmlDocument.CreateElement("DefaultVenue");
            newNode.InnerText = venue;

            // find old default venue node
            XmlNode oldNode = xmlDocument.DocumentElement?.SelectSingleNode("Venues")?.SelectSingleNode("DefaultVenue");

            // replace old node with new, or append if not found
            if(oldNode is not null)
                xmlDocument.DocumentElement?.SelectSingleNode("Venues")?.ReplaceChild(newNode, oldNode);
            else
                xmlDocument.DocumentElement?.SelectSingleNode("Venues")?.AppendChild(newNode);

            // save xml file
            xmlDocument.Save(filename);
        }

        /// <summary>
        /// Create a default template data xml file
        /// </summary>
        private static void CreateDefaultTemplateDataFile()
        {
            // new blank xml file
            var doc = new XmlDocument();

            // root node
            var rootNode = doc.CreateElement("TemplateData");

            // formats node
            var childNode = doc.CreateElement("Formats");
            var baseNode = doc.CreateElement("Format");
            var defaultNode = doc.CreateElement("DefaultFormat");
            childNode.AppendChild(baseNode);
            childNode.AppendChild(defaultNode);
            rootNode.AppendChild(childNode);

            // game types node
            childNode = doc.CreateElement("GameTypes");
            baseNode = doc.CreateElement("GameType");
            defaultNode = doc.CreateElement("DefaultGameType");
            childNode.AppendChild(baseNode);
            childNode.AppendChild(defaultNode);
            rootNode.AppendChild(childNode);

            // venues node
            childNode = doc.CreateElement("Venues");
            baseNode = doc.CreateElement("Venue");
            defaultNode = doc.CreateElement("DefaultVenue");
            childNode.AppendChild(baseNode);
            childNode.AppendChild(defaultNode);
            rootNode.AppendChild(childNode);

            // default values node
            childNode = doc.CreateElement("DefaultValues");
            baseNode = doc.CreateElement(nameof(TournamentTemplate.TournamentName));
            childNode.AppendChild(baseNode);
            baseNode = doc.CreateElement(nameof(TournamentTemplate.AddonBaseCost));
            baseNode.InnerText = "0";
            childNode.AppendChild(baseNode);
            baseNode = doc.CreateElement(nameof(TournamentTemplate.AddonRakeCost));
            baseNode.InnerText = "0";
            childNode.AppendChild(baseNode);
            baseNode = doc.CreateElement(nameof(TournamentTemplate.BlindLevels));
            baseNode.InnerText = "10";
            childNode.AppendChild(baseNode);
            baseNode = doc.CreateElement(nameof(TournamentTemplate.BuyinBaseCost));
            baseNode.InnerText = "10";
            childNode.AppendChild(baseNode);
            baseNode = doc.CreateElement(nameof(TournamentTemplate.BuyinRakeCost));
            baseNode.InnerText = "1";
            childNode.AppendChild(baseNode);
            baseNode = doc.CreateElement(nameof(TournamentTemplate.Guarantee));
            baseNode.InnerText = "0";
            childNode.AppendChild(baseNode);
            baseNode = doc.CreateElement(nameof(TournamentTemplate.LateReg));
            baseNode.InnerText = "60";
            childNode.AppendChild(baseNode);
            baseNode = doc.CreateElement(nameof(TournamentTemplate.RebuyBaseCost));
            baseNode.InnerText = "0";
            childNode.AppendChild(baseNode);
            baseNode = doc.CreateElement(nameof(TournamentTemplate.RebuyRakeCost));
            baseNode.InnerText = "0";
            childNode.AppendChild(baseNode);
            baseNode = doc.CreateElement(nameof(TournamentTemplate.StartTime));
            baseNode.InnerText = "1/1/1111 5:00:00 PM";
            childNode.AppendChild(baseNode);
            baseNode = doc.CreateElement(nameof(TournamentTemplate.StackSizeAddon));
            baseNode.InnerText = "0";
            childNode.AppendChild(baseNode);
            baseNode = doc.CreateElement(nameof(TournamentTemplate.StackSizeRebuy));
            baseNode.InnerText = "0";
            childNode.AppendChild(baseNode);
            baseNode = doc.CreateElement(nameof(TournamentTemplate.StackSizeStarting));
            baseNode.InnerText = "5000";
            childNode.AppendChild(baseNode);
            baseNode = doc.CreateElement(nameof(TournamentTemplate.TableSize));
            baseNode.InnerText = "9";
            childNode.AppendChild(baseNode);

            rootNode.AppendChild(childNode);

            // add root node to xml file
            doc.AppendChild(rootNode);

            // save xml file
            doc.Save(XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.TemplateDataFileName);
        }

        /// <summary>
        /// Create a default template data xml file
        /// </summary>
        public static void CreateDefaultTemplateDataFile(string profile)
        {
            // new blank xml file
            var doc = new XmlDocument();

            // root node
            var rootNode = doc.CreateElement("TemplateData");

            // formats node
            var childNode = doc.CreateElement("Formats");
            var baseNode = doc.CreateElement("Format");
            var defaultNode = doc.CreateElement("DefaultFormat");
            childNode.AppendChild(baseNode);
            childNode.AppendChild(defaultNode);
            rootNode.AppendChild(childNode);

            // game types node
            childNode = doc.CreateElement("GameTypes");
            baseNode = doc.CreateElement("GameType");
            defaultNode = doc.CreateElement("DefaultGameType");
            childNode.AppendChild(baseNode);
            childNode.AppendChild(defaultNode);
            rootNode.AppendChild(childNode);

            // venues node
            childNode = doc.CreateElement("Venues");
            baseNode = doc.CreateElement("Venue");
            defaultNode = doc.CreateElement("DefaultVenue");
            childNode.AppendChild(baseNode);
            childNode.AppendChild(defaultNode);
            rootNode.AppendChild(childNode);

            // default values node
            childNode = doc.CreateElement("DefaultValues");
            baseNode = doc.CreateElement(nameof(TournamentTemplate.TournamentName));
            childNode.AppendChild(baseNode);
            baseNode = doc.CreateElement(nameof(TournamentTemplate.AddonBaseCost));
            baseNode.InnerText = "0";
            childNode.AppendChild(baseNode);
            baseNode = doc.CreateElement(nameof(TournamentTemplate.AddonRakeCost));
            baseNode.InnerText = "0";
            childNode.AppendChild(baseNode);
            baseNode = doc.CreateElement(nameof(TournamentTemplate.BlindLevels));
            baseNode.InnerText = "10";
            childNode.AppendChild(baseNode);
            baseNode = doc.CreateElement(nameof(TournamentTemplate.BuyinBaseCost));
            baseNode.InnerText = "10";
            childNode.AppendChild(baseNode);
            baseNode = doc.CreateElement(nameof(TournamentTemplate.BuyinRakeCost));
            baseNode.InnerText = "1";
            childNode.AppendChild(baseNode);
            baseNode = doc.CreateElement(nameof(TournamentTemplate.Guarantee));
            baseNode.InnerText = "0";
            childNode.AppendChild(baseNode);
            baseNode = doc.CreateElement(nameof(TournamentTemplate.LateReg));
            baseNode.InnerText = "60";
            childNode.AppendChild(baseNode);
            baseNode = doc.CreateElement(nameof(TournamentTemplate.RebuyBaseCost));
            baseNode.InnerText = "0";
            childNode.AppendChild(baseNode);
            baseNode = doc.CreateElement(nameof(TournamentTemplate.RebuyRakeCost));
            baseNode.InnerText = "0";
            childNode.AppendChild(baseNode);
            baseNode = doc.CreateElement(nameof(TournamentTemplate.StartTime));
            baseNode.InnerText = "1/1/1111 5:00:00 PM";
            childNode.AppendChild(baseNode);
            baseNode = doc.CreateElement(nameof(TournamentTemplate.StackSizeAddon));
            baseNode.InnerText = "0";
            childNode.AppendChild(baseNode);
            baseNode = doc.CreateElement(nameof(TournamentTemplate.StackSizeRebuy));
            baseNode.InnerText = "0";
            childNode.AppendChild(baseNode);
            baseNode = doc.CreateElement(nameof(TournamentTemplate.StackSizeStarting));
            baseNode.InnerText = "5000";
            childNode.AppendChild(baseNode);
            baseNode = doc.CreateElement(nameof(TournamentTemplate.TableSize));
            baseNode.InnerText = "9";
            childNode.AppendChild(baseNode);

            rootNode.AppendChild(childNode);

            // add root node to xml file
            doc.AppendChild(rootNode);

            // save xml file
            doc.Save(XmlHelper.PreferencesFolderName + profile + XmlHelper.TemplateDataFileName);
        }
    }
}
