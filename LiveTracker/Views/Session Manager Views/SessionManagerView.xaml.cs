using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels.Session_Manager_ViewModels;

namespace Tournament_Life.Views.Session_Manager_Views
{
    /// <summary>
    /// Interaction logic for SessionManagerView.xaml
    /// </summary>
    public partial class SessionManagerView : ChromelessWindow
    {
        public SessionManagerView(SessionManagerViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
