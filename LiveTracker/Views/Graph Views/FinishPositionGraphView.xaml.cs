using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels.Graph_ViewModels;

namespace Tournament_Life.Views.Graph_Views
{
    /// <summary>
    /// Interaction logic for FinishPositionGraphView.xaml
    /// </summary>
    public partial class FinishPositionGraphView : ChromelessWindow
    {
        public FinishPositionGraphView(FinishPositionGraphViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
