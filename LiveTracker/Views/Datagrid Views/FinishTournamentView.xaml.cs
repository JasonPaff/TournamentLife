using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels.Datagrid_ViewModels;

namespace Tournament_Life.Views.Datagrid_Views
{
    /// <summary>
    /// Interaction logic for FinishTournamentView.xaml
    /// </summary>
    public partial class FinishTournamentView : ChromelessWindow
    {
        public FinishTournamentView(FinishTournamentViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
