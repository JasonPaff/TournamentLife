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
using LiveTracker.ViewModels.Misc_View_Models;
using Syncfusion.Windows.Shared;

namespace LiveTracker.Views.Misc_Views
{
    /// <summary>
    /// Interaction logic for NewProfileView.xaml
    /// </summary>
    public partial class NewProfileView : ChromelessWindow
    {
        public NewProfileView(NewProfileViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
