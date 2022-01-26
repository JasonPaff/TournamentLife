using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels.Session_Manager_ViewModels;

namespace Tournament_Life.Views.Session_Manager_Views
{
    /// <summary>
    /// Interaction logic for SessionStartView.xaml
    /// </summary>
    public partial class SessionStartView : ChromelessWindow
    {
        public SessionStartView(SessionStartViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
