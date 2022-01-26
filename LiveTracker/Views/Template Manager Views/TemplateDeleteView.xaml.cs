using LiveTracker.ViewModels.Template_Manager_ViewModels;
using Syncfusion.Windows.Shared;

namespace LiveTracker.Views.Template_Manager_Views
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
