using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels;

namespace Tournament_Life.Views
{
    /// <summary>
    /// Interaction logic for OkView.xaml
    /// </summary>
    public partial class OkView : ChromelessWindow
    {
        public OkView(OkViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
