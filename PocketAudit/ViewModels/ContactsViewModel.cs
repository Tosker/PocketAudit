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
    public class ContactsViewModel : ObservableObject
    {
        //List of all merchandise suppliers
        public ObservableCollection<Supplier> Contacts { get; set; }

        //The selected contact for viewing
        private Supplier _viewedContact { get; set; }
        //The model of the selected contact for viewing(For editing purposes)
        private Supplier _contactModel { get; set; }

        //All commands for updating, adding and removing contacts
        private ICommand _viewContact { get; set; }
        private ICommand _selectContact { get; set; }
        private ICommand _updateContact { get; set; }
        private ICommand _clearContact { get; set; }
        private ICommand _addContact { get; set; }
        private ICommand _removeContact { get; set; }

        public Supplier ViewedContact
        {
            get
            {
                return _viewedContact;
            }
            set
            {
                _viewedContact = value;
                OnPropertyChanged("ViewedContact");
            }
        }

        public Supplier ContactModel
        {
            get
            {
                return _contactModel;
            }
            set
            {
                _contactModel = value;
                OnPropertyChanged("ContactModel");
            }
        }

        public ICommand ViewContactCommand
        {
            get
            {
                return _viewContact;
            }
        }

        public ICommand SelectContactCommand
        {
            get
            {
                return _selectContact;
            }
        }

        public ICommand UpdateContactCommand
        {
            get
            {
                return _updateContact;
            }
        }

        public ICommand ClearContactCommand
        {
            get
            {
                return _clearContact;
            }
        }

        public ICommand AddContactCommand
        {
            get
            {
                return _addContact;
            }
        }

        public ICommand RemoveContactCommand
        {
            get
            {
                return _removeContact;
            }
        }

        public ContactsViewModel()
        {
            //Load all contacts to collection
            Contacts = StorageManager.LoadContacts();
            //Create a default empty model
            ContactModel = new Supplier();

            //Assign commands to functions
            _viewContact = new RelayCommand(ViewContact);
            _selectContact = new RelayCommand(SelectContact);
            _updateContact = new RelayCommand(UpdateContact);
            _clearContact = new RelayCommand(ClearContact);
            _addContact = new RelayCommand(AddContact);
            _removeContact = new RelayCommand(RemoveContact);
        }

        //Set the contact to be viewed
        public void ViewContact(object supplier)
        {
            var contact = supplier as Supplier;
            ViewedContact = contact;
        }

        //Select the existing contact to be edited/updated
        public void SelectContact(object supplier)
        {
            var contact = supplier as Supplier;
            ContactModel = contact;
        }

        //Add a new contact to collection
        public void AddContact()
        {
            //Assign a new unique ID to the contact
            AssignId(ContactModel);

            //Add contact to the collection
            Contacts.Add(ContactModel);
            //Save the new contact to file
            StorageManager.SaveNewContact(Contacts);
            ClearContact();
        }

        //Update existing contact
        public void UpdateContact()
        {
            //Retrieve contact with the same ID of the model we modified
            var contact = Contacts.FirstOrDefault(param => param.Id == ContactModel.Id);
            //Set the contact to the model we created
            contact = ContactModel;
            //Save the updated contact collection to file
            StorageManager.SaveNewContact(Contacts);
            ClearContact();
        }

        //Remove contact from collection
        public void RemoveContact(object contact)
        {
            var oldContact = contact as Supplier;
            Contacts.Remove(oldContact);
            //Save the contact collection with removed contact to file
            StorageManager.SaveNewContact(Contacts);
        }

        //Clear for fresh contact model
        public void ClearContact()
        {
            ContactModel = new Supplier();
            OnPropertyChanged("ContactModel");
        }

        private void AssignId(Supplier contact)
        {
            //Default ID if this is the first and only contact
            contact.Id = 0;
            if (Contacts.Count > 0)
            {
                //To prevent repeats, find the biggest id based on value and increment from that
                contact.Id = Contacts.Max(i => i.Id);
                contact.Id++;
            }
        }
    }
}
