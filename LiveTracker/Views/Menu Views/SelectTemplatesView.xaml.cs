using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels.Menu_ViewModels;

namespace Tournament_Life.Views.Menu_Views
{
    /// <summary>
    /// Interaction logic for SelectTemplatesView.xaml
    /// </summary>
    public partial class SelectTemplatesView : ChromelessWindow
    {
        public SelectTemplatesView(SelectTemplatesViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
