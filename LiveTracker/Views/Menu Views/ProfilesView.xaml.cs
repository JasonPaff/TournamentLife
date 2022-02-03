using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels.Menu_ViewModels;

namespace Tournament_Life.Views.Menu_Views
{
    /// <summary>
    /// Interaction logic for ProfilesView.xaml
    /// </summary>
    public partial class ProfilesView : ChromelessWindow
    {
        public ProfilesView(ProfilesViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
