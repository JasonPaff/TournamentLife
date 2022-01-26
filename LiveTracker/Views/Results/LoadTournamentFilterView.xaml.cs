using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels.Results;

namespace Tournament_Life.Views.Results
{
    /// <summary>
    /// Interaction logic for LoadTournamentFilterView.xaml
    /// </summary>
    public partial class LoadTournamentFilterView : ChromelessWindow
    {
        public LoadTournamentFilterView(LoadTournamentFilterViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
