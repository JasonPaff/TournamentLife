using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels.Graph_ViewModels;

namespace Tournament_Life.Views.Graph_Views
{
    /// <summary>
    /// Interaction logic for TournamentProfitGraphNoLabelView.xaml
    /// </summary>
    public partial class TournamentProfitGraphNoLabelView : ChromelessWindow
    {
        public TournamentProfitGraphNoLabelView(TournamentProfitGraphNoLabelViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
