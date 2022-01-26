using Tournament_Life.ViewModels.Graph_ViewModels;
using Syncfusion.Windows.Shared;

namespace Tournament_Life.Views.Graph_Views
{
    /// <summary>
    /// Interaction logic for TournamentVenueRoiChartView.xaml
    /// </summary>
    public partial class TournamentVenueRoiChartView : ChromelessWindow
    {
        public TournamentVenueRoiChartView(TournamentVenueRoiChartViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
