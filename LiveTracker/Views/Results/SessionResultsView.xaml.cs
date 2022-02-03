using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels.Results;

namespace Tournament_Life.Views.Results
{
    /// <summary>
    /// Interaction logic for SessionResultsView.xaml
    /// </summary>
    public partial class SessionResultsView : ChromelessWindow
    {
        public SessionResultsView(SessionResultsViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
