﻿using System;
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
using LiveTracker.ViewModels.Bankroll_ViewModels;
using Syncfusion.Windows.Shared;

namespace LiveTracker.Views.Bankroll_Views
{
    /// <summary>
    /// Interaction logic for ViewTransactionsView.xaml
    /// </summary>
    public partial class ViewTransactionsView : ChromelessWindow
    {
        public ViewTransactionsView(ViewTransactionsViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}