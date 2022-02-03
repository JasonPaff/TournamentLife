using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using Tournament_Life.Models.Tournaments;

namespace Tournament_Life.Models.Sessions
{
    public class SessionModel
    {
        public decimal AddonCostTotal =>
            Tournaments.Count is 0 ? 0 : Tournaments.Sum(i => i.AddonTotalCost * i.AddonCount);

        public decimal AverageAddonCostTotal =>
            TournamentCount is 0 || AddonCostTotal is 0 ? 0 : AddonCostTotal / TournamentCount;

        public decimal AverageBuyinCostTotal =>
            TournamentCount is 0 || BuyinCostTotal is 0 ? 0 : BuyinCostTotal / TournamentCount;

        public TimeSpan AverageLength => TournamentCount is 0
            ? TimeSpan.Zero
            : new TimeSpan((long) Tournaments.Average(i => i.Length.Ticks));

        public decimal AveragePercentBeaten
        {
            get
            {
                if (TournamentCount is 0 || Tournaments.Sum(i => i.PercentFieldBeaten * 100) is 0)
                    return 0;

                return (Tournaments.Sum(i => i.PercentFieldBeaten * 100) / TournamentCount) / 100;
            }
        }

        public decimal AveragePrizeWon
        {
            get
            {
                if (PrizesWonTotal is 0 || TournamentCount is 0)
                    return 0;
        
                return PrizesWonTotal / TournamentCount;
            }
        }

        public decimal AveragePrizesWonTotal
        {
            get
            {
                if (PrizesWonTotal is 0 || TournamentCount is 0)
                    return 0;

                return PrizesWonTotal / TournamentCount;
            }
        }

        public decimal AverageProfitWon
        {
            get
            {
                if (TotalProfit is 0 || TournamentCount is 0)
                    return 0;

                return TotalProfit / TournamentCount;
            }
        }

        public decimal AverageRebuyCostTotal
        {
            get
            {
                if (RebuyCostTotal is 0 || TournamentCount is 0)
                    return 0;

                return RebuyCostTotal / TournamentCount;
            }
        }

        public decimal AverageTotalCost
        {
            get
            {
                if (TotalCost is 0 || TournamentCount is 0)
                    return 0;

                return TotalCost / TournamentCount;
            }
        }

        public decimal AverageTotalProfit
        {
            get
            {
                if (TotalProfit is 0 || TournamentCount is 0)
                    return 0;

                return TotalProfit / TournamentCount;
            }
        }


        public decimal BuyinCostTotal
        {
            get
            {
                if (Tournaments.Count <= 0)
                    return 0;

                return Tournaments.Sum(i => i.BuyinTotalCost);
            }
        }

        public decimal EarlyMidPercentFinish
        {
            get
            {
                return
                    Tournaments.Where(i => i.PercentFieldBeaten * 100 >= 10 && i.PercentFieldBeaten * 100 < 30).Any() &&
                    Tournaments.Count > 0
                        ? Tournaments.Where(i => i.PercentFieldBeaten * 100 >= 10 && i.PercentFieldBeaten * 100 < 30)
                            .Count() / (decimal) Tournaments.Count
                        : 0;
            }
        }

        public decimal EarlyPercentFinish
        {
            get
            {
                return
                    Tournaments.Where(i => i.PercentFieldBeaten * 100 >= 0 && i.PercentFieldBeaten * 100 < 10).Any() &&
                    Tournaments.Count > 0
                        ? Tournaments.Where(i => i.PercentFieldBeaten * 100 >= 0 && i.PercentFieldBeaten * 100 < 10)
                            .Count() / (decimal) Tournaments.Count
                        : 0;
            }
        }

        public DateTime EndTime
        {
            get
            {
                if (Tournaments.Count > 0)
                    return Tournaments.Max(i => i.EndTime);
                else
                    return DateTime.MaxValue;
            }
        }

        public int FinalTables
        {
            get
            {
                return Tournaments.Select(x => x).Where(x => x.FinishPosition > 0 && x.FinishPosition <= x.TableSize)
                    .Count();
            }
        }

        public string GraphDescription
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("Tournament Count: " + TournamentCount);

                sb.AppendLine(String.Format(new CultureInfo("en-US"), "\nStart Date: {0:M/dd/y}", StartTime));
                sb.AppendLine(String.Format(new CultureInfo("en-US"), "Start Time: {0:h:mm tt}", StartTime));
                sb.AppendLine();
                sb.AppendLine(String.Format(new CultureInfo("en-US"), "End Date: {0:M/dd/y}", EndTime));
                sb.AppendLine(String.Format(new CultureInfo("en-US"), "End Time: {0:h:mm tt}", EndTime));
                sb.AppendLine();
                sb.AppendLine(String.Format(new CultureInfo("en-US"), "Session Length: {0:h\\:mm}", SessionLength));
                sb.AppendLine(String.Format(new CultureInfo("en-US"), "Average Tournament Length: {0:h\\:mm}",
                    AverageLength));
                sb.AppendLine();
                sb.AppendLine("ROI: " + Roi.ToString("P", new CultureInfo("en-US")));
                sb.AppendLine("ITM: " + Itm.ToString("P", new CultureInfo("en-US")));
                sb.AppendLine();
                sb.AppendLine("Total Profit: " + TotalProfit.ToString("C2", new CultureInfo("en-US")));
                sb.AppendLine("Total Cost: " + TotalCost.ToString("C2", new CultureInfo("en-US")));
                sb.AppendLine("Total Prizes: " + PrizesWonTotal.ToString("C2", new CultureInfo("en-US")));
                sb.AppendLine();
                sb.AppendLine("Average Profit: " + AverageTotalProfit.ToString("C2", new CultureInfo("en-US")));
                sb.AppendLine("Average Cost: " + AverageTotalCost.ToString("C2", new CultureInfo("en-US")));
                sb.AppendLine("Average Prizes: " + AveragePrizesWonTotal.ToString("C2", new CultureInfo("en-US")));
                sb.AppendLine();
                sb.AppendLine("Late Finish: " + LatePercentFinish.ToString("P", new CultureInfo("en-US")));
                sb.AppendLine("Mid/Late Finish: " + MidLatePercentFinish.ToString("P", new CultureInfo("en-US")));
                sb.AppendLine("Mid Finish: " + MidPercentFinish.ToString("P", new CultureInfo("en-US")));
                sb.AppendLine("Early/Mid Finish: " + EarlyMidPercentFinish.ToString("P", new CultureInfo("en-US")));
                sb.AppendLine("Early Finish: " + EarlyPercentFinish.ToString("P", new CultureInfo("en-US")));
                sb.AppendLine("Average Finish: " + AveragePercentBeaten.ToString("P", new CultureInfo("en-US")));
                sb.AppendLine();
                sb.AppendLine("Final Tables: " + FinalTables);
                sb.AppendLine("Min-Cashes: " + MinCashes);
                sb.AppendLine("Bubbles: " + NearBubbles);
                sb.AppendLine("Stone Bubbles: " + StoneBubbles);

                return sb.ToString().TrimEnd();
            }
        }

        public int ID { get; set; }

        public decimal Itm
        {
            get
            {
                if (TournamentCount is 0 ||
                    Tournaments.Count(x => x.PrizeWon > 0 && x.FinishPosition <= x.EntrantsPaid) is 0) return 0;

                return Tournaments.Count(x => x.PrizeWon > 0 && x.FinishPosition <= x.EntrantsPaid) /
                       (decimal) TournamentCount;
            }
        }

        public decimal LatePercentFinish
        {
            get
            {
                return Tournaments.Any(i => i.PercentFieldBeaten * 100 >= 90 && i.PercentFieldBeaten * 100 < 100) &&
                       Tournaments.Count > 0
                    ? Tournaments.Count(i => i.PercentFieldBeaten * 100 >= 90 && i.PercentFieldBeaten * 100 < 100) /
                      (decimal) Tournaments.Count
                    : 0;
            }
        }

        public int MinCashes
        {
            get
            {
                return Tournaments.Count(x =>
                    x.FinishPosition >= x.EntrantsPaid - Math.Round(x.EntrantsPaid * ((double) 5 / 100)) &&
                    x.FinishPosition <= x.EntrantsPaid);
            }
        }

        public decimal MidLatePercentFinish
        {
            get
            {
                return
                    Tournaments.Where(i => i.PercentFieldBeaten * 100 >= 70 && i.PercentFieldBeaten * 100 < 90).Any() &&
                    Tournaments.Count > 0
                        ? Tournaments.Where(i => i.PercentFieldBeaten * 100 >= 70 && i.PercentFieldBeaten * 100 < 90)
                            .Count() / (decimal) Tournaments.Count
                        : 0;
            }
        }

        public decimal MidPercentFinish
        {
            get
            {
                return
                    Tournaments.Where(i => i.PercentFieldBeaten * 100 >= 30 && i.PercentFieldBeaten * 100 < 70).Any() &&
                    Tournaments.Count > 0
                        ? Tournaments.Where(i => i.PercentFieldBeaten * 100 >= 30 && i.PercentFieldBeaten * 100 < 70)
                            .Count() / (decimal) Tournaments.Count
                        : 0;
            }
        }

        public int NearBubbles
        {
            get
            {
                return Tournaments.Count(x =>
                    x.FinishPosition >= x.EntrantsPaid + 1 && x.FinishPosition <=
                    x.EntrantsPaid + 1 + Math.Round(x.EntrantsPaid * ((double) 1 / 100)));
            }
        }

        public decimal PrizesWonTotal
        {
            get
            {
                if (Tournaments.Count is 0)
                    return 0;

                return Tournaments.Sum(i => i.PrizeWon);
            }
        }

        public decimal ProfitPerTournament =>
            TotalProfit is 0 || TournamentCount is 0 ? 0 : TotalProfit / TournamentCount;

        public decimal RebuyCostTotal
        {
            get
            {
                if (Tournaments.Count <= 0)
                    return 0;

                return Tournaments.Sum(i => i.RebuyTotalCost * i.RebuyCount);
            }
        }

        public decimal Roi
        {
            get
            {
                if (TotalProfit is 0 || TotalCost is 0)
                    return 0;

                return (TotalProfit / TotalCost);
            }
        }

        public ObservableCollection<TournamentFinished> Tournaments { get; private set; } =
            new ObservableCollection<TournamentFinished>();

        public TimeSpan SessionLength
        {
            get
            {
                if (EndTime < StartTime)
                    return TimeSpan.Zero;

                return new TimeSpan((EndTime - StartTime).Hours, (EndTime - StartTime).Minutes, 0);
            }
        }

        public DateTime StartTime => Tournaments.Count <= 0 ? DateTime.MinValue : Tournaments.Min(i => i.StartTime);

        public int StoneBubbles
        {
            get { return Tournaments.Select(x => x).Count(x => x.FinishPosition == x.EntrantsPaid + 1); }
        }

        public int TournamentCount => Tournaments.Count;
        public decimal TotalCost => BuyinCostTotal + RebuyCostTotal + AddonCostTotal;

        public decimal TotalProfit => PrizesWonTotal - TotalCost;

        public void AddTournament(TournamentFinished tournament)
        {
            Tournaments.Add(tournament);
        }
    }
}