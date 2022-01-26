using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels.Results;

namespace Tournament_Life.Views.Results
{
    /// <summary>
    /// Interaction logic for TournamentsView.xaml
    /// </summary>
    public partial class TournamentsView : ChromelessWindow
    {
        public TournamentsView(TournamentsViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
