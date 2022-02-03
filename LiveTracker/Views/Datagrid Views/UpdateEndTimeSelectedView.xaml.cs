using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels.Datagrid_ViewModels;

namespace Tournament_Life.Views.Datagrid_Views
{
    /// <summary>
    /// Interaction logic for UpdateEndTimeSelectedView.xaml
    /// </summary>
    public partial class UpdateEndTimeSelectedView : ChromelessWindow
    {
        public UpdateEndTimeSelectedView(UpdateEndTimeSelectedViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
