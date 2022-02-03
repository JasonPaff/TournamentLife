using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels.Template_Manager_ViewModels;

namespace Tournament_Life.Views.Template_Manager_Views
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
