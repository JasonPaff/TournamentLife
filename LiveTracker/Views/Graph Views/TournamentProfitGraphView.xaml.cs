using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels.Graph_ViewModels;

namespace Tournament_Life.Views.Graph_Views
{
    /// <summary>
    /// Interaction logic for TournamentProfitGraphView.xaml
    /// </summary>
    public partial class TournamentProfitGraphView : ChromelessWindow
    {
        public TournamentProfitGraphView(TournamentProfitGraphViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
