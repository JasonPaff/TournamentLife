using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels.Template_Manager_ViewModels;

namespace Tournament_Life.Views.Template_Manager_Views
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
