using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels.Datagrid_ViewModels;

namespace Tournament_Life.Views.Datagrid_Views
{
    /// <summary>
    /// Interaction logic for RemoveFromSessionView.xaml
    /// </summary>
    public partial class RemoveFromSessionView : ChromelessWindow
    {
        public RemoveFromSessionView(RemoveFromSessionViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
