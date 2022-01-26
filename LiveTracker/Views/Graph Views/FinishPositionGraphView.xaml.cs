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
using LiveTracker.ViewModels.Graph_ViewModels;
using Syncfusion.Windows.Shared;

namespace LiveTracker.Views.Graph_Views
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
