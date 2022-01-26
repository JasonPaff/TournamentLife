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
using LiveTracker.ViewModels.Results;
using Syncfusion.Windows.Shared;

namespace LiveTracker.Views.Results
{
    /// <summary>
    /// Interaction logic for QuickResultsView.xaml
    /// </summary>
    public partial class QuickResultsView : ChromelessWindow
    {
        public QuickResultsView(QuickResultsViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
