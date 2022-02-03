using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels.Bankroll_ViewModels;

namespace Tournament_Life.Views.Bankroll_Views
{
    /// <summary>
    /// Interaction logic for RemoveVenueView.xaml
    /// </summary>
    public partial class RemoveVenueView : ChromelessWindow
    {
        public RemoveVenueView(RemoveVenueViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
