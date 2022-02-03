using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels.Session_Manager_ViewModels;

namespace Tournament_Life.Views.Session_Manager_Views
{
    /// <summary>
    /// Interaction logic for SessionListView.xaml
    /// </summary>
    public partial class SessionListView : ChromelessWindow
    {
        public SessionListView(SessionListViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
