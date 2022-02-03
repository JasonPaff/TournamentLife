using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels.Misc_View_Models;

namespace Tournament_Life.Views.Misc_Views
{
    /// <summary>
    /// Interaction logic for NewProfileView.xaml
    /// </summary>
    public partial class NewProfileView : ChromelessWindow
    {
        public NewProfileView(NewProfileViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
