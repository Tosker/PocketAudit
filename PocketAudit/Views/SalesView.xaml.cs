using SaleTool.Models;
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
    /// Interaction logic for SalesView.xaml
    /// </summary>
    public partial class SalesView : UserControl
    {
        SalesViewModel ViewModel;

        public SalesView()
        {
            InitializeComponent();
            DataContextChanged += new DependencyPropertyChangedEventHandler(View_DataContextChanged);
        }

        void View_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ViewModel = DataContext as SalesViewModel;
        }

        private void Checkout_Clicked(object sender, RoutedEventArgs e)
        {
            //Open checkout window and send it our current sale with the cart
            CheckoutWindow checkoutWindow = new CheckoutWindow(ViewModel);
            checkoutWindow.ShowDialog();
        }
    }
}
