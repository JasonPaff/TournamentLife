using LiveTracker.Helpers;
using Syncfusion.Windows.Shared;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace LiveTracker.Models.Bankroll_Model
{
    public class Bankroll : NotificationObject
    {
        public Bankroll() { }
        public Bankroll(string venue, double startingAmount)
        {
            Venue = venue;
            StartingAmount = startingAmount;
        }

        public string BankrollHeader => $"{Venue} - {Total.ToString("C")}";
        public string FontColor => PreferencesHelper.FindPreference("LiveTracker", "Window", "FontColor");
        public string FontSize => PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize");
        public double StartingAmount { get; set; }
        public string ToolTip
        {
            get
            {
                var sb = new StringBuilder();

                sb.AppendLine($"Starting Amount: {StartingAmount.ToString("C2")}");
                sb.AppendLine();
                sb.AppendLine($"Total Deposits: {Transactions.Where(i => i.TransactionType == TransactionTypes.Deposit).Sum(i => i.Amount).ToString("C2")}");
                sb.AppendLine($"Total Withdrawals: {Transactions.Where(i => i.TransactionType == TransactionTypes.Withdrawal).Sum(i => i.Amount).ToString("C2")}");
                sb.AppendLine($"Total Bonuses: {Transactions.Where(i => i.TransactionType == TransactionTypes.Bonus).Sum(i => i.Amount).ToString("C2")}");
                sb.AppendLine();
                sb.AppendLine($"Total Profit: {TournamentProfit.ToString("C2")}");

                return sb.ToString().Trim();
            }
        }
        public double Total { get; set; }
        public double TournamentProfit { get; set; }
        public string Venue { get; set; }
        public ObservableCollection<TransactionListBoxItem> TransactionListBoxItems { get; set; } = new ObservableCollection<TransactionListBoxItem>();
        public ObservableCollection<Transaction> Transactions { get; set; } = new ObservableCollection<Transaction>();

        /// <summary>
        /// Add a transaction to the bankroll
        /// </summary>
        /// <param name="transaction">transaction to add</param>
        public void AddTransaction(Transaction transaction)
        {
            // null check
            if(transaction is null) return;

            // add transaction to transactions collection
            Transactions.Add(transaction);

            // update list box items
            UpdateTransactionListBoxItems();

            // update total property
            UpdateTotal();
        }

        /// <summary>
        /// Create list box items out of the transactions
        /// </summary>
        public void UpdateTransactionListBoxItems()
        {
            // list box item collection
            TransactionListBoxItems = new ObservableCollection<TransactionListBoxItem>();

            // sort transactions by date
            Transactions = new ObservableCollection<Transaction>(Transactions.OrderBy(i => i.Date));

            // loop transactions
            foreach (var transaction in Transactions)
            {
                // create list box item for each transaction
                var transactionListBoxItem = new TransactionListBoxItem()
                {
                    Amount = transaction.Amount,
                    BonusType = transaction.BonusType,
                    Date = transaction.Date,
                    IsSelected = false,
                    Name = transaction.Venue,
                    TransactionType = transaction.TransactionType,
                };

                // add list box item to collection
                TransactionListBoxItems.Add(transactionListBoxItem);
            }
        }

        /// <summary>
        /// Calculate the total value of the bankroll
        /// </summary>
        /// <returns>bankroll total</returns>
        public void UpdateTotal()
        {
            // bankroll starting amount
            var total = 0.0;

            // add deposits to total
            var depositTotal = (double)Transactions.Where(i => i.TransactionType is TransactionTypes.Deposit)?.Sum(i => i.Amount);
            total += depositTotal;

            // subtract withdrawals from total
            var withdrawalTotal = (double)Transactions.Where(i => i.TransactionType is TransactionTypes.Withdrawal)?.Sum(i => i.Amount);
            total -= withdrawalTotal;

            // add bonus to total
            var bonusTotal = (double)Transactions.Where(i => i.TransactionType is TransactionTypes.Bonus)?.Sum(i => i.Amount);
            total += bonusTotal;

            // add tournament profit to total
            total += TournamentProfit;

            // set total property
            Total = total;
        }
    }
}
