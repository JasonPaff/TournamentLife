using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels.Results;

namespace Tournament_Life.Views.Results
{
    /// <summary>
    /// Interaction logic for DeleteTournamentFilterView.xaml
    /// </summary>
    public partial class DeleteTournamentFilterView : ChromelessWindow
    {
        public DeleteTournamentFilterView(DeleteTournamentFilterViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
