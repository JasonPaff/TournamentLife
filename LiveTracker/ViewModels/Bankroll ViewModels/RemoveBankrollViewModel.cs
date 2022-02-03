using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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
    public class RemoveBankrollViewModel : NotificationObject
    {
        private readonly BankrollListBoxItem _selectedBankroll;
        public bool Saved { get; set; }
        public string Theme { get; set; }
        public int FontSize { get; set; }
        public BankrollListBoxItem SelectedBankrolls
        {
            get => _selectedBankroll;
            set
            {
                var selected = Bankrolls.Count(i => i.IsSelected);
                RaisePropertyChanged(nameof(SelectedBankrolls));
            }
        }
        public ObservableCollection<BankrollListBoxItem> Bankrolls { get; set; }
        public ObservableCollection<Bankroll> BankrollsToDelete { get; set; }
        public ICommand SaveCommand => new BaseCommand(Save);
        public ICommand CancelCommand => new BaseCommand(Close);

        public RemoveBankrollViewModel()
        {
            FontSize = int.Parse(PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize"));
            Theme = PreferencesHelper.FindPreference("LiveTracker", "Window", "Theme");

            // create list box items
            Bankrolls = new ObservableCollection<BankrollListBoxItem>(BankrollHelper.CreateListBoxItems(BankrollHelper.LoadBankrolls()));
        }

        /// <summary>
        /// Cancel and close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Close(object parameter)
        {
            Application.Current.Windows.OfType<RemoveBankrollView>().FirstOrDefault().Close();
        }

        /// <summary>
        /// Save and close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void Save(object parameter)
        {
            Saved = true;

            BankrollsToDelete = new ObservableCollection<Bankroll>();

            var removedString = new StringBuilder();

            removedString.AppendLine();

            var bankrolls = BankrollHelper.LoadBankrolls();

            foreach (var listBoxItem in Bankrolls)
            {
                if (listBoxItem.IsSelected is false)
                    continue;

                removedString.AppendLine(listBoxItem.Name);

                BankrollsToDelete.Add(bankrolls.FirstOrDefault(i => i.Venue == listBoxItem.Name));
            }

            removedString.AppendLine();

            // confirm for multiple bankrolls
            if (BankrollsToDelete.Count > 1)
            {
                var vm = new YesNoViewModel($"Are you sure you want to remove these bankrolls?\n {removedString.ToString().TrimEnd()}", "Remove Bankrolls");
                var window = new YesNoView(vm)
                {
                    Owner = Application.Current.MainWindow,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                window.ShowDialog();

                if (vm.Saved is false)
                    return;
            }

            // confirm for single bankrolls
            if (BankrollsToDelete.Count is 1)
            {
                var vm = new YesNoViewModel($"Are you sure you want to remove this bankroll?\n {removedString.ToString().TrimEnd()}", "Remove Bankroll");
                var window = new YesNoView(vm)
                {
                    Owner = Application.Current.MainWindow,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                window.ShowDialog();

                if (vm.Saved is false)
                    return;
            }

            BankrollHelper.DeleteBankrolls(new List<Bankroll>(BankrollsToDelete));

            MenuCommands.GetMenuViewModel().LoadBankrolls(DatabaseHelper.LoadDatabase());

            Close(null);
        }
    }
}
