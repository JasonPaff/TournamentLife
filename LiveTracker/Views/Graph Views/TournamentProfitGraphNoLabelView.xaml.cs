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

namespace LiveTracker.Views.Graph_Views
{
    /// <summary>
    /// Interaction logic for TournamentProfitGraphNoLabelView.xaml
    /// </summary>
    public partial class TournamentProfitGraphNoLabelView : ChromelessWindow
    {
        public TournamentProfitGraphNoLabelView(TournamentProfitGraphNoLabelViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
