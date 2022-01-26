using LiveTracker.ViewModels.Template_Manager_ViewModels;
using Syncfusion.Windows.Shared;

namespace LiveTracker.Views.Template_Manager_Views
{
    /// <summary>
    /// Interaction logic for TemplateExportView.xaml
    /// </summary>
    public partial class TemplateExportView : ChromelessWindow
    {
        public TemplateExportView(TemplateExportViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
