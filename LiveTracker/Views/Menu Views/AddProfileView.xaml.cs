using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels.Menu_ViewModels;

namespace Tournament_Life.Views.Menu_Views
{
    /// <summary>
    /// Interaction logic for AddProfileView.xaml
    /// </summary>
    public partial class AddProfileView : ChromelessWindow
    {
        public AddProfileView(AddProfileViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
