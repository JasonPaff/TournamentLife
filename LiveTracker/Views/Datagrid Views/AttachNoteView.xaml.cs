using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels.Datagrid_ViewModels;

namespace Tournament_Life.Views.Datagrid_Views
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
