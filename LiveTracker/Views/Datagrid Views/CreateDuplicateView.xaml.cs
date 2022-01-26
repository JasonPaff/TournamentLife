using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels.Datagrid_ViewModels;

namespace Tournament_Life.Views.Datagrid_Views
{
    /// <summary>
    /// Interaction logic for CreateDuplicateView.xaml
    /// </summary>
    public partial class CreateDuplicateView : ChromelessWindow
    {
        public CreateDuplicateView(CreateDuplicateViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
