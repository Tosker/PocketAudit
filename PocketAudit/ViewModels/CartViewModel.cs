using SaleTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleTool.ViewModels
{
    public class CartViewModel : ObservableObject
    {
        public Product Product { get; private set; }
        public double Total { get; private set; }
        public int Quantity { get; private set; }

        public CartViewModel(Product product)
        {
            Product = product;
            Quantity = 1;
            Total = Product.Price * Quantity;
        }

        public void AddQuantity(int value)
        {
            Quantity += value;
            Total = Product.Price * Quantity;
            OnPropertyChanged("Total");
            OnPropertyChanged("Quantity");
        }
    }
}
