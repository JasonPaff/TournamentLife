using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;
using Tournament_Life.Models.Tournaments;

namespace Tournament_Life.Helpers
{
    public static class ProfileHelper
    {
        /// <summary>
        /// Add a profile to the xml file
        /// </summary>
        /// <param name="profile">profile to add</param>
        /// <returns>false if duplicate</returns>
        public static bool AddProfile(string profile)
        {
            // load xml file
            var xmlDocument = XmlHelper.LoadXmlFile(XmlHelper.PreferencesFolderName + XmlHelper.ProfilesFileName, null);

            // create default if xml file doesn't exist
            if (xmlDocument is null)
            {
                CreateDefaultProfile();
                xmlDocument = XmlHelper.LoadXmlFile(XmlHelper.PreferencesFolderName + XmlHelper.ProfilesFileName, null);
                return true;
            }

            // load profiles
            var profiles = LoadProfiles();

            // check for duplicate
            if(profiles.Any(i => i.ToUpper() == profile.ToUpper()))
                return false;

            // create new profile node
            XmlNode profileNode = xmlDocument.CreateElement("Profile");

            // set profile node text
            profileNode.InnerText = profile;

            // add new profile to xml file
            xmlDocument?.DocumentElement?.AppendChild(profileNode);

            // save xml file
            xmlDocument.Save(XmlHelper.PreferencesFolderName + XmlHelper.ProfilesFileName);

            // create default files
            _ = new PreferencesHelper(profile);

            // create default database
            DatabaseHelper.CreateDatabase(profile);

            // success
            return true;
        }

        /// <summary>
        /// Prompts the user to create a default profile
        /// then creates a default profiles.xml file
        /// </summary>
        public static void CreateDefaultProfile()
        {
            // xml file
            var xmlDocument = new XmlDocument();

            // root node
            XmlNode rootNode = xmlDocument.CreateElement("Profiles");
            xmlDocument.AppendChild(rootNode);

            // default profile
            XmlNode profileNode = xmlDocument.CreateElement("Profile");
            profileNode.InnerText = "Default Profile";
            rootNode.AppendChild(profileNode);

            // current node
            XmlNode currentNode = xmlDocument.CreateElement("Current");
            currentNode.InnerText = "Default Profile";
            rootNode.AppendChild(currentNode);

            // save xml file
            xmlDocument.Save(XmlHelper.PreferencesFolderName + XmlHelper.ProfilesFileName);

            // create default files
            _ = new PreferencesHelper(ProfileHelper.GetCurrentProfile());

            // create default database
            DatabaseHelper.CreateDatabase(ProfileHelper.GetCurrentProfile());

            // create default tournament data file
            TournamentTemplateDataHelper.CreateDefaultTemplateDataFile(ProfileHelper.GetCurrentProfile());

            // some starter formats
            TournamentTemplateDataHelper.AddFormat("1R1A");
            TournamentTemplateDataHelper.AddFormat("2R1A");
            TournamentTemplateDataHelper.AddFormat("3R1A");
            TournamentTemplateDataHelper.AddFormat("2x-Chance");
            TournamentTemplateDataHelper.AddFormat("Ante Up");
            TournamentTemplateDataHelper.AddFormat("Big Ante");
            TournamentTemplateDataHelper.AddFormat("Bounty");
            TournamentTemplateDataHelper.AddFormat("Deep");
            TournamentTemplateDataHelper.AddFormat("Freezeout");
            TournamentTemplateDataHelper.AddFormat("Heads-Up");
            TournamentTemplateDataHelper.AddFormat("Hyper-Turbo");
            TournamentTemplateDataHelper.AddFormat("Jackpot Sng");
            TournamentTemplateDataHelper.AddFormat("Progressive Bounty");
            TournamentTemplateDataHelper.AddFormat("Progressive Total Bounty");
            TournamentTemplateDataHelper.AddFormat("Re-Entry");
            TournamentTemplateDataHelper.AddFormat("Rebuy");
            TournamentTemplateDataHelper.AddFormat("Satellite");
            TournamentTemplateDataHelper.AddFormat("Shootout");
            TournamentTemplateDataHelper.AddFormat("Shovefest");
            TournamentTemplateDataHelper.AddFormat("Super Bounty");
            TournamentTemplateDataHelper.AddFormat("Super Deep");
            TournamentTemplateDataHelper.AddFormat("Sng");
            TournamentTemplateDataHelper.AddFormat("Total Bounty");
            TournamentTemplateDataHelper.AddFormat("Turbo");
            List<string> defaultFormats = new List<string> { "Freezeout", "Deep" };
            TournamentTemplateDataHelper.SetDefaultFormats(defaultFormats);

            // some starter game types
            TournamentTemplateDataHelper.AddGameType("NLHE");
            TournamentTemplateDataHelper.AddGameType("PLO");
            TournamentTemplateDataHelper.AddGameType("PLO8");
            TournamentTemplateDataHelper.AddGameType("FLHE");
            TournamentTemplateDataHelper.SetDefaultGameType("NLHE");

            // some starter venues
            TournamentTemplateDataHelper.AddVenue("Global Poker");
            TournamentTemplateDataHelper.AddVenue("BetOnline");
            TournamentTemplateDataHelper.AddVenue("Americas Cardroom");
            TournamentTemplateDataHelper.AddVenue("Ignition");
            TournamentTemplateDataHelper.SetDefaultVenue("Global Poker");

            // starter templates
            CreateDefaultTemplates();
        }

        /// <summary>
        /// Create some default templates
        /// </summary>
        public static void CreateDefaultTemplates()
        {
            // load xml file
            var doc = XmlHelper.LoadXmlFile("Templates.xml");
            if (doc is null)
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

                // add new game types to template data
                TournamentTemplateDataHelper.AddGameType(template.GameType);

                // add new venues to template data
                TournamentTemplateDataHelper.AddVenue(template.Venue);

                // add new formats to template data
                foreach (var format in template.Formats)
                    TournamentTemplateDataHelper.AddFormat(format);

                TournamentTemplateHelper.SaveTournamentTemplate(template);
            }
        }

        /// <summary>
        /// Deletes the passed in profile from the xml file
        /// </summary>
        public static void DeleteProfile(string profile)
        {
            // load xml file
            var xmlDocument = XmlHelper.LoadXmlFile(XmlHelper.PreferencesFolderName + XmlHelper.ProfilesFileName, null);

            // no xml file, quit
            if(xmlDocument is null) return;

            // get profiles nodes
            var profilesNodeList = xmlDocument?.DocumentElement?.SelectNodes("Profile");

            // profile we want to delete
            XmlNode profileToDelete = null;

            // find matching node
            foreach(XmlNode profileNode in profilesNodeList) if(profileNode.InnerText == profile) profileToDelete = profileNode;

            // no match, leave
            if(profileToDelete is null) return;

            // remove matching node
            xmlDocument.DocumentElement.RemoveChild(profileToDelete);

            // save xml file
            xmlDocument.Save(XmlHelper.PreferencesFolderName + XmlHelper.ProfilesFileName);

            // clean up profile files
            DeleteProfileFiles(profile);
        }

        /// <summary>
        /// deletes all files associated with a profile
        /// </summary>
        private static void DeleteProfileFiles(string profile)
        {
            // bankroll file
            if(File.Exists(XmlHelper.PreferencesFolderName + profile + XmlHelper.BankrollFileName)) File.Delete(XmlHelper.PreferencesFolderName + profile + XmlHelper.BankrollFileName);

            // bankroll file
            if (File.Exists(XmlHelper.PreferencesFolderName + profile + XmlHelper.FiltersFileName)) File.Delete(XmlHelper.PreferencesFolderName + profile + XmlHelper.FiltersFileName);

            // preferences file
            if (File.Exists(XmlHelper.PreferencesFolderName + profile + XmlHelper.PreferencesFileName)) File.Delete(XmlHelper.PreferencesFolderName + profile + XmlHelper.PreferencesFileName);

            // recent tournaments file
            if (File.Exists(XmlHelper.PreferencesFolderName + profile + XmlHelper.RecentTournamentsFileName)) File.Delete(XmlHelper.PreferencesFolderName + profile + XmlHelper.RecentTournamentsFileName);

            // running tournaments file
            if (File.Exists(XmlHelper.PreferencesFolderName + profile + XmlHelper.RunningTournamentsFileName)) File.Delete(XmlHelper.PreferencesFolderName + profile + XmlHelper.RunningTournamentsFileName);

            // sessions file
            if (File.Exists(XmlHelper.PreferencesFolderName + profile + XmlHelper.SessionsFileName)) File.Delete(XmlHelper.PreferencesFolderName + profile + XmlHelper.SessionsFileName);

            // templates file
            if (File.Exists(XmlHelper.PreferencesFolderName + profile + XmlHelper.TemplatesFileName)) File.Delete(XmlHelper.PreferencesFolderName + profile + XmlHelper.TemplatesFileName);

            // templates data file
            if (File.Exists(XmlHelper.PreferencesFolderName + profile + XmlHelper.TemplateDataFileName)) File.Delete(XmlHelper.PreferencesFolderName + profile + XmlHelper.TemplateDataFileName);

            // tournaments database file
            if (File.Exists(XmlHelper.PreferencesFolderName + profile + "Tournaments.sdf")) File.Delete(XmlHelper.PreferencesFolderName + profile + DatabaseHelper.DatabaseName);
        }

        /// <summary>
        /// static return for the current profile
        /// </summary>
        /// <returns>current profile name</returns>
        public static string GetCurrentProfile()
        {
            // return current profile
            return XmlHelper.LoadXmlFile(XmlHelper.PreferencesFolderName + XmlHelper.ProfilesFileName)?.DocumentElement?.SelectSingleNode("Current")?.InnerText ?? "";
        }

        /// <summary>
        /// Returns a list of the profiles in the profiles.xml file
        /// </summary>
        /// <returns></returns>
        public static List<string> LoadProfiles()
        {
            // load xml file, create default if it doesn't exist
            var xmlDocument = XmlHelper.LoadXmlFile(XmlHelper.PreferencesFolderName + XmlHelper.ProfilesFileName);

            if (xmlDocument is null)
            {
                CreateDefaultProfile();
                xmlDocument = XmlHelper.LoadXmlFile(XmlHelper.PreferencesFolderName + XmlHelper.ProfilesFileName);
            }

            // profiles list
            var profiles = new List<string>();
            var profileNodes = xmlDocument?.DocumentElement?.SelectNodes("Profile");

            // grab profiles from xml file into profiles list
            foreach (XmlNode profileNode in profileNodes)
                profiles.Add(profileNode.InnerText);

            // return profiles list
            return profiles;
        }

        /// <summary>
        /// Sets the current profile in the xml file
        /// </summary>
        /// <param name="profile">profile to make current</param>
        public static void SetCurrentProfile(string profile)
        {
            // load xml file, create default if it doesn't exist
            var xmlDocument = XmlHelper.LoadXmlFile(XmlHelper.PreferencesFolderName + XmlHelper.ProfilesFileName, null);
            if (xmlDocument is null) { CreateDefaultProfile(); xmlDocument = XmlHelper.LoadXmlFile(XmlHelper.PreferencesFolderName + XmlHelper.ProfilesFileName, null); }

            // create new current node
            XmlNode currentNode = xmlDocument.CreateElement("Current");
            currentNode.InnerText = profile;

            // find old current node
            XmlNode oldCurrentNode = xmlDocument.DocumentElement?.SelectSingleNode("Current");

            // replace current node in xml file
            xmlDocument?.DocumentElement?.ReplaceChild(currentNode, oldCurrentNode);

            // save xml file
            xmlDocument.Save(XmlHelper.PreferencesFolderName + XmlHelper.ProfilesFileName);
        }
    }
}