using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using ContactManager.Models.Validation;

namespace ContactManager.Models
{
    public class ContactManagerService : ContactManager.Models.IContactManagerService
    {
        private IValidationDictionary _validationDictionary;
        private IContactManagerRepository _repository;

        public ContactManagerService(IValidationDictionary validationDictionary): this(validationDictionary , new EntityContactManagerRepository())
        { }

        public ContactManagerService(IValidationDictionary validationDictionary, IContactManagerRepository repository)
        {
            _validationDictionary = validationDictionary;
            _repository = repository;
        }

        public bool ValidateContact(Contact contactToValidate)
        {
            if (contactToValidate.FirstName == null ||  contactToValidate.FirstName.Trim().Length == 0)
                _validationDictionary.AddError("FirstName", "First name is requiried.");
            if (contactToValidate.LastName == null || contactToValidate.LastName.Trim().Length == 0)
                _validationDictionary.AddError("LastName", "Last name is required.");
            if (contactToValidate.Phone.Length > 10)// && !Regex.IsMatch(contactToValidate.Phone, @"(\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"))
                _validationDictionary.AddError("Phone", "Invalid phone number.");
            if (contactToValidate.Email.Length > 0 && !Regex.IsMatch(contactToValidate.Email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
                _validationDictionary.AddError("Email", "Invalid email address.");
            return _validationDictionary.IsValid;
        }

        #region IContactManagerService Members

        public bool CreateContact(Contact contactToCreate)
        { 
            //Validation Logic
            if(!ValidateContact(contactToCreate))
                return false;

            //Database Logic
            try
            {
                _repository.CreateContact(contactToCreate);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool EditContact(Contact contactToEdit)
        { 
            //Validation Logic
            if (!ValidateContact(contactToEdit))
                return false;

            //Database Logic
            try
            {
                _repository.EditContact(contactToEdit);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool DeleteContact(Contact contactToDelete)
        {
            try
            {
                _repository.DeleteContact(contactToDelete);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public Contact GetContact(int id)
        {
            return _repository.GetContact(id);
        }

        public IEnumerable<Contact> ListContacts()
        {
            return _repository.ListContacts();
        }

        #endregion
    }
}