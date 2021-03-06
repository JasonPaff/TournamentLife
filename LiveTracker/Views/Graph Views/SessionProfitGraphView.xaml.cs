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
using Syncfusion.Windows.Shared;
using Tournament_Life.ViewModels.Graph_ViewModels;

namespace Tournament_Life.Views.Graph_Views
{
    /// <summary>
    /// Interaction logic for SessionProfitGraphView.xaml
    /// </summary>
    public partial class SessionProfitGraphView : ChromelessWindow
    {
        public SessionProfitGraphView(SessionProfitGraphViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
