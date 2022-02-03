using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels.Bankroll_ViewModels;

namespace Tournament_Life.Views.Bankroll_Views
{
    /// <summary>
    /// Interaction logic for AddVenueView.xaml
    /// </summary>
    public partial class AddVenueView : ChromelessWindow
    {
        public AddVenueView(AddVenueViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
