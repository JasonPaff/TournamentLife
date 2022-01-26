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
using LiveTracker.ViewModels.Datagrid_ViewModels;
using Syncfusion.Windows.Shared;

namespace LiveTracker.Views.Datagrid_Views
{
    /// <summary>
    /// Interaction logic for RemoveFromSessionView.xaml
    /// </summary>
    public partial class RemoveFromSessionView : ChromelessWindow
    {
        public RemoveFromSessionView(RemoveFromSessionViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
