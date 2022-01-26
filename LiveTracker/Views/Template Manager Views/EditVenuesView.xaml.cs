using LiveTracker.ViewModels.Template_Manager_ViewModels;
using Syncfusion.Windows.Shared;

namespace LiveTracker.Views.Template_Manager_Views
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
