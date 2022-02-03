using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Tournament_Life.Helpers;
using Tournament_Life.Models.Tournaments;

namespace Tournament_Life.Models.Sessions
{
    public class SessionStats
    {
        public SessionStats()
        {
            Sessions = SortSessions(LoadTournaments());

            FilterStats();
        }

        public SessionStats(ObservableCollection<TournamentFinished> tournaments)
        {
            Sessions = SortSessions(new List<TournamentFinished>(tournaments.OrderBy(i => i.StartTime)));

            FilterStats();
        }

        public ObservableCollection<SessionModel> Sessions { get; private set; }

        public decimal Hourly
        {
            get
            {
                if (Sessions is null || Sessions.Count is 0) return 0;

                return CalculateHourly(Sessions);
            }
        }
        public int Played
        {
            get
            {
                if (Sessions is null)
                    return 0;

                return Sessions.Count;
            }
        }
        public int Even { get; set; }
        public int Won { get; set; }
        public int Lost { get; set; }
        public int WinningStreak { get; set; }
        public int LosingStreak { get; set; }
        public int TotalTournaments { get; set; }
        public int AverageTournaments { get; set; }
        public int MostTournaments { get; set; }
        public int LeastTournaments { get; set; }
        public decimal DollarsPerTournament
        {
            get
            {
                if (TotalProfit is 0 || TotalTournaments is 0)
                    return 0;

                return TotalProfit / TotalTournaments;
            }
        }
        public decimal TotalPrizes { get; set; }
        public decimal MostPrizes { get; set; }
        public decimal LeastPrizes { get; set; }
        public decimal AveragePrizes { get; set; }
        public decimal TotalProfit { get; set; }
        public decimal MostProfit { get; set; }
        public decimal LeastProfit { get; set; }
        public decimal AverageProfit { get; set; }
        public decimal TotalCost { get; set; }
        public decimal MostCost { get; set; }
        public decimal LeastCost { get; set; }
        public decimal AverageCost { get; set; }
        public decimal AverageBuyin { get; set; }
        public decimal AverageBuyinCost { get; set; }
        public decimal Roi
        {
            get
            {
                if (TotalProfit is 0 || TotalCost is 0)
                    return 0;

                return TotalProfit / TotalCost;
            }
        }
        public decimal Itm { get; set; }
        public decimal FieldBeaten { get; set; }
        public TimeSpan TotalTime { get; set; }
        public TimeSpan LongestTime { get; set; }
        public TimeSpan ShortestTime { get; set; }
        public TimeSpan AverageTime { get; set; }
        public TimeSpan SessionTime { get; set; }
        public TimeSpan AverageTournamentTime { get; set; }

        /// <summary>
        /// calculate hourly rate from sessions
        /// </summary>
        /// <param name="sessions">sessions list</param>
        /// <returns>hourly rate</returns>
        public static decimal CalculateHourly(ObservableCollection<SessionModel> sessions)
        {
            // null/zero check
            if (sessions is null || sessions.Count is 0 || sessions.Sum(i => i.TotalProfit) is 0 || sessions.Sum(i => i.SessionLength.TotalHours) is 0) return 0;

            // return hourly
            return sessions.Sum(i => i.TotalProfit) / (decimal)sessions.Sum(i => i.SessionLength.TotalHours);
        }

        /// <summary>
        /// calculate time played for sessions
        /// </summary>
        /// <param name="sessions"></param>
        /// <returns></returns>
        public static TimeSpan CalculateTimePlayed(ObservableCollection<SessionModel> sessions)
        {
            return sessions.Any() ? new TimeSpan(sessions.Sum(i => i.SessionLength.Ticks)) : TimeSpan.Zero;
        }

        /// <summary>
        /// Filter stats
        /// </summary>
        public void FilterStats()
        {
            var tournaments = new List<TournamentFinished>();

            foreach (var session in Sessions) foreach (var tournament in session.Tournaments) tournaments.Add(tournament);

            AverageBuyin = tournaments.Any() ? (decimal)tournaments?.Average(i => i.BuyinTotalCost) : 0;
            AverageBuyinCost = tournaments.Any() ? (decimal)tournaments?.Average(i => i.TotalCost) : 0;

            MostCost = Sessions.Any() ? Sessions.Max(i => i.TotalCost) : 0;
            LeastCost = Sessions.Any() ? Sessions.Min(i => i.TotalCost) : 0;
            TotalCost = Sessions.Any() ? Sessions.Sum(i => i.TotalCost) : 0;
            AverageCost = Sessions.Any() ? Sessions.Average(i => i.TotalCost) : 0;

            MostProfit = Sessions.Any() ? Sessions.Max(i => i.TotalProfit) : 0;
            LeastProfit = Sessions.Any() ? Sessions.Min(i => i.TotalProfit) : 0;
            TotalProfit = Sessions.Any() ? Sessions.Sum(i => i.TotalProfit) : 0;
            AverageProfit = Sessions.Any() ? Sessions.Average(i => i.TotalProfit) : 0;

            MostPrizes = Sessions.Any() ? Sessions.Max(i => i.PrizesWonTotal) : 0;
            LeastPrizes = Sessions.Any(i => i.PrizesWonTotal > 0) ? Sessions.Where(i => i.PrizesWonTotal > 0).Min(i => i.PrizesWonTotal) : 0;
            TotalPrizes = Sessions.Any() ? Sessions.Sum(i => i.PrizesWonTotal) : 0;
            AveragePrizes = Sessions.Any() ? Sessions.Average(i => i.PrizesWonTotal) : 0;

            FieldBeaten = Sessions.Any() ? Sessions.Average(i => i.AveragePercentBeaten) : 0;
            Itm = Sessions.Any() ? Sessions.Average(i => i.Itm) : 0;

            ShortestTime = Sessions.Any() ? Sessions.Min(i => i.SessionLength) : TimeSpan.Zero;
            LongestTime = Sessions.Any() ? Sessions.Max(i => i.SessionLength) : TimeSpan.Zero;
            TotalTime = Sessions.Any() ? new TimeSpan(Sessions.Sum(i => i.SessionLength.Ticks)) : TimeSpan.Zero;
            AverageTime = Sessions.Any() ? new TimeSpan((long)Sessions.Average(i => i.SessionLength.Ticks)) : TimeSpan.Zero;

            LoadCountingStats();

            LoadStreaks();
        }

        /// <summary>
        /// Load counting stats
        /// </summary>
        private void LoadCountingStats()
        {
            MostTournaments = Sessions.Select(i => i.TournamentCount).Any() ? Sessions.Max(i => i.TournamentCount) : 0;
            LeastTournaments = Sessions.Select(i => i.TournamentCount).Any() ? Sessions.Min(i => i.TournamentCount) : 0;
            TotalTournaments = Sessions.Any() ? Sessions.Sum(i => i.TournamentCount) : 0;
            AverageTournaments = Sessions.Any() ? (int)Sessions.Average(i => i.TournamentCount) : 0;

            Won = Sessions.Any(i => i.TotalProfit > 0) ? Sessions.Count(i => i.TotalProfit > 0) : 0;
            Lost = Sessions.Any(i => i.TotalProfit < 0) ? Sessions.Count(i => i.TotalProfit < 0) : 0;
            Even = Sessions.Any(i => i.TotalProfit is 0) ? Sessions.Count(i => i.TotalProfit is 0) : 0;
        }

        /// <summary>
        /// load streak stats
        /// </summary>
        private void LoadStreaks()
        {

            // find winning streak and losing streak
            int currentWinningStreak = 0;
            int currentLosingStreak = 0;

            foreach (var session in Sessions)
            {
                if (session.TotalProfit > 0)
                {
                    currentWinningStreak++;

                    if (currentLosingStreak > LosingStreak)
                        LosingStreak = currentLosingStreak;

                    currentLosingStreak = 0;
                }
                else
                {
                    currentLosingStreak++;

                    if (currentWinningStreak > WinningStreak)
                        WinningStreak = currentWinningStreak;

                    currentWinningStreak = 0;
                }
            }
        }

        /// <summary>
        /// Load tournaments from database
        /// </summary>
        /// <returns></returns>
        private List<TournamentFinished> LoadTournaments()
        {
            // temp collection
            var tournaments = new List<TournamentFinished>();

            // load tournaments from database and sort by starting time
            tournaments = new List<TournamentFinished>(DatabaseHelper.LoadDatabase().OrderBy(i => i.StartTime));

            // return tournaments
            return tournaments;
        }

        /// <summary>
        /// reload sessions
        /// </summary>
        public void ReloadSessions()
        {
            Sessions = SortSessions(LoadTournaments());
            FilterStats();
        }

        /// <summary>
        /// sort tournaments into sessions
        /// </summary>
        /// <param name="tournaments">tournaments to sort</param>
        /// <returns>sessions</returns>
        private ObservableCollection<SessionModel> SortSessions(List<TournamentFinished> tournaments)
        {
            var sessions = new ObservableCollection<SessionModel>();

            if (tournaments is null || tournaments.Count is 0) return sessions;

            var startTime = tournaments.ElementAt(0).StartTime;
            var endTime = tournaments.ElementAt(0).EndTime;

            var session = new SessionModel();

            // loop through tournaments and sort into sessions based on overlapping play times
            foreach (var tournament in tournaments)
            {
                if (tournament.StartTime >= startTime && tournament.StartTime <= endTime)
                {
                    if (tournament.EndTime >= endTime)
                        endTime = tournament.EndTime;

                    session.AddTournament(tournament);
                }

                if (tournament.StartTime > endTime)
                {
                    session.ID = sessions.Count + 1;
                    sessions.Add(session);

                    session = new SessionModel();
                    session.AddTournament(tournament);

                    startTime = tournament.StartTime;
                    endTime = tournament.EndTime;
                }
            }

            sessions.Add(session);
            session.ID = sessions.Count;

            return sessions;
        }

        /// <summary>
        /// update session stats
        /// </summary>
        /// <param name="Sessions"></param>
        public void UpdateSessions(ObservableCollection<SessionModel> Sessions)
        {
            this.Sessions = Sessions;
            FilterStats();
        }
    }
}
