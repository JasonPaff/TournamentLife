using Tournament_Life.ViewModels.Graph_ViewModels;
using Syncfusion.Windows.Shared;

namespace Tournament_Life.Views.Graph_Views
{
    /// <summary>
    /// Interaction logic for TournamentFormatRoiChartView.xaml
    /// </summary>
    public partial class TournamentFormatRoiChartView : ChromelessWindow
    {
        public TournamentFormatRoiChartView(TournamentFormatRoiChartViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
