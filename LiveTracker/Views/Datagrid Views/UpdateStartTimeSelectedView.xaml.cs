using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels.Datagrid_ViewModels;

namespace Tournament_Life.Views.Datagrid_Views
{
    /// <summary>
    /// Interaction logic for UpdateStartTimeSelectedView.xaml
    /// </summary>
    public partial class UpdateStartTimeSelectedView : ChromelessWindow
    {
        public UpdateStartTimeSelectedView(UpdateStartTimeSelectedViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
