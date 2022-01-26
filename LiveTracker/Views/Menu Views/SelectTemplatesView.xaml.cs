using LiveTracker.ViewModels.Menu_ViewModels;
using Syncfusion.Windows.Shared;
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

namespace LiveTracker.Views.Menu_Views
{
    /// <summary>
    /// Interaction logic for SelectTemplatesView.xaml
    /// </summary>
    public partial class SelectTemplatesView : ChromelessWindow
    {
        public SelectTemplatesView(SelectTemplatesViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
