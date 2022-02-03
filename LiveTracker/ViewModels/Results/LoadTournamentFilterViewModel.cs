using Syncfusion.UI.Xaml.Utility;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using Tournament_Life.Helpers;
using Tournament_Life.Views.Results;
using Tournament_Life.Models.Filters;
using Tournament_Life.Models.Tournaments;
using Tournament_Life.Views;

namespace Tournament_Life.ViewModels.Results
{
    public class LoadTournamentFilterViewModel : NotificationObject
    {
        public ICommand CancelCommand => new BaseCommand(CloseWindow);
        public ICommand LoadCommand => new BaseCommand(LoadFilter);
        public ICommand RenameCommand => new BaseCommand(RenameFilter);

        public LoadTournamentFilterViewModel()
        {
            // font size
            FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));

            // visual theme
            Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");

            // set title
            Title = "Load Filter";

            // load filters from file
            CreateFilters();
        }

        public string CurrentFilter { get; set; }
        public int FontSize { get; set; }
        public string Theme { get; set; }
        public string Title { get; set; }
        public ObservableCollection<FilterListBoxItem> FilterList { get; set; }

        /// <summary>
        /// close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void CloseWindow(object parameter)
        {
            // find the window
            if (Application.Current.Windows.OfType<LoadTournamentFilterView>().FirstOrDefault() is not LoadTournamentFilterView window) return;

            // close window
            window?.Close();
        }

        /// <summary>
        /// load saved filters from file
        /// </summary>
        private void CreateFilters()
        {
            // new filters collection
            FilterList = new ObservableCollection<FilterListBoxItem>();

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

                // filter items
                item.AddonCostLow = decimal.Parse(filterNode.SelectSingleNode("AddonCostLow")?.InnerText ?? "0");
                item.AddonCostHigh = decimal.Parse(filterNode.SelectSingleNode("AddonCostHigh")?.InnerText ?? "0");
                item.AddonCountLow = int.Parse(filterNode.SelectSingleNode("AddonCountLow")?.InnerText ?? "0");
                item.AddonCountHigh = int.Parse(filterNode.SelectSingleNode("AddonCountHigh")?.InnerText ?? "0");
                item.AddonStackLow = int.Parse(filterNode.SelectSingleNode("AddonStackLow")?.InnerText ?? "0");
                item.AddonStackHigh = int.Parse(filterNode.SelectSingleNode("AddonStackHigh")?.InnerText ?? "0");
                item.BlindLevelsLow = int.Parse(filterNode.SelectSingleNode("BlindLevelsLow")?.InnerText ?? "0");
                item.BlindLevelsHigh = int.Parse(filterNode.SelectSingleNode("BlindLevelsHigh")?.InnerText ?? "0");
                item.BuyinCostLow = decimal.Parse(filterNode.SelectSingleNode("BuyinCostLow")?.InnerText ?? "0" );
                item.BuyinCostHigh = decimal.Parse(filterNode.SelectSingleNode("BuyinCostHigh")?.InnerText ?? "0");
                item.EntrantsLow = int.Parse(filterNode.SelectSingleNode("EntrantsLow")?.InnerText ?? "0");
                item.EntrantsHigh = int.Parse(filterNode.SelectSingleNode("EntrantsHigh")?.InnerText ?? "0");
                item.EntrantsPaidLow = int.Parse(filterNode.SelectSingleNode("EntrantsPaidLow")?.InnerText ?? "0");
                item.EntrantsPaidHigh = int.Parse(filterNode.SelectSingleNode("EntrantsPaidHigh")?.InnerText ?? "0");

                // filter exclude days items
                var excludeDayNode = filterNode.SelectSingleNode("ExcludedDays");
                var excludeDay = excludeDayNode?.SelectNodes("ExcludeDay");
                if (excludeDay is not null) foreach (XmlNode node in excludeDay) item.ExcludeDays.Add(node.InnerText);

                // filter exclude format items
                var excludeFormatsNode = filterNode.SelectSingleNode("ExcludedFormats");
                var excludeFormats = excludeFormatsNode?.SelectNodes("ExcludeFormat");
                if (excludeFormats is not null) foreach (XmlNode node in excludeFormats) item.ExcludeFormats.Add(node.InnerText);

                // filter exclude game type items
                var excludeGameTypesNode = filterNode.SelectSingleNode("ExcludedGameTypes");
                var excludeGameTypes = excludeGameTypesNode?.SelectNodes("ExcludeGameTypes");
                if (excludeGameTypes is not null) foreach (XmlNode node in excludeGameTypes) item.ExcludeGameTypes.Add(node.InnerText);

                // filter exclude months items
                var excludeMonthNode = filterNode.SelectSingleNode("ExcludedMonths");
                var excludeMonth = excludeMonthNode?.SelectNodes("ExcludeMonths");
                if (excludeMonth is not null) foreach (XmlNode node in excludeMonth) item.ExcludeMonths.Add(node.InnerText);

                // filter exclude venue items
                var excludeVenueNode = filterNode.SelectSingleNode("ExcludedVenues");
                var excludeVenue = excludeVenueNode?.SelectNodes("ExcludeVenue");
                if (excludeVenue is not null) foreach (XmlNode node in excludeVenue) item.ExcludeVenues.Add(node.InnerText);

                // filter items
                item.FieldBeatenLow = decimal.Parse(filterNode.SelectSingleNode("FieldBeatenLow")?.InnerText ?? "0");
                item.FieldBeatenHigh = decimal.Parse(filterNode.SelectSingleNode("FieldBeatenHigh")?.InnerText ?? "0");
                item.FinishPositionLow = int.Parse(filterNode.SelectSingleNode("FinishPositionLow")?.InnerText ?? "0");
                item.FinishPositionHigh = int.Parse(filterNode.SelectSingleNode("FinishPositionHigh")?.InnerText ?? "0");
                item.GuaranteeLow = decimal.Parse(filterNode.SelectSingleNode("GuaranteeLow")?.InnerText ?? "0");
                item.GuaranteeHigh = decimal.Parse(filterNode.SelectSingleNode("GuaranteeHigh")?.InnerText ?? "0");

                // filter include days items
                var includeDayNode = filterNode.SelectSingleNode("IncludedDays");
                var includeDay = includeDayNode?.SelectNodes("IncludeDay");
                if (includeDay is not null) foreach (XmlNode node in includeDay) item.IncludeDays.Add(node.InnerText);

                // filter include format items
                var includeFormatsNode = filterNode.SelectSingleNode("IncludedFormats");
                var includeFormats = includeFormatsNode?.SelectNodes("IncludeFormat");
                if (includeFormats is not null) foreach (XmlNode node in includeFormats) item.IncludeFormats.Add(node.InnerText);

                // filter include game type items
                var includeGameTypesNode = filterNode.SelectSingleNode("IncludedGameTypes");
                var includeGameTypes = includeGameTypesNode?.SelectNodes("IncludeGameType");
                if (includeGameTypes is not null) foreach (XmlNode node in includeGameTypes) item.IncludeGameTypes.Add(node.InnerText);

                // filter include months items
                var includeMonthNode = filterNode.SelectSingleNode("IncludedMonths");
                var includeMonth = includeMonthNode?.SelectNodes("IncludeMonths");
                if (includeMonth is not null) foreach (XmlNode node in includeMonth) item.IncludeMonths.Add(node.InnerText);

                // filter include venue items
                var includeVenueNode = filterNode.SelectSingleNode("IncludedVenues");
                var includeVenue = includeVenueNode?.SelectNodes("IncludeVenue");
                if (includeVenue is not null) foreach (XmlNode node in includeVenue) item.IncludeVenues.Add(node.InnerText);

                // filter items
                item.LateRegLow = int.Parse(filterNode.SelectSingleNode("LateRegLow")?.InnerText ?? "0");
                item.LateRegHigh = int.Parse(filterNode.SelectSingleNode("LateRegHigh")?.InnerText ?? "0");
                item.NumberOfResults = int.Parse(filterNode.SelectSingleNode("NumberOfResults")?.InnerText ?? "0");
                item.PrizeWonLow = decimal.Parse(filterNode.SelectSingleNode("PrizeWonLow")?.InnerText ?? "0");
                item.PrizeWonHigh = decimal.Parse(filterNode.SelectSingleNode("PrizeWonHigh")?.InnerText ?? "0");
                item.RebuyCostLow = decimal.Parse(filterNode.SelectSingleNode("RebuyCostLow")?.InnerText ?? "0");
                item.RebuyCostHigh = decimal.Parse(filterNode.SelectSingleNode("RebuyCostHigh")?.InnerText ?? "0");
                item.RebuyCountLow = int.Parse(filterNode.SelectSingleNode("RebuyCountLow")?.InnerText ?? "0");
                item.RebuyCountHigh = int.Parse(filterNode.SelectSingleNode("RebuyCountHigh")?.InnerText ?? "0");
                item.RebuyStackLow = int.Parse(filterNode.SelectSingleNode("RebuyStackLow")?.InnerText ?? "0");
                item.RebuyStackHigh = int.Parse(filterNode.SelectSingleNode("RebuyStackHigh")?.InnerText ?? "0");
                item.StartingStackLow = int.Parse(filterNode.SelectSingleNode("StartingStackLow")?.InnerText ?? "0");
                item.StartingStackHigh = int.Parse(filterNode.SelectSingleNode("StartingStackHigh")?.InnerText ?? "0");
                item.TableSizeLow = int.Parse(filterNode.SelectSingleNode("TableSizeLow")?.InnerText ?? "2");
                item.TableSizeHigh = int.Parse(filterNode.SelectSingleNode("TableSizeHigh")?.InnerText ?? "2");
                item.TotalCostLow = decimal.Parse(filterNode.SelectSingleNode("TotalCostLow")?.InnerText ?? "0");
                item.TotalCostHigh = decimal.Parse(filterNode.SelectSingleNode("TotalCostHigh")?.InnerText ?? "0");

                // build description
                item.Description += item.Name + "\n";
                if (item.AddonCostLow is not 0 || item.AddonCostHigh is not 0) item.Description += "\nAddon Cost: " + item.AddonCostLow.ToString("C") + " to " + item.AddonCostHigh.ToString("C");
                if (item.AddonCountLow is not 0 || item.AddonCountHigh is not 0) item.Description += "\nAddon Count: " + item.AddonCountLow + " to " + item.AddonCountHigh;
                if (item.AddonStackLow is not 0 || item.AddonStackHigh is not 0) item.Description += "\nAddon Stack: " + item.AddonStackLow + " to " + item.AddonStackHigh;
                if (item.BlindLevelsLow is not 0 || item.BlindLevelsHigh is not 0) item.Description += "\nBlind Levels: " + item.BlindLevelsLow + " to " + item.BlindLevelsHigh;
                if (item.BuyinCostLow is not 0 || item.BuyinCostHigh is not 0) item.Description += "\nBuyin Cost: " + item.BuyinCostLow.ToString("C") + " to " + item.BuyinCostHigh.ToString("C");
                if (item.EntrantsLow is not 0 || item.EntrantsHigh is not 0) item.Description += "\nEntrants: " + item.EntrantsLow + " to " + item.EntrantsHigh;
                if (item.EntrantsPaidLow is not 0 || item.EntrantsPaidHigh is not 0) item.Description += "\nEntrants Paid: " + item.EntrantsPaidLow + " to " + item.EntrantsPaidHigh;

                // build excluded days description
                var excludeDaysDescription = "\nExcluded Days: ";
                if (item.ExcludeDays.Count > 0) foreach (var excludeDays in item.ExcludeDays) excludeDaysDescription += excludeDays + ",";
                if (item.ExcludeDays.Count > 0) { excludeDaysDescription = excludeDaysDescription.TrimEnd(','); item.Description += excludeDaysDescription; }

                // build excluded formats description
                var excludeFormatsDescription = "\nExcluded Formats: ";
                if (item.ExcludeFormats.Count > 0) foreach (var excludeFormat in item.ExcludeFormats) excludeFormatsDescription += excludeFormat + ",";
                if (item.ExcludeFormats.Count > 0) { excludeFormatsDescription = excludeFormatsDescription.TrimEnd(','); item.Description += excludeFormatsDescription; }

                // build excluded game types description
                var excludeGameTypesDescription = "\nExcluded Game Types: ";
                if (item.ExcludeGameTypes.Count > 0) foreach (var excludeGameType in item.ExcludeGameTypes) excludeGameTypesDescription += excludeGameType + ",";
                if (item.ExcludeGameTypes.Count > 0) { excludeGameTypesDescription = excludeGameTypesDescription.TrimEnd(','); item.Description += excludeGameTypesDescription; }

                // build excluded months description
                var excludeMonthsDescription = "\nExcluded Months: ";
                if (item.ExcludeMonths.Count > 0) foreach (var excludeMonths in item.ExcludeMonths) excludeMonthsDescription += excludeMonths + ",";
                if (item.ExcludeMonths.Count > 0) { excludeMonthsDescription = excludeMonthsDescription.TrimEnd(','); item.Description += excludeMonthsDescription; }

                // build excluded venue description
                var excludeVenuesDescription = "\nExcluded Venues: ";
                if (item.ExcludeVenues.Count > 0) foreach (var excludeVenues in item.ExcludeVenues) excludeVenuesDescription += excludeVenues + ",";
                if (item.ExcludeVenues.Count > 0) { excludeVenuesDescription = excludeVenuesDescription.TrimEnd(','); item.Description += excludeVenuesDescription; }

                // build description
                if (item.FieldBeatenLow is not 0 || item.FieldBeatenHigh is not 0) item.Description += "\n% Field Beaten: " + item.FieldBeatenLow + " to " + item.FieldBeatenHigh;
                if (item.FinishPositionLow is not 0 || item.FinishPositionHigh is not 0) item.Description += "\nFinish Position: " + item.FinishPositionLow + " to " + item.FinishPositionHigh;
                if (item.GuaranteeLow is not 0 || item.GuaranteeHigh is not 0) item.Description += "\nGuarantee: " + item.GuaranteeLow.ToString("C") + " to " + item.GuaranteeHigh.ToString("C");

                // build included days description
                var includeDaysDescription = "\nIncluded Days: ";
                if (item.IncludeDays.Count > 0) foreach (var includeDays in item.IncludeDays) includeDaysDescription += includeDays + ",";
                if (item.IncludeDays.Count > 0) { includeDaysDescription = includeDaysDescription.TrimEnd(','); item.Description += includeDaysDescription; }

                // build included formats description
                var includeFormatsDescription = "\nIncluded Formats: ";
                if (item.IncludeFormats.Count > 0) foreach (var includeFormat in item.IncludeFormats) includeFormatsDescription += includeFormat + ",";
                if (item.IncludeFormats.Count > 0) { includeFormatsDescription = includeFormatsDescription.TrimEnd(','); item.Description += includeFormatsDescription; }

                // build included game types description
                var includeGameTypesDescription = "\nIncluded Game Types: ";
                if (item.IncludeGameTypes.Count > 0) foreach (var includeGameType in item.IncludeGameTypes) includeGameTypesDescription += includeGameType + ",";
                if (item.IncludeGameTypes.Count > 0) { includeGameTypesDescription = includeGameTypesDescription.TrimEnd(','); item.Description += includeGameTypesDescription; }

                // build included months description
                var includeMonthsDescription = "\nIncluded Months: ";
                if (item.IncludeMonths.Count > 0) foreach (var includeMonths in item.IncludeMonths) includeMonthsDescription += includeMonths + ",";
                if (item.IncludeMonths.Count > 0) { includeMonthsDescription = includeMonthsDescription.TrimEnd(','); item.Description += includeMonthsDescription; }

                // build included venues description
                var includeVenuesDescription = "\nIncluded Venues: ";
                if (item.IncludeVenues.Count > 0) foreach (var includeVenues in item.IncludeVenues) includeVenuesDescription += includeVenues + ",";
                if (item.IncludeVenues.Count > 0) { includeVenuesDescription = includeVenuesDescription.TrimEnd(','); item.Description += includeVenuesDescription; }

                // build description
                if (item.LateRegLow is not 0 || item.LateRegHigh is not 0) item.Description += "\nLate Reg: " + item.LateRegLow + " to " + item.LateRegHigh;
                if (item.NumberOfResults is not 0 ) item.Description += "\nNumber of Results: " + item.NumberOfResults;
                if (item.PrizeWonLow is not 0 || item.PrizeWonHigh is not 0) item.Description += "\nPrize Won: " + item.PrizeWonLow.ToString("C") + " to " + item.PrizeWonHigh.ToString("C");
                if (item.RebuyCostLow is not 0 || item.RebuyCostHigh is not 0) item.Description += "\nRebuy Cost: " + item.AddonCostLow.ToString("C") + " to " + item.RebuyCostHigh.ToString("C");
                if (item.RebuyCountLow is not 0 || item.RebuyCountHigh is not 0) item.Description += "\nRebuy Count: " + item.AddonCountLow + " to " + item.RebuyCountHigh;
                if (item.RebuyStackLow is not 0 || item.RebuyStackHigh is not 0) item.Description += "\nRebuy Stack: " + item.AddonStackLow + " to " + item.RebuyStackHigh;
                if (item.StartingStackLow is not 0 || item.StartingStackHigh is not 0) item.Description += "\nStarting Stack: " + item.StartingStackLow + " to " + item.StartingStackHigh;
                if (item.TableSizeLow is not 2 || item.TableSizeHigh is not 2) item.Description += "\nTable Size: " + item.TableSizeLow + " to " + item.TableSizeHigh;
                if (item.TotalCostLow is not 0 || item.TotalCostHigh is not 0) item.Description += "\nTotal Cost: " + item.TotalCostLow.ToString("C") + " to " + item.TotalCostHigh.ToString("C");

                // remove blank line from top of description
                item.Description = item.Description.TrimStart('\n');

                // add filter item to filter list
                FilterList.Add(item);
            }

            // order list by name
            FilterList = new ObservableCollection<FilterListBoxItem>(FilterList.OrderBy(i => i.Name));
        }

        /// <summary>
        /// load selected filter
        /// </summary>
        /// <param name="parameter"></param>
        private void LoadFilter(object parameter)
        {
            // null check
            if (FilterList is null) return;

            // nothing selected
            if (FilterList.Any(i => i.IsSelected) is false) return;

            // get selected filter
            var filter = FilterList.FirstOrDefault(i => i.IsSelected);

            // null check
            if (filter is null) return;

            // yes/no view model
            var vm = new YesNoViewModel("Are you sure you want to load this filter?\n\n" + filter.Description, "Load Filter");

            // create/show yes/no window
            var window = new YesNoView(vm)
            {
                Owner = Application.Current.Windows.OfType<LoadTournamentFilterView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            window.ShowDialog();

            // didn't save, exit
            if (vm.Saved is false) return;

            // grab tournament results window
            if (Application.Current.Windows.OfType<TournamentsResultsView>().FirstOrDefault()?.DataContext is not TournamentsResultsViewModel tournamentResultsViewModel) return;

            // grab unfiltered tournaments list
            ObservableCollection<TournamentFinished> list = new ObservableCollection<TournamentFinished>(tournamentResultsViewModel.UnfilteredTournaments);

            // null/zero check
            if (list is null || list.Count is 0) return;

            // reset current filter text
            CurrentFilter = filter.Name + "\n";

            // track whether we want to filter that stat or not
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

            // check the filters user wants to use
            if (filter.AddonCostLow is not 0 || filter.AddonCostHigh is not 0) if (filter.AddonCostLow <= filter.AddonCostHigh) filterAddonCost = true;
            if (filter.AddonCountLow is not 0 || filter.AddonCountHigh is not 0) if (filter.AddonCountLow <= filter.AddonCountHigh) filterAddonCount = true;
            if (filter.AddonStackLow is not 0 || filter.AddonStackHigh is not 0) if (filter.AddonStackLow <= filter.AddonStackHigh) filterAddonStack = true;
            if (filter.BlindLevelsLow is not 0 || filter.BlindLevelsHigh is not 0) if (filter.BlindLevelsLow <= filter.BlindLevelsHigh) filterBlindLevels = true;
            if (filter.BuyinCostLow is not 0 || filter.BuyinCostHigh is not 0) if (filter.BuyinCostLow <= filter.BuyinCostHigh) filterBuyinCost = true;
            if (filter.EntrantsLow is not 0 || filter.EntrantsHigh is not 0) if (filter.EntrantsLow <= filter.EntrantsHigh) filterEntrants = true;
            if (filter.EntrantsPaidLow is not 0 || filter.EntrantsPaidHigh is not 0) if (filter.EntrantsPaidLow <= filter.EntrantsPaidHigh) filterEntrantsPaid = true;
            if (filter.FieldBeatenLow is not 0 || filter.FieldBeatenHigh is not 0) if (filter.FieldBeatenLow <= filter.FieldBeatenHigh) filterFieldBeaten = true;
            if (filter.FinishPositionLow is not 0 || filter.FinishPositionHigh is not 0) if (filter.FinishPositionLow <= filter.FinishPositionHigh) filterFinishPosition = true;
            if (filter.GuaranteeLow is not 0 || filter.GuaranteeHigh is not 0) if (filter.GuaranteeLow <= filter.GuaranteeHigh) filterGuarantee = true;
            if (filter.LateRegLow is not 0 || filter.LateRegHigh is not 0) if (filter.LateRegLow <= filter.LateRegHigh) filterLateReg = true;
            if (filter.NameExcludes.Length is not 0) filterNameExcludes = true;
            if (filter.NameIncludes.Length is not 0) filterNameIncludes = true;
            if (filter.NumberOfResults is not 0) filterNumberOfResults = true;
            if (filter.PrizeWonLow is not 0 || filter.PrizeWonHigh is not 0) if (filter.PrizeWonLow <= filter.PrizeWonHigh) filterPrizeWon = true;
            if (filter.RebuyCostLow is not 0 || filter.RebuyCostHigh is not 0) if (filter.RebuyCostLow <= filter.RebuyCostHigh) filterRebuyCost = true;
            if (filter.RebuyCountLow is not 0 || filter.RebuyCountHigh is not 0) if (filter.RebuyCountLow <= filter.RebuyCountHigh) filterRebuyCount = true;
            if (filter.RebuyStackLow is not 0 || filter.RebuyStackHigh is not 0) if (filter.RebuyStackLow <= filter.RebuyStackHigh) filterRebuyStack = true;
            //if (filter.StartDateLow != _startingDate || filter.StartDateHigh != _startingDate) if (filter.StartDateLow <= filter.StartDateHigh) filterStartDate = true;
            if (filter.StartingStackLow is not 0 || filter.StartingStackHigh is not 0) if (filter.StartingStackLow <= filter.StartingStackHigh) filterStartingStack = true;
            //if (filter.StartTimeLow != _startingTime || filter.StartTimeHigh != _startingTime) if (filter.StartTimeLow <= filter.StartTimeHigh) filterStartTime = true;
            if (filter.TableSizeLow is not 2 || filter.TableSizeHigh is not 2) if (filter.TableSizeLow <= filter.TableSizeHigh) filterTableSize = true;
            if (filter.TotalCostLow is not 0 || filter.TotalCostHigh is not 0) if (filter.TotalCostLow <= filter.TotalCostHigh) filterTotalCost = true;

            // add addon cost to filter
            if (filterAddonCost) { list = list.Where(i => i.AddonTotalCost >= filter.AddonCostLow && i.AddonTotalCost <= filter.AddonCostHigh).Any() ? new ObservableCollection<TournamentFinished>(list.Where(i => i.AddonTotalCost >= filter.AddonCostLow && i.AddonTotalCost <= filter.AddonCostHigh)) : list; CurrentFilter += "\nAddon Cost: " + filter.AddonCostLow.ToString("C") + " to " + filter.AddonCostHigh.ToString("C"); }

            // add addon count to filter
            if (filterAddonCount) { list = list.Where(i => i.AddonCount >= filter.AddonCountLow && i.AddonCount <= filter.AddonCountHigh).Any() ? new ObservableCollection<TournamentFinished>(list.Where(i => i.AddonCount >= filter.AddonCountLow && i.AddonCount <= filter.AddonCountHigh)) : list; CurrentFilter += "\nAddon Count: " + filter.AddonCountLow + " to " + filter.AddonCountHigh; }

            // add addon stack to filter
            if (filterAddonStack) { list = list.Where(i => i.StackSizeAddon >= filter.AddonStackLow && i.StackSizeAddon <= filter.AddonStackHigh).Any() ? new ObservableCollection<TournamentFinished>(list.Where(i => i.StackSizeAddon >= filter.AddonStackLow && i.StackSizeAddon <= filter.AddonStackHigh)) : list; CurrentFilter += "\nAddon Stack: " + filter.AddonStackLow + " to " + filter.AddonStackHigh; }

            // add blind levels to filter
            if (filterBlindLevels) { list = list.Where(i => i.BlindLevels >= filter.BlindLevelsHigh && i.BlindLevels <= filter.BlindLevelsHigh).Any() ? new ObservableCollection<TournamentFinished>(list.Where(i => i.BlindLevels >= filter.BlindLevelsLow && i.BlindLevels <= filter.BlindLevelsHigh)) : list; CurrentFilter += "\nBlind Levels: " + filter.BlindLevelsLow + " to " + filter.BlindLevelsHigh; }

            // add buy-in cost to filter
            if (filterBuyinCost) { list = list.Where(i => i.BuyinTotalCost >= filter.BuyinCostLow && i.BuyinTotalCost <= filter.BuyinCostHigh).Any() ? new ObservableCollection<TournamentFinished>(list.Where(i => i.BuyinTotalCost >= filter.BuyinCostLow && i.BuyinTotalCost <= filter.BuyinCostHigh)) : list; CurrentFilter += "\nBuyin Cost: " + filter.BuyinCostLow.ToString("C") + " to " + filter.BuyinCostHigh.ToString("C"); }

            // add entrants to filter
            if (filterEntrants) { list = list.Where(i => i.Entrants >= filter.EntrantsLow && i.Entrants <= filter.EntrantsHigh).Any() ? new ObservableCollection<TournamentFinished>(list.Where(i => i.Entrants >= filter.EntrantsLow && i.Entrants <= filter.EntrantsHigh)) : list; CurrentFilter += "\nEntrants: " + filter.EntrantsLow + " to " + filter.EntrantsHigh; }

            // add entrants paid to filter
            if (filterEntrantsPaid) { list = list.Where(i => i.EntrantsPaid >= filter.EntrantsPaidLow && i.EntrantsPaid <= filter.EntrantsPaidHigh).Any() ? new ObservableCollection<TournamentFinished>(list.Where(i => i.EntrantsPaid >= filter.EntrantsPaidLow && i.EntrantsPaid <= filter.EntrantsPaidHigh)) : list; CurrentFilter += "\nEntrants Paid: " + filter.EntrantsPaidLow + " to " + filter.EntrantsPaidHigh; }

            // add field beaten to filter
            if (filterFieldBeaten) { list = list.Where(i => i.PercentFieldBeaten >= filter.FieldBeatenLow && i.PercentFieldBeaten <= filter.FieldBeatenHigh).Any() ? new ObservableCollection<TournamentFinished>(list.Where(i => i.PercentFieldBeaten >= filter.FieldBeatenLow && i.PercentFieldBeaten <= filter.FieldBeatenHigh)) : list; CurrentFilter += "\nField Beaten: " + filter.FieldBeatenLow + " to " + filter.FieldBeatenHigh; }

            // add finish position to filter
            if (filterFinishPosition) { list = list.Where(i => i.FinishPosition >= filter.FinishPositionLow && i.FinishPosition <= filter.FinishPositionHigh).Any() ? new ObservableCollection<TournamentFinished>(list.Where(i => i.FinishPosition >= filter.FinishPositionLow && i.FinishPosition <= filter.FinishPositionHigh)) : list; CurrentFilter += "\nFinish Position: " + filter.FinishPositionLow + " to " + filter.FinishPositionHigh; }

            // add guarantee to filter
            if (filterGuarantee) { list = list.Where(i => i.Guarantee >= filter.GuaranteeLow && i.Guarantee <= filter.GuaranteeHigh).Any() ? new ObservableCollection<TournamentFinished>(list.Where(i => i.Guarantee >= filter.GuaranteeLow && i.Guarantee <= filter.GuaranteeHigh)) : list; CurrentFilter += "\nGuarantee: " + filter.GuaranteeLow.ToString("C") + " to " + filter.GuaranteeHigh.ToString("C"); }

            // add late reg to filter
            if (filterLateReg) { list = list.Where(i => i.LateReg >= filter.LateRegLow && i.LateReg <= filter.LateRegHigh).Any() ? new ObservableCollection<TournamentFinished>(list.Where(i => i.LateReg >= filter.LateRegLow && i.LateReg <= filter.LateRegHigh)) : list; CurrentFilter += "\nLate Reg: " + filter.LateRegLow + " to " + filter.LateRegHigh; }

            // add name excludes to filter
            if (filterNameExcludes) { foreach (string excludeString in filter.NameExcludes.Split()) foreach (var tournament in list.ToList()) if (tournament.TournamentName.Trim().ToUpper(new CultureInfo("en-US")).Contains(excludeString.Trim().ToUpper(new CultureInfo("en-US")))) list.Remove(tournament); CurrentFilter += "\nName Excludes: " + filter.NameExcludes; }

            // add name includes to filter
            if (filterNameIncludes) { foreach (string includeString in filter.NameIncludes.Split()) foreach (var tournament in list.ToList()) if (!tournament.TournamentName.Trim().ToUpper(new CultureInfo("en-US")).Contains(includeString.Trim().ToUpper(new CultureInfo("en-US")))) list.Remove(tournament); CurrentFilter += "\nName Includes: " + filter.NameIncludes; }

            // add prize won to filter
            if (filterPrizeWon) { list = list.Where(i => i.PrizeWon >= filter.PrizeWonLow && i.PrizeWon <= filter.PrizeWonHigh).Any() ? new ObservableCollection<TournamentFinished>(list.Where(i => i.PrizeWon >= filter.PrizeWonLow && i.PrizeWon <= filter.PrizeWonHigh)) : list; CurrentFilter += "\nPrize Won: " + filter.PrizeWonLow.ToString("C") + " to " + filter.PrizeWonHigh.ToString("C"); }

            // add rebuy cost to filter
            if (filterRebuyCost) { list = list.Where(i => i.RebuyTotalCost >= filter.RebuyCostLow && i.RebuyTotalCost <= filter.RebuyCostHigh).Any() ? new ObservableCollection<TournamentFinished>(list.Where(i => i.RebuyTotalCost >= filter.RebuyCostLow && i.RebuyTotalCost <= filter.RebuyCostHigh)) : list; CurrentFilter += "\nRebuy Cost: " + filter.RebuyCostLow.ToString("C") + " to " + filter.RebuyCostHigh.ToString("C"); }

            // add rebuy count to filter
            if (filterRebuyCount) { list = list.Where(i => i.RebuyCount >= filter.RebuyCountLow && i.RebuyCount <= filter.RebuyCountHigh).Any() ? new ObservableCollection<TournamentFinished>(list.Where(i => i.RebuyCount >= filter.RebuyCountLow && i.RebuyCount <= filter.RebuyCountHigh)) : list; CurrentFilter += "\nRebuy Count: " + filter.RebuyCountLow + " to " + filter.RebuyCountHigh; }

            // add rebuy stack to filter
            if (filterRebuyStack) { list = list.Where(i => i.StackSizeRebuy >= filter.RebuyStackLow && i.StackSizeRebuy <= filter.RebuyStackHigh).Any() ? new ObservableCollection<TournamentFinished>(list.Where(i => i.StackSizeRebuy >= filter.RebuyStackLow && i.StackSizeRebuy <= filter.RebuyStackHigh)) : list; CurrentFilter += "\nRebuy Stack: " + filter.RebuyStackLow + " to " + filter.RebuyStackHigh; }

            // add starting stack to filter
            if (filterStartingStack) { list = list.Where(i => i.StackSizeStarting >= filter.StartingStackLow && i.StackSizeStarting <= filter.StartingStackHigh).Any() ? new ObservableCollection<TournamentFinished>(list.Where(i => i.StackSizeStarting >= filter.StartingStackLow && i.StackSizeStarting <= filter.StartingStackHigh)) : list; CurrentFilter += "\nStarting Stack: " + filter.StartingStackLow + " to " + filter.StartingStackHigh; }

            // add start date to filter
            if (filterStartDate) { list = list.Where(i => i.StartTime >= filter.StartDateLow && i.StartTime <= filter.StartDateHigh).Any() ? new ObservableCollection<TournamentFinished>(list.Where(i => i.StartTime >= filter.StartDateLow && i.StartTime <= filter.StartDateHigh)) : list; CurrentFilter += "\nStart Date: " + filter.StartDateLow.ToString("MM/dd/yyyy", new CultureInfo("en-US")) + " to " + filter.StartDateHigh.ToString("MM/dd/yyyy", new CultureInfo("en-US")); }

            // add start time to filter
            if (filterStartTime)
            {
                foreach (var tournament in list.ToList()) if (tournament.StartTime.Hour < filter.StartTimeLow.Hour) list.Remove(tournament);
                foreach (var tournament in list.ToList()) if (tournament.StartTime.Hour == filter.StartTimeLow.Hour && tournament.StartTime.Minute < filter.StartTimeLow.Minute) list.Remove(tournament);
                foreach (var tournament in list.ToList()) if (tournament.StartTime.Hour > filter.StartTimeHigh.Hour) list.Remove(tournament);
                foreach (var tournament in list.ToList()) if (tournament.StartTime.Hour == filter.StartTimeHigh.Hour && tournament.StartTime.Minute > filter.StartTimeHigh.Minute) list.Remove(tournament);

                CurrentFilter += "\nStart Time: " + String.Format(new CultureInfo("en-US"), "{0:h:mm tt}", filter.StartTimeLow) + " to " + String.Format(new CultureInfo("en-US"), "{0:h:mm tt}", filter.StartTimeHigh);
            }

            // add table size to filter
            if (filterTableSize) { list = list.Where(i => i.TableSize >= filter.TableSizeLow && i.TableSize <= filter.TableSizeHigh).Any() ? new ObservableCollection<TournamentFinished>(list.Where(i => i.TableSize >= filter.TableSizeLow && i.TableSize <= filter.TableSizeHigh)) : list; CurrentFilter += "\nTable Size: " + filter.TableSizeLow + " to " + filter.TableSizeHigh; }

            // add total cost to filter
            if (filterTotalCost) { list = list.Where(i => i.TotalCost >= filter.TotalCostLow && i.TotalCost <= filter.TotalCostHigh).Any() ? new ObservableCollection<TournamentFinished>(list.Where(i => i.TotalCost >= filter.TotalCostLow && i.TotalCost <= filter.TotalCostHigh)) : list; CurrentFilter += "\nTotal Cost: " + filter.TotalCostLow.ToString("C") + " to " + filter.TotalCostHigh.ToString("C"); }

            // exclude days
            if (filter.ExcludeDays.Count > 0)
            {
                // holds excluded terms
                var excludes = new List<string>();

                // loop for day matches and add descriptions to list
                foreach (var excludeString in filter.ExcludeDays) if (excludes.Contains(excludeString) is false) excludes.Add(excludeString);

                // loop tournaments
                foreach (var tournament in list.ToList())
                {
                    // no match
                    bool match = false;

                    // loop for day matches
                    foreach (var excludeString in filter.ExcludeDays)
                    {
                        // flag match
                        if (tournament.StartDate.DayOfWeek.ToString() == excludeString) match = true;

                        // add to excluded terms list if not already there
                        if (excludes.Contains(excludeString) is false) excludes.Add(excludeString);
                    }

                    // remove tournament
                    if (match) list.Remove(tournament);
                }

                // hold the combined excludes string
                var excludesString = "";

                // combine excludes into one string
                foreach (var exclude in excludes) excludesString += ", " + exclude;

                // remove leading comma
                excludesString = excludesString.TrimStart(',');

                // add excludes string to current filter if needed
                if (excludesString.Length > 0) CurrentFilter += "\n\nExclude Day(s): " + excludesString;
            }

            // exclude formats
            if (filter.ExcludeFormats.Count > 0)
            {
                // holds excluded terms
                var excludes = new List<string>();

                // loop exclude formats and build description list
                foreach (var excludeFormatItem in filter.ExcludeFormats) if (excludes.Contains(excludeFormatItem) is false) excludes.Add(excludeFormatItem);

                // loop tournaments
                foreach (var tournament in list.ToList())
                {
                    // no match
                    bool match = false;

                    // see if format matches
                    foreach (string formatString in tournament.Formats)
                        foreach (var excludeFormatItem in filter.ExcludeFormats)
                        {
                            // flag as a matching format to exclude
                            if (formatString == excludeFormatItem) match = true;

                            // add to excluded terms list if not already there
                            if (excludes.Contains(excludeFormatItem) is false) excludes.Add(excludeFormatItem);
                        }

                    // remove tournament
                    if (match) list.Remove(tournament);
                }

                // hold the combined excludes string
                var excludesString = "";

                // combine excludes into one string
                foreach (var exclude in excludes) excludesString += ", " + exclude;

                // remove leading comma
                excludesString = excludesString.TrimStart(',');

                // add excludes string to current filter if needed
                if (excludesString.Length > 0) CurrentFilter += "\nExclude Format(s): " + excludesString;
            }

            // exclude game types
            if (filter.ExcludeGameTypes.Count > 0)
            {
                // holds excluded terms
                var excludes = new List<string>();

                // loop filter game types and build description list
                foreach (var excludeString in filter.ExcludeGameTypes) if (excludes.Contains(excludeString) is false) excludes.Add(excludeString);

                // loop tournaments
                foreach (var tournament in list.ToList())
                {
                    // no match
                    bool match = false;

                    // loop for game type matches and flag
                    foreach (var excludeString in filter.ExcludeGameTypes) if (tournament.GameType == excludeString) match = true;

                    // remove tournament
                    if (match) list.Remove(tournament);
                }

                // hold the combined excludes string
                var excludesString = "";

                // combine excludes into one string
                foreach (var exclude in excludes) excludesString += ", " + exclude;

                // remove leading comma
                excludesString = excludesString.TrimStart(',');

                // add excludes string to current filter if needed
                if (excludesString.Length > 0) CurrentFilter += "\nExclude Game Type(s): " + excludesString;
            }

            // exclude months
            if (filter.ExcludeMonths.Count > 0)
            {
                // holds excluded terms
                var excludes = new List<string>();

                // loop tournaments
                foreach (var tournament in list.ToList())
                {
                    // no match
                    bool match = false;

                    // loop for month matches
                    foreach (var excludeString in filter.ExcludeMonths)
                    {
                        // flag match
                        if (tournament.StartDate.Month == 1 && excludeString == "January") match = true;
                        if (tournament.StartDate.Month == 2 && excludeString == "February") match = true;
                        if (tournament.StartDate.Month == 3 && excludeString == "March") match = true;
                        if (tournament.StartDate.Month == 4 && excludeString == "April") match = true;
                        if (tournament.StartDate.Month == 5 && excludeString == "May") match = true;
                        if (tournament.StartDate.Month == 6 && excludeString == "June") match = true;
                        if (tournament.StartDate.Month == 7 && excludeString == "July") match = true;
                        if (tournament.StartDate.Month == 8 && excludeString == "August") match = true;
                        if (tournament.StartDate.Month == 9 && excludeString == "September") match = true;
                        if (tournament.StartDate.Month == 10 && excludeString == "October") match = true;
                        if (tournament.StartDate.Month == 11 && excludeString == "November") match = true;
                        if (tournament.StartDate.Month == 12 && excludeString == "December") match = true;

                        // add to excluded terms list if not already there
                        if (excludes.Contains(excludeString) is false) excludes.Add(excludeString);
                    }

                    // remove tournament
                    if (match) list.Remove(tournament);
                }

                // hold the combined excludes string
                var excludesString = "";

                // combine excludes into one string
                foreach (var exclude in excludes) excludesString += ", " + exclude;

                // remove leading comma
                excludesString = excludesString.TrimStart(',');

                // add excludes string to current filter if needed
                if (excludesString.Length > 0) CurrentFilter += "\nExclude Month(s): " + excludesString;
            }

            // exclude venues
            if (filter.ExcludeVenues.Count > 0)
            {
                // holds excluded terms
                var excludes = new List<string>();

                // loop for venues and build the description list
                foreach (var excludeString in filter.ExcludeVenues) if (excludes.Contains(excludeString) is false) excludes.Add(excludeString);

                // loop tournaments
                foreach (var tournament in list.ToList())
                {
                    // no match
                    bool match = false;

                    // loop and flag for venue matches
                    foreach (var excludeString in filter.ExcludeVenues) if (tournament.Venue == excludeString) match = true;

                    // remove tournament
                    if (match) list.Remove(tournament);
                }

                // hold the combined excludes string
                var excludesString = "";

                // combine excludes into one string
                foreach (var exclude in excludes) excludesString += ", " + exclude;

                // remove leading comma
                excludesString = excludesString.TrimStart(',');

                // add excludes string to current filter if needed
                if (excludesString.Length > 0) CurrentFilter += "\nExclude Venue(s): " + excludesString;
            }

            // include days
            if (filter.IncludeDays.Count > 0)
            {
                // holds included terms
                var includes = new List<string>();

                // loop for day matches and add to description list
                foreach (var includeString in filter.IncludeDays) if (includes.Contains(includeString) is false) includes.Add(includeString);

                // loop tournaments
                foreach (var tournament in list.ToList())
                {
                    // no match
                    bool match = false;

                    // loop for day matches
                    foreach (var includeString in filter.IncludeDays) if (tournament.StartDate.DayOfWeek.ToString() == includeString) match = true;

                    // remove tournament
                    if (!match) list.Remove(tournament);
                }

                // hold the combined includes string
                var includesString = "";

                // combine includes into one string
                foreach (var include in includes) includesString += ", " + include;

                // remove leading comma
                includesString = includesString.TrimStart(',');

                // add includes string to current filter if needed
                if (includesString.Length > 0) CurrentFilter += "\n\nInclude Day(s): " + includesString;
            }

            // include formats
            if (filter.IncludeFormats.Count > 0)
            {
                // holds included terms
                var includes = new List<string>();

                // loop for include formats and build the description list
                foreach (var formatItem in filter.IncludeFormats) if (includes.Contains(formatItem) is false) includes.Add(formatItem);

                // loop tournaments
                foreach (var tournament in list.ToList())
                {
                    // number matching formats
                    int matchingFormatsCounter = 0;

                    // get matching formats count
                    foreach (string formatString in tournament.Formats) foreach (var formatItem in filter.IncludeFormats) if (formatString == formatItem) matchingFormatsCounter++;

                    // remove tournaments that don't match the formats count
                    if (matchingFormatsCounter != filter.IncludeFormats.Count) list.Remove(tournament);
                }

                // hold the combined includes string
                var includesString = "";

                // combine excludes into one string
                foreach (var include in includes) includesString += ", " + include;

                // remove leading comma
                includesString = includesString.TrimStart(',');

                // add excludes string to current filter if needed
                if (includesString.Length > 0) CurrentFilter += "\nInclude Format(s): " + includesString;
            }

            // include game types
            if (filter.IncludeGameTypes.Count > 0)
            {
                // holds excluded terms
                var includes = new List<string>();

                // loop for game type and build the description list
                foreach (var includeString in filter.IncludeGameTypes) if (includes.Contains(includeString) is false) includes.Add(includeString);

                // loop tournaments
                foreach (var tournament in list.ToList())
                {
                    // no match
                    bool match = false;

                    // loop for game type matches
                    foreach (var includeString in filter.IncludeGameTypes)
                    {
                        // flag match
                        if (tournament.GameType == includeString) match = true;

                        // add to excluded terms list if not already there
                        if (includes.Contains(includeString) is false) includes.Add(includeString);
                    }

                    // remove tournament
                    if (!match) list.Remove(tournament);
                }

                // hold the combined excludes string
                var includesString = "";

                // combine excludes into one string
                foreach (var include in includes) includesString += ", " + include;

                // remove leading comma
                includesString = includesString.TrimStart(',');

                // add excludes string to current filter if needed
                if (includesString.Length > 0) CurrentFilter += "\nInclude Game Type(s): " + includesString;
            }

            // include months
            if (filter.IncludeMonths.Count > 0)
            {
                // holds included terms
                var includes = new List<string>();

                // add to included terms list if not already there
                foreach (var includeString in filter.IncludeMonths) if (includes.Contains(includeString) is false) includes.Add(includeString);

                // loop tournaments
                foreach (var tournament in list.ToList())
                {
                    // no match
                    bool match = false;

                    // loop for month matches
                    foreach (var includeString in filter.IncludeMonths)
                    {
                        // flag match
                        if (tournament.StartDate.Month == 1 && includeString == "January") match = true;
                        if (tournament.StartDate.Month == 2 && includeString == "February") match = true;
                        if (tournament.StartDate.Month == 3 && includeString == "March") match = true;
                        if (tournament.StartDate.Month == 4 && includeString == "April") match = true;
                        if (tournament.StartDate.Month == 5 && includeString == "May") match = true;
                        if (tournament.StartDate.Month == 6 && includeString == "June") match = true;
                        if (tournament.StartDate.Month == 7 && includeString == "July") match = true;
                        if (tournament.StartDate.Month == 8 && includeString == "August") match = true;
                        if (tournament.StartDate.Month == 9 && includeString == "September") match = true;
                        if (tournament.StartDate.Month == 10 && includeString == "October") match = true;
                        if (tournament.StartDate.Month == 11 && includeString == "November") match = true;
                        if (tournament.StartDate.Month == 12 && includeString == "December") match = true;
                    }

                    // remove tournament
                    if (!match) list.Remove(tournament);
                }

                // hold the combined includes string
                var includesString = "";

                // combine includes into one string
                foreach (var include in includes) includesString += ", " + include;

                // remove leading comma
                includesString = includesString.TrimStart(',');

                // add includes string to current filter if needed
                if (includesString.Length > 0) CurrentFilter += "\nInclude Month(s): " + includesString;
            }

            // include venues
            if (filter.IncludeVenues.Count > 0)
            {
                // hold include strings
                var includes = new List<string>();

                // loop the venues and build the description list
                foreach (var includeVenue in filter.IncludeVenues) if (includes.Contains(includeVenue) is false) includes.Add(includeVenue);

                // loop tournaments
                foreach (var tournament in list.ToList())
                {
                    // flag for matching
                    bool match = false;

                    // loop the includes and flag matches
                    foreach (var includeVenue in filter.IncludeVenues) if (tournament.Venue == includeVenue) match = true;

                    // add matches to include list
                    if (!match) list.Remove(tournament);
                }

                // include description string
                var includeString = "";

                // add each include separated by a comma
                foreach (var include in includes) includeString += ", " + include;

                // remove leading comma
                includeString = includeString.TrimStart(',');

                // add include to description
                if (includeString.Length > 0) CurrentFilter += "\nInclude Venue(s): " + includeString + "\n";
            }

            // limit results to the number of results
            if (filterNumberOfResults)
            {
                list = new ObservableCollection<TournamentFinished>(list.TakeLast(filter.NumberOfResults));
                CurrentFilter += "\nResults Limit: " + filter.NumberOfResults;
            }

            // update tournaments
            tournamentResultsViewModel.Tournaments = new ObservableCollection<TournamentFinished>(list);

            // filter tournaments
            tournamentResultsViewModel.Filter();

            // update current filter to new filter
            tournamentResultsViewModel.CurrentFilter = CurrentFilter;

            // update current filter to new filter
            tournamentResultsViewModel.FilterName = filter.Name;

            // close window
            CloseWindow(null);

            // create/show success message
            //var okayVM1 = new OkViewModel(filter.Name + " was loaded successfully", "Success!");
            //var okayWindow1 = new OkView(okayVM1) { Owner = Application.Current.Windows.OfType<TournamentsResultsView>().FirstOrDefault(), WindowStartupLocation = WindowStartupLocation.CenterOwner, }; okayWindow1.ShowDialog();
        }

        /// <summary>
        /// rename a filter
        /// </summary>
        /// <param name="parameter"></param>
        private void RenameFilter(object parameter)
        {
            // null check
            if (FilterList is null) return;

            // nothing selected
            if (FilterList.Any(i => i.IsSelected) is false) return;

            // get selected filter
            var filter = FilterList.FirstOrDefault(i => i.IsSelected);

            // null check
            if (filter is null) return;

            // create/show rename filter window
            var vm = new RenameFilterViewModel(filter, FilterList);
            var window = new RenameFilterView(vm)
            {
                Owner = Application.Current.Windows.OfType<LoadTournamentFilterView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            window.ShowDialog();

            // didn't update
            if (vm.Saved is false) return;

            // filter file name
            var fileName = XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.FiltersFileName;

            // load filters file
            var doc = XmlHelper.LoadXmlFile(fileName);

            // no file
            if (doc is null) return;

            // last blank check
            if (vm.Text.Trim().Length is 0) return;

            // loop through filter nodes
            foreach (XmlNode node in doc?.DocumentElement.SelectSingleNode("TournamentFilters")?.SelectNodes("Filter"))
            {
                // find the matching node
                if (node.Attributes["Name"]?.Value == filter.Name)
                {
                    // copy node
                    var copy = node.Clone();

                    // remove node
                    node.ParentNode?.RemoveChild(node);

                    // save xml file
                    doc.Save(fileName);

                    // update name
                    copy.Attributes["Name"].Value = vm.Text;

                    // add update node
                    doc.DocumentElement.SelectSingleNode("TournamentFilters").AppendChild(copy);

                    // save xml file
                    doc.Save(fileName);

                    // update filters
                    CreateFilters();

                    // leave loop
                    break;
                }
            }
        }
    }
}