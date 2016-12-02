using SaleTool.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Threading;

namespace SaleTool
{
    /// <summary>
    /// © 2016 Toskrs Corner
    /// </summary>
    public partial class MainWindow
    {
        private DispatcherTimer _clockTimer;

        public MainWindow()
        {
            InitializeComponent();

            //Make sure all required files exist in directory
            ConfirmFileDependencies();

            _clockTimer = new DispatcherTimer();
            _clockTimer.Interval = new TimeSpan(1000);
            _clockTimer.Tick += _clockTimer_Tick;
            _clockTimer.Start();

            this.DataContext = new MerchandiseViewModel();
        }

        private void _clockTimer_Tick(object sender, EventArgs e)
        {
            clockLabel.Content = DateTime.Now.ToShortTimeString();
        }

        private void MenuButton_Merchandise_Clicked(object sender, RoutedEventArgs e)
        {
            this.DataContext = new MerchandiseViewModel();
        }

        private void MenuButton_Sales_Clicked(object sender, RoutedEventArgs e)
        {
            this.DataContext = new SalesViewModel();
        }

        private void MenuButton_Contacts_Clicked(object sender, RoutedEventArgs e)
        {
            this.DataContext = new ContactsViewModel();
        }

        private void MenuButton_History_Clicked(object sender, RoutedEventArgs e)
        {
            this.DataContext = new HistoryViewModel();
        }

        private void ConfirmFileDependencies()
        {
            if (!File.Exists("transactions.json"))
            {
                File.Create("transactions.json");
            }
            if (!File.Exists("contacts.json"))
            {
                File.Create("contacts.json");
            }
            if (!File.Exists("merch.json"))
            {
                File.Create("merch.json");
            }
        }
    }
}
