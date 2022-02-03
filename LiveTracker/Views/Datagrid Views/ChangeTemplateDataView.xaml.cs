using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels.Datagrid_ViewModels;

namespace Tournament_Life.Views.Datagrid_Views
{
    /// <summary>
    /// Interaction logic for ChangeTemplateDataView.xaml
    /// </summary>
    public partial class ChangeTemplateDataView : ChromelessWindow
    {
        public ChangeTemplateDataView(ChangeTemplateDataViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
