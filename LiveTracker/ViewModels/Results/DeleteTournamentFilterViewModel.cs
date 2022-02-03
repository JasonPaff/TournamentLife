using Syncfusion.UI.Xaml.Utility;
using Syncfusion.Windows.Shared;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using Tournament_Life.Helpers;
using Tournament_Life.Views.Results;
using Tournament_Life.Models.Filters;
using Tournament_Life.Views;

namespace Tournament_Life.ViewModels.Results
{
    public class DeleteTournamentFilterViewModel : NotificationObject
    {
        public ICommand DeleteCommand => new BaseCommand(DeleteFilter);
        public ICommand CancelCommand => new BaseCommand(CloseWindow);

        public DeleteTournamentFilterViewModel()
        {
            // font size
            FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));

            // visual theme
            Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");

            // set title
            Title = "Delete Filter";

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
            if (Application.Current.Windows.OfType<DeleteTournamentFilterView>().FirstOrDefault() is not DeleteTournamentFilterView window) return;

            // close window
            window?.Close();
        }

        /// <summary>
        /// delete selected filter
        /// </summary>
        /// <param name="parameter"></param>
        private void DeleteFilter(object parameter)
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
            var vm = new YesNoViewModel("Are you sure you want to delete this filter?\n\n" + filter.Description, "Delete Filter");

            // create/show yes/no window
            var window = new YesNoView(vm)
            {
                Owner = Application.Current.Windows.OfType<DeleteTournamentFilterView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            window.ShowDialog();

            // didn't save, exit
            if (vm.Saved is false) return;

            // filter file name
            var fileName = XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.FiltersFileName;

            // load filters file
            var doc = XmlHelper.LoadXmlFile(fileName);

            // no file
            if (doc is null) return;

            // loop all filters and delete the matching one
            foreach(XmlNode node in doc?.DocumentElement.SelectSingleNode("TournamentFilters")?.SelectNodes("Filter")) if (node.Attributes["Name"]?.Value == filter.Name) { node.ParentNode?.RemoveChild(node); doc.Save(fileName); CreateFilters(); break; }

            // create/show success message
            var okayVM1 = new OkViewModel(filter.Name + " was deleted successfully", "Success!");
            var okayWindow1 = new OkView(okayVM1) { Owner = Application.Current.Windows.OfType<DeleteTournamentFilterView>().FirstOrDefault(), WindowStartupLocation = WindowStartupLocation.CenterOwner, }; okayWindow1.ShowDialog();
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
                item.BuyinCostLow = decimal.Parse(filterNode.SelectSingleNode("BuyinCostLow")?.InnerText ?? "0");
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
                if (item.NumberOfResults is not 0) item.Description += "\nNumber of Results: " + item.NumberOfResults;
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

            // order filter list by name
            FilterList = new ObservableCollection<FilterListBoxItem>(FilterList.OrderBy(i => i.Name));
        }
    }
}
