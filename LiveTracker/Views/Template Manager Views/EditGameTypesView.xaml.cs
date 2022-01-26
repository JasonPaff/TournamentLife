using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LiveTracker.ViewModels.Template_Manager_ViewModels;
using Syncfusion.Windows.Shared;

namespace LiveTracker.Views.Template_Manager_Views
{
    /// <summary>
    /// Interaction logic for EditGameTypesView.xaml
    /// </summary>
    public partial class EditGameTypesView : ChromelessWindow
    {
        public EditGameTypesView(EditGameTypesViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
