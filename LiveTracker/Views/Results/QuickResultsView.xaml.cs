using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels.Results;

namespace Tournament_Life.Views.Results
{
    /// <summary>
    /// Interaction logic for QuickResultsView.xaml
    /// </summary>
    public partial class QuickResultsView : ChromelessWindow
    {
        public QuickResultsView(QuickResultsViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
