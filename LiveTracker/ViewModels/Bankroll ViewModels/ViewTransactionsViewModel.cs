using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Syncfusion.UI.Xaml.Utility;
using Syncfusion.Windows.Shared;
using Tournament_Life.Helpers;
using Tournament_Life.Models.Bankroll_Model;
using Tournament_Life.ViewModels.Menu_ViewModels;
using Tournament_Life.Views;
using Tournament_Life.Views.Bankroll_Views;

namespace Tournament_Life.ViewModels.Bankroll_ViewModels
{
    public class ViewTransactionsViewModel : NotificationObject
    {
        private ObservableCollection<Bankroll> _bankrolls;
        private readonly TransactionListBoxItem _selectedTransactions;

        public ViewTransactionsViewModel(ObservableCollection<Bankroll> bankrolls)
        {
            FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));
            Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");

            // bankrolls collection
            _bankrolls = bankrolls;
            Bankrolls = new ObservableCollection<string>();
            foreach(var bankroll in _bankrolls) Bankrolls.Add(bankroll.Venue);

            // set combo box bankroll to first bankroll
            SelectedBankroll = Bankrolls[0];

            // set transaction list to selected bankrolls transactions list
            BankrollChanged(null);
        }

        public bool Saved { get; set; }
        public string SelectedBankroll { get; set; }
        public ObservableCollection<string> Bankrolls { get; set; }
        public ObservableCollection<TransactionListBoxItem> Transactions { get; set; }
        public TransactionListBoxItem SelectedTransactions
        {
            get => _selectedTransactions;
            set
            {
                var selected = Transactions.Count(i => i.IsSelected);
                RaisePropertyChanged(nameof(SelectedTransactions));
            }
        }
        public ObservableCollection<Transaction> TransactionsToDelete { get; set; }
        public string Theme { get; set; }
        public int FontSize { get; set; }
        public ICommand BankrollChangedCommand => new BaseCommand(BankrollChanged);
        public ICommand CancelCommand => new BaseCommand(Cancel);
        public ICommand EditCommand => new BaseCommand(Edit);
        public ICommand SaveCommand => new BaseCommand(Save);

        /// <summary>
        /// fires whenever the user changes the bankroll in the bankrolls combo box
        /// </summary>
        /// <param name="parameter"></param>
        private void BankrollChanged(object parameter)
        {
            // set transaction list to selected bankrolls transactions list
            Transactions = _bankrolls.FirstOrDefault(i => i.Venue == SelectedBankroll)?.TransactionListBoxItems;

            // sort by date
            if (Transactions is not null)
                Transactions = new ObservableCollection<TransactionListBoxItem>(Transactions.OrderByDescending(i => i.Date));
        }

        /// <summary>
        /// Cancel and close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Cancel(object parameter)
        {
            Application.Current.Windows.OfType<ViewTransactionsView>().FirstOrDefault()?.Close();
        }

        /// <summary>
        /// Delete a transaction
        /// </summary>
        /// <param name="transaction">transaction to delete</param>
        private void DeleteTransactions(Transaction transaction)
        {
            // menu view model
            if (Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.LiveTrackerMenu?.DataContext is not MenuViewModel menuViewModel)
                return;

            // get bankroll to remove transactions from
            var selectedBankroll = menuViewModel.Bankrolls?.FirstOrDefault(i => i.Venue == SelectedBankroll);

            // temp list for transactions to be deleted
            var tempTransactionList = new List<Transaction>(); tempTransactionList.Add(transaction);

            // remove transactions from bankrolls xml file
            BankrollHelper.DeleteTransactions(selectedBankroll, tempTransactionList);
        }

        /// <summary>
        /// Delete a transaction
        /// </summary>
        /// <param name="transaction">transaction to delete</param>
        private void DeleteTransactions(List<Transaction> transactions)
        {
            // loop and delete each transaction
            foreach (var transaction in transactions.ToList())
                DeleteTransactions(transaction);
        }

        /// <summary>
        /// edit transaction
        /// </summary>
        /// <param name="parameter"></param>
        private void Edit(object parameter)
        {
            // get selected transactions
            var transactions = Transactions.Where(i => i.IsSelected);

            // null/zero check
            if (transactions is null || transactions.Count() is 0 )
                return;

            // too many transactions selected
            if (transactions.Count() > 1)
            {
                // ok view model
                var theVm = new OkViewModel("You can only edit one transaction at a time", "Too Many Selected");

                // create/show ok window
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.Windows.OfType<ViewTransactionsView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                // exit
                return;
            }

            // did save
            Saved = true;

            // selected transaction
            var selectedTransaction = Transactions.Where(i => i.IsSelected).FirstOrDefault();

            // null check
            if (selectedTransaction is null)
                return;

            // create transaction we are editing
            var editedTransaction = new Transaction()
            {
                Amount = selectedTransaction.Amount,
                BonusType = selectedTransaction.BonusType,
                Date = selectedTransaction.Date,
                TransactionType = selectedTransaction.TransactionType,
                Venue = SelectedBankroll,
            };

            // find menu view model
            if (Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.LiveTrackerMenu?.DataContext is not MenuViewModel menuViewModel)
                return;

            // create/show bankroll transaction window
            var vm = new BankrollTransactionViewModel(menuViewModel.Bankrolls, editedTransaction);
            var window = new BankrollTransactionView(vm)
            {
                Owner = Application.Current.Windows.OfType<ViewTransactionsView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            window.ShowDialog();

            // didn't save, leave
            if (vm.Saved is false)
                return;

            // delete transactions
            DeleteTransactions(editedTransaction);

            // update bankrolls
            UpdateBankrolls();

            // notify bankroll changed
            BankrollChanged(null);

            // reload bankrolls in the menu
            menuViewModel.LoadBankrolls(DatabaseHelper.LoadDatabase());
        }

        /// <summary>
        /// Save and close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Save(object parameter)
        {
            // did save
            Saved = true;

            // collection of transactions to remove
            TransactionsToDelete = new ObservableCollection<Transaction>();

            var removedString = new StringBuilder();

            // string for name of transactions to be removed
            removedString.AppendLine($"\nBankroll: {SelectedBankroll}");

            // add each selected transaction to the collection
            foreach (var listBoxItem in Transactions)
            {
                if (listBoxItem.IsSelected is false)
                    continue;

                // header for message box
                removedString.AppendLine(listBoxItem.Header);

                // add transaction to removing list
                var bankroll = _bankrolls.FirstOrDefault(i => i.Venue == SelectedBankroll);
                TransactionsToDelete.Add(bankroll?.Transactions.FirstOrDefault(i => i.Venue == listBoxItem.Name && i.Amount - listBoxItem.Amount is 0 && i.Date == listBoxItem.Date));
            }

            // nothing to delete, leave
            if (TransactionsToDelete.Count is 0)
                return;

            // add blank line
            removedString.AppendLine("\n");

            // confirm for multiple bankrolls
            if (TransactionsToDelete.Count > 1)
            {
                // yes/no view model
                var vm = new YesNoViewModel($"Are you sure you want to delete these transactions?\n{removedString.ToString().TrimEnd()}", "Delete Transactions");

                // create/show yes/no window
                var window = new YesNoView(vm)
                {
                    Owner = Application.Current.Windows.OfType<ViewTransactionsView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                window.ShowDialog();

                // not saved
                if (vm.Saved is false)
                    return;
            }

            // confirm for single bankrolls
            if (TransactionsToDelete.Count is 1)
            {
                // yes/no view model
                var vm = new YesNoViewModel($"Are you sure you want to delete this transaction?\n{removedString.ToString().TrimEnd()}", "Delete Transaction");

                // create/show yes/no window
                var window = new YesNoView(vm)
                {
                    Owner = Application.Current.Windows.OfType<ViewTransactionsView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                window.ShowDialog();

                // not saved
                if (vm.Saved is false)
                    return;
            }

            // delete transactions
            DeleteTransactions(new List<Transaction>(TransactionsToDelete));

            // update bankrolls
            UpdateBankrolls();

            // notify bankroll changed
            BankrollChanged(null);

            // reload bankrolls in the menu
            if (Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.LiveTrackerMenu?.DataContext is MenuViewModel menuViewModel)
                menuViewModel.LoadBankrolls(DatabaseHelper.LoadDatabase());
        }

        /// <summary>
        /// update bankrolls
        /// </summary>
        private void UpdateBankrolls()
        {
            // temp holder for selected bankroll
            var oldSelectedBankroll = SelectedBankroll;

            // load bankrolls from xml file
            _bankrolls = new ObservableCollection<Bankroll>(BankrollHelper.LoadBankrolls());

            // clear bankrolls collection
            Bankrolls = new ObservableCollection<string>();

            // loop bankrolls and add to collection
            foreach (var bankroll in _bankrolls)
                Bankrolls.Add(bankroll.Venue);

            // restore selected bankroll
            SelectedBankroll = oldSelectedBankroll;
        }
    }
}
