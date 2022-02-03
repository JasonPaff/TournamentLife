using System;
using Tournament_Life.Helpers;

namespace Tournament_Life.Models.Bankroll_Model
{
    public enum TransactionTypes
    {
        Bonus,
        Deposit,
        Withdrawal,
    }

    public enum BonusTypes
    {
        Cash,
        Credit,
        Freeroll,
        Giveaway,
        Lottery,
        Rakeback,
        Ticket,
        Tournament,
    }

    public class Transaction
    {
        public DateTime Date { get; set; }
        public string FontColor => PreferencesHelper.FindPreference("LiveTracker", "Window", "FontColor");
        public string FontSize => PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize");
        public decimal Amount { get; set; }
        public TransactionTypes TransactionType { get; set; }
        public BonusTypes BonusType { get; set; }
        public string Venue { get; set; }
    }
}
