using LiveTracker.ViewModels.Menu_ViewModels;
using Syncfusion.Windows.Shared;

namespace LiveTracker.Views.Menu_Views
{
    /// <summary>
    /// Interaction logic for RegisterView.xaml
    /// </summary>
    public partial class RegisterView : ChromelessWindow
    {
        public RegisterView(RegisterViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
