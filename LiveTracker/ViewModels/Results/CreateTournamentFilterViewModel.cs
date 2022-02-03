using System;
using System.Collections.ObjectModel;
using System.Linq;
using Syncfusion.Windows.Shared;
using System.Windows.Input;
using Syncfusion.UI.Xaml.Utility;
using System.Windows;
using Tournament_Life.Views.Results;
using System.Xml;
using System.Globalization;
using Tournament_Life.Commands;
using Tournament_Life.Helpers;
using Tournament_Life.Models;
using Tournament_Life.Models.Filters;
using Tournament_Life.Models.Tournaments;
using Tournament_Life.Views;

namespace Tournament_Life.ViewModels.Results
{
    public class CreateTournamentFilterViewModel : NotificationObject
    {
        private ObservableCollection<TournamentFinished> _tournaments;
        private DateTime _startingDate;
        private DateTime _startingTime;

        public CreateTournamentFilterViewModel(ObservableCollection<TournamentFinished> tournaments)
        {
            // font size
            FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));

            // visual theme
            Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");

            // copy tournaments
            _tournaments = tournaments;

            // starting date time
            _startingDate = DateTime.Now;
            _startingTime = new DateTime(_startingDate.Year, _startingDate.Month, _startingDate.Day, 12, 0, 0);

            // filter date
            StartDateLow = _startingDate;
            StartDateHigh = _startingDate;

            // filter time
            StartTimeLow = _startingTime;
            StartTimeHigh = _startingTime;

            // name properties to blanks
            NameIncludes = "";
            NameExcludes = "";

            // table size to min and max
            TableSizeLow = 2;
            TableSizeHigh = 2;

            // create list box items
            CreateListBoxItems();
        }

        public string CurrentFilter { get; set; }
        public int FontSize { get; set; }
        public string Theme { get; set; }

        #region Properties

        public decimal AddonCostLow { get; set; }
        public decimal AddonCostHigh { get; set; }
        public int AddonCountLow { get; set; }
        public int AddonCountHigh { get; set; }
        public int AddonStackLow { get; set; }
        public int AddonStackHigh { get; set; }
        public int BlindLevelsLow { get; set; }
        public int BlindLevelsHigh { get; set; }
        public decimal BuyinCostLow { get; set; }
        public decimal BuyinCostHigh { get; set; }
        public int EntrantsLow { get; set; }
        public int EntrantsHigh { get; set; }
        public int EntrantsPaidLow { get; set; }
        public int EntrantsPaidHigh { get; set; }
        public decimal FieldBeatenLow { get; set; }
        public decimal FieldBeatenHigh { get; set; }
        public string FilterName { get; set; }
        public int FinishPositionLow { get; set; }
        public int FinishPositionHigh { get; set; }
        public decimal GuaranteeLow { get; set; }
        public decimal GuaranteeHigh { get; set; }
        public int LateRegLow { get; set; }
        public int LateRegHigh { get; set; }
        public string NameIncludes { get; set; }
        public string NameExcludes { get; set; }
        public int NumberOfResults { get; set; }
        public decimal PrizeWonLow { get; set; }
        public decimal PrizeWonHigh { get; set; }
        public decimal RebuyCostLow { get; set; }
        public decimal RebuyCostHigh { get; set; }
        public int RebuyStackLow { get; set; }
        public int RebuyStackHigh { get; set; }
        public int RebuyCountLow { get; set; }
        public int RebuyCountHigh { get; set; }
        public DateTime StartDateLow { get; set; }
        public DateTime StartDateHigh { get; set; }
        public int StartingStackLow { get; set; }
        public int StartingStackHigh { get; set; }
        public DateTime StartTimeLow { get; set; }
        public DateTime StartTimeHigh { get; set; }
        public int TableSizeLow { get; set; }
        public int TableSizeHigh { get; set; }
        public decimal TotalCostHigh { get; set; }
        public decimal TotalCostLow { get; set; }

        public bool Filtering { get; set; }

        public bool IncludeDays { get; set; } = true;
        public bool ExcludeDays { get; set; }
        public bool IncludeGameTypes { get; set; } = true;
        public bool ExcludeGameTypes { get; set; }
        public bool IncludeMonths { get; set; } = true;
        public bool ExcludeMonths { get; set; }
        public bool IncludeVenues { get; set; } = true;
        public bool ExcludeVenues { get; set; }

        #endregion

        public ObservableCollection<ListBoxItem> ExcludeFormats { get; set; }
        public ObservableCollection<ListBoxItem> IncludeFormats { get; set; }
        public ObservableCollection<FilterListBoxItem> FilterNameList { get; set; }
        public ObservableCollection<ListBoxItem> GameTypes { get; set; }
        public ObservableCollection<ListBoxItem> Venues { get; set; }
        public ObservableCollection<ListBoxItem> Days { get; set; }
        public ObservableCollection<ListBoxItem> Months { get; set; }

        public ICommand CancelCommand => new BaseCommand(Exit);
        public ICommand LoadCommand => new BaseCommand(Load);
        public ICommand ResetCommand => new BaseCommand(Reset);
        public ICommand ResetTournamentsCommand => new BaseCommand(ResetTournaments);
        public ICommand SaveCommand => new BaseCommand(Save);

        /// <summary>
        /// create a filter from the selected filter options
        /// </summary>
        private void CreateFilter()
        {
            // filters file
            var fileName = XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.FiltersFileName;

            // load filters
            var doc = XmlHelper.LoadXmlFile(fileName);

            // create one if it doesn't exist
            if (doc is null)
            {
                doc = new XmlDocument();
                XmlNode root = doc.CreateElement("Filters");
                XmlNode child = doc.CreateElement("TournamentFilters");
                root.AppendChild(child);
                child = doc.CreateElement("SessionFilters");
                root.AppendChild(child);
                doc.AppendChild(root);
            }

            // create new filter node
            XmlNode filterNode = doc.CreateElement("Filter");

            // create/append filter node name attribute
            var nameAtt = doc.CreateAttribute("Name");
            nameAtt.Value = FilterName;
            filterNode.Attributes.Append(nameAtt);

            // track whether we want to save the filter or not
            var filterAddonCost = false;
            var filterAddonCount = false;
            var filterAddonStack = false;
            var filterBlindLevels = false;
            var filterBuyinCost = false;
            var filterEntrants = false;
            var filterEntrantsPaid = false;
            var filterFieldBeaten = false;
            var filterFinishPosition = false;
            var filterGuarantee = false;
            var filterLateReg = false;
            var filterNameExcludes = false;
            var filterNameIncludes = false;
            var filterNumberOfResults = false;
            var filterPrizeWon = false;
            var filterRebuyCost = false;
            var filterRebuyCount = false;
            var filterRebuyStack = false;
            var filterStartDate = false;
            var filterStartingStack = false;
            var filterStartTime = false;
            var filterTableSize = false;
            var filterTotalCost = false;

            // check the filter options for changes and flag the filters that changed
            if (AddonCostLow is not 0 || AddonCostHigh is not 0) if (AddonCostLow <= AddonCostHigh) filterAddonCost = true;
            if (AddonCountLow is not 0 || AddonCountHigh is not 0) if (AddonCountLow <= AddonCountHigh) filterAddonCount = true;
            if (AddonStackLow is not 0 || AddonStackHigh is not 0) if (AddonStackLow <= AddonStackHigh) filterAddonStack = true;
            if (BlindLevelsLow is not 0 || BlindLevelsHigh is not 0) if (BlindLevelsLow <= BlindLevelsHigh) filterBlindLevels = true;
            if (BuyinCostLow is not 0 || BuyinCostHigh is not 0) if (BuyinCostLow <= BuyinCostHigh) filterBuyinCost = true;
            if (EntrantsLow is not 0 || EntrantsHigh is not 0) if (EntrantsLow <= EntrantsHigh) filterEntrants = true;
            if (EntrantsPaidLow is not 0 || EntrantsPaidHigh is not 0) if (EntrantsPaidLow <= EntrantsPaidHigh) filterEntrantsPaid = true;
            if (FieldBeatenLow is not 0 || FieldBeatenHigh is not 0) if (FieldBeatenLow <= FieldBeatenHigh) filterFieldBeaten = true;
            if (FinishPositionLow is not 0 || FinishPositionHigh is not 0) if (FinishPositionLow <= FinishPositionHigh) filterFinishPosition = true;
            if (GuaranteeLow is not 0 || GuaranteeHigh is not 0) if (GuaranteeLow <= GuaranteeHigh) filterGuarantee = true;
            if (LateRegLow is not 0 || LateRegHigh is not 0) if (LateRegLow <= LateRegHigh) filterLateReg = true;
            if (NameExcludes.Length is not 0) filterNameExcludes = true;
            if (NameIncludes.Length is not 0) filterNameIncludes = true;
            if (NumberOfResults is not 0) filterNumberOfResults = true;
            if (PrizeWonLow is not 0 || PrizeWonHigh is not 0) if (PrizeWonLow <= PrizeWonHigh) filterPrizeWon = true;
            if (RebuyCostLow is not 0 || RebuyCostHigh is not 0) if (RebuyCostLow <= RebuyCostHigh) filterRebuyCost = true;
            if (RebuyCountLow is not 0 || RebuyCountHigh is not 0) if (RebuyCountLow <= RebuyCountHigh) filterRebuyCount = true;
            if (RebuyStackLow is not 0 || RebuyStackHigh is not 0) if (RebuyStackLow <= RebuyStackHigh) filterRebuyStack = true;
            if (StartDateLow != _startingDate || StartDateHigh != _startingDate) if (StartDateLow <= StartDateHigh) filterStartDate = true;
            if (StartingStackLow is not 0 || StartingStackHigh is not 0) if (StartingStackLow <= StartingStackHigh) filterStartingStack = true;
            if (StartTimeLow != _startingTime || StartTimeHigh != _startingTime) if (StartTimeLow <= StartTimeHigh) filterStartTime = true;
            if (TableSizeLow is not 2 || TableSizeHigh is not 2) if (TableSizeLow <= TableSizeHigh) filterTableSize = true;
            if (TotalCostLow is not 0 || TotalCostHigh is not 0) if (TotalCostLow <= TotalCostHigh) filterTotalCost = true;

            // add addon cost to filter
            if (filterAddonCost)
            {
                XmlNode addonCostLowNode = doc.CreateElement("AddonCostLow");
                addonCostLowNode.InnerText = AddonCostLow.ToString();
                filterNode.AppendChild(addonCostLowNode);

                XmlNode addonCostHighNode = doc.CreateElement("AddonCostHigh");
                addonCostHighNode.InnerText = AddonCostHigh.ToString();
                filterNode.AppendChild(addonCostHighNode);
            }

            // add addon count to filter
            if (filterAddonCount)
            {
                XmlNode addonCountLowNode = doc.CreateElement("AddonCountLow");
                addonCountLowNode.InnerText = AddonCountLow.ToString();
                filterNode.AppendChild(addonCountLowNode);

                XmlNode addonCountHighNode = doc.CreateElement("AddonCountHigh");
                addonCountHighNode.InnerText = AddonCountHigh.ToString();
                filterNode.AppendChild(addonCountHighNode);
            }

            // add addon stack to filter
            if (filterAddonStack)
            {
                XmlNode addonStackLowNode = doc.CreateElement("AddonStackLow");
                addonStackLowNode.InnerText = AddonStackLow.ToString();
                filterNode.AppendChild(addonStackLowNode);

                XmlNode addonStackHighNode = doc.CreateElement("AddonStackHigh");
                addonStackHighNode.InnerText = AddonStackHigh.ToString();
                filterNode.AppendChild(addonStackHighNode);
            }

            // add blind levels to filter
            if (filterBlindLevels)
            {
                XmlNode blindLevelsLowNode = doc.CreateElement("BlindLevelsLow");
                blindLevelsLowNode.InnerText = BlindLevelsLow.ToString();
                filterNode.AppendChild(blindLevelsLowNode);

                XmlNode blindLevelsHighNode = doc.CreateElement("BlindLevelsHigh");
                blindLevelsHighNode.InnerText = BlindLevelsHigh.ToString();
                filterNode.AppendChild(blindLevelsHighNode);
            }

            // add buy-in cost to filter
            if (filterBuyinCost)
            {
                XmlNode buyinCostLowNode = doc.CreateElement("BuyinCostLow");
                buyinCostLowNode.InnerText = BuyinCostLow.ToString();
                filterNode.AppendChild(buyinCostLowNode);

                XmlNode buyinCostHighNode = doc.CreateElement("BuyinCostHigh");
                buyinCostHighNode.InnerText = BuyinCostHigh.ToString();
                filterNode.AppendChild(buyinCostHighNode);
            }

            // add entrants to filter
            if (filterEntrants)
            {
                XmlNode entrantsLowNode = doc.CreateElement("EntrantsLow");
                entrantsLowNode.InnerText = EntrantsLow.ToString();
                filterNode.AppendChild(entrantsLowNode);

                XmlNode entrantsHighNode = doc.CreateElement("EntrantsHigh");
                entrantsHighNode.InnerText = EntrantsHigh.ToString();
                filterNode.AppendChild(entrantsHighNode);
            }

            // add entrants paid to filter
            if (filterEntrantsPaid)
            {
                XmlNode entrantsPaidLowNode = doc.CreateElement("EntrantsPaidLow");
                entrantsPaidLowNode.InnerText = EntrantsPaidLow.ToString();
                filterNode.AppendChild(entrantsPaidLowNode);

                XmlNode entrantsPaidHighNode = doc.CreateElement("EntrantsPaidHigh");
                entrantsPaidHighNode.InnerText = EntrantsPaidHigh.ToString();
                filterNode.AppendChild(entrantsPaidHighNode);
            }

            // add field beaten to filter
            if (filterFieldBeaten)
            {
                XmlNode fieldBeatenLowNode = doc.CreateElement("FieldBeatenLow");
                fieldBeatenLowNode.InnerText = FieldBeatenLow.ToString();
                filterNode.AppendChild(fieldBeatenLowNode);

                XmlNode fieldBeatenHighNode = doc.CreateElement("FieldBeatenHigh");
                fieldBeatenHighNode.InnerText = FieldBeatenHigh.ToString();
                filterNode.AppendChild(fieldBeatenHighNode);
            }

            // add finish position to filter
            if (filterFinishPosition)
            {
                XmlNode finishPositionLowNode = doc.CreateElement("FinishPositionLow");
                finishPositionLowNode.InnerText = FinishPositionLow.ToString();
                filterNode.AppendChild(finishPositionLowNode);

                XmlNode finishPositionHighNode = doc.CreateElement("FinishPositionHigh");
                finishPositionHighNode.InnerText = FinishPositionHigh.ToString();
                filterNode.AppendChild(finishPositionHighNode);
            }

            // add guarantee to filter
            if (filterGuarantee)
            {
                XmlNode guaranteeLowNode = doc.CreateElement("GuaranteeLow");
                guaranteeLowNode.InnerText = GuaranteeLow.ToString();
                filterNode.AppendChild(guaranteeLowNode);

                XmlNode guaranteeHighNode = doc.CreateElement("GuaranteeHigh");
                guaranteeHighNode.InnerText = GuaranteeHigh.ToString();
                filterNode.AppendChild(guaranteeHighNode);
            }

            // add late reg to filter
            if (filterLateReg)
            {
                XmlNode lateRegLowNode = doc.CreateElement("LateRegLow");
                lateRegLowNode.InnerText = LateRegLow.ToString();
                filterNode.AppendChild(lateRegLowNode);

                XmlNode lateRegHighNode = doc.CreateElement("LateRegHigh");
                lateRegHighNode.InnerText = LateRegHigh.ToString();
                filterNode.AppendChild(lateRegHighNode);
            }

            // add name excludes to filter
            if (filterNameExcludes)
            {
                XmlNode nameExcludeNode = doc.CreateElement("NameExclude");
                nameExcludeNode.InnerText = NameExcludes;
                filterNode.AppendChild(nameExcludeNode);
            }

            // add name includes to filter
            if (filterNameIncludes)
            {
                XmlNode nameIncludeNode = doc.CreateElement("NameInclude");
                nameIncludeNode.InnerText = NameIncludes;
                filterNode.AppendChild(nameIncludeNode);
            }

            // add number of results to filter
            if (filterNumberOfResults)
            {
                XmlNode numberOfResultsNode = doc.CreateElement("NumberOfResults");
                numberOfResultsNode.InnerText = NumberOfResults.ToString();
                filterNode.AppendChild(numberOfResultsNode);
            }

            // add prize won to filter
            if (filterPrizeWon)
            {
                XmlNode prizeWonLowNode = doc.CreateElement("PrizeWonLow");
                prizeWonLowNode.InnerText = PrizeWonLow.ToString();
                filterNode.AppendChild(prizeWonLowNode);

                XmlNode prizeWonHighNode = doc.CreateElement("PrizeWonHigh");
                prizeWonHighNode.InnerText = PrizeWonHigh.ToString();
                filterNode.AppendChild(prizeWonHighNode);
            }

            // add rebuy cost to filter
            if (filterRebuyCost)
            {
                XmlNode rebuyCostLowNode = doc.CreateElement("RebuyCostLow");
                rebuyCostLowNode.InnerText = RebuyCostLow.ToString();
                filterNode.AppendChild(rebuyCostLowNode);

                XmlNode rebuyCostHighNode = doc.CreateElement("RebuyCostHigh");
                rebuyCostHighNode.InnerText = RebuyCostHigh.ToString();
                filterNode.AppendChild(rebuyCostHighNode);
            }

            // add rebuy count to filter
            if (filterRebuyCount)
            {
                XmlNode rebuyCountLowNode = doc.CreateElement("RebuyCountLow");
                rebuyCountLowNode.InnerText = RebuyCountLow.ToString();
                filterNode.AppendChild(rebuyCountLowNode);

                XmlNode rebuyCountHighNode = doc.CreateElement("RebuyCountHigh");
                rebuyCountHighNode.InnerText = RebuyCountHigh.ToString();
                filterNode.AppendChild(rebuyCountHighNode);
            }

            // add rebuy stack to filter
            if (filterRebuyStack)
            {
                XmlNode rebuyStackLowNode = doc.CreateElement("RebuyStackLow");
                rebuyStackLowNode.InnerText = RebuyStackLow.ToString();
                filterNode.AppendChild(rebuyStackLowNode);

                XmlNode rebuyStackHighNode = doc.CreateElement("RebuyStackHigh");
                rebuyStackHighNode.InnerText = RebuyStackHigh.ToString();
                filterNode.AppendChild(rebuyStackHighNode);
            }

            // add starting stack to filter
            if (filterStartingStack)
            {
                XmlNode startingStackLowNode = doc.CreateElement("StartingStackLow");
                startingStackLowNode.InnerText = StartingStackLow.ToString();
                filterNode.AppendChild(startingStackLowNode);

                XmlNode startingStackHighNode = doc.CreateElement("StartingStackHigh");
                startingStackHighNode.InnerText = StartingStackHigh.ToString();
                filterNode.AppendChild(startingStackHighNode);
            }

            // add start date to filter
            if (filterStartDate)
            {
                XmlNode startDateLowNode = doc.CreateElement("StartDateLow");
                startDateLowNode.InnerText = StartDateLow.ToString();
                filterNode.AppendChild(startDateLowNode);

                XmlNode startDateHighNode = doc.CreateElement("StartDateHigh");
                startDateHighNode.InnerText = StartDateHigh.ToString();
                filterNode.AppendChild(startDateHighNode);
            }

            // add start time to filter
            if (filterStartTime)
            {
                XmlNode startTimeLowNode = doc.CreateElement("StartTimeLow");
                startTimeLowNode.InnerText = StartTimeLow.ToString();
                filterNode.AppendChild(startTimeLowNode);

                XmlNode startTimeHighNode = doc.CreateElement("StartTimeHigh");
                startTimeHighNode.InnerText = StartTimeHigh.ToString();
                filterNode.AppendChild(startTimeHighNode);
            }

            // add table size to filter
            if (filterTableSize)
            {
                XmlNode tableSizeLowNode = doc.CreateElement("TableSizeLow");
                tableSizeLowNode.InnerText = TableSizeLow.ToString();
                filterNode.AppendChild(tableSizeLowNode);

                XmlNode tableSizeHighNode = doc.CreateElement("TableSizeHigh");
                tableSizeHighNode.InnerText = TableSizeHigh.ToString();
                filterNode.AppendChild(tableSizeHighNode);
            }

            // add total cost to filter
            if (filterTotalCost)
            {
                XmlNode totalCostLowNode = doc.CreateElement("TotalCostLow");
                totalCostLowNode.InnerText = TotalCostLow.ToString();
                filterNode.AppendChild(totalCostLowNode);

                XmlNode totalCostHighNode = doc.CreateElement("TotalCostHigh");
                totalCostHighNode.InnerText = TotalCostHigh.ToString();
                filterNode.AppendChild(totalCostHighNode);
            }

            // selected list box items
            var selectedVenues = Venues.Where(i => i.IsSelected).Any() ? new ObservableCollection<ListBoxItem>(Venues.Where(i => i.IsSelected)) : new ObservableCollection<ListBoxItem>();
            var selectedIncludeFormats = IncludeFormats.Where(i => i.IsSelected).Any() ? new ObservableCollection<ListBoxItem>(IncludeFormats.Where(i => i.IsSelected)) : new ObservableCollection<ListBoxItem>();
            var selectedExcludeFormats = ExcludeFormats.Where(i => i.IsSelected).Any() ? new ObservableCollection<ListBoxItem>(ExcludeFormats.Where(i => i.IsSelected)) : new ObservableCollection<ListBoxItem>();
            var selectedGameTypes = GameTypes.Where(i => i.IsSelected).Any() ? new ObservableCollection<ListBoxItem>(GameTypes.Where(i => i.IsSelected)) : new ObservableCollection<ListBoxItem>();
            var selectedDays = Days.Where(i => i.IsSelected).Any() ? new ObservableCollection<ListBoxItem>(Days.Where(i => i.IsSelected)) : new ObservableCollection<ListBoxItem>();
            var selectedMonths = Months.Where(i => i.IsSelected).Any() ? new ObservableCollection<ListBoxItem>(Months.Where(i => i.IsSelected)) : new ObservableCollection<ListBoxItem>();

            // include formats
            if (selectedIncludeFormats.Count is not 0)
            {
                XmlNode includedFormatsNode = doc.CreateElement("IncludedFormats");

                foreach (var format in selectedIncludeFormats)
                {
                    XmlNode includedFormatNode = doc.CreateElement("IncludeFormat");
                    includedFormatNode.InnerText = format.Name;
                    includedFormatsNode.AppendChild(includedFormatNode);
                }

                filterNode.AppendChild(includedFormatsNode);
            }

            // exclude formats
            if (selectedExcludeFormats.Count is not 0)
            {
                XmlNode excludedFormatsNode = doc.CreateElement("ExcludedFormats");

                foreach (var format in selectedExcludeFormats)
                {
                    XmlNode excludedFormatNode = doc.CreateElement("ExcludeFormat");
                    excludedFormatNode.InnerText = format.Name;
                    excludedFormatsNode.AppendChild(excludedFormatNode);
                }

                filterNode.AppendChild(excludedFormatsNode);
            }

            // venues
            if (selectedVenues.Count is not 0)
            {
                if(IncludeVenues)
                {
                    XmlNode includedVenuesNode = doc.CreateElement("IncludedVenues");

                    foreach(var venue in selectedVenues)
                    {
                        XmlNode includedVenueNode = doc.CreateElement("IncludeVenue");
                        includedVenueNode.InnerText = venue.Name;
                        includedVenuesNode.AppendChild(includedVenueNode);
                    }

                    filterNode.AppendChild(includedVenuesNode);
                }

                if (ExcludeVenues)
                {
                    XmlNode excludedVenuesNode = doc.CreateElement("ExcludedVenues");

                    foreach (var venue in selectedVenues)
                    {
                        XmlNode excludedVenueNode = doc.CreateElement("ExcludeVenue");
                        excludedVenueNode.InnerText = venue.Name;
                        excludedVenuesNode.AppendChild(excludedVenueNode);
                    }

                    filterNode.AppendChild(excludedVenuesNode);

                }
            }

            // game types
            if (selectedGameTypes.Count is not 0)
            {
                if (IncludeGameTypes)
                {
                    XmlNode includedGameTypesNode = doc.CreateElement("IncludedGameTypes");

                    foreach (var gameType in selectedGameTypes)
                    {
                        XmlNode includedGameTypeNode = doc.CreateElement("IncludeGameType");
                        includedGameTypeNode.InnerText = gameType.Name;
                        includedGameTypesNode.AppendChild(includedGameTypeNode);
                    }

                    filterNode.AppendChild(includedGameTypesNode);
                }

                if (ExcludeGameTypes)
                {
                    XmlNode excludedGameTypesNode = doc.CreateElement("ExcludedGameTypes");

                    foreach (var gameType in selectedGameTypes)
                    {
                        XmlNode excludedGameTypeNode = doc.CreateElement("ExcludeGameTypes");
                        excludedGameTypeNode.InnerText = gameType.Name;
                        excludedGameTypesNode.AppendChild(excludedGameTypeNode);
                    }

                    filterNode.AppendChild(excludedGameTypesNode);
                }
            }

            // days
            if (selectedDays.Count is not 0)
            {
                if (IncludeDays)
                {
                    XmlNode includedDaysNode = doc.CreateElement("IncludedDays");

                    foreach (var day in selectedDays)
                    {
                        XmlNode includedDayNode = doc.CreateElement("IncludeDay");
                        includedDayNode.InnerText = day.Name;
                        includedDaysNode.AppendChild(includedDayNode);
                    }

                    filterNode.AppendChild(includedDaysNode);
                }

                if (ExcludeDays)
                {
                    XmlNode excludedDaysNode = doc.CreateElement("ExcludedDays");

                    foreach (var day in selectedDays)
                    {
                        XmlNode excludedDayNode = doc.CreateElement("ExcludeDay");
                        excludedDayNode.InnerText = day.Name;
                        excludedDaysNode.AppendChild(excludedDayNode);
                    }

                    filterNode.AppendChild(excludedDaysNode);
                }
            }

            // months
            if (selectedMonths.Count is not 0)
            {
                if (IncludeMonths)
                {
                    XmlNode includedMonthsNode = doc.CreateElement("IncludedMonths");

                    foreach (var month in selectedMonths)
                    {
                        XmlNode includedMonthNode = doc.CreateElement("IncludeMonths");
                        includedMonthNode.InnerText = month.Name;
                        includedMonthsNode.AppendChild(includedMonthNode);
                    }

                    filterNode.AppendChild(includedMonthsNode);
                }

                if (ExcludeMonths)
                {
                    XmlNode excludedMonthsNode = doc.CreateElement("ExcludedMonths");

                    foreach (var month in selectedMonths)
                    {
                        XmlNode excludedMonthNode = doc.CreateElement("ExcludeMonths");
                        excludedMonthNode.InnerText = month.Name;
                        excludedMonthsNode.AppendChild(excludedMonthNode);
                    }

                    filterNode.AppendChild(excludedMonthsNode);
                }
            }

            // add filter to filters list
            doc.DocumentElement.SelectSingleNode("TournamentFilters").AppendChild(filterNode);

            // save file if requested
            doc.Save(fileName);
        }

        /// <summary>
        /// Create a list box item
        /// </summary>
        /// <param name="item"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private ListBoxItem CreateListBoxItem(string item, int id = -1)
        {
            ListBoxItem listBoxItem = new ListBoxItem()
            {
                Name = item,
                IsSelected = false,
                Id = id,
            };

            return listBoxItem;
        }

        /// <summary>
        /// create list box items
        /// </summary>
        private void CreateListBoxItems()
        {
            // Days
            Days = new ObservableCollection<ListBoxItem>
            {
                CreateListBoxItem("Sunday", 1),
                CreateListBoxItem("Monday", 2),
                CreateListBoxItem("Tuesday", 3),
                CreateListBoxItem("Wednesday", 4),
                CreateListBoxItem("Thursday", 5),
                CreateListBoxItem("Friday", 6),
                CreateListBoxItem("Saturday", 7)
            };

            // Months
            Months = new ObservableCollection<ListBoxItem>
            {
                CreateListBoxItem("January", 1),
                CreateListBoxItem("February", 2),
                CreateListBoxItem("March", 3),
                CreateListBoxItem("April", 4),
                CreateListBoxItem("May", 5),
                CreateListBoxItem("June", 6),
                CreateListBoxItem("July", 7),
                CreateListBoxItem("August", 8),
                CreateListBoxItem("September", 9),
                CreateListBoxItem("October", 10),
                CreateListBoxItem("November", 11),
                CreateListBoxItem("December", 12)
            };

            // venues
            Venues = new ObservableCollection<ListBoxItem>();
            foreach (string venue in _tournaments.Select(i => i.Venue).Distinct()) if (venue.Trim() != "") Venues.Add(CreateListBoxItem(venue));
            Venues = new ObservableCollection<ListBoxItem>(Venues.OrderBy(i => i.Name));

            // game types
            GameTypes = new ObservableCollection<ListBoxItem>();
            foreach (string gameType in _tournaments.Select(i => i.GameType).Distinct()) if(gameType.Trim() != "") GameTypes.Add(CreateListBoxItem(gameType));
            GameTypes = new ObservableCollection<ListBoxItem>(GameTypes.OrderBy(i => i.Name));

            // Formats
            IncludeFormats = new ObservableCollection<ListBoxItem>();
            ExcludeFormats = new ObservableCollection<ListBoxItem>();
            var tempFormats = new ObservableCollection<string>();
            foreach (var tournament in _tournaments) foreach (var format in tournament.Formats) if(tempFormats.Contains(format) is false) if(format.Trim() != "") tempFormats.Add(format);
            foreach (var format in tempFormats) { IncludeFormats.Add(CreateListBoxItem(format)); ExcludeFormats.Add(CreateListBoxItem(format)); }
            IncludeFormats = new ObservableCollection<ListBoxItem>(IncludeFormats.OrderBy(i => i.Name));
            ExcludeFormats = new ObservableCollection<ListBoxItem>(ExcludeFormats.OrderBy(i => i.Name));
        }

        /// <summary>
        /// Exit window
        /// </summary>
        /// <param name="parameter"></param>
        private void Exit(object parameter)
        {
            // grab window
            var window = Application.Current.Windows.OfType<CreateTournamentFilterView>().FirstOrDefault();

            // null check
            if (window is not null) window.Close();
        }

        /// <summary>
        /// Filter the tournament results
        /// </summary>
        private void Filter()
        {
            // grab tournament results window view model
            if (Application.Current.Windows.OfType<TournamentsResultsView>().FirstOrDefault()?.DataContext is not TournamentsResultsViewModel vm) return;

            // grab unfiltered tournaments list
            ObservableCollection<TournamentFinished> list = new ObservableCollection<TournamentFinished>(vm.UnfilteredTournaments);

            // null/zero check
            if (list is null || list.Count is 0) return;

            // flag filtering
            Filtering = true;

            // reset current filter text
            CurrentFilter = "";

            // track whether we want to save the filter or not
            var filterAddonCost = false;
            var filterAddonCount = false;
            var filterAddonStack = false;
            var filterBlindLevels = false;
            var filterBuyinCost = false;
            var filterEntrants = false;
            var filterEntrantsPaid = false;
            var filterFieldBeaten = false;
            var filterFinishPosition = false;
            var filterGuarantee = false;
            var filterLateReg = false;
            var filterNameExcludes = false;
            var filterNameIncludes = false;
            var filterNumberOfResults = false;
            var filterPrizeWon = false;
            var filterRebuyCost = false;
            var filterRebuyCount = false;
            var filterRebuyStack = false;
            var filterStartDate = false;
            var filterStartingStack = false;
            var filterStartTime = false;
            var filterTableSize = false;
            var filterTotalCost = false;

            // check the filter options for changes and flag the filters that changed
            if (AddonCostLow is not 0 || AddonCostHigh is not 0) if (AddonCostLow <= AddonCostHigh) filterAddonCost = true;
            if (AddonCountLow is not 0 || AddonCountHigh is not 0) if (AddonCountLow <= AddonCountHigh) filterAddonCount = true;
            if (AddonStackLow is not 0 || AddonStackHigh is not 0) if (AddonStackLow <= AddonStackHigh) filterAddonStack = true;
            if (BlindLevelsLow is not 0 || BlindLevelsHigh is not 0) if (BlindLevelsLow <= BlindLevelsHigh) filterBlindLevels = true;
            if (BuyinCostLow is not 0 || BuyinCostHigh is not 0) if (BuyinCostLow <= BuyinCostHigh) filterBuyinCost = true;
            if (EntrantsLow is not 0 || EntrantsHigh is not 0) if (EntrantsLow <= EntrantsHigh) filterEntrants = true;
            if (EntrantsPaidLow is not 0 || EntrantsPaidHigh is not 0) if (EntrantsPaidLow <= EntrantsPaidHigh) filterEntrantsPaid = true;
            if (FieldBeatenLow is not 0 || FieldBeatenHigh is not 0) if (FieldBeatenLow <= FieldBeatenHigh) filterFieldBeaten = true;
            if (FinishPositionLow is not 0 || FinishPositionHigh is not 0) if (FinishPositionLow <= FinishPositionHigh) filterFinishPosition = true;
            if (GuaranteeLow is not 0 || GuaranteeHigh is not 0) if (GuaranteeLow <= GuaranteeHigh) filterGuarantee = true;
            if (LateRegLow is not 0 || LateRegHigh is not 0) if (LateRegLow <= LateRegHigh) filterLateReg = true;
            if (NameExcludes.Length is not 0) filterNameExcludes = true;
            if (NameIncludes.Length is not 0) filterNameIncludes = true;
            if (NumberOfResults is not 0) filterNumberOfResults = true;
            if (PrizeWonLow is not 0 || PrizeWonHigh is not 0) if (PrizeWonLow <= PrizeWonHigh) filterPrizeWon = true;
            if (RebuyCostLow is not 0 || RebuyCostHigh is not 0) if (RebuyCostLow <= RebuyCostHigh) filterRebuyCost = true;
            if (RebuyCountLow is not 0 || RebuyCountHigh is not 0) if (RebuyCountLow <= RebuyCountHigh) filterRebuyCount = true;
            if (RebuyStackLow is not 0 || RebuyStackHigh is not 0) if (RebuyStackLow <= RebuyStackHigh) filterRebuyStack = true;
            if (StartDateLow != _startingDate || StartDateHigh != _startingDate) if (StartDateLow <= StartDateHigh) filterStartDate = true;
            if (StartingStackLow is not 0 || StartingStackHigh is not 0) if (StartingStackLow <= StartingStackHigh) filterStartingStack = true;
            if (StartTimeLow != _startingTime || StartTimeHigh != _startingTime) if (StartTimeLow <= StartTimeHigh) filterStartTime = true;
            if (TableSizeLow is not 2 || TableSizeHigh is not 2) if (TableSizeLow <= TableSizeHigh) filterTableSize = true;
            if (TotalCostLow is not 0 || TotalCostHigh is not 0) if (TotalCostLow <= TotalCostHigh) filterTotalCost = true;

            // add addon cost to filter
            if (filterAddonCost) { list = list.Where(i => i.AddonTotalCost >= AddonCostLow && i.AddonTotalCost <= AddonCostHigh).Any() ? new ObservableCollection<TournamentFinished>(list.Where(i => i.AddonTotalCost >= AddonCostLow && i.AddonTotalCost <= AddonCostHigh)) : list; CurrentFilter += "\nAddon Cost: " + AddonCostLow.ToString("C") + " to " + AddonCostHigh.ToString("C"); }

            // add addon count to filter
            if (filterAddonCount) { list = list.Where(i => i.AddonCount >= AddonCountLow && i.AddonCount <= AddonCountHigh).Any() ? new ObservableCollection<TournamentFinished>(list.Where(i => i.AddonCount >= AddonCountLow && i.AddonCount <= AddonCountHigh)) : list; CurrentFilter += "\nAddon Count: " + AddonCountLow + " to " + AddonCountHigh; }

            // add addon stack to filter
            if (filterAddonStack) { list = list.Where(i => i.StackSizeAddon >= AddonStackLow && i.StackSizeAddon <= AddonStackHigh).Any() ? new ObservableCollection<TournamentFinished>(list.Where(i => i.StackSizeAddon >= AddonStackLow && i.StackSizeAddon <= AddonStackHigh)) : list; CurrentFilter += "\nAddon Stack: " + AddonStackLow + " to " + AddonStackHigh; }

            // add blind levels to filter
            if (filterBlindLevels) { list = list.Where(i => i.BlindLevels >= BlindLevelsHigh && i.BlindLevels <= BlindLevelsHigh).Any() ? new ObservableCollection<TournamentFinished>(list.Where(i => i.BlindLevels >= BlindLevelsLow && i.BlindLevels <= BlindLevelsHigh)) : list; CurrentFilter += "\nBlind Levels: " + BlindLevelsLow + " to " + BlindLevelsHigh; }

            // add buy-in cost to filter
            if (filterBuyinCost) { list = list.Where(i => i.BuyinTotalCost >= BuyinCostLow && i.BuyinTotalCost <= BuyinCostHigh).Any() ? new ObservableCollection<TournamentFinished>(list.Where(i => i.BuyinTotalCost >= BuyinCostLow && i.BuyinTotalCost <= BuyinCostHigh)) : list; CurrentFilter += "\nBuyin Cost: " + BuyinCostLow.ToString("C") + " to " + BuyinCostHigh.ToString("C"); }

            // add entrants to filter
            if (filterEntrants) { list = list.Where(i => i.Entrants >= EntrantsLow && i.Entrants <= EntrantsHigh).Any() ? new ObservableCollection<TournamentFinished>(list.Where(i => i.Entrants >= EntrantsLow && i.Entrants <= EntrantsHigh)) : list; CurrentFilter += "\nEntrants: " + EntrantsLow + " to " + EntrantsHigh; }

            // add entrants paid to filter
            if (filterEntrantsPaid) { list = list.Where(i => i.EntrantsPaid >= EntrantsPaidLow && i.EntrantsPaid <= EntrantsPaidHigh).Any() ? new ObservableCollection<TournamentFinished>(list.Where(i => i.EntrantsPaid >= EntrantsPaidLow && i.EntrantsPaid <= EntrantsPaidHigh)) : list; CurrentFilter += "\nEntrants Paid: " + EntrantsPaidLow + " to " + EntrantsPaidHigh; }

            // add field beaten to filter
            if (filterFieldBeaten) { list = list.Where(i => i.PercentFieldBeaten >= FieldBeatenLow && i.PercentFieldBeaten <= FieldBeatenHigh).Any() ? new ObservableCollection<TournamentFinished>(list.Where(i => i.PercentFieldBeaten >= FieldBeatenLow && i.PercentFieldBeaten <= FieldBeatenHigh)) : list; CurrentFilter += "\nField Beaten: " + FieldBeatenLow + " to " + FieldBeatenHigh; }

            // add finish position to filter
            if (filterFinishPosition) {list = list.Where(i => i.FinishPosition >= FinishPositionLow && i.FinishPosition <= FinishPositionHigh).Any() ? new ObservableCollection<TournamentFinished>(list.Where(i => i.FinishPosition >= FinishPositionLow && i.FinishPosition <= FinishPositionHigh)) : list; CurrentFilter += "\nFinish Position: " + FinishPositionLow + " to " + FinishPositionHigh; }

            // add guarantee to filter
            if (filterGuarantee) { list = list.Where(i => i.Guarantee >= GuaranteeLow && i.Guarantee <= GuaranteeHigh).Any() ? new ObservableCollection<TournamentFinished>(list.Where(i => i.Guarantee >= GuaranteeLow && i.Guarantee <= GuaranteeHigh)) : list; CurrentFilter += "\nGuarantee: " + GuaranteeLow.ToString("C") + " to " + GuaranteeHigh.ToString("C"); }

            // add late reg to filter
            if (filterLateReg) { list = list.Where(i => i.LateReg >= LateRegLow && i.LateReg <= LateRegHigh).Any() ? new ObservableCollection<TournamentFinished>(list.Where(i => i.LateReg >= LateRegLow && i.LateReg <= LateRegHigh)) : list; CurrentFilter += "\nLate Reg: " + LateRegLow + " to " + LateRegHigh; }

            // add name excludes to filter
            if (filterNameExcludes) { foreach (string excludeString in NameExcludes.Split()) foreach (var tournament in list.ToList()) if (tournament.TournamentName.Trim().ToUpper(new CultureInfo("en-US")).Contains(excludeString.Trim().ToUpper(new CultureInfo("en-US")))) list.Remove(tournament); CurrentFilter += "\nName Excludes: " + NameExcludes; }

            // add name includes to filter
            if (filterNameIncludes) { foreach (string includeString in NameIncludes.Split()) foreach (var tournament in list.ToList()) if (!tournament.TournamentName.Trim().ToUpper(new CultureInfo("en-US")).Contains(includeString.Trim().ToUpper(new CultureInfo("en-US")))) list.Remove(tournament); CurrentFilter += "\nName Includes: " + NameIncludes; }

            // add prize won to filter
            if (filterPrizeWon) { list = list.Where(i => i.PrizeWon >= PrizeWonLow && i.PrizeWon <= PrizeWonHigh).Any() ? new ObservableCollection<TournamentFinished>(list.Where(i => i.PrizeWon >= PrizeWonLow && i.PrizeWon <= PrizeWonHigh)) : list; CurrentFilter += "\nPrize Won: " + PrizeWonLow.ToString("C") + " to " + PrizeWonHigh.ToString("C"); }

            // add rebuy cost to filter
            if (filterRebuyCost) { list = list.Where(i => i.RebuyTotalCost >= RebuyCostLow && i.RebuyTotalCost <= RebuyCostHigh).Any() ? new ObservableCollection<TournamentFinished>(list.Where(i => i.RebuyTotalCost >= RebuyCostLow && i.RebuyTotalCost <= RebuyCostHigh)) : list; CurrentFilter += "\nRebuy Cost: " + RebuyCostLow.ToString("C") + " to " + RebuyCostHigh.ToString("C"); }

            // add rebuy count to filter
            if (filterRebuyCount) { list = list.Where(i => i.RebuyCount >= RebuyCountLow && i.RebuyCount <= RebuyCountHigh).Any() ? new ObservableCollection<TournamentFinished>(list.Where(i => i.RebuyCount >= RebuyCountLow && i.RebuyCount <= RebuyCountHigh)) : list; CurrentFilter += "\nRebuy Count: " + RebuyCountLow + " to " + RebuyCountHigh; }

            // add rebuy stack to filter
            if (filterRebuyStack) { list = list.Where(i => i.StackSizeRebuy >= RebuyStackLow && i.StackSizeRebuy <= RebuyStackHigh).Any() ? new ObservableCollection<TournamentFinished>(list.Where(i => i.StackSizeRebuy >= RebuyStackLow && i.StackSizeRebuy <= RebuyStackHigh)) : list; CurrentFilter += "\nRebuy Stack: " + RebuyStackLow + " to " + RebuyStackHigh; }

            // add starting stack to filter
            if (filterStartingStack) { list = list.Where(i => i.StackSizeStarting >= StartingStackLow && i.StackSizeStarting <= StartingStackHigh).Any() ? new ObservableCollection<TournamentFinished>(list.Where(i => i.StackSizeStarting >= StartingStackLow && i.StackSizeStarting <= StartingStackHigh)) : list; CurrentFilter += "\nStarting Stack: " + StartingStackLow + " to " + StartingStackHigh; }

            // add start date to filter
            if (filterStartDate) { list = list.Where(i => i.StartTime >= StartDateLow && i.StartTime <= StartDateHigh).Any() ? new ObservableCollection<TournamentFinished>(list.Where(i => i.StartTime >= StartDateLow && i.StartTime <= StartDateHigh)) : list; CurrentFilter += "\nStart Date: " + StartDateLow.ToString("MM/dd/yyyy", new CultureInfo("en-US")) + " to " + StartDateHigh.ToString("MM/dd/yyyy", new CultureInfo("en-US")); }

            // add start time to filter
            if (filterStartTime)
            {
                foreach (var tournament in list.ToList()) if (tournament.StartTime.Hour < StartTimeLow.Hour) list.Remove(tournament);
                foreach (var tournament in list.ToList()) if (tournament.StartTime.Hour == StartTimeLow.Hour && tournament.StartTime.Minute < StartTimeLow.Minute) list.Remove(tournament);
                foreach (var tournament in list.ToList()) if (tournament.StartTime.Hour > StartTimeHigh.Hour) list.Remove(tournament);
                foreach (var tournament in list.ToList()) if (tournament.StartTime.Hour == StartTimeHigh.Hour && tournament.StartTime.Minute > StartTimeHigh.Minute) list.Remove(tournament);

                CurrentFilter += "\nStart Time: " + String.Format(new CultureInfo("en-US"), "{0:h:mm tt}", StartTimeLow) + " to " + String.Format(new CultureInfo("en-US"), "{0:h:mm tt}", StartTimeHigh);
            }

            // add table size to filter
            if (filterTableSize) { list = list.Where(i => i.TableSize >= TableSizeLow && i.TableSize <= TableSizeHigh).Any() ? new ObservableCollection<TournamentFinished>(list.Where(i => i.TableSize >= TableSizeLow && i.TableSize <= TableSizeHigh)) : list; CurrentFilter += "\nTable Size: " + TableSizeLow + " to " + TableSizeHigh; }

            // add total cost to filter
            if (filterTotalCost) { list = list.Where(i => i.TotalCost >= TotalCostLow && i.TotalCost <= TotalCostHigh).Any() ? new ObservableCollection<TournamentFinished>(list.Where(i => i.TotalCost >= TotalCostLow && i.TotalCost <= TotalCostHigh)) : list; CurrentFilter += "\nTotal Cost: " + TotalCostLow.ToString("C") + " to " + TotalCostHigh.ToString("C"); }

            // filter formats
            var selectedIncludeFormats = IncludeFormats.Where(i => i.IsSelected).Any() ? new ObservableCollection<ListBoxItem>(IncludeFormats.Where(i => i.IsSelected)) : new ObservableCollection<ListBoxItem>();
            var selectedExcludeFormats = ExcludeFormats.Where(i => i.IsSelected).Any() ? new ObservableCollection<ListBoxItem>(ExcludeFormats.Where(i => i.IsSelected)) : new ObservableCollection<ListBoxItem>();

            // filter for include formats
            if (selectedIncludeFormats.Count is not 0)
            {
                foreach (var tournament in list.ToList())
                {
                    int matchingFormatsCounter = 0;
                    foreach (string formatString in tournament.Formats) foreach (var formatItem in selectedIncludeFormats) if (formatString == formatItem.Name) matchingFormatsCounter++;

                    if (matchingFormatsCounter != selectedIncludeFormats.Count) list.Remove(tournament);
                }

                // format filter description
                var filterString = "\nInclude Format(s): ";

                // add formats to filter description
                foreach (var item in selectedIncludeFormats) filterString += item.Name + ",";

                // remove extra ',' from the end
                filterString = filterString.TrimEnd(',');

                // add format filter description to current filter string
                CurrentFilter += filterString;
            }

            // filter for exclude formats
            if (selectedExcludeFormats.Count is not 0)
            {
                foreach (var tournament in list.ToList())
                {
                    bool match = false;
                    foreach (string formatString in tournament.Formats) foreach (var excludeFormatItem in selectedExcludeFormats) if (formatString == excludeFormatItem.Name) match = true;

                    if (match) list.Remove(tournament);
                }

                // format filter description
                var filterString = "\nExclude Format(s): ";

                // add formats to filter description
                foreach (var item in selectedExcludeFormats) filterString += item.Name + ",";

                // remove extra ',' from the end
                filterString = filterString.TrimEnd(',');

                // add format filter description to current filter string
                CurrentFilter += filterString;
            }

            // selected game type filters
            var selectedGameTypes = GameTypes.Where(i => i.IsSelected).Any() ? new ObservableCollection<ListBoxItem>(GameTypes.Where(i => i.IsSelected)) : new ObservableCollection<ListBoxItem>();

            // filter game types
            if (selectedGameTypes.Count is not 0)
            {
                foreach (var tournament in list.ToList())
                {
                    bool match = false;
                    foreach (var gameTypeItem in selectedGameTypes) if (gameTypeItem.Name == tournament.GameType) match = true;

                    if (IncludeGameTypes) if (!match) list.Remove(tournament);
                    if (ExcludeGameTypes) if (match) list.Remove(tournament);
                }

                var filterString = "";

                if (IncludeGameTypes) filterString = "\nInclude Game Type(s): ";
                if (ExcludeGameTypes) filterString = "\nExclude Game Type(s): ";

                foreach (var item in selectedGameTypes) filterString += item.Name + ",";

                filterString.TrimEnd(',');

                CurrentFilter += filterString;
            }

            // selected venue filters
            var selectedVenues = Venues.Where(i => i.IsSelected).Any() ? new ObservableCollection<ListBoxItem>(Venues.Where(i => i.IsSelected)) : new ObservableCollection<ListBoxItem>();

            // filter out venues
            if (selectedVenues.Count is not 0)
            {
                foreach (var tournament in list.ToList())
                {
                    bool match = false;
                    foreach (var venueItem in selectedVenues) if (venueItem.Name == tournament.Venue) match = true;

                    if (IncludeVenues) if (!match) list.Remove(tournament);
                    if (ExcludeVenues) if (match) list.Remove(tournament);
                }

                var filterString = "";

                if (IncludeVenues) filterString = "\nInclude Venue(s): ";
                if (ExcludeVenues) filterString = "\nExclude Venue(s): ";

                foreach (var item in selectedVenues) filterString += item.Name + ',';

                CurrentFilter += filterString.TrimEnd(',');
            }

            // selected day filters
            var selectedDays = Days.Where(i => i.IsSelected).Any() ? new ObservableCollection<ListBoxItem>(Days.Where(i => i.IsSelected)) : new ObservableCollection<ListBoxItem>();

            // filter days
            if (selectedDays.Count is not 0)
            {
                foreach (var tournament in list.ToList())
                {
                    bool match = false;
                    foreach (var dayItem in selectedDays) if (tournament.StartTime.DayOfWeek.ToString() == dayItem.Name) match = true;

                    if (IncludeDays) if (!match) list.Remove(tournament);
                    if (ExcludeDays) if (match) list.Remove(tournament);
                }

                var filterString = "";

                if (IncludeDays) filterString = "\nInclude Day(s): ";
                if (ExcludeDays) filterString = "\nExclude Day(s): ";

                foreach (var item in selectedDays) filterString += item.Name + ",";

                filterString.TrimEnd(',');

                CurrentFilter += filterString;
            }

            // filter months
            var selectedMonths = Months.Where(i => i.IsSelected).Any() ? new ObservableCollection<ListBoxItem>(Months.Where(i => i.IsSelected)) : new ObservableCollection<ListBoxItem>();

            // filter out months
            if (selectedMonths.Count is not 0)
            {
                foreach (var tournament in list.ToList())
                {
                    bool match = false;
                    foreach (var monthItem in selectedMonths) if (tournament.StartTime.Month == monthItem.Id) match = true;

                    if (IncludeMonths) if (!match) list.Remove(tournament);
                    if (ExcludeMonths) if (match) list.Remove(tournament);
                }

                var filterString = "";

                if (IncludeMonths) filterString = "\nInclude Month(s): ";
                if (ExcludeMonths) filterString = "\nExclude Month(s): ";

                foreach (var item in selectedMonths) filterString += item.Name + ",";

                filterString.TrimEnd(',');

                CurrentFilter += filterString;
            }

            // limit results to the number of results
            if (filterNumberOfResults)
            {
                list = new ObservableCollection<TournamentFinished>(list.TakeLast(NumberOfResults));
                CurrentFilter += "\nResults Limit: " + NumberOfResults;
            }

            // update tournaments
            vm.Tournaments = new ObservableCollection<TournamentFinished>(list);

            // update current filter to new filter
            if(CurrentFilter.Length > 0) vm.CurrentFilter = CurrentFilter;

            // updater filter name to custom filter
            vm.FilterName = "Custom Filter";

            // filter tournaments
            vm.Filter();

            // create/show success message
            var okayVM1 = new OkViewModel("Results Filtered!\n\n" + CurrentFilter, "Tournament Filter");
            var okayWindow1 = new OkView(okayVM1) { Owner = Application.Current.Windows.OfType<TournamentsResultsView>().FirstOrDefault(), WindowStartupLocation = WindowStartupLocation.CenterOwner, }; okayWindow1.ShowDialog();

            // remove filtering flag
            Filtering = false;

            // give focus to tournament results window
            Application.Current.Windows.OfType<TournamentsResultsView>()?.FirstOrDefault()?.Activate();
        }

        /// <summary>
        /// Save and load filters
        /// </summary>
        /// <param name="parameter"></param>
        private void Load(object parameter)
        {
            // filter results, if not already filtering
            if (!Filtering) { Filter(); }
        }

        /// <summary>
        /// load saved filters from file
        /// </summary>
        private void LoadFilterNames()
        {
            // new filters collection
            FilterNameList = new ObservableCollection<FilterListBoxItem>();

            // filter file name
            var filename = XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.FiltersFileName;

            // load filters file
            var doc = XmlHelper.LoadXmlFile(filename);

            // no file
            if (doc is null) return;

            // get all the saved tournament filters
            var tournamentFilterNodes = doc.DocumentElement.SelectSingleNode("TournamentFilters").SelectNodes("Filter");

            // loop through and create list box item for each filter
            foreach (XmlNode filterNode in tournamentFilterNodes)
            {
                // new filter list box item
                var item = new FilterListBoxItem();

                // filter name
                item.Name = filterNode.Attributes["Name"].Value;

                // add filter item to filter list
                FilterNameList.Add(item);
            }
        }

        /// <summary>
        /// Reset filters to starting values
        /// </summary>
        /// <param name="parameter"></param>
        private void Reset(object parameter)
        {
            AddonCostLow = 0.0m;
            AddonCostHigh = 0.0m;
            AddonCountLow = 0;
            AddonCountHigh = 0;
            AddonStackLow = 0;
            AddonStackHigh = 0;
            BlindLevelsLow = 0;
            BlindLevelsHigh = 0;
            BuyinCostLow = 0.0m;
            BuyinCostHigh = 0.0m;
            EntrantsLow = 0;
            EntrantsHigh = 0;
            FieldBeatenLow = 0.0m;
            FieldBeatenHigh = 0.0m;
            FilterName = "";
            FinishPositionLow = 0;
            FinishPositionHigh = 0;
            GuaranteeLow = 0.0m;
            GuaranteeHigh = 0.0m;
            LateRegLow = 0;
            LateRegHigh = 0;
            NameIncludes = "";
            NameExcludes = "";
            NumberOfResults = 0;
            PrizeWonLow = 0.0m;
            PrizeWonHigh = 0.0m;
            RebuyCostLow = 0.0m;
            RebuyCostHigh = 0.0m;
            RebuyCountLow = 0;
            RebuyCountHigh = 0;
            RebuyStackLow = 0;
            RebuyStackHigh = 0;
            StartingStackLow = 0;
            StartingStackHigh = 0;
            TableSizeLow = 2;
            TableSizeHigh = 2;
            TotalCostLow = 0.0m;
            TotalCostHigh = 0.0m;

            _startingDate = DateTime.Now;
            _startingTime = new DateTime(_startingDate.Year, _startingDate.Month, _startingDate.Day, 12, 0, 0);

            StartDateLow = _startingDate;
            StartDateHigh = _startingDate;

            StartTimeLow = _startingTime;
            StartTimeHigh = _startingTime;


            foreach (var item in Days) item.IsSelected = false;
            foreach (var item in Months) item.IsSelected = false;
            foreach (var item in Venues) item.IsSelected = false;
            foreach (var item in GameTypes) item.IsSelected = false;
            foreach (var item in ExcludeFormats) item.IsSelected = false;
            foreach (var item in IncludeFormats) item.IsSelected = false;

            IncludeDays = true;
            IncludeGameTypes = true;
            IncludeMonths = true;
            IncludeVenues = true;

            CurrentFilter = "All Results";
        }

        /// <summary>
        /// Reset tournaments to starting values
        /// </summary>
        /// <param name="parameter"></param>
        private void ResetTournaments(object parameter)
        {
            // clear tournament filters and reset tournaments results window to default results
            TournamentResultsCommands.MenuItem("ClearFilter");

            // create/show success message
            var okayVM1 = new OkViewModel("The tournament results were reset and the filter was cleared\n", "Filter Reset");
            var okayWindow1 = new OkView(okayVM1) { Owner = Application.Current.Windows.OfType<TournamentsResultsView>().FirstOrDefault(), WindowStartupLocation = WindowStartupLocation.CenterOwner, }; okayWindow1.ShowDialog();
        }

        /// <summary>
        /// Save filter without loading
        /// </summary>
        /// <param name="parameter"></param>
        private void Save(object parameter)
        {
            // check for name
            if (FilterName is null || FilterName.Length is 0)
            {
                // ok view model
                var vm = new OkViewModel("Filter name can't be blank", "Name Blank");

                // create/show ok window
                var window = new OkView(vm)
                {
                    Owner = Application.Current.Windows.OfType<CreateTournamentFilterView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                window.ShowDialog();

                // exit
                return;
            }

            // create filter list box items
            LoadFilterNames();

            // check for duplicate filter name
            foreach(var filter in FilterNameList) if(filter.Name == FilterName.Trim())
                {
                    {
                        // ok view model
                        var vm = new OkViewModel("Filter with the same name already exists", "Name Already Exists");

                        // create/show ok window
                        var window = new OkView(vm)
                        {
                            Owner = Application.Current.Windows.OfType<CreateTournamentFilterView>().FirstOrDefault(),
                            WindowStartupLocation = WindowStartupLocation.CenterOwner,
                        };
                        window.ShowDialog();

                        // exit
                        return;
                    }
                }

            // create the filter
            CreateFilter();

            // ok view model
            var theVm = new OkViewModel("Filter saved successfully!" + "\n\nFilter Name: " + FilterName + "\n" + CurrentFilter, "Filter Saved");

            // create/show ok window
            var theWindow = new OkView(theVm)
            {
                Owner = Application.Current.Windows.OfType<CreateTournamentFilterView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            theWindow.ShowDialog();

            // reset filter name to blank
            FilterName = "";
        }
    }
}
