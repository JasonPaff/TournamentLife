using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels.Datagrid_ViewModels;

namespace Tournament_Life.Views.Datagrid_Views
{
    /// <summary>
    /// Interaction logic for CancelSelectTournamentsView.xaml
    /// </summary>
    public partial class CancelSelectTournamentsView : ChromelessWindow
    {
        public CancelSelectTournamentsView(CancelSelectTournamentsViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
