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
    /// Interaction logic for MerchandiseView.xaml
    /// </summary>
    public partial class MerchandiseView : UserControl
    {
        MerchandiseViewModel ViewModel;

        public MerchandiseView()
        {
            InitializeComponent();
            //Assign event call when data context is set
            DataContextChanged += new DependencyPropertyChangedEventHandler(View_DataContextChanged);
        }

        //Set 'ViewModel'
        void View_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //Set viewmodel to the data context
            ViewModel = DataContext as MerchandiseViewModel;
            //By default, select the first product
            productListview.SelectedIndex = 0;
        }

        //Edit the product in listing
        private void Edit_Clicked(object sender, RoutedEventArgs e)
        {
            ToggleProductButtons();
        }

        //Cancle updating product
        private void Cancle_Clicked(object sender, RoutedEventArgs e)
        {
            ToggleProductButtons();
        }

        //Update the product in editing
        private void Update_Clicked(object sender, RoutedEventArgs e)
        {
            ToggleProductButtons();
        }

        //Toggle the panel displays for add/editing products
        private void ToggleProductButtons()
        {
            if (addPanel.Visibility == Visibility.Visible)
                addPanel.Visibility = Visibility.Collapsed;
            else
                addPanel.Visibility = Visibility.Visible;

            if (updatePanel.Visibility == Visibility.Visible)
                updatePanel.Visibility = Visibility.Collapsed;
            else
                updatePanel.Visibility = Visibility.Visible;
        }
    }
}
