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
using LiveTracker.ViewModels.Menu_ViewModels;
using Syncfusion.Windows.Shared;

namespace LiveTracker.Views.Menu_Views
{
    /// <summary>
    /// Interaction logic for ProfilesView.xaml
    /// </summary>
    public partial class ProfilesView : ChromelessWindow
    {
        public ProfilesView(ProfilesViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
