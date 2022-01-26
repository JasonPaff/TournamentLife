using LiveTracker.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using LiveTracker.Models.Sessions;

namespace LiveTracker.Models.Tournaments
{
    public class TournamentStats
    {
        private readonly ObservableCollection<TournamentFinished> tournaments;

        public TournamentStats(ObservableCollection<TournamentFinished> tournaments)
        {
            if (tournaments is null) return;

            this.tournaments = tournaments;
            InitStatsCounting();
            InitStatsCost();
            InitStatsDateTime();
            InitStatsFinish();
            InitStatsPosition();
            CalculateHourlyAndMinMaxTournaments();
        }

        public decimal AverageBuyin { get; set; }
        public decimal AverageCost { get; set; }
        public int AverageEntrants { get; set; }
        public int AverageFinishPosition { get; set; }
        public TimeSpan AverageLength { get; set; }
        public decimal AveragePercentBeaten { get; set; }
        public decimal AveragePrizeWon { get; set; }
        public decimal AverageProfit { get; set; }
        public decimal AverageRebuys { get; set; }
        public decimal AverageTotal { get; set; }
        public int AverageTournaments { get; set; }
        public int BreakevenDays { get; set; }
        public int CashingStreak { get; set; }
        public DateTime EarliestStartDate { get; set; }
        public decimal EarlyMidFinish { get; set; }
        public decimal EarlyFinish { get; set; }
        public decimal FinalTablePercentage
        {
            get
            {
                if (FinalTables is 0 || TournamentsPlayed is 0) return 0;

                return FinalTables / (decimal)TournamentsPlayed;
            }
        }
        public int FinalTableBubbles { get ; set; }
        public int FinalTables { get; set; }
        public decimal FinalTableWinningPercentage
        {
            get
            {
                if (TotalWins is 0 || FinalTables is 0) return 0;

                return TotalWins / (decimal)FinalTables;
            }
        }
        public decimal HighestBuyin { get; set; }
        public decimal HighestCost { get; set; }
        public int HighestEntrants { get; set; }
        public int HighestFinishPosition { get; set; }
        public decimal HighestPrizeWon { get; set; }
        public decimal HighestProfit { get; set; }
        public int HighestRebuys { get; set; }
        public decimal Hourly { get; set; }
        public decimal Itm { get; set; }
        public decimal LateFinish { get; set; }
        public DateTime LatestStartDate { get; set; }
        public TimeSpan LongestLength { get; set; }
        public int LosingDays { get; set; }
        public int LosingStreak { get; set; }
        public decimal LowestBuyin { get; set; }
        public decimal LowestCost { get; set; }
        public int LowestEntrants { get; set; }
        public int LowestFinishPosition { get; set; }
        public decimal LowestPrizeWon { get; set; }
        public decimal LowestProfit { get; set; }
        public int LowestRebuys { get; set; }
        public int MaxTournaments { get; set; }
        public decimal MidFinish { get; set; }
        public decimal MidLateFinish { get; set; }
        public int MinCashes { get; set; }
        public int MinTournaments { get; set; }
        public int NearBubbles { get; set; }
        public decimal ProfitPerTournament
        {
            get
            {
                if (TotalProfit is 0 || TournamentsPlayed is 0) return 0;

                return TotalProfit / TournamentsPlayed;
            }
        }
        public decimal RakePaid { get; set; }
        public decimal Roi
        {
            get
            {
                if (TotalProfit is 0 || TotalCost is 0) return 0;

                return TotalProfit / TotalCost;
            }
        }
        public TimeSpan ShortestLength { get; set; }
        public int StoneBubbles { get; set; }
        public TimeSpan TimePlayed { get; set; }
        public decimal TotalAddonCost { get; set; }
        public decimal TotalBuyinCost { get; set; }
        public decimal TotalCost
        {
            get
            {
                return TotalBuyinCost + TotalAddonCost + TotalRebuyCost;
            }
        }
        public int TotalDays { get; set; }
        public decimal TotalPrizeWon { get; set; }
        public decimal TotalProfit { get; set; }
        public decimal TotalRebuyCost { get; set; }
        public TimeSpan TotalTime { get; set; }
        public int TotalWins { get; set; }
        public int TournamentsPlayed { get; set; }
        public int WinningDays { get; set; }
        public decimal WinningPercentage
        {
            get
            {
                if (TotalWins is 0 || TournamentsPlayed is 0) return 0;

                return TotalWins / (decimal)TournamentsPlayed;
            }
        }
        public int WinningStreak { get; set; }

        /// <summary>
        ///  get hourly
        /// </summary>
        private void CalculateHourlyAndMinMaxTournaments()
        {
            // sort tournaments into sessions
            ObservableCollection<SessionModel> sessions = SessionTemplateHelper.SortTournamentsIntoSessions(tournaments);

            if (sessions is null || sessions.Count is 0) return;

            // get hourly rate from sessions
            Hourly = SessionStats.CalculateHourly(sessions);

            // get min/max/average tournament counts for a single day
            MaxTournaments = sessions.Select(i => i.TournamentCount).Any() ? sessions.Max(i => i.TournamentCount) : 0;
            MinTournaments = sessions.Select(i => i.TournamentCount).Any() ? sessions.Min(i => i.TournamentCount) : 0;
            AverageTournaments = sessions.Any() ? (int)sessions.Average(i => i.TournamentCount) : 0;
        }

        /// <summary>
        /// init stats
        /// </summary>
        private void InitStatsCost()
        {
            TotalBuyinCost = tournaments.Any() ? tournaments.Sum(i => i.BuyinTotalCost) : 0;
            HighestBuyin = tournaments.Any() ? tournaments.Max(i => i.BuyinTotalCost) : 0;
            LowestBuyin = tournaments.Any(i => i.BuyinTotalCost > 0) ? tournaments.Where(i => i.BuyinTotalCost > 0).Min(i => i.BuyinTotalCost) : 0;
            AverageBuyin = tournaments.Any() ? tournaments.Average(i => i.BuyinTotalCost) : 0;

            HighestCost = tournaments.Any() ? tournaments.Max(i => i.TotalCost) : 0;
            LowestCost = tournaments.Any(i => i.BuyinTotalCost > 0) ? tournaments.Where(i => i.TotalCost > 0).Min(i => i.TotalCost) : 0;
            AverageCost = tournaments.Any() ? tournaments.Average(i => i.TotalCost) : 0;

            TotalPrizeWon = tournaments.Any() ? tournaments.Sum(i => i.PrizeWon) : 0;
            HighestPrizeWon = tournaments.Any() ? tournaments.Max(i => i.PrizeWon) : 0;
            LowestPrizeWon = tournaments.Any(i => i.PrizeWon > 0) ? tournaments.Where(i => i.PrizeWon > 0).Min(i => i.PrizeWon) : 0;
            AveragePrizeWon = tournaments.Any() ? tournaments.Average(i => i.PrizeWon) : 0;

            TotalProfit = tournaments.Any() ? tournaments.Sum(i => i.Profit) : 0;
            HighestProfit = tournaments.Any() ? tournaments.Max(i => i.Profit) : 0;
            LowestProfit = tournaments.Any(i => i.Profit > 0) ? tournaments.Where(i => i.Profit > 0).Min(i => i.Profit) : 0;
            AverageProfit = tournaments.Any() ? tournaments.Average(i => i.Profit) : 0;

            TotalRebuyCost = tournaments.Any() ? tournaments.Sum(i => i.RebuyTotalCost * i.RebuyCount) : 0;
            TotalAddonCost = tournaments.Any() ? tournaments.Sum(i => i.AddonTotalCost * i.AddonCount) : 0;

            AverageTotal = tournaments.Any() ? tournaments.Average(i => i.TotalCost) : 0;

            RakePaid = tournaments.Any() ? tournaments.Sum(i => i.TotalRakePaid) : 0;
        }

        /// <summary>
        /// init stats
        /// </summary>
        private void InitStatsCounting()
        {
            TournamentsPlayed = tournaments.Count;

            LowestRebuys = tournaments.Any(i => i.RebuyCount > 0) ? LowestRebuys = tournaments.Where(i => i.RebuyCount > 0).Min(i => i.RebuyCount) : 0;
            HighestRebuys = tournaments.Any(i => i.RebuyCount > 0) ? tournaments.Max(i => i.RebuyCount) : 0;
            AverageRebuys = tournaments.Any(i => i.StackSizeRebuy > 0) ? (decimal)tournaments.Where(i => i.StackSizeRebuy > 0).Average(i => i.RebuyCount) : 0;

            HighestEntrants = tournaments.Any(i => i.Entrants > 0) ? tournaments.Max(i => i.Entrants) : 0;
            LowestEntrants = tournaments.Any(i => i.Entrants > 0) ? tournaments.Where(i => i.Entrants > 0).Min(i => i.Entrants) : 0;
            AverageEntrants = tournaments.Any(i => i.Entrants > 0) ? (int)tournaments.Average(i => i.Entrants) : 0;

            TotalWins = tournaments.Any(i => i.FinishPosition is 1) ? tournaments.Count(i => i.FinishPosition is 1) : 0;

            FinalTables = tournaments.Any(i => i.FinishPosition <= i.TableSize && i.FinishPosition > 0) ? tournaments.Count(i => i.FinishPosition <= i.TableSize && i.FinishPosition > 0) : 0;

            var stoneBubble = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "StoneBubble"));
            StoneBubbles = tournaments.Any(i => i.FinishPosition == i.EntrantsPaid + stoneBubble) ? tournaments.Count(i => i.FinishPosition == i.EntrantsPaid + stoneBubble) : 0;

            FinalTableBubbles = tournaments.Any(i => i.FinishPosition == (i.TableSize + 1) && i.FinishPosition > 0) ? tournaments.Count(i => i.FinishPosition == (i.TableSize + 1) && i.FinishPosition > 0) : 0;

            int itmStreakCounter = 0;
            int losingStreakCounter = 0;
            int winningStreakCounter = 0;

            foreach (TournamentFinished tournament in tournaments)
            {
                if (tournament.PrizeWon > 0)
                {
                    itmStreakCounter++;
                    losingStreakCounter = 0;
                }
                else if (tournament.PrizeWon <= 0)
                {
                    itmStreakCounter = 0;
                    losingStreakCounter++;
                }

                if (tournament.FinishPosition is 1) winningStreakCounter++;
                else winningStreakCounter = 0;

                if (winningStreakCounter > WinningStreak) WinningStreak = winningStreakCounter;

                if (losingStreakCounter > LosingStreak) LosingStreak = losingStreakCounter;

                if (itmStreakCounter > CashingStreak) CashingStreak = itmStreakCounter;
            }
        }

        /// <summary>
        /// init stats
        /// </summary>
        private void InitStatsDateTime()
        {
            LongestLength = tournaments.Any() ? tournaments.Max(i => i.Length) : TimeSpan.Zero;
            ShortestLength = tournaments.Any() ? tournaments.Where(i => i.Length != TimeSpan.Zero).Min(i => i.Length) : TimeSpan.Zero;
            LatestStartDate = tournaments.Any() ? tournaments.Max(i => i.StartTime) : DateTime.MaxValue;
            EarliestStartDate = tournaments.Any() ? tournaments.Min(i => i.StartTime) : DateTime.MinValue;

            AverageLength = tournaments.Any() ? TimeSpan.FromMinutes(tournaments.Select(i => i.Length).Average(timespan => timespan.TotalMinutes)) : TimeSpan.Zero;
            AverageLength = new TimeSpan(AverageLength.Hours, AverageLength.Minutes, 0); // remove seconds

            // get each individual date played
            var daysPlayed = tournaments.Any() ? tournaments.Select(i => i.StartTime).GroupBy(x => x.Date).Select(x => new { Date = x.Key, Values = x.Distinct()}) : null;

            // get total number of individual dates played
            if (daysPlayed is not null) TotalDays = daysPlayed.Count();

            // make sure at least one day was played
            if (daysPlayed is not null)
            {
                // loop through each individual date
                foreach(var day in daysPlayed)
                {
                    var prizesWon = 0.0m;
                    var totalCost = 0.0m;

                    // loop through the tournaments
                    foreach(var tournament in tournaments)
                    {
                        // check for matching date
                        if (tournament.StartDate.Month == day.Date.Month && tournament.StartDate.Day == day.Date.Day && tournament.StartDate.Year == day.Date.Year)
                        {
                            // add prize won to counter
                            prizesWon += tournament.PrizeWon;

                            // add total cost to counter
                            totalCost += tournament.TotalCost;
                        }
                    }

                    // winning day
                    if (prizesWon > totalCost) WinningDays++;

                    // losing day
                    if (prizesWon < totalCost) LosingDays++;

                    // break-even day
                    if (prizesWon == totalCost) BreakevenDays++;
                }
            }
        }

        /// <summary>
        /// init stats
        /// </summary>
        private void InitStatsFinish()
        {
            LateFinish = tournaments.Any(i => i.PercentFieldBeaten * 100 >= 90 && i.PercentFieldBeaten * 100 < 100) && tournaments.Count > 0 ? tournaments.Count(i => i.PercentFieldBeaten * 100 >= 90 && i.PercentFieldBeaten * 100 < 100) / (decimal)tournaments.Count : 0;
            MidLateFinish = tournaments.Any(i => i.PercentFieldBeaten * 100 >= 70 && i.PercentFieldBeaten * 100 < 90) && tournaments.Count > 0 ? tournaments.Count(i => i.PercentFieldBeaten * 100 >= 70 && i.PercentFieldBeaten * 100 < 90) / (decimal)tournaments.Count : 0;
            MidFinish = tournaments.Any(i => i.PercentFieldBeaten * 100 >= 30 && i.PercentFieldBeaten * 100 < 70) && tournaments.Count > 0 ? tournaments.Count(i => i.PercentFieldBeaten * 100 >= 30 && i.PercentFieldBeaten * 100 < 70) / (decimal)tournaments.Count : 0;
            EarlyMidFinish = tournaments.Any(i => i.PercentFieldBeaten * 100 >= 10 && i.PercentFieldBeaten * 100 < 30) && tournaments.Count > 0 ? tournaments.Count(i => i.PercentFieldBeaten * 100 >= 10 && i.PercentFieldBeaten * 100 < 30) / (decimal)tournaments.Count : 0;
            EarlyFinish = tournaments.Any(i => i.PercentFieldBeaten * 100 >= 0 && i.PercentFieldBeaten * 100 < 10) && tournaments.Count > 0 ? tournaments.Count(i => i.PercentFieldBeaten * 100 >= 0 && i.PercentFieldBeaten * 100 < 10) / (decimal)tournaments.Count : 0;
        }

        /// <summary>
        /// init stats
        /// </summary>
        private void InitStatsPosition()
        {
            var stoneBubble = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "StoneBubble"));
            var nearBubble = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "NearBubble"));

            HighestFinishPosition = tournaments.Any() ? tournaments.Max(i => i.FinishPosition) : 0;
            LowestFinishPosition = tournaments.Any(i => i.FinishPosition > 0) ? tournaments.Where(i => i.FinishPosition > 0).Min(i => i.FinishPosition) : 0;
            AverageFinishPosition = tournaments.Any() ? (int)tournaments.Average(i => i.FinishPosition) : 0;

            NearBubbles = tournaments.Any(i => i.FinishPosition > i.EntrantsPaid + stoneBubble && i.FinishPosition <= i.EntrantsPaid + Math.Round(i.EntrantsPaid * (nearBubble / 100)) + stoneBubble) ? tournaments.Count(i => i.FinishPosition > i.EntrantsPaid + stoneBubble && i.FinishPosition <= i.EntrantsPaid + Math.Round(i.EntrantsPaid * (nearBubble / 100)) + stoneBubble) : 0;
            MinCashes = tournaments.Where(i => i.FinishPosition <= i.EntrantsPaid && i.FinishPosition >= i.EntrantsPaid - Math.Round(i.EntrantsPaid * (nearBubble / 100))).Any() ? tournaments.Where(i => i.FinishPosition <= i.EntrantsPaid && i.FinishPosition >= i.EntrantsPaid - Math.Round(i.EntrantsPaid * (nearBubble / 100))).Count() : 0;

            Itm = tournaments.Any(i => i.PrizeWon > 0 && i.FinishPosition <= i.EntrantsPaid) && tournaments.Count > 0 ? tournaments.Count(i => i.PrizeWon > 0 && i.FinishPosition <= i.EntrantsPaid) / (decimal)tournaments.Count : 0;
            AveragePercentBeaten = tournaments.Any() ? (tournaments.Sum(i => i.PercentFieldBeaten * 100) / tournaments.Count) / 100 : 0;
        }
    }
}