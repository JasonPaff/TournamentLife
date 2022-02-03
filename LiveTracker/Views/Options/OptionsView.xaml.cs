using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels.Options;

namespace Tournament_Life.Views.Options
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
