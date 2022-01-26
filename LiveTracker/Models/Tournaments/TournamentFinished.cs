using System;
using System.Globalization;
using System.Text;
using System.Windows.Input;

namespace LiveTracker.Models
{
    public class TournamentFinished : TournamentRunning
    {
        public TournamentFinished() { }

        public TournamentFinished(TournamentRunning tournament)
        {
            AddonBaseCost = tournament.AddonBaseCost;
            AddonCount = tournament.AddonCount;
            AddonRakeCost = tournament.AddonRakeCost;
            BlindLevels = tournament.BlindLevels;
            Bounty = tournament.Bounty;
            BountyCount = tournament.BountyCount;
            BuyinBaseCost = tournament.BuyinBaseCost;
            BuyinRakeCost = tournament.BuyinRakeCost;
            EndTime = tournament.EndTime;
            Entrants = tournament.Entrants;
            EntrantsPaid = tournament.EntrantsPaid;
            FinishPosition = tournament.FinishPosition;
            Formats = tournament.Formats;
            GameType = tournament.GameType;
            Guarantee = tournament.Guarantee;
            IsSng = tournament.IsSng;
            JackpotSpinMultiplier = tournament.JackpotSpinMultiplier;
            LateReg = tournament.LateReg;
            Note = tournament.Note;
            PrizeWon = tournament.PrizeWon;
            RebuyBaseCost = tournament.RebuyBaseCost;
            RebuyCount = tournament.RebuyCount;
            RebuyRakeCost = tournament.RebuyRakeCost;
            ScreenshotFilename = tournament.ScreenshotFilename;
            SngPayouts = tournament.SngPayouts;
            StartTime = tournament.StartTime;
            StackSizeAddon = tournament.StackSizeAddon;
            StackSizeRebuy = tournament.StackSizeRebuy;
            StackSizeStarting = tournament.StackSizeStarting;
            TableSize = tournament.TableSize;
            TemplateId = tournament.TemplateId;
            TimeZoneName = tournament.TimeZoneName;
            TournamentName = tournament.TournamentName;
            Venue = tournament.Venue;
        }

        public DateTime EndDate => EndTime;
        public string GraphDescription
        {
            get
            {
                var sb = new StringBuilder();

                sb.AppendFormat(new CultureInfo("en-US"), "Name: {0}", TournamentName).AppendLine();
                sb.AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Game Type: {0}", GameType).AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Formats: {0}", FormatString).AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Site: {0}", Venue).AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Guarantee: {0:C2}", Guarantee).AppendLine();
                sb.AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Start Time: {0:h:mm tt}", StartTime).AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Start Date: {0:M/dd/y}", StartTime).AppendLine();
                sb.AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "End Time: {0:h:mm tt}", EndTime).AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "End Date: {0:M/dd/y}", EndTime).AppendLine();
                sb.AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Length: {0:h\\:mm}", Length).AppendLine();
                sb.AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Buyin: {0:C2}", BuyinTotalCost).AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Rebuy: {0:C2}", RebuyTotalCost).AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Addon: {0:C2}", AddonTotalCost).AppendLine();
                sb.AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Rebuy Count: {0}", RebuyCount).AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Addon Count: {0}", AddonCount).AppendLine();
                if (JackpotSpinMultiplier > 0) sb.AppendFormat(new CultureInfo("en-US"), "Jackpot Spin Multiplier: {0}", JackpotSpinMultiplier).AppendLine();
                sb.AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Prize Won: {0:C2}", PrizeWon).AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Total Cost: {0:C2}", TotalCost).AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Total Profit: {0:C2}", Profit).AppendLine();
                sb.AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Entrants: {0}", Entrants).AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Entrants Paid: {0}", EntrantsPaid).AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Finish Position: {0}", FinishPosition).AppendLine();
                sb.AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Table Size: {0}", TableSize).AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Late Reg: {0} minutes", LateReg).AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Blind Levels: {0} minutes", BlindLevels).AppendLine();
                sb.AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Starting Stack: {0}", StackSizeStarting).AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Rebuy Stack: {0}", StackSizeRebuy).AppendLine();
                sb.AppendFormat(new CultureInfo("en-US"), "Addon Stack: {0}", StackSizeAddon).AppendLine();

                return sb.ToString().Trim();
            }
        }
        public string ImportDisplayString
        {
            get
            {
                return $"{Venue} - {StartTime.ToShortDateString()} - {TournamentName}";
            }
        }
        public TimeSpan Length => new TimeSpan((EndTime - StartTime).Hours, (EndTime - StartTime).Minutes, (EndTime - StartTime).Seconds);
        public decimal PercentFieldBeaten
        {
            get
            {
                if (FinishPosition is 0 || Entrants is 0)
                    return 0;

                return 1 - (FinishPosition / (decimal)Entrants);
            }
        }
        public decimal Profit => PrizeWon - TotalCost;
        public DateTime StartDate => StartTime;
        public decimal TotalRakePaid => BuyinRakeCost + (RebuyRakeCost * RebuyCount) + (AddonRakeCost * AddonCount);
        public ICommand CommandViewScreenshot { get; set; }

        public static bool Equals(TournamentFinished x, TournamentFinished y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.AddonCount == y.AddonCount && x.EndTime.Hour.Equals(y.EndTime.Hour) && x.EndTime.Minute.Equals(y.EndTime.Minute) &&
                   x.FinishPosition == y.FinishPosition && x.PrizeWon == y.PrizeWon && x.RebuyCount == y.RebuyCount &&
                   x.AddonBaseCost == y.AddonBaseCost && x.AddonRakeCost == y.AddonRakeCost && x.BlindLevels == y.BlindLevels &&
                   x.BuyinBaseCost == y.BuyinBaseCost && x.BuyinRakeCost == y.BuyinRakeCost && x.Entrants == y.Entrants && x.EntrantsPaid == y.EntrantsPaid &&
                   string.Equals(x.FormatString.Trim(), y.FormatString.Trim(), StringComparison.CurrentCultureIgnoreCase) &&
                   string.Equals(x.GameType.Trim(), y.GameType.Trim(), StringComparison.CurrentCultureIgnoreCase) && x.Guarantee == y.Guarantee && x.LateReg == y.LateReg &&
                   x.RebuyBaseCost == y.RebuyBaseCost && x.RebuyRakeCost == y.RebuyRakeCost && x.SngPayouts == y.SngPayouts && x.StackSizeAddon == y.StackSizeAddon &&
                   x.StackSizeRebuy == y.StackSizeRebuy && x.StackSizeStarting == y.StackSizeStarting &&
                   x.StartTime.Hour == y.StartTime.Hour && x.StartTime.Minute == y.StartTime.Minute && x.TableSize == y.TableSize &&
                   string.Equals(x.TimeZoneName.Trim(), y.TimeZoneName.Trim(), StringComparison.CurrentCultureIgnoreCase) &&
                   x.JackpotSpinMultiplier == y.JackpotSpinMultiplier && x.IsSng == y.IsSng && x.Bounty == y.Bounty && x.BountyCount == y.BountyCount &&
                   string.Equals(x.TournamentName.Trim(), y.TournamentName.Trim(), StringComparison.CurrentCultureIgnoreCase) &&
                   string.Equals(x.Venue.Trim(), y.Venue.Trim(), StringComparison.CurrentCultureIgnoreCase);
        }
        public int GetHashCode(TournamentFinished obj)
        {
            var hashCode = new HashCode();
            hashCode.Add(obj.AddonBaseCost);
            hashCode.Add(obj.AddonCount);
            hashCode.Add(obj.AddonRakeCost);
            hashCode.Add(obj.BlindLevels);
            hashCode.Add(obj.BuyinBaseCost);
            hashCode.Add(obj.BuyinRakeCost);
            hashCode.Add(obj.EndTime);
            hashCode.Add(obj.Entrants);
            hashCode.Add(obj.EntrantsPaid);
            hashCode.Add(obj.FinishPosition);
            hashCode.Add(obj.GameType);
            hashCode.Add(obj.Guarantee);
            hashCode.Add(obj.LateReg);
            hashCode.Add(obj.PrizeWon);
            hashCode.Add(obj.RebuyBaseCost);
            hashCode.Add(obj.RebuyCount);
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
        /// Update tournaments data
        /// </summary>
        public void UpdateData(TournamentFinished tournament)
        {
            AddonBaseCost = tournament.AddonBaseCost;
            AddonCount = tournament.AddonCount;
            AddonRakeCost = tournament.AddonRakeCost;
            BlindLevels = tournament.BlindLevels;
            Bounty = tournament.Bounty;
            BountyCount = tournament.BountyCount;
            BuyinBaseCost = tournament.BuyinBaseCost;
            BuyinRakeCost = tournament.BuyinRakeCost;
            EndTime = tournament.EndTime;
            Entrants = tournament.Entrants;
            EntrantsPaid = tournament.EntrantsPaid;
            FinishPosition = tournament.FinishPosition;
            Formats = tournament.Formats;
            GameType = tournament.GameType;
            Guarantee = tournament.Guarantee;
            JackpotSpinMultiplier = tournament.JackpotSpinMultiplier;
            IsBovadaBounty = tournament.IsBovadaBounty;
            IsFavorite = tournament.IsFavorite;
            IsSng = tournament.IsSng;
            LateReg = tournament.LateReg;
            Note = tournament.Note;
            PrizeWon = tournament.PrizeWon;
            RebuyBaseCost = tournament.RebuyBaseCost;
            RebuyCount = tournament.RebuyCount;
            RebuyRakeCost = tournament.RebuyRakeCost;
            ScreenshotFilename = tournament.ScreenshotFilename;
            SngPayouts = tournament.SngPayouts;
            StartTime = tournament.StartTime;
            StackSizeAddon = tournament.StackSizeAddon;
            StackSizeRebuy = tournament.StackSizeRebuy;
            StackSizeStarting = tournament.StackSizeStarting;
            TableSize = tournament.TableSize;
            TemplateId = tournament.TemplateId;
            TimeZoneName = tournament.TimeZoneName;
            TournamentName = tournament.TournamentName;
            Venue = tournament.Venue;

            SetInitialTournamentStatus();
        }
    }
}
