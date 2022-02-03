using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels.Bankroll_ViewModels;

namespace Tournament_Life.Views.Bankroll_Views
{
    /// <summary>
    /// Interaction logic for ViewTransactionsView.xaml
    /// </summary>
    public partial class ViewTransactionsView : ChromelessWindow
    {
        public ViewTransactionsView(ViewTransactionsViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
