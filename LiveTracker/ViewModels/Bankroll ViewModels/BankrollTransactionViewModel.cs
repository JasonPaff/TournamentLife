using System;
using System.Collections.ObjectModel;
using System.Linq;
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
    public class BankrollTransactionViewModel : NotificationObject
    {
        public TransactionTypes _type;
        public bool _editMode;
        private string _selectedTransactionType;

        public BankrollTransactionViewModel(ObservableCollection<Bankroll> bankrolls, TransactionTypes type = Models.Bankroll_Model.TransactionTypes.Deposit)
        {
            // transaction type
            _type = type;

            // init bankroll transaction
            InitializeTransaction(bankrolls);
        }

        public BankrollTransactionViewModel(ObservableCollection<Bankroll> bankrolls, Transaction transaction)
        {
            // transaction type
            _type = transaction.TransactionType;

            // flag edit
            _editMode = true;

            // init bankroll transaction
            InitializeTransaction(bankrolls);

            // init value for transaction
            Amount = transaction.Amount;
            Date = transaction.Date;
            SelectedBankroll = transaction.Venue;
            SelectedTransactionType = transaction.TransactionType.ToString();
            TransactionType = transaction.TransactionType;

            // add bonus type
            if (SelectedTransactionType == Models.Bankroll_Model.TransactionTypes.Bonus.ToString())
            {
                SelectedBonus = transaction.BonusType.ToString();
                BonusType = transaction.BonusType;
            }
        }

        public decimal Amount { get; set; }
        public ObservableCollection<string> Bankrolls { get; set; }
        public ObservableCollection<string> BonusList { get; set; }
        public BonusTypes BonusType { get; set; }
        public bool BonusVisible { get; set; }
        public DateTime Date { get; set; }
        public int FontSize { get; set; }
        public string SaveButton { get; set; }
        public bool Saved { get; set; }
        public string SelectedBankroll { get; set; }
        public string SelectedBonus { get; set; }
        public string SelectedTransactionType
        {
            get => _selectedTransactionType;
            set
            {
                _selectedTransactionType = value;
                RaisePropertyChanged(nameof(SelectedTransactionType));
                UpdateSaveButtonText();
            }
        }
        public string Theme { get; set; }
        public Transaction Transaction { get; set; }
        public TransactionTypes TransactionType { get; set; }
        public string Title { get; set; }
        public ObservableCollection<string> TransactionTypes { get; set; }
        public string Venue { get; set; }
        public ICommand SaveCommand => new BaseCommand(Save);
        public ICommand CancelCommand => new BaseCommand(Cancel);

        /// <summary>
        /// Cancel and close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Cancel(object parameter)
        {
            Application.Current.Windows.OfType<BankrollTransactionView>().FirstOrDefault()?.Close();
        }

        /// <summary>
        /// initialize transaction data
        /// </summary>
        private void InitializeTransaction(ObservableCollection<Bankroll> bankrolls)
        {
            // init prefs
            FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));
            Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");

            // create bankroll list
            Bankrolls = new ObservableCollection<string>();

            foreach (var bankroll in bankrolls)
                Bankrolls.Add(bankroll.Venue);

            SelectedBankroll = Bankrolls[0];

            // set title based on transaction type
            Title = "Bankroll Deposit";

            // set save button based on transaction type
            SaveButton = "Save Deposit";

            // set date to now
            Date = DateTime.Now;

            // show bonus info
            if (_type is Models.Bankroll_Model.TransactionTypes.Bonus)
                BonusVisible = true;

            // list of bonus items
            BonusList = new ObservableCollection<string>();

            // add bonus items
            BonusList.Add("Cash");
            BonusList.Add("Credit");
            BonusList.Add("Freeroll");
            BonusList.Add("Giveaway");
            BonusList.Add("Lottery");
            BonusList.Add("Rakeback");
            BonusList.Add("Ticket");
            BonusList.Add("Tournament $");

            // starting bonus item
            SelectedBonus = "Cash";

            // list of transaction types
            TransactionTypes = new ObservableCollection<string>();

            // add transaction types
            TransactionTypes.Add("Bonus");
            TransactionTypes.Add("Deposit");
            TransactionTypes.Add("Withdrawal");

            // starting transaction type
            SelectedTransactionType = "Deposit";

            UpdateSaveButtonText();
        }

        /// <summary>
        /// Save and close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Save(object parameter)
        {
            // find window
            var app = Application.Current.Windows.OfType<BankrollTransactionView>().FirstOrDefault();

            // update date
            if (app?.DatePicker?.Value is not null)
                Date = (DateTime)app.DatePicker.Value;

            // get transaction type
            if (SelectedTransactionType == "Bonus")
                _type = Models.Bankroll_Model.TransactionTypes.Bonus;
            if (SelectedTransactionType == "Deposit")
                _type = Models.Bankroll_Model.TransactionTypes.Deposit;
            if (SelectedTransactionType == "Withdrawal")
                _type = Models.Bankroll_Model.TransactionTypes.Withdrawal;

            // confirm transaction
            var vm = new YesNoViewModel("","");
            if (_type == Models.Bankroll_Model.TransactionTypes.Bonus)
            {
                vm = new YesNoViewModel("Are you sure you want to save this bonus?", "Save Bonus");
                if (_editMode) vm = new YesNoViewModel("Are you sure you want to update this bonus?", "Update Bonus");
            }
            if (_type == Models.Bankroll_Model.TransactionTypes.Deposit)
            {
                vm = new YesNoViewModel("Are you sure you want to complete this deposit?", "Complete Deposit");
                if (_editMode) vm = new YesNoViewModel("Are you sure you want to update this deposit?", "Update Deposit");
            }
            if (_type == Models.Bankroll_Model.TransactionTypes.Withdrawal)
            {
                vm = new YesNoViewModel("Are you sure you want to complete this withdrawal?", "Complete Withdrawal");
                if (_editMode) vm = new YesNoViewModel("Are you sure you want to update this withdrawal?", "Update Withdrawal");
            }

            var window = new YesNoView(vm)
            {
                Owner = Application.Current.Windows.OfType<BankrollTransactionView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            window.ShowDialog();

            // not saved
            if (vm.Saved is false)
                return;

            // find menu view model
            if (Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.LiveTrackerMenu?.DataContext is not MenuViewModel menuViewModel)
                return;

            // get selected bankroll
            var bank = menuViewModel.Bankrolls.FirstOrDefault(i => i.Venue == SelectedBankroll);

            // null check
            if (bank is null)
                return;

            // create new transaction
            var transaction = new Transaction()
            {
                Amount = Amount,
                BonusType = BonusType,
                Date = Date,
                TransactionType = _type,
                Venue = SelectedBankroll,
            };

            // check for bonus
            if (_type == Models.Bankroll_Model.TransactionTypes.Bonus)
            {
                if (SelectedBonus == "Cash") transaction.BonusType = BonusTypes.Cash;
                if (SelectedBonus == "Credit") transaction.BonusType = BonusTypes.Credit;
                if (SelectedBonus == "Freeroll") transaction.BonusType = BonusTypes.Freeroll;
                if (SelectedBonus == "Giveaway") transaction.BonusType = BonusTypes.Giveaway;
                if (SelectedBonus == "Lottery") transaction.BonusType = BonusTypes.Lottery;
                if (SelectedBonus == "Rakeback") transaction.BonusType = BonusTypes.Rakeback;
                if (SelectedBonus == "Ticket") transaction.BonusType = BonusTypes.Ticket;
                if (SelectedBonus == "Tournament") transaction.BonusType = BonusTypes.Tournament;
            }

            // add transaction to bankroll
            bank?.Transactions.Add(transaction);

            // save transaction to xml file
            BankrollHelper.SaveTransaction(bank, transaction);

            // update bankrolls collection and menu headers in menu view model
            menuViewModel.LoadBankrolls(DatabaseHelper.LoadDatabase());

            // reset amount
            Amount = 0.00m;

            // set transaction
            Transaction = transaction;

            // flag saved
            Saved = true;

            // edit, exit
            if (_editMode) Cancel(null);
        }

        /// <summary>
        /// update save button text
        /// </summary>
        private void UpdateSaveButtonText()
        {
            if (SelectedTransactionType == "Bonus")
            {
                BonusVisible = true;
                Title = "Bankroll Bonus";
                SaveButton = "Save Bonus";

                if(_editMode)
                {
                    Title = "Edit Bonus";
                    SaveButton = "Edit Bonus";
                }
            }
            else BonusVisible = false;

            if (SelectedTransactionType == "Deposit")
            {
                Title = "Bankroll Deposit";
                SaveButton = "Make Deposit";

                if (_editMode)
                {
                    Title = "Edit Deposit";
                    SaveButton = "Edit Deposit";
                }
            }

            if (SelectedTransactionType == "Withdrawal")
            {
                Title = "Bankroll Withdrawal";
                SaveButton = "Withdrawal";

                if (_editMode)
                {
                    Title = "Edit Withdrawal";
                    SaveButton = "Edit Withdrawal";
                }
            }
        }
    }
}
