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
    public class MerchandiseViewModel : ObservableObject
    {
        //Collection of all products avaiable on file
        private ObservableCollection<Product> _productListing { get; set; }
        //Collection of viewable products
        public ObservableCollection<Product> Products { get; set; }
        //Collection of suppliers on file
        public ObservableCollection<Supplier> Carriers { get; set; }

        //Model of the product we are creating, updating or removing
        private Product _productModel { get; set; }
        //Model of the product we are currently viewing
        private Product _viewedProduct;
        //Model of the selected carrier assign to the product model
        private Supplier _selectedCarrier { get; set; }

        //String we are filtering viewable products with
        private string _filterString;

        //Property for setting and flagging change in filtering viewed products
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

        //Property to access the product model(See above)
        public Product ProductModel
        {
            get
            {
                return _productModel;
            }
            set
            {
                _productModel = value;
                OnPropertyChanged("ProductModel");
            }
        }

        //Property to access the selected carrier model of product model(See above)
        public Supplier SelectedCarrier
        {
            get
            {
                return _selectedCarrier;
            }
            set
            {
                _selectedCarrier = value;
                //Since carriers are identified by IDs we set the ID here but have the object for user viewing
                ProductModel.CarrierId = _selectedCarrier.Id;
                OnPropertyChanged("SelectedCarrier");
            }
        }

        //Property to access the product being viewed (See above)
        public Product ViewedProduct
        {
            get { return _viewedProduct; }
            set
            {
                _viewedProduct = value;

                //Admittedly a messy logistical way for setting carrier for viewed product
                if(value != null && Carriers.Count > 0)
                {
                    //Get the carrier(contact) from collection based on viewed product ID
                    var carrier = Carriers.FirstOrDefault(param => param.Id == _viewedProduct.CarrierId);
                    //If carrier was found, set its proper string name for user view(Again, since we go by ID)
                    if (carrier != null) _viewedProduct.CarrierName = carrier.Name;
                }

                OnPropertyChanged("ViewedProduct");
            }
        }

        private ICommand _editProduct { get; set; }
        public ICommand EditCommand
        {
            get
            {
                return _editProduct;
            }
        }

        private ICommand _cancleEdit { get; set; }
        public ICommand CancleCommand
        {
            get
            {
                return _cancleEdit;
            }
        }

        private ICommand _updateEdit { get; set; }
        public ICommand UpdateCommand
        {
            get
            {
                return _updateEdit;
            }
        }

        private ICommand _addProduct { get; set; }
        public ICommand AddCommand
        {
            get
            {
                return _addProduct;
            }
        }

        private ICommand _removeProduct { get; set; }
        public ICommand RemoveCommand
        {
            get
            {
                return _removeProduct;
            }
        }

        public MerchandiseViewModel()
        {
            //Load all existing products on file
            _productListing = StorageManager.LoadProductListing();
            //Initially set viewed products to all existing ones
            Products = _productListing;
            //Load all merch supplier contacts on file
            Carriers = StorageManager.LoadContacts();
            //Set a default empty product model for viewing
            ProductModel = new Product();
            //Set a default empty supplier contact for viewing
            SelectedCarrier = new Supplier();

            //Set all commands for updating, adding and removing merchandise
            _editProduct = new RelayCommand(EditProduct);
            _addProduct = new RelayCommand(AddProduct);
            _removeProduct = new RelayCommand(RemoveProduct);
            _cancleEdit = new RelayCommand(Cancle);
            _updateEdit = new RelayCommand(Update);
        }

        //Create product model to the current model information passed
        public void EditProduct(object product)
        {
            //Set model and carrier to the passed product
            ProductModel = product as Product;
            SelectedCarrier.Id = ProductModel.CarrierId;
            //Set carrier based on colleciton. If contact has been removed, use the first one in collection by default
            SelectedCarrier = Carriers.FirstOrDefault(c => c.Id == ProductModel.CarrierId) ?? Carriers[0];
            OnPropertyChanged("ProductModel");
        }

        //Add a new product to the colleciton
        public void AddProduct()
        {
            //Assign an ID to the new product being added
            AssignId(ProductModel);
            //Set carrier based on selected one
            ProductModel.CarrierId = SelectedCarrier.Id;
            //Add product to product collection
            _productListing.Add(ProductModel);

            Update();
        }

        //Remove the product passed from collection
        public void RemoveProduct(object product)
        {
            //Get the product passed through parameter
            var oldProduct = product as Product;
            //Remove product from collection
            _productListing.Remove(oldProduct);

            Update();
        }

        //Clears the product model and saves collection to file
        public void Update()
        {
            ProductModel = new Product();
            StorageManager.SaveNewProductListing(_productListing);
        }

        //Cancle updating selected product and create a new
        public void Cancle()
        {
            ProductModel = new Product();
        }

        //Set viewed product collection based on string value relevence
        public void SearchProducts(string value)
        {
            //If no value, show all products
            if(value == String.Empty)
            {
                Products = _productListing;
            }
            else
            {
                //Set value to lower case to avoid case sensitivity errors
                value = value.ToLower();
                //Set viewed product colelction to relevent products based on string value vs name
                Products = new ObservableCollection<Product>(_productListing.Where(o => o.Name.ToLower().Contains(value))
                    .OrderByDescending(m => m.Name.ToLower().StartsWith(value))
                    .Take(10));
            }
            OnPropertyChanged("Products");
        }

        //Assign a new ID to a product
        private void AssignId(Product product)
        {
            //Default ID if this is the first and only product
            product.Id = 100;
            if (Products.Count > 0)
            {
                //Get the biggest ID based on value and increment form that for unique id
                product.Id = Products.Max(i => i.Id);
                product.Id++;
            }
        }
    }
}
