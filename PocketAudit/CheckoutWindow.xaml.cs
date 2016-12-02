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
using System.Windows.Shapes;

namespace SaleTool
{
    /// <summary>
    /// Interaction logic for CheckoutWindow.xaml
    /// </summary>
    public partial class CheckoutWindow : Window
    {
        CheckoutViewModel ViewModel;

        public CheckoutWindow(SalesViewModel sale)
        {
            InitializeComponent();
            ViewModel = new CheckoutViewModel(sale);
            DataContext = ViewModel;
        }

        private void payment_Selected(object sender, SelectionChangedEventArgs e)
        {
            //Get the combo box and its selected index and select the payment type
            var comboBox = sender as ComboBox;
            if (comboBox.SelectedIndex == 2)
                paypalPanel.Visibility = Visibility.Visible;
            else
                paypalPanel.Visibility = Visibility.Collapsed;
        }

        private void Finalize_Clicked(object sender, RoutedEventArgs e)
        {

        }
    }
}
