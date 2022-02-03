using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels.Template_Manager_ViewModels;

namespace Tournament_Life.Views.Template_Manager_Views
{
    /// <summary>
    /// Interaction logic for EditFormatsView.xaml
    /// </summary>
    public partial class EditFormatsView : ChromelessWindow
    {
        public EditFormatsView(EditFormatsViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
