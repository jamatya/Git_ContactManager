﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactManager.Models
{
    public class EntityContactManagerRepository : ContactManager.Models.IContactManagerRepository
    {
        private ContactManagerEntities _entities = new ContactManagerEntities();

        public Contact GetContact(int id)
        {
            return (from c in _entities.Contacts
                           where c.Id == id
                           select c).FirstOrDefault();
        }

        public IEnumerable<Contact> ListContacts()
        {
            return _entities.Contacts.ToList();
        }

        public Contact CreateContact(Contact contactToCreate)
        {
            _entities.AddToContacts(contactToCreate);
            _entities.SaveChanges();
            return contactToCreate;        
        }

        public Contact EditContact(Contact contactToEdit)
        {
            var originalContact = GetContact(contactToEdit.Id);
            _entities.ApplyPropertyChanges(originalContact.EntityKey.EntitySetName, contactToEdit);
            _entities.SaveChanges();
            return contactToEdit;
        }

        public void DeleteContact(Contact contactToDelete)
        {
            var originalContact = GetContact(contactToDelete.Id);
            _entities.DeleteObject(originalContact);
            _entities.SaveChanges();
        }
    }
}