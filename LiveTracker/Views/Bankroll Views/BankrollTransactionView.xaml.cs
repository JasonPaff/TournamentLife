using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels.Bankroll_ViewModels;

namespace Tournament_Life.Views.Bankroll_Views
{
    /// <summary>
    /// Interaction logic for BankrollTransactionView.xaml
    /// </summary>
    public partial class BankrollTransactionView : ChromelessWindow
    {
        public BankrollTransactionView(BankrollTransactionViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
