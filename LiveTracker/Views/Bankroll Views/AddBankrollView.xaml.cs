using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels.Bankroll_ViewModels;

namespace Tournament_Life.Views.Bankroll_Views
{
    /// <summary>
    /// Interaction logic for AddBankrollView.xaml
    /// </summary>
    public partial class AddBankrollView : ChromelessWindow
    {
        public AddBankrollView(AddBankrollViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
