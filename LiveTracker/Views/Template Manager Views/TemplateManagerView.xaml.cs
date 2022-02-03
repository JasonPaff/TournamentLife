using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels.Template_Manager_ViewModels;

namespace Tournament_Life.Views.Template_Manager_Views
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
