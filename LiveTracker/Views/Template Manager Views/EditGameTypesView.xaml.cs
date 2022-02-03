using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels.Template_Manager_ViewModels;

namespace Tournament_Life.Views.Template_Manager_Views
{
    /// <summary>
    /// Interaction logic for EditGameTypesView.xaml
    /// </summary>
    public partial class EditGameTypesView : ChromelessWindow
    {
        public EditGameTypesView(EditGameTypesViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
