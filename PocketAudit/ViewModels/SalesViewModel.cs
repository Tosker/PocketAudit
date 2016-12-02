using SaleTool.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SaleTool.ViewModels
{
    public class SalesViewModel : ObservableObject
    {
        //Collection of available products on file
        private ObservableCollection<Product> _productListing { get; set; }
        //Collection of viewable products
        public ObservableCollection<Product> Products { get; set; }
        //Collection of products to the sales cart
        public ObservableCollection<CartViewModel> Cart { get; set; }
        //Selected carrier for shipping
        public ShippingCarriers SelectedCarrier { get; set; }

        //User commands for cart
        private ICommand _addToCart { get; set; }
        private ICommand _removeFromCart { get; set; }

        //Financial totals for payment
        private double _subTotal { get; set; }
        private double _shipping { get; set; }
        private double _tax { get; set; }
        private double _total { get; set; }

        //String used to flter for relevents in product listing
        private string _filterString;

        //Access and set filter for viewed products
        public string FilterString
        {
            get
            {
                return _filterString;
            }
            set
            {
                _filterString = value;
                SearchProducts(value);
            }
        }

        /// <summary>
        /// Following property fields are for rounded values of the
        /// financial total for the sale. These are displayed to the user.
        /// </summary>
        public double SubTotal
        {
            get
            {
                return _subTotal;
            }
            set
            {
                _subTotal = Math.Round(value, 2);
                OnPropertyChanged("SubTotal");
            }
        }
        public double Shipping
        {
            get
            {
                return _shipping;
            }
            set
            {
                _shipping = Math.Round(value, 2);
                OnPropertyChanged("Shipping");
            }
        }

        public double Tax
        {
            get
            {
                return _tax;
            }
            set
            {
                _tax = Math.Round(value, 2);
                OnPropertyChanged("Tax");
            }
        }
        public double Total
        {
            get
            {
                return _total;
            }
            set
            {
                _total = Math.Round(value, 2);
                OnPropertyChanged("Total");
            }
        }

        public ICommand AddToCartCommand
        {
            get
            {
                return _addToCart;
            }
        }

        public ICommand RemoveFromCartCommand
        {
            get
            {
                return _removeFromCart;
            }
        }

        public SalesViewModel()
        {
            //Create fresh empty collection of a product cart
            Cart = new ObservableCollection<CartViewModel>();
            //Load all avaiable products on file
            _productListing = StorageManager.LoadProductListing();
            //Default for viewed products will be all of them
            Products = StorageManager.LoadProductListing();
            //Set user commands for managing cart
            _addToCart = new RelayCommand(AddProductToCart);
            _removeFromCart = new RelayCommand(RemoveProductFromCart);
        }

        //Add selected product in viewing to cart
        public void AddProductToCart(object product)
        {
            //get product passed in parameter from view
            var newProduct = product as Product;
            //If the item already exists in the cart get it
            var existingItem = Cart.FirstOrDefault(param => param.Product.Id == newProduct.Id);
            
            //If item already exists, update it's quantity
            if(existingItem != null)
                existingItem.AddQuantity(1);
            else
            {
                //Add new item to cart
                var cartItem = new CartViewModel(newProduct);
                Cart.Add(cartItem);
            }
            
            //Update balance of the current sale
            UpdateBalance();
        }

        //Remove a product from the cart
        public void RemoveProductFromCart(object product)
        {
            var oldProduct = product as Product;
            Cart.Remove(Cart.First(i => i.Product == oldProduct));
            UpdateBalance();
        }

        //Retreive all products in the cart (For checkout)
        public List<Product> CartProducts()
        {
            var products = new List<Product>();
            foreach(var item in Cart)
            {
                products.Add(item.Product);
            }

            return products;
        }

        //Transaction is finished so clear way for a new
        public void TransactionFinalized()
        {
            Cart.Clear();
        }

        public void ClearTransaction()
        {
            Cart = new ObservableCollection<CartViewModel>();
            SubTotal = 0;
            Shipping = 0;
            Tax = 0;
            Total = 0;
        }

        //Search for products based on relevens of name compared to string value
        public void SearchProducts(string value)
        {
            //If empty, show all products
            if (value == String.Empty)
            {
                Products = _productListing;
            }
            else
            {
                //Send all to lower so its not case sensitive
                value = value.ToLower();
                //Get all relevent products and set to viewed products
                Products = new ObservableCollection<Product>(_productListing.Where(o => o.Name.ToLower().Contains(value))
                    .OrderByDescending(m => m.Name.ToLower().StartsWith(value))
                    .Take(10));
            }
            OnPropertyChanged("Products");
        }

        //Update the balance of the sale
        private void UpdateBalance()
        {
            double sub = 0;
            double tax = 0;

            foreach(var item in Cart)
            {
                sub += item.Product.Price * item.Quantity;
                tax += (item.Product.Price * .08) * item.Quantity;
            }

            SubTotal = sub;
            Tax = tax;
            Shipping = 4.99;
            Total = SubTotal + Shipping + Tax;
        }
    }
}
