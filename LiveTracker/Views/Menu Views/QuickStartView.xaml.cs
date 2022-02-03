using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels.Menu_ViewModels;

namespace Tournament_Life.Views.Menu_Views
{
    /// <summary>
    /// Interaction logic for QuickStartView.xaml
    /// </summary>
    public partial class QuickStartView : ChromelessWindow
    {
        public QuickStartView(QuickStartViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
