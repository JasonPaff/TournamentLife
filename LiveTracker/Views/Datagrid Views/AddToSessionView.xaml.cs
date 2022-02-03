using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels.Datagrid_ViewModels;

namespace Tournament_Life.Views.Datagrid_Views
{
    /// <summary>
    /// Interaction logic for AddToSessionView.xaml
    /// </summary>
    public partial class AddToSessionView : ChromelessWindow
    {
        public AddToSessionView(AddToSessionViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
