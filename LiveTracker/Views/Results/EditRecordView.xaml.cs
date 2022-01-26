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
using Tournament_Life.ViewModels.Results;

namespace Tournament_Life.Views.Results
{
    /// <summary>
    /// Interaction logic for EditRecordView.xaml
    /// </summary>
    public partial class EditRecordView : ChromelessWindow
    {
        public EditRecordView(EditRecordViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
