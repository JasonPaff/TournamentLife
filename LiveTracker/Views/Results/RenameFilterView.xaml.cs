using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels.Results;

namespace Tournament_Life.Views.Results
{
    /// <summary>
    /// Interaction logic for RenameFilterView.xaml
    /// </summary>
    public partial class RenameFilterView : ChromelessWindow
    {
        public RenameFilterView(RenameFilterViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
