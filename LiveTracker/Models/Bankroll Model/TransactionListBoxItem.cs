using Syncfusion.Windows.Shared;
using System;
using System.Globalization;

namespace LiveTracker.Models.Bankroll_Model
{
    public class TransactionListBoxItem : NotificationObject
    {
        public decimal Amount { get; set; }
        public string BankrollName { get; set; }
        public BonusTypes BonusType { get; set; }
        public DateTime Date { get; set; }
        public string Header
        {
            get
            {
                // return bonus type included in bonus transaction
                if(TransactionType == TransactionTypes.Bonus)
                    return $"{TransactionType.ToString()} ({BonusType.ToString()} - {Amount.ToString("C")} - {Date.ToShortDateString()})";

                // return normal transaction header
                return $"{TransactionType.ToString()} - {Amount.ToString("C")} - {Date.ToShortDateString()}";
            }
        }
        public bool IsSelected { get; set; }
        public string Name { get; set; }
        public TransactionTypes TransactionType { get; set; }

        /// <summary>
        /// copy the transactional list box item into a new object
        /// </summary>
        /// <returns></returns>
        public TransactionListBoxItem Copy()
        {
            var listBoxItem = new TransactionListBoxItem()
            {
                Amount = Amount,
                BankrollName = BankrollName,
                BonusType = BonusType,
                Date = Date,
                IsSelected = IsSelected,
                Name = Name,
                TransactionType = TransactionType,
            };
            return listBoxItem;
        }
    }
}
