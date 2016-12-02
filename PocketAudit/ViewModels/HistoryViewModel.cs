using SaleTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleTool.ViewModels
{
    public class HistoryViewModel : ObservableObject
    {
        public List<Transaction> Transactions { get; private set; }
        public Transaction SelectedTransaction { get; private set; }

        public HistoryViewModel()
        {
            //Load all transactions from file
            Transactions = StorageManager.LoadTransactions();
        }

        public void SelectTransaction(Transaction transaction)
        {
            //Set selected transaction
            SelectedTransaction = transaction;
            OnPropertyChanged("SelectedTransaction");
        }
    }
}
