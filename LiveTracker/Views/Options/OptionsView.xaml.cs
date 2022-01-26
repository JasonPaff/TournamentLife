using LiveTracker.ViewModels.Options;
using Syncfusion.Windows.Shared;

namespace LiveTracker.Views.Options
{
    /// <summary>
    /// Interaction logic for OptionsView.xaml
    /// </summary>
    public partial class OptionsView : ChromelessWindow
    {
        public OptionsView(OptionsViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
