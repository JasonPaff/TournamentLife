using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels.Bankroll_ViewModels;

namespace Tournament_Life.Views.Bankroll_Views
{
    /// <summary>
    /// Interaction logic for RemoveBankrollView.xaml
    /// </summary>
    public partial class RemoveBankrollView : ChromelessWindow
    {
        public RemoveBankrollView(RemoveBankrollViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
