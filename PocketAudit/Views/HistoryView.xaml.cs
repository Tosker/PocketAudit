﻿using SaleTool.Models;
using SaleTool.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SaleTool.Views
{
    /// <summary>
    /// Interaction logic for HistoryView.xaml
    /// </summary>
    public partial class HistoryView : UserControl
    {
        HistoryViewModel ViewModel;

        public HistoryView()
        {
            InitializeComponent();
            DataContextChanged += new DependencyPropertyChangedEventHandler(View_DataContextChanged);
        }

        void View_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ViewModel = DataContext as HistoryViewModel;
        }

        private void Transaction_Selected(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var listview = sender as ListView;
                var product = listview.SelectedItems[0] as Transaction;
                ViewModel.SelectTransaction(product);
            }
            catch (Exception)
            {
            }
        }
    }
}
