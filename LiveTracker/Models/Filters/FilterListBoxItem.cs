using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.Windows.Shared;

namespace Tournament_Life.Models.Filters
{
    public class FilterListBoxItem : NotificationObject
    {
        public FilterListBoxItem()
        {
            ExcludeDays = new List<string>();
            ExcludeFormats = new List<string>();
            ExcludeGameTypes = new List<string>();
            ExcludeMonths = new List<string>();
            ExcludeVenues = new List<string>();

            IncludeDays = new List<string>();
            IncludeFormats = new List<string>();
            IncludeGameTypes = new List<string>();
            IncludeMonths = new List<string>();
            IncludeVenues = new List<string>();
        }

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
        public string Description { get; set; } = "";
        public int EntrantsLow { get; set; }
        public int EntrantsHigh { get; set; }
        public int EntrantsPaidLow { get; set; }
        public int EntrantsPaidHigh { get; set; }
        public List<string> ExcludeDays { get; set; }
        public List<string> ExcludeFormats { get; set; }
        public List<string> ExcludeGameTypes { get; set; }
        public List<string> ExcludeMonths { get; set; }
        public List<string> ExcludeVenues { get; set; }
        public decimal FieldBeatenLow { get; set; }
        public decimal FieldBeatenHigh { get; set; }
        public int FinishPositionLow { get; set; }
        public int FinishPositionHigh { get; set; }
        public decimal GuaranteeLow { get; set; }
        public decimal GuaranteeHigh { get; set; }
        public List<string> IncludeDays { get; set; }
        public List<string> IncludeFormats { get; set; }
        public List<string> IncludeGameTypes { get; set; }
        public List<string> IncludeMonths { get; set; }
        public List<string> IncludeVenues { get; set; }
        public bool IsSelected { get; set; }
        public int LateRegLow { get; set; }
        public int LateRegHigh { get; set; }
        public string Name { get; set; } = "";
        public string NameIncludes { get; set; } = "";
        public string NameExcludes { get; set; } = "";
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
        public int TableSizeLow { get; set; } = 2;
        public int TableSizeHigh { get; set; } = 2;
        public decimal TotalCostHigh { get; set; }
        public decimal TotalCostLow { get; set; }
    }
}
