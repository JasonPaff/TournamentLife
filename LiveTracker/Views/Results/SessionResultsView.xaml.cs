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
using LiveTracker.ViewModels.Results;

namespace LiveTracker.Views.Results
{
    /// <summary>
    /// Interaction logic for SessionResultsView.xaml
    /// </summary>
    public partial class SessionResultsView : ChromelessWindow
    {
        public SessionResultsView(SessionResultsViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
