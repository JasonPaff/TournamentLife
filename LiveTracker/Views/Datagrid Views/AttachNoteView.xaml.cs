using LiveTracker.ViewModels.Datagrid_ViewModels;
using Syncfusion.Windows.Shared;

namespace LiveTracker.Views.Datagrid_Views
{
    /// <summary>
    /// Interaction logic for AttachNoteView.xaml
    /// </summary>
    public partial class AttachNoteView : ChromelessWindow
    {
        public AttachNoteView(AttachNoteViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
