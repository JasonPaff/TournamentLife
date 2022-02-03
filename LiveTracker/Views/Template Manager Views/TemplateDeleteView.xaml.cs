using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels.Template_Manager_ViewModels;

namespace Tournament_Life.Views.Template_Manager_Views
{
    /// <summary>
    /// Interaction logic for TemplateDeleteView.xaml
    /// </summary>
    public partial class TemplateDeleteView : ChromelessWindow
    {
        public TemplateDeleteView(TemplateDeleteViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
