using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContactManager.Models;
using System.Text.RegularExpressions;

namespace ContactManager.Controllers
{
    public class ContactController : Controller
    {
  
        //private ContactManagerEntities _entities = new ContactManagerEntities();

        private IContactManagerRepository _repository;
        
        public ContactController() : this(new EntityContactManagerRepository())
        {}

        public ContactController(IContactManagerRepository repository)
        {
            this._repository = repository;
        }
       
        protected void ValidateContact(Contact contactToValidate)
        {
            if (contactToValidate.FirstName == null || contactToValidate.FirstName.Trim().Length == 0)
                ModelState.AddModelError("FirstName", "First name is required.");
            if (contactToValidate.LastName == null || contactToValidate.LastName.Trim().Length == 0)
                ModelState.AddModelError("LastName", "Last name is required.");
            if (contactToValidate.Phone.Length > 0 && !Regex.IsMatch(contactToValidate.Phone, @"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"))
                ModelState.AddModelError("Phone", "Invalid phone number.");
            if (contactToValidate.Email.Length > 0 && !Regex.IsMatch(contactToValidate.Email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
                ModelState.AddModelError("Email", "Invalid email address.");
        }

        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View(_repository.ListContacts()); // View(_entities.Contacts.ToList());
        }

        //
        // GET: /Home/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Home/Create
        //Returns a view tha contains the HTML form for creating a new contact
        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Home/Create
        //This method will be invoked when posting the HTML form for creating a new contact
        //[HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([Bind(Exclude="Id")] Contact contactToCreate)
        {
            // Validation logic
            ValidateContact(contactToCreate);
  
            if (!ModelState.IsValid)
                return View();

            //Insert Logic
            try
            {
                _repository.CreateContact(contactToCreate);
                //_entities.AddToContacts(contactToCreate);
                //_entities.SaveChanges();
                return RedirectToAction("Index");                
            }
            catch
            {
                return View();
            }           
        }
        
        //
        // GET: /Home/Edit/5
 
        public ActionResult Edit(int id)
        {
            //var contactToEdit = (from c in _entities.Contacts
            //                     where c.Id == id
            //                     select c).FirstOrDefault();

            return View(_repository.GetContact(id)); // View(contactToEdit);
        }

        //
        // POST: /Home/Edit/5

        //[HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Contact contactToEdit)
        {
            try
            {
                // Validation logic
                ValidateContact(contactToEdit);

                if (!ModelState.IsValid)
                    return View();

                // Update Logic
                //var originalContact = (from c in _entities.Contacts
                //                       where c.Id == contactToEdit.Id
                //                       select c).FirstOrDefault();

                //_entities.ApplyPropertyChanges(originalContact.EntityKey.EntitySetName, contactToEdit);
                //_entities.SaveChanges();

                _repository.EditContact(contactToEdit);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Home/Delete/5
 
        public ActionResult Delete(int id)
        {
            //var contactToDelete = (from c in _entities.Contacts
            //                       where c.Id == id
            //                       select c).FirstOrDefault();
            return View(_repository.GetContact(id)); // View(contactToDelete);
        }

        //
        // POST: /Home/Delete/5

        //[HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(Contact contactToDelete)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();

                //Delete Logic
                //var originalContact = (from c in _entities.Contacts
                //                       where c.Id == contactToDelete.Id
                //                       select c).FirstOrDefault();

                //_entities.DeleteObject(originalContact);
                //_entities.SaveChanges();

                _repository.DeleteContact(contactToDelete);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
