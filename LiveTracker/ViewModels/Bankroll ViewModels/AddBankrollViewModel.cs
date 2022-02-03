using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Syncfusion.UI.Xaml.Utility;
using Syncfusion.Windows.Shared;
using Tournament_Life.Commands;
using Tournament_Life.Helpers;
using Tournament_Life.Models.Bankroll_Model;
using Tournament_Life.Views;
using Tournament_Life.Views.Bankroll_Views;

namespace Tournament_Life.ViewModels.Bankroll_ViewModels
{
    public class AddBankrollViewModel : NotificationObject
    {
        public AddBankrollViewModel()
        {
            LoadPreferences();

            Venues = new ObservableCollection<string>(TournamentTemplateDataHelper.LoadVenues());

            if(Venues.Count is not 0)
                    Venue = Venues[0];
        }

        public double Amount { get; set; }
        public bool Saved { get; set; }
        public string Venue { get; set; }
        public string Theme { get; set; }
        public int FontSize { get; set; }
        public ICommand SaveCommand => new BaseCommand(Save);
        public ICommand CancelCommand => new BaseCommand(Close);
        public ICommand AddNewVenueCommand => new BaseCommand(AddNewVenue);
        public ICommand RemoveVenueCommand => new BaseCommand(RemoveVenue);
        public ObservableCollection<string> Venues { get; set; }

        /// <summary>
        /// Add a new venue to the venues list
        /// </summary>
        /// <param name="parameter">null</param>
        private void AddNewVenue(object parameter)
        {
            var vm = new AddVenueViewModel();
            var window = new AddVenueView(vm)
            {
                Owner = Application.Current.Windows.OfType<AddBankrollView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            window.ShowDialog();

            if (vm.Saved is false)
                return;

            if (TournamentTemplateDataHelper.AddVenue(vm.Venue) is false)
                return;

            Venues.Add(vm.Venue);

            Venues = new ObservableCollection<string>(Venues.OrderBy(i => i));

            Venue = vm.Venue;
        }

        /// <summary>
        /// Cancel and close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Close(object parameter)
        {
            Application.Current.Windows.OfType<AddBankrollView>().FirstOrDefault()?.Close();
        }

        /// <summary>
        /// load initial preferences
        /// </summary>
        private void LoadPreferences()
        {
            FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));
            Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");
        }

        /// <summary>
        /// remove venues
        /// </summary>
        /// <param name="parameter"></param>
        private void RemoveVenue(object parameter)
        {
            if(Venues.Count is 0)
                return;

            var vm = new RemoveVenueViewModel();
            var window = new RemoveVenueView(vm)
            {
                Owner = Application.Current.Windows.OfType<AddBankrollView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            window.ShowDialog();

            if (vm.Saved is false)
                return;

            foreach (var venue in vm.VenuesToDelete)
            {
                if(TournamentTemplateDataHelper.RemoveVenue(venue) is false)
                    continue;

                Venues.Remove(venue);
            }

            Venues = new ObservableCollection<string>(Venues.OrderBy(i => i));

            if (Venues.Count is not 0)
                Venue = Venues[0];
        }

        /// <summary>
        /// Save and close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Save(object parameter)
        {
            if(Venue is null || Venues.Count is 0)
                return;

            var vm = new YesNoViewModel("Are you sure you want to create this bankroll?", "Create Bankroll");
            var window = new YesNoView(vm)
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            window.ShowDialog();

            if (vm.Saved is false)
                return;

            Saved = true;

            // create new bankroll
            var bankroll = new Bankroll(Venue, Amount);

            // save new bankroll
            BankrollHelper.SaveBankroll(bankroll);

            // create initial deposit transaction
            var transaction = new Transaction();
            transaction.Amount = (decimal)Amount;
            transaction.Venue = Venue;
            transaction.TransactionType = TransactionTypes.Deposit;
            transaction.Date = DateTime.Now;

            // save transaction
            if (transaction.Amount is not 0)
                BankrollHelper.SaveTransaction(bankroll, transaction);

            // update bankrolls
            MenuCommands.GetMenuViewModel()?.LoadBankrolls(DatabaseHelper.LoadDatabase());

            Close(null);
        }
    }
}