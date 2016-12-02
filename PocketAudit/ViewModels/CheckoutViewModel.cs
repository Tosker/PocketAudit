using SaleTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SaleTool.ViewModels
{
    public class CheckoutViewModel : ObservableObject
    {
        //The sale the transaction derives from
        public SalesViewModel Sale { get; private set; }
        //What the finalized sale will be stored as
        public Transaction Transaction { get; private set; }

        //Finalizes the transaction
        private ICommand _finalizeCommand { get; set; }
        public ICommand FinalizeCommand
        {
            get
            {
                return _finalizeCommand;
            }
        }

        public CheckoutViewModel(SalesViewModel sale)
        {
            //Set the sale this checkout is created from
            Sale = sale;
            //Create a new instance for the transaction to be stored as
            Transaction = new Transaction();
            //Instantiate the command for the sale to be finalized
            _finalizeCommand = new RelayCommand(FinalizeTransaction);
        }

        public void FinalizeTransaction()
        {
            //Assign a new transaction ID
            AssignId(Transaction);

            //Set all transaction data based on sale information
            Transaction.ConsumerName = Transaction.ConsumerFirstName + " " + Transaction.ConsumerLastName;
            Transaction.Date = DateTime.Now;
            Transaction.ShortDate = DateTime.Now.ToShortDateString();
            Transaction.SubTotal = Sale.SubTotal;
            Transaction.ShippingTotal = Sale.Shipping;
            Transaction.TaxTotal = Sale.Tax;
            Transaction.Total = Sale.Total;
            Transaction.Carrier = Sale.SelectedCarrier;

            //Save the new transaction to history and clear
            StorageManager.SaveNewTransaction(Transaction);
            Sale.ClearTransaction();
        }

        private void AssignId(Transaction transaction)
        {
            //Get all transactions recorded
            var history = StorageManager.LoadTransactions();

            //Default ID if it's the first and only
            transaction.Id = 100;

            //If there are existing transactions, set ID
            if (history.Count > 0)
            {
                //To avoid repeats, find the highest number and increment from that
                transaction.Id = history.Max(i => i.Id);
                transaction.Id++;
            }
        }
    }
}
