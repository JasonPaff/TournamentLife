using LiveTracker.ViewModels.Session_Manager_ViewModels;
using Syncfusion.Windows.Shared;

namespace LiveTracker.Views.Session_Manager_Views
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
