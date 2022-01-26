using LiveTracker.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Xml;

namespace LiveTracker.Helpers
{
    public static class TournamentHelper
    {
        /// <summary>
        /// Load recently finished tournaments from file
        /// </summary>
        /// <returns>List of recently finished tournaments</returns>
        public static ObservableCollection<TournamentRunning> LoadRecentlyFinishedTournaments()
        {
            // temp collection for any tournament we may load
            var recentlyFinished = new ObservableCollection<TournamentRunning>();

            // load recently finished xml file
            var xmlDocument = XmlHelper.LoadXmlFile(XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.RecentTournamentsFileName);
            if (xmlDocument is null) return recentlyFinished;

            // loop through xml doc and create tournaments
            foreach (XmlNode tournamentNode in xmlDocument.GetElementsByTagName("Tournament"))
            {
                // create the tournament
                var tournament = new TournamentRunning()
                {
                    AddonBaseCost = decimal.Parse(tournamentNode.SelectSingleNode("AddonCost")?.InnerText ?? "0", new CultureInfo("en-US")),
                    AddonCount = int.Parse(tournamentNode.SelectSingleNode("AddonCount")?.InnerText ?? "0", new CultureInfo("en-US")),
                    AddonRakeCost = decimal.Parse(tournamentNode.SelectSingleNode("AddonRake")?.InnerText ?? "0", new CultureInfo("en-US")),
                    BlindLevels = int.Parse(tournamentNode.SelectSingleNode("BlindLevels")?.InnerText ?? "10", new CultureInfo("en-US")),
                    Bounty = decimal.Parse(tournamentNode.SelectSingleNode("Bounty")?.InnerText ?? "0", new CultureInfo("en-US")),
                    BountyCount = int.Parse(tournamentNode.SelectSingleNode("BountyCount")?.InnerText ?? "0", new CultureInfo("en-US")),
                    BuyinBaseCost = decimal.Parse(tournamentNode.SelectSingleNode("BuyinCost")?.InnerText ?? "10", new CultureInfo("en-US")),
                    BuyinRakeCost = decimal.Parse(tournamentNode.SelectSingleNode("BuyinRake")?.InnerText ?? "1", new CultureInfo("en-US")),
                    DatabaseId = int.Parse(tournamentNode.SelectSingleNode("DatabaseId")?.InnerText ?? "-1", new CultureInfo("en-US")),
                    Entrants = int.Parse(tournamentNode.SelectSingleNode("Entrants")?.InnerText ?? "0", new CultureInfo("en-US")),
                    EntrantsPaid = int.Parse(tournamentNode.SelectSingleNode("EntrantsPaid")?.InnerText ?? "0", new CultureInfo("en-US")),
                    EndTime = DateTime.Parse(tournamentNode.SelectSingleNode("EndTime")?.InnerText ?? "1111/1/1 12:00:00 AM", new CultureInfo("en-US")),
                    FinishPosition = int.Parse(tournamentNode.SelectSingleNode("FinishPosition")?.InnerText ?? "0", new CultureInfo("en-US")),
                    GameType = tournamentNode.SelectSingleNode("GameType")?.InnerText ?? "",
                    Guarantee = decimal.Parse(tournamentNode.SelectSingleNode("Guarantee")?.InnerText ?? "0", new CultureInfo("en-US")),
                    IsBovadaBounty = bool.Parse(tournamentNode.SelectSingleNode("IsBovadaBounty")?.InnerText ?? "False"),
                    IsFavorite = bool.Parse(tournamentNode.SelectSingleNode("Favorite")?.InnerText ?? "False"),
                    IsSng = bool.Parse(tournamentNode.SelectSingleNode("Favorite")?.InnerText ?? "False"),
                    JackpotSpinMultiplier = int.Parse(tournamentNode.SelectSingleNode("JackpotSpinMultiplier")?.InnerText ?? "0", new CultureInfo("en-US")),
                    LateReg = int.Parse(tournamentNode.SelectSingleNode("LateReg")?.InnerText ?? "0", new CultureInfo("en-US")),
                    Note = tournamentNode.SelectSingleNode("Note")?.InnerText ?? "",
                    PrizeWon = decimal.Parse(tournamentNode.SelectSingleNode("PrizeWon")?.InnerText ?? "0", new CultureInfo("en-US")),
                    RebuyBaseCost = decimal.Parse(tournamentNode.SelectSingleNode("RebuyCost")?.InnerText ?? "0", new CultureInfo("en-US")),
                    RebuyCount = int.Parse(tournamentNode.SelectSingleNode("RebuyCount")?.InnerText ?? "0", new CultureInfo("en-US")),
                    RebuyRakeCost = decimal.Parse(tournamentNode.SelectSingleNode("RebuyRake")?.InnerText ?? "0", new CultureInfo("en-US")),
                    ScreenshotFilename = tournamentNode.SelectSingleNode("Screenshot")?.InnerText ?? "",
                    SngPayouts = tournamentNode.SelectSingleNode("SngPayouts")?.InnerText ?? "",
                    StackSizeAddon = int.Parse(tournamentNode.SelectSingleNode("StackSizeAddon")?.InnerText ?? "0", new CultureInfo("en-US")),
                    StackSizeRebuy = int.Parse(tournamentNode.SelectSingleNode("StackSizeRebuy")?.InnerText ?? "0", new CultureInfo("en-US")),
                    StackSizeStarting = int.Parse(tournamentNode.SelectSingleNode("StackSizeStarting")?.InnerText ?? "0", new CultureInfo("en-US")),
                    StartTime = DateTime.Parse(tournamentNode.SelectSingleNode("StartTime")?.InnerText ?? "1111/1/1 12:00:00 AM", new CultureInfo("en-US")),
                    TableSize = int.Parse(tournamentNode.SelectSingleNode("TableSize")?.InnerText ?? "0", new CultureInfo("en-US")),
                    TemplateId = int.Parse(tournamentNode.SelectSingleNode("ID")?.InnerText ?? "-1", new CultureInfo("en-US")),
                    TimeZoneName = tournamentNode.SelectSingleNode("TimeZoneName")?.InnerText ?? TimeZoneInfo.Local.StandardName,
                    TournamentName = tournamentNode.SelectSingleNode("TournamentName")?.InnerText ?? "", Venue = tournamentNode.SelectSingleNode("Venue")?.InnerText ?? "",
                };

                // add formats
                var formatNodes = tournamentNode.SelectSingleNode("Formats")?.ChildNodes;
                if (formatNodes != null) foreach (XmlNode formatNode in formatNodes) tournament.Formats.Add(formatNode.InnerText);

                // add tournament
                recentlyFinished.Add(tournament);
            }

            return recentlyFinished;
        }

        /// <summary>
        /// load running tournaments from file
        /// </summary>
        /// <returns>collection of tournaments</returns>
        public static ObservableCollection<TournamentRunning> LoadRunningTournaments()
        {
            // save filename
            var filename = XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.RunningTournamentsFileName;

            // temp collection for any tournament we may load
            var runningTournaments = new ObservableCollection<TournamentRunning>();

            // load running tournaments file, leave if not found
            var xmlDocument = XmlHelper.LoadXmlFile(filename);
            if (xmlDocument is null) 
                return runningTournaments;

            // loop and add all the tournaments to the collection
            foreach (XmlNode tournamentNode in xmlDocument.GetElementsByTagName("Tournament"))
            {
                var tournament = new TournamentRunning()
                {
                    AddonBaseCost = decimal.Parse(tournamentNode.SelectSingleNode("AddonCost")?.InnerText ?? "0", new CultureInfo("en-US")),
                    AddonCount = int.Parse(tournamentNode.SelectSingleNode("AddonCount")?.InnerText ?? "0", new CultureInfo("en-US")),
                    AddonRakeCost = decimal.Parse(tournamentNode.SelectSingleNode("AddonRake")?.InnerText ?? "0", new CultureInfo("en-US")),
                    BlindLevels = int.Parse(tournamentNode.SelectSingleNode("BlindLevels")?.InnerText ?? "10", new CultureInfo("en-US")),
                    Bounty = decimal.Parse(tournamentNode.SelectSingleNode("Bounty")?.InnerText ?? "0", new CultureInfo("en-US")),
                    BountyCount = int.Parse(tournamentNode.SelectSingleNode("BountyCount")?.InnerText ?? "0", new CultureInfo("en-US")),
                    BuyinBaseCost = decimal.Parse(tournamentNode.SelectSingleNode("BuyinCost")?.InnerText ?? "10", new CultureInfo("en-US")),
                    BuyinRakeCost = decimal.Parse(tournamentNode.SelectSingleNode("BuyinRake")?.InnerText ?? "1", new CultureInfo("en-US")),
                    DatabaseId = int.Parse(tournamentNode.SelectSingleNode("DatabaseId")?.InnerText ?? "-1", new CultureInfo("en-US")),
                    Entrants = int.Parse(tournamentNode.SelectSingleNode("Entrants")?.InnerText ?? "0", new CultureInfo("en-US")),
                    EntrantsPaid = int.Parse(tournamentNode.SelectSingleNode("EntrantsPaid")?.InnerText ?? "0", new CultureInfo("en-US")),
                    EndTime = DateTime.Parse(tournamentNode.SelectSingleNode("EndTime")?.InnerText ?? "1111/1/1 12:00:00 AM", new CultureInfo("en-US")),
                    FinishPosition = int.Parse(tournamentNode.SelectSingleNode("FinishPosition")?.InnerText ?? "0", new CultureInfo("en-US")),
                    GameType = tournamentNode.SelectSingleNode("GameType")?.InnerText ?? "",
                    Guarantee = decimal.Parse(tournamentNode.SelectSingleNode("Guarantee")?.InnerText ?? "0", new CultureInfo("en-US")),
                    IsBovadaBounty = bool.Parse(tournamentNode.SelectSingleNode("IsBovadaBounty")?.InnerText ?? "False"),
                    IsFavorite = bool.Parse(tournamentNode.SelectSingleNode("Favorite")?.InnerText ?? "False"),
                    IsSng = bool.Parse(tournamentNode.SelectSingleNode("IsSng")?.InnerText ?? "False"),
                    JackpotSpinMultiplier = int.Parse(tournamentNode.SelectSingleNode("JackpotSpinMultiplier")?.InnerText ?? "0", new CultureInfo("en-US")),
                    LateReg = int.Parse(tournamentNode.SelectSingleNode("LateReg")?.InnerText ?? "0", new CultureInfo("en-US")),
                    Note = tournamentNode.SelectSingleNode("Note")?.InnerText ?? "",
                    PrizeWon = decimal.Parse(tournamentNode.SelectSingleNode("PrizeWon")?.InnerText ?? "0", new CultureInfo("en-US")),
                    RebuyBaseCost = decimal.Parse(tournamentNode.SelectSingleNode("RebuyCost")?.InnerText ?? "0", new CultureInfo("en-US")),
                    RebuyCount = int.Parse(tournamentNode.SelectSingleNode("RebuyCount")?.InnerText ?? "0", new CultureInfo("en-US")),
                    RebuyRakeCost = decimal.Parse(tournamentNode.SelectSingleNode("RebuyRake")?.InnerText ?? "0", new CultureInfo("en-US")),
                    ScreenshotFilename = tournamentNode.SelectSingleNode("Screenshot")?.InnerText ?? "",
                    SngPayouts = tournamentNode.SelectSingleNode("SngPayouts")?.InnerText ?? "",
                    StackSizeAddon = int.Parse(tournamentNode.SelectSingleNode("StackSizeAddon")?.InnerText ?? "0", new CultureInfo("en-US")),
                    StackSizeRebuy = int.Parse(tournamentNode.SelectSingleNode("StackSizeRebuy")?.InnerText ?? "0", new CultureInfo("en-US")),
                    StackSizeStarting = int.Parse(tournamentNode.SelectSingleNode("StackSizeStarting")?.InnerText ?? "0", new CultureInfo("en-US")),
                    StartTime = DateTime.Parse(tournamentNode.SelectSingleNode("StartTime")?.InnerText ?? "1111/1/1 12:00:00 AM", new CultureInfo("en-US")),
                    TableSize = int.Parse(tournamentNode.SelectSingleNode("TableSize")?.InnerText ?? "0", new CultureInfo("en-US")),
                    TemplateId = int.Parse(tournamentNode.SelectSingleNode("ID")?.InnerText ?? "-1", new CultureInfo("en-US")),
                    TimeZoneName = tournamentNode.SelectSingleNode("TimeZoneName")?.InnerText ?? TimeZoneInfo.Local.StandardName,
                    TournamentName = tournamentNode.SelectSingleNode("TournamentName")?.InnerText ?? "",
                    Venue = tournamentNode.SelectSingleNode("Venue")?.InnerText ?? "",
                };

                // add formats
                var formatNodes = tournamentNode.SelectSingleNode("Formats")?.ChildNodes;
                if (formatNodes is not null) 
                    foreach (XmlNode formatNode in formatNodes) 
                        tournament.Formats.Add(formatNode.InnerText);

                // remove seconds from start time and end time
                tournament.StartTime = new DateTime(tournament.StartTime.Year, tournament.StartTime.Month, tournament.StartTime.Day, tournament.StartTime.Hour, tournament.StartTime.Minute, tournament.StartTime.Second);
                tournament.EndTime = new DateTime(tournament.EndTime.Year, tournament.EndTime.Month, tournament.EndTime.Day, tournament.EndTime.Hour, tournament.EndTime.Minute, tournament.StartTime.Second);

                // update tournament starting visibility
                tournament.SetInitialTournamentStatus();

                // add tournament
                runningTournaments.Add(tournament);
            }

            // return tournaments loaded from file
            return runningTournaments;
        }

        /// <summary>
        /// Save recently finished tournaments list
        /// </summary>
        /// <param name="tournaments">tournaments to save</param>
        public static void SaveRecentlyFinishedTournaments(ObservableCollection<TournamentRunning> tournaments)
        {
            // recently finished file
            var filename = XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.RecentTournamentsFileName;

            // create new xml document
            var xmlDocument = new XmlDocument();

            // root tournaments node
            var tournamentsNode = xmlDocument.CreateElement("Tournaments");

            // add root node to xml file
            xmlDocument.AppendChild(tournamentsNode);

            // null/zero check, save blank if true
            if (tournaments is null || tournaments.Count is 0)
            {
                try { xmlDocument.Save(filename); }
                catch (IOException ioException) { }
                return;
            }

            // loop and load all tournaments from the file
            foreach (var tournament in tournaments)
            {
                var tournamentNode = xmlDocument.CreateElement("Tournament");

                var tournamentId = xmlDocument.CreateElement("ID");
                tournamentId.InnerText = tournament.TemplateId.ToString(new CultureInfo("en-US"));
                tournamentNode.AppendChild(tournamentId);

                var databaseId = xmlDocument.CreateElement("DatabaseId");
                databaseId.InnerText = tournament.DatabaseId.ToString(new CultureInfo("en-US"));
                tournamentNode.AppendChild(databaseId);

                var tournamentAddonNode = xmlDocument.CreateElement("AddonCost");
                tournamentAddonNode.InnerText = tournament.AddonBaseCost.ToString(new CultureInfo("en-US"));
                tournamentNode.AppendChild(tournamentAddonNode);

                var tournamentAddonCount = xmlDocument.CreateElement("AddonCount");
                tournamentAddonCount.InnerText = tournament.AddonCount.ToString(new CultureInfo("en-US"));
                tournamentNode.AppendChild(tournamentAddonCount);

                var tournamentAddonRakeNode = xmlDocument.CreateElement("AddonRake");
                tournamentAddonRakeNode.InnerText = tournament.AddonRakeCost.ToString(new CultureInfo("en-US"));
                tournamentNode.AppendChild(tournamentAddonRakeNode);

                var tournamentBlindLevelsNode = xmlDocument.CreateElement("BlindLevels");
                tournamentBlindLevelsNode.InnerText = tournament.BlindLevels.ToString(new CultureInfo("en-US"));
                tournamentNode.AppendChild(tournamentBlindLevelsNode);

                var tournamentBuyinNode = xmlDocument.CreateElement("BuyinCost");
                tournamentBuyinNode.InnerText = tournament.BuyinBaseCost.ToString(new CultureInfo("en-US"));
                tournamentNode.AppendChild(tournamentBuyinNode);

                var tournamentBuyinRakeNode = xmlDocument.CreateElement("BuyinRake");
                tournamentBuyinRakeNode.InnerText = tournament.BuyinRakeCost.ToString(new CultureInfo("en-US"));
                tournamentNode.AppendChild(tournamentBuyinRakeNode);

                var tournamentEndTimeNode = xmlDocument.CreateElement("EndTime");
                tournamentEndTimeNode.InnerText = tournament.EndTime.ToString(new CultureInfo("en-US"));
                tournamentNode.AppendChild(tournamentEndTimeNode);

                var tournamentEntrantsNode = xmlDocument.CreateElement("Entrants");
                tournamentEntrantsNode.InnerText = tournament.Entrants.ToString(new CultureInfo("en-US"));
                tournamentNode.AppendChild(tournamentEntrantsNode);

                var tournamentEntrantsPaidNode = xmlDocument.CreateElement("EntrantsPaid");
                tournamentEntrantsPaidNode.InnerText = tournament.EntrantsPaid.ToString(new CultureInfo("en-US"));
                tournamentNode.AppendChild(tournamentEntrantsPaidNode);

                var tournamentSngPayoutsNode = xmlDocument.CreateElement("SngPayouts");
                tournamentSngPayoutsNode.InnerText = tournament.SngPayouts;
                tournamentNode.AppendChild(tournamentSngPayoutsNode);

                var tournamentFavorite = xmlDocument.CreateElement("Favorite");
                tournamentFavorite.InnerText = tournament.IsFavorite.ToString(new CultureInfo("en-US"));
                tournamentNode.AppendChild(tournamentFavorite);

                var tournamentFinishNode = xmlDocument.CreateElement("FinishPosition");
                tournamentFinishNode.InnerText = tournament.FinishPosition.ToString(new CultureInfo("en-US"));
                tournamentNode.AppendChild(tournamentFinishNode);

                var tournamentFormatsNode = xmlDocument.CreateElement("Formats");
                foreach (var gameFormat in tournament.Formats)
                {
                    var tournamentFormatNode = xmlDocument.CreateElement("GameFormat");
                    tournamentFormatNode.InnerText = gameFormat;
                    tournamentFormatsNode.AppendChild(tournamentFormatNode);
                }
                tournamentNode.AppendChild(tournamentFormatsNode);

                var tournamentTypeNode = xmlDocument.CreateElement("GameType");
                tournamentTypeNode.InnerText = tournament.GameType;
                tournamentNode.AppendChild(tournamentTypeNode);

                var tournamentGuaranteeNode = xmlDocument.CreateElement("Guarantee");
                tournamentGuaranteeNode.InnerText = tournament.Guarantee.ToString(new CultureInfo("en-US"));
                tournamentNode.AppendChild(tournamentGuaranteeNode);

                var tournamentJackpotSpinMultiplierNode = xmlDocument.CreateElement("JackpotSpinMultiplier");
                tournamentJackpotSpinMultiplierNode.InnerText = tournament.JackpotSpinMultiplier.ToString(new CultureInfo("en-US"));
                tournamentNode.AppendChild(tournamentJackpotSpinMultiplierNode);

                var tournamentLateRegNode = xmlDocument.CreateElement("LateReg");
                tournamentLateRegNode.InnerText = tournament.LateReg.ToString(new CultureInfo("en-US"));
                tournamentNode.AppendChild(tournamentLateRegNode);

                var noteNode = xmlDocument.CreateElement("Note");
                noteNode.InnerText = tournament.Note;
                tournamentNode.AppendChild(noteNode);

                var tournamentPrizeNode = xmlDocument.CreateElement("PrizeWon");
                tournamentPrizeNode.InnerText = tournament.PrizeWon.ToString(new CultureInfo("en-US"));
                tournamentNode.AppendChild(tournamentPrizeNode);

                var tournamentRebuyNode = xmlDocument.CreateElement("RebuyCost");
                tournamentRebuyNode.InnerText = tournament.RebuyBaseCost.ToString(new CultureInfo("en-US"));
                tournamentNode.AppendChild(tournamentRebuyNode);

                var tournamentRebuyCount = xmlDocument.CreateElement("RebuyCount");
                tournamentRebuyCount.InnerText = tournament.RebuyCount.ToString(new CultureInfo("en-US"));
                tournamentNode.AppendChild(tournamentRebuyCount);

                var tournamentRebuyRakeNode = xmlDocument.CreateElement("RebuyRake");
                tournamentRebuyRakeNode.InnerText = tournament.RebuyRakeCost.ToString(new CultureInfo("en-US"));
                tournamentNode.AppendChild(tournamentRebuyRakeNode);

                var screenshotNode = xmlDocument.CreateElement("Screenshot");
                screenshotNode.InnerText = tournament.ScreenshotFilename;
                tournamentNode.AppendChild(screenshotNode);

                var tournamentAddonStackNode = xmlDocument.CreateElement("StackSizeAddon");
                tournamentAddonStackNode.InnerText = tournament.StackSizeAddon.ToString(new CultureInfo("en-US"));
                tournamentNode.AppendChild(tournamentAddonStackNode);

                var tournamentRebuyStackNode = xmlDocument.CreateElement("StackSizeRebuy");
                tournamentRebuyStackNode.InnerText = tournament.StackSizeRebuy.ToString(new CultureInfo("en-US"));

                var tournamentStartingStackNode = xmlDocument.CreateElement("StackSizeStarting");
                tournamentStartingStackNode.InnerText = tournament.StackSizeStarting.ToString(new CultureInfo("en-US"));
                tournamentNode.AppendChild(tournamentStartingStackNode);
                tournamentNode.AppendChild(tournamentRebuyStackNode);

                var tournamentStartTimeNode = xmlDocument.CreateElement("StartTime");
                tournamentStartTimeNode.InnerText = tournament.StartTime.ToString(new CultureInfo("en-US"));
                tournamentNode.AppendChild(tournamentStartTimeNode);

                var tournamentTableSize = xmlDocument.CreateElement("TableSize");
                tournamentTableSize.InnerText = tournament.TableSize.ToString(new CultureInfo("en-US"));
                tournamentNode.AppendChild(tournamentTableSize);

                var tournamentTimeZoneName = xmlDocument.CreateElement("TimeZoneName");
                tournamentTimeZoneName.InnerText = tournament.TimeZoneName;
                tournamentNode.AppendChild(tournamentTimeZoneName);

                var tournamentNameNode = xmlDocument.CreateElement("TournamentName");
                tournamentNameNode.InnerText = tournament.TournamentName;
                tournamentNode.AppendChild(tournamentNameNode);

                var tournamentSiteNode = xmlDocument.CreateElement("Venue");
                tournamentSiteNode.InnerText = tournament.Venue;
                tournamentNode.AppendChild(tournamentSiteNode);

                var tournamentChildNode = xmlDocument.CreateElement("IsBovadaBounty");
                tournamentChildNode.InnerText = tournament.IsBovadaBounty.ToString(new CultureInfo("en-US"));
                tournamentNode.AppendChild(tournamentChildNode);

                tournamentChildNode = xmlDocument.CreateElement("IsSng");
                tournamentChildNode.InnerText = tournament.IsSng.ToString(new CultureInfo("en-US"));
                tournamentNode.AppendChild(tournamentChildNode);

                tournamentChildNode = xmlDocument.CreateElement("Bounty");
                tournamentChildNode.InnerText = tournament.Bounty.ToString(new CultureInfo("en-US"));
                tournamentNode.AppendChild(tournamentChildNode);

                tournamentChildNode = xmlDocument.CreateElement("BountyCount");
                tournamentChildNode.InnerText = tournament.BountyCount.ToString(new CultureInfo("en-US"));
                tournamentNode.AppendChild(tournamentChildNode);

                tournamentsNode.AppendChild(tournamentNode);
            }

            // save file
            try { xmlDocument.Save(filename); }
            catch (IOException Ex) { }
        }

        /// <summary>
        /// Save running tournaments to file
        /// </summary>
        /// <param name="tournaments">tournaments to save</param>
        public static void SaveRunningTournaments(List<TournamentRunning> tournaments)
        {
            // save filename
            var filename = XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.RunningTournamentsFileName;

            // blank xml document
            var xmlDocument = new XmlDocument();

            // create root tournaments node & append to xml document
            var tournamentsNode = xmlDocument.CreateElement("Tournaments");
            xmlDocument.AppendChild(tournamentsNode);

            // null/zero check, save blank file and return if true
            if (tournaments is null || tournaments.Count is 0)
            {
                try { xmlDocument.Save(filename); }
                catch (IOException ioException) { }
                return;
            }

            // loop tournaments, add/create xml nodes and add to xml file
            foreach (var tournament in tournaments)
            {
                // null check
                if (tournament is null) continue;

                var tournamentRootNode = xmlDocument.CreateElement("Tournament");

                var tournamentChildNode = xmlDocument.CreateElement("ID");
                tournamentChildNode.InnerText = tournament.TemplateId.ToString(new CultureInfo("en-US"));
                tournamentRootNode.AppendChild(tournamentChildNode);

                tournamentChildNode = xmlDocument.CreateElement("Favorite");
                tournamentChildNode.InnerText = tournament.IsFavorite.ToString(new CultureInfo("en-US"));
                tournamentRootNode.AppendChild(tournamentChildNode);

                tournamentChildNode = xmlDocument.CreateElement("Screenshot");
                tournamentChildNode.InnerText = tournament.ScreenshotFilename;
                tournamentRootNode.AppendChild(tournamentChildNode);

                tournamentChildNode = xmlDocument.CreateElement("Note");
                tournamentChildNode.InnerText = tournament.Note;
                tournamentRootNode.AppendChild(tournamentChildNode);

                tournamentChildNode = xmlDocument.CreateElement("TournamentName");
                tournamentChildNode.InnerText = tournament.TournamentName;
                tournamentRootNode.AppendChild(tournamentChildNode);

                tournamentChildNode = xmlDocument.CreateElement("Bounty");
                tournamentChildNode.InnerText = tournament.Bounty.ToString(new CultureInfo("en-US"));
                tournamentRootNode.AppendChild(tournamentChildNode);

                tournamentChildNode = xmlDocument.CreateElement("BountyCount");
                tournamentChildNode.InnerText = tournament.BountyCount.ToString(new CultureInfo("en-US"));
                tournamentRootNode.AppendChild(tournamentChildNode);

                tournamentChildNode = xmlDocument.CreateElement("Entrants");
                tournamentChildNode.InnerText = tournament.Entrants.ToString(new CultureInfo("en-US"));
                tournamentRootNode.AppendChild(tournamentChildNode);

                tournamentChildNode = xmlDocument.CreateElement("EntrantsPaid");
                tournamentChildNode.InnerText = tournament.EntrantsPaid.ToString(new CultureInfo("en-US"));
                tournamentRootNode.AppendChild(tournamentChildNode);

                tournamentChildNode = xmlDocument.CreateElement("SngPayouts");
                tournamentChildNode.InnerText = tournament.SngPayouts;
                tournamentRootNode.AppendChild(tournamentChildNode);

                tournamentChildNode = xmlDocument.CreateElement("IsBovadaBounty");
                tournamentChildNode.InnerText = tournament.IsBovadaBounty.ToString(new CultureInfo("en-US"));
                tournamentRootNode.AppendChild(tournamentChildNode);

                tournamentChildNode = xmlDocument.CreateElement("IsSng");
                tournamentChildNode.InnerText = tournament.IsSng.ToString();
                tournamentRootNode.AppendChild(tournamentChildNode);

                tournamentChildNode = xmlDocument.CreateElement("FinishPosition");
                tournamentChildNode.InnerText = tournament.FinishPosition.ToString(new CultureInfo("en-US"));
                tournamentRootNode.AppendChild(tournamentChildNode);

                tournamentChildNode = xmlDocument.CreateElement("JackpotSpinMultiplier");
                tournamentChildNode.InnerText = tournament.JackpotSpinMultiplier.ToString(new CultureInfo("en-US"));
                tournamentRootNode.AppendChild(tournamentChildNode);

                tournamentChildNode = xmlDocument.CreateElement("PrizeWon");
                tournamentChildNode.InnerText = tournament.PrizeWon.ToString(new CultureInfo("en-US"));
                tournamentRootNode.AppendChild(tournamentChildNode);

                tournamentChildNode = xmlDocument.CreateElement("BuyinCost");
                tournamentChildNode.InnerText = tournament.BuyinBaseCost.ToString(new CultureInfo("en-US"));
                tournamentRootNode.AppendChild(tournamentChildNode);

                tournamentChildNode = xmlDocument.CreateElement("BuyinRake");
                tournamentChildNode.InnerText = tournament.BuyinRakeCost.ToString(new CultureInfo("en-US"));
                tournamentRootNode.AppendChild(tournamentChildNode);

                tournamentChildNode = xmlDocument.CreateElement("AddonCost");
                tournamentChildNode.InnerText = tournament.AddonBaseCost.ToString(new CultureInfo("en-US"));
                tournamentRootNode.AppendChild(tournamentChildNode);

                tournamentChildNode = xmlDocument.CreateElement("AddonRake");
                tournamentChildNode.InnerText = tournament.AddonRakeCost.ToString(new CultureInfo("en-US"));
                tournamentRootNode.AppendChild(tournamentChildNode);

                tournamentChildNode = xmlDocument.CreateElement("RebuyCost");
                tournamentChildNode.InnerText = tournament.RebuyBaseCost.ToString(new CultureInfo("en-US"));
                tournamentRootNode.AppendChild(tournamentChildNode);

                tournamentChildNode = xmlDocument.CreateElement("RebuyRake");
                tournamentChildNode.InnerText = tournament.RebuyRakeCost.ToString(new CultureInfo("en-US"));
                tournamentRootNode.AppendChild(tournamentChildNode);

                tournamentChildNode = xmlDocument.CreateElement("StackSizeStarting");
                tournamentChildNode.InnerText = tournament.StackSizeStarting.ToString(new CultureInfo("en-US"));
                tournamentRootNode.AppendChild(tournamentChildNode);

                tournamentChildNode = xmlDocument.CreateElement("StackSizeAddon");
                tournamentChildNode.InnerText = tournament.StackSizeAddon.ToString(new CultureInfo("en-US"));
                tournamentRootNode.AppendChild(tournamentChildNode);

                tournamentChildNode = xmlDocument.CreateElement("StackSizeRebuy");
                tournamentChildNode.InnerText = tournament.StackSizeRebuy.ToString(new CultureInfo("en-US"));
                tournamentRootNode.AppendChild(tournamentChildNode);

                tournamentChildNode = xmlDocument.CreateElement("RebuyCount");
                tournamentChildNode.InnerText = tournament.RebuyCount.ToString(new CultureInfo("en-US"));
                tournamentRootNode.AppendChild(tournamentChildNode);

                tournamentChildNode = xmlDocument.CreateElement("AddonCount");
                tournamentChildNode.InnerText = tournament.AddonCount.ToString(new CultureInfo("en-US"));
                tournamentRootNode.AppendChild(tournamentChildNode);

                tournamentChildNode = xmlDocument.CreateElement("Guarantee");
                tournamentChildNode.InnerText = tournament.Guarantee.ToString(new CultureInfo("en-US"));
                tournamentRootNode.AppendChild(tournamentChildNode);

                tournamentChildNode = xmlDocument.CreateElement("StartTime");
                tournamentChildNode.InnerText = tournament.StartTime.ToString(new CultureInfo("en-US"));
                tournamentRootNode.AppendChild(tournamentChildNode);

                tournamentChildNode = xmlDocument.CreateElement("EndTime");
                tournamentChildNode.InnerText = tournament.EndTime.ToString(new CultureInfo("en-US"));
                tournamentRootNode.AppendChild(tournamentChildNode);

                tournamentChildNode = xmlDocument.CreateElement("BlindLevels");
                tournamentChildNode.InnerText = tournament.BlindLevels.ToString(new CultureInfo("en-US"));
                tournamentRootNode.AppendChild(tournamentChildNode);

                tournamentChildNode = xmlDocument.CreateElement("LateReg");
                tournamentChildNode.InnerText = tournament.LateReg.ToString(new CultureInfo("en-US"));
                tournamentRootNode.AppendChild(tournamentChildNode);

                tournamentChildNode = xmlDocument.CreateElement("TableSize");
                tournamentChildNode.InnerText = tournament.TableSize.ToString(new CultureInfo("en-US"));
                tournamentRootNode.AppendChild(tournamentChildNode);

                tournamentChildNode = xmlDocument.CreateElement("TimeZoneName");
                tournamentChildNode.InnerText = tournament.TimeZoneName;
                tournamentRootNode.AppendChild(tournamentChildNode);

                tournamentChildNode = xmlDocument.CreateElement("Venue");
                tournamentChildNode.InnerText = tournament.Venue;
                tournamentRootNode.AppendChild(tournamentChildNode);

                tournamentChildNode = xmlDocument.CreateElement("GameType");
                tournamentChildNode.InnerText = tournament.GameType;
                tournamentRootNode.AppendChild(tournamentChildNode);

                tournamentChildNode = xmlDocument.CreateElement("Formats");
                foreach (var gameFormat in tournament.Formats)
                {
                    var tournamentFormatNode = xmlDocument.CreateElement("GameFormat");
                    tournamentFormatNode.InnerText = gameFormat;
                    tournamentChildNode.AppendChild(tournamentFormatNode);
                }
                tournamentRootNode.AppendChild(tournamentChildNode);

                tournamentsNode.AppendChild(tournamentRootNode);
            }

            // save file
            try { xmlDocument.Save(filename); }
            catch (IOException ioException) { }
        }
    }
}