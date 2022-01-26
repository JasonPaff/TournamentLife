using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels.Results;

namespace Tournament_Life.Views.Results
{
    /// <summary>
    /// Interaction logic for CreateTournamentFilterView.xaml
    /// </summary>
    public partial class CreateTournamentFilterView : ChromelessWindow
    {
        public CreateTournamentFilterView(CreateTournamentFilterViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
