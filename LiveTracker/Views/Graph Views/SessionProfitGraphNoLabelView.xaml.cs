using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels.Graph_ViewModels;

namespace Tournament_Life.Views.Graph_Views
{
    /// <summary>
    /// Interaction logic for SessionProfitGraphNoLabelView.xaml
    /// </summary>
    public partial class SessionProfitGraphNoLabelView : ChromelessWindow
    {
        public SessionProfitGraphNoLabelView(SessionProfitGraphNoLabelViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
