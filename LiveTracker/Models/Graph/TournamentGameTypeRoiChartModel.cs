using System;

namespace Tournament_Life.Models.Graph
{
    public class TournamentGameTypeRoiChartModel
    {
        public decimal AverageBeaten { get; set; }
        public int AverageEntrants { get; set; }
        public int AverageFinish { get; set; }
        public TimeSpan AverageTournament { get; set; }
        public int BestFinish { get; set; }
        public decimal Buyin { get; set; }
        public int Count { get; set; }
        public string Description { get; set; }
        public decimal EarlyFinish { get; set; }
        public decimal EarlyMidFinish { get; set; }
        public int FinalTableBubbles { get; set; }
        public decimal FinalTablePercent { get; set; }
        public int FinalTables { get; set; }
        public decimal FinalTableWinPercent { get; set; }
        public DateTime FirstDate { get; set; }
        public string GameType { get; set; }
        public decimal Itm { get; set; }
        public DateTime LastDate { get; set; }
        public decimal LateFinish { get; set; }
        public int LeastEntrants { get; set; }
        public TimeSpan LongestTournament { get; set; }
        public decimal MidFinish { get; set; }
        public decimal MidLateFinish { get; set; }
        public int MostEntrants { get; set; }
        public decimal Profit { get; set; }
        public decimal ProfitPerTournament { get; set; }
        public decimal Roi { get; set; }
        public TimeSpan ShortestTournament { get; set; }
        public int StoneBubbles { get; set; }
        public decimal TotalCost { get; set; }
        public decimal TotalPrizes { get; set; }
        public int TotalWins { get; set; }
        public decimal WinPercent { get; set; }
        public int WorstFinish { get; set; }
    }
}
