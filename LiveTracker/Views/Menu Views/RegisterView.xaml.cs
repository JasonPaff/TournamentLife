using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels.Menu_ViewModels;

namespace Tournament_Life.Views.Menu_Views
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
