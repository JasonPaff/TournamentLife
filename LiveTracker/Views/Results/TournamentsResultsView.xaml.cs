using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels.Results;

namespace Tournament_Life.Views.Results
{
    /// <summary>
    /// Interaction logic for TournamentsResultsView.xaml
    /// </summary>
    public partial class TournamentsResultsView : ChromelessWindow
    {
        public TournamentsResultsView(TournamentsResultsViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
