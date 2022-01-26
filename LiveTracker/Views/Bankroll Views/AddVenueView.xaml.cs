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
    /// Interaction logic for AddVenueView.xaml
    /// </summary>
    public partial class AddVenueView : ChromelessWindow
    {
        public AddVenueView(AddVenueViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
