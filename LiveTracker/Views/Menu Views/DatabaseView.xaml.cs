using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels.Menu_ViewModels;

namespace Tournament_Life.Views.Menu_Views
{
    /// <summary>
    /// Interaction logic for DatabaseView.xaml
    /// </summary>
    public partial class DatabaseView : ChromelessWindow
    {
        public DatabaseView(DatabaseViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
