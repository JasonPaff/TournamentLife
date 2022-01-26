using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels;

namespace Tournament_Life.Views
{
    /// <summary>
    /// Interaction logic for YesNoView.xaml
    /// </summary>
    public partial class YesNoView : ChromelessWindow
    {
        public YesNoView(YesNoViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
