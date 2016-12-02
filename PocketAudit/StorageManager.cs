using SaleTool.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleTool
{
    public enum ProductTypes
    {
        General,
        Books,
        Office,
        Electronics,
        Misc
    }

    public enum ShippingCarriers
    {
        Usps,
        Ups,
        Fedex
    }
    public enum PaymentTypes
    {
        Credit,
        Debit,
        Paypal,
        Cash
    }

    public static class StorageManager
    {
        public static void SaveNewProductListing(ObservableCollection<Product> products)
        {
            JSONController.WriteToJsonFile("merch.json", products);
        }

        public static ObservableCollection<Product> LoadProductListing()
        {
            var listing = JSONController.ReadFromJsonFile<ObservableCollection<Product>>("merch.json");

            if (listing == null)
                listing = new ObservableCollection<Product>();

            return listing;
        }

        public static void SaveNewTransaction(Transaction transaction)
        {
            try
            {
                var history = new List<Transaction>();
                history = LoadTransactions();
                history.Add(transaction);
                JSONController.WriteToJsonFile("transactions.json", history);
            }
            catch
            {
                var history = new List<Transaction>();
                history.Add(transaction);
                JSONController.WriteToJsonFile("transactions.json", history);
            }
        }

        public static List<Transaction> LoadTransactions()
        {
            var listing = JSONController.ReadFromJsonFile<List<Transaction>>("transactions.json");

            if (listing == null)
                listing = new List<Transaction>();

            return listing;
        }

        public static void SaveNewContact(ObservableCollection<Supplier> contacts)
        {
            JSONController.WriteToJsonFile("contacts.json", contacts);
        }

        public static ObservableCollection<Supplier> LoadContacts()
        {
            var listing = JSONController.ReadFromJsonFile<ObservableCollection<Supplier>>("contacts.json");

            if (listing == null)
                listing = new ObservableCollection<Supplier>();

            return listing;
        }
    }
}
