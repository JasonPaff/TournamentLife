using Tournament_Life.ViewModels.Graph_ViewModels;
using Syncfusion.Windows.Shared;

namespace Tournament_Life.Views.Graph_Views
{
    /// <summary>
    /// Interaction logic for TournamentGameTypeRoiChartView.xaml
    /// </summary>
    public partial class TournamentGameTypeRoiChartView : ChromelessWindow
    {
        public TournamentGameTypeRoiChartView(TournamentGameTypeRoiChartViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
