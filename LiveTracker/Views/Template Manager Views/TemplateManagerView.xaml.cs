using LiveTracker.ViewModels.Template_Manager_ViewModels;
using Syncfusion.Windows.Shared;

namespace LiveTracker.Views.Template_Manager_Views
{
    /// <summary>
    /// Interaction logic for TemplateManagerView.xaml
    /// </summary>
    public partial class TemplateManagerView : ChromelessWindow
    {
        public TemplateManagerView(TemplateManagerViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
