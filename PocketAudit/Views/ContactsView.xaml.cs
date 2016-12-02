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
    /// Interaction logic for ContactsView.xaml
    /// </summary>
    public partial class ContactsView : UserControl
    {
        ContactsViewModel ViewModel;

        public ContactsView()
        {
            InitializeComponent();
            DataContextChanged += new DependencyPropertyChangedEventHandler(View_DataContextChanged);
        }

        void View_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ViewModel = DataContext as ContactsViewModel;
        }


        private void Edit_Clicked(object sender, RoutedEventArgs e)
        {
            ToggleProductButtons();
        }

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


        private void Update_Clicked(object sender, RoutedEventArgs e)
        {
            ToggleProductButtons();
        }

        private void Cancle_Clicked(object sender, RoutedEventArgs e)
        {
            ToggleProductButtons();
        }
    }
}
