using LiveTracker.ViewModels.Template_Manager_ViewModels;
using Syncfusion.Windows.Shared;

namespace LiveTracker.Views.Template_Manager_Views
{
    /// <summary>
    /// Interaction logic for EditDefaultsView.xaml
    /// </summary>
    public partial class EditDefaultsView : ChromelessWindow
    {
        public EditDefaultsView(EditDefaultsViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
