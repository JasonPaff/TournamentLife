using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels.Template_Manager_ViewModels;

namespace Tournament_Life.Views.Template_Manager_Views
{
    /// <summary>
    /// Interaction logic for EditVenuesView.xaml
    /// </summary>
    public partial class EditVenuesView : ChromelessWindow
    {
        public EditVenuesView(EditVenuesViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
