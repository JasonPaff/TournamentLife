using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Syncfusion.Windows.Shared;
using Tournament_Life.Helpers;

namespace Tournament_Life.Models.Tournaments
{
    public class TournamentTemplate :  NotificationObject
    {
        private DateTime _startTime;

        /// <summary>
        /// Standard constructor
        /// </summary>
        public TournamentTemplate()
        {
            Formats = new List<string>();
            StartTime = new DateTime(1111, 1, 1, 1, 1, 0);
            TemplateId = -1;
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="template">template we are copying</param>
        public TournamentTemplate(TournamentTemplate template)
        {
            if (template is null) 
                return;

            AddonBaseCost = template.AddonBaseCost;
            AddonRakeCost = template.AddonRakeCost;
            BlindLevels = template.BlindLevels;
            Bounty = template.Bounty;
            BuyinBaseCost = template.BuyinBaseCost;
            BuyinRakeCost = template.BuyinRakeCost;
            Entrants = template.Entrants;
            EntrantsPaid = template.EntrantsPaid;
            Formats = template.Formats;
            GameType = template.GameType;
            Guarantee = template.Guarantee;
            IsBovadaBounty = template.IsBovadaBounty;
            IsFavorite = template.IsFavorite;
            IsSng = template.IsSng;
            LateReg = template.LateReg;
            RebuyBaseCost = template.RebuyBaseCost;
            RebuyRakeCost = template.RebuyRakeCost;
            SngPayouts = template.SngPayouts;
            StartTime = new DateTime(template.StartTime.Year, template.StartTime.Month, template.StartTime.Day, template.StartTime.Hour, template.StartTime.Minute, 0);
            StackSizeAddon = template.StackSizeAddon;
            StackSizeRebuy = template.StackSizeRebuy;
            StackSizeStarting = template.StackSizeStarting;
            TableSize = template.TableSize;
            TemplateId = template.TemplateId;
            TimeZoneName = template.TimeZoneName;
            TournamentName = template.TournamentName;
            Venue = template.Venue;
        }
        public decimal AddonBaseCost { get; set; } = 0;
        public decimal AddonTotalCost => AddonBaseCost + AddonRakeCost;
        public decimal AddonRakeCost { get; set; } = 0;
        public int BlindLevels { get; set; } = 0;
        public decimal Bounty { get; set; } = 0;
        public decimal BuyinBaseCost { get; set; } = 0;
        public decimal BuyinTotalCost => BuyinBaseCost + BuyinRakeCost;
        public decimal BuyinRakeCost { get; set; } = 0;
        public string Description
        {
            get
            {
                var sb = new StringBuilder();

                sb.AppendFormat(new CultureInfo("en-US"), $"Name: {TournamentName}").AppendLine();
                sb.AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), $"Format(s): {FormatString}").AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), $"Game Type: {GameType}").AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), $"Venue: {Venue}", Venue).AppendLine();
                sb.AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Start Date: {0:M/dd/y}", StartTime).AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Start Time: {0:h:mm tt}", StartTime).AppendLine();
                sb.AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Buyin: {0:C2}", BuyinTotalCost).AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Rebuy: {0:C2}", RebuyTotalCost).AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Addon: {0:C2}", AddonTotalCost).AppendLine();
                sb.AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Guarantee: {0:C0}", Guarantee).AppendLine();
                sb.AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), $"Late Reg: {LateReg} minutes").AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), $"Blind Levels: {BlindLevels} minutes").AppendLine();
                sb.AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), $"Table Size: {TableSize}").AppendLine();
                sb.AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), $"Starting Stack: {StackSizeStarting}").AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), $"Rebuy Stack: {StackSizeRebuy}").AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), $"Addon Stack: {StackSizeAddon}").AppendLine();
                sb.AppendLine();

                if (Entrants > 0)
                    sb.AppendFormat(new CultureInfo("en-US"), $"Entrants: {Entrants}").AppendLine();
                if (EntrantsPaid > 0)
                    sb.AppendFormat(new CultureInfo("en-US"), $"Entrants Paid: {EntrantsPaid}").AppendLine();

                return sb.ToString().Trim();
            }
        }
        public string DescriptionWithoutDayMonthYear
        {
            get
            {
                var sb = new StringBuilder();

                sb.AppendFormat(new CultureInfo("en-US"), "Name: {0}", TournamentName).AppendLine();
                sb.AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), $"Format(s): {FormatString}").AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), $"Game Type: {GameType}").AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), $"Venue: {Venue}", Venue).AppendLine();
                sb.AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Start Time: {0:h:mm tt}", StartTime).AppendLine();
                sb.AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Buyin: {0:C2}", BuyinTotalCost).AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Rebuy: {0:C2}", RebuyTotalCost).AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Addon: {0:C2}", AddonTotalCost).AppendLine();
                sb.AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Guarantee: {0:C0}", Guarantee).AppendLine();
                sb.AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), $"Late Reg: {LateReg} minutes").AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), $"Blind Levels: {BlindLevels} minutes").AppendLine();
                sb.AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), $"Table Size: {TableSize}").AppendLine();
                sb.AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), $"Starting Stack: {StackSizeStarting}").AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), $"Rebuy Stack: {StackSizeRebuy}").AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), $"Addon Stack: {StackSizeAddon}").AppendLine();
                sb.AppendLine();

                if (Entrants > 0)
                    sb.AppendFormat(new CultureInfo("en-US"), $"Entrants: {Entrants}").AppendLine();
                if (EntrantsPaid > 0)
                    sb.AppendFormat(new CultureInfo("en-US"), $"Entrants Paid: {EntrantsPaid}").AppendLine();

                return sb.ToString().Trim();
            }
        }
        public int Entrants { get; set; } = 0;
        public int EntrantsPaid { get; set; } = 0;
        public string FavoriteMenuString => IsFavorite ? "Remove from Favorites" : "Add to Favorites";
        public string FontColor => PreferencesHelper.FindPreference("LiveTracker", "Window", "FontColor");
        public string FontSize => PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize");
        public List<string> Formats { get; set; }
        public string FormatString
        {
            get
            {
                var sb = new StringBuilder();

                // build a string with each format separated by a comma
                foreach (var format in Formats)
                    sb.Append(format.Trim()).Append(',');

                // remove the extra comma off the end of the last format
                return sb.ToString().TrimEnd(',');
            }
        }
        public string GameType { get; set; } = "";
        public decimal Guarantee { get; set; } = 0;
        public bool IsBovadaBounty { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsSng { get; set; }
        public int LateReg { get; set; } = 0;
        public List<int> NumberOfTemplatesRemoved { get; set; }
        public decimal RebuyBaseCost { get; set; } = 0;
        public decimal RebuyTotalCost => RebuyBaseCost + RebuyRakeCost;
        public decimal RebuyRakeCost { get; set; } = 0;
        public List<int> RemovedFromSessionIds { get; set; }
        public string SngPayouts { get; set; }
        public int StackSizeAddon { get; set; } = 0;
        public int StackSizeRebuy { get; set; } = 0;
        public int StackSizeStarting { get; set; } = 0;
        public DateTime StartTime
        {
            get => _startTime;
            set
            {
                // remove seconds anytime start time is changed
                _startTime = new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, 0);
                RaisePropertyChanged(nameof(StartTime));
            }
        }
        public int TableSize { get; set; } = 0;
        public int TemplateId { get; set; }
        public string TemplateIdString => $"Template {TemplateId}";
        public string TimeZoneName { get; set; } = "";
        public string TournamentName { get; set;} = "";
        public string Venue { get; set; } = "";
        public ICommand Command {get; set; }
        public static bool Equals(TournamentTemplate x, TournamentTemplate y)
        {
            if (x is null || y is null) return false;

            return x.AddonBaseCost == y.AddonBaseCost && x.AddonRakeCost == y.AddonRakeCost && x.BlindLevels == y.BlindLevels && x.Bounty == y.Bounty &&
                   x.BuyinBaseCost == y.BuyinBaseCost && x.BuyinRakeCost == y.BuyinRakeCost && x.Entrants == y.Entrants && x.EntrantsPaid == y.EntrantsPaid &&
                   string.Equals(x.FormatString.Trim(), y.FormatString.Trim(), StringComparison.CurrentCultureIgnoreCase) && x.IsBovadaBounty == y.IsBovadaBounty &&
                   x.IsSng == y.IsSng && string.Equals(x.GameType.Trim(), y.GameType.Trim(), StringComparison.CurrentCultureIgnoreCase) &&
                   x.Guarantee == y.Guarantee && x.LateReg == y.LateReg && x.RebuyBaseCost == y.RebuyBaseCost && x.RebuyRakeCost == y.RebuyRakeCost && x.StackSizeAddon == y.StackSizeAddon &&
                   x.StackSizeRebuy == y.StackSizeRebuy && x.StackSizeStarting == y.StackSizeStarting && x.StartTime.Hour == y.StartTime.Hour && x.StartTime.Minute == y.StartTime.Minute && x.TableSize == y.TableSize &&
                   string.Equals(x.TournamentName.Trim(), y.TournamentName.Trim(), StringComparison.CurrentCultureIgnoreCase) &&
                   string.Equals(x.Venue.Trim(), y.Venue.Trim(), StringComparison.CurrentCultureIgnoreCase);
        }

        public int GetHashCode(TournamentTemplate obj)
        {
            var hashCode = new HashCode();
            hashCode.Add(obj.AddonBaseCost);
            hashCode.Add(obj.AddonRakeCost);
            hashCode.Add(obj.BlindLevels);
            hashCode.Add(obj.BuyinBaseCost);
            hashCode.Add(obj.BuyinRakeCost);
            hashCode.Add(obj.Entrants);
            hashCode.Add(obj.EntrantsPaid);
            hashCode.Add(obj.GameType);
            hashCode.Add(obj.Guarantee);
            hashCode.Add(obj.LateReg);
            hashCode.Add(obj.RebuyBaseCost);
            hashCode.Add(obj.RebuyRakeCost);
            hashCode.Add(obj.StackSizeAddon);
            hashCode.Add(obj.StackSizeRebuy);
            hashCode.Add(obj.StackSizeStarting);
            hashCode.Add(obj.TableSize);
            hashCode.Add(obj.TournamentName);
            hashCode.Add(obj.Venue);
            return hashCode.ToHashCode();
        }

        /// <summary>
        /// Load formats from a string into the formats collection
        /// </summary>
        /// <param name="formatsString"></param>
        public void LoadFormats(string formatsString)
        {
            if (formatsString is null)
                return;

            foreach (var format in formatsString.Split(','))
                if (format.Trim().Length > 0)
                    Formats.Add(format);

            Formats = new List<string>(Formats.OrderBy(i => i));
        }
    }
}
