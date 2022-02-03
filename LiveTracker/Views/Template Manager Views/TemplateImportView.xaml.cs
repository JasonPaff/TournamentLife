using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels.Template_Manager_ViewModels;

namespace Tournament_Life.Views.Template_Manager_Views
{
    /// <summary>
    /// Interaction logic for TemplateImportView.xaml
    /// </summary>
    public partial class TemplateImportView : ChromelessWindow
    {
        public TemplateImportView(TemplateImportViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
