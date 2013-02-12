using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContactManager.Models;
using System.Text.RegularExpressions;
using ContactManager.Models.Validation;

namespace ContactManager.Controllers
{
    public class ContactController : Controller
    {
  
        //private ContactManagerEntities _entities = new ContactManagerEntities();

        //private IContactManagerRepository _repository;

        private IContactManagerService _service;

        public ContactController()
        {
            this._service = new ContactManagerService(new ModelStateWrapper(this.ModelState));
        }

        public ContactController(IContactManagerService service)
        {
            this._service = service;
        }
       
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View(_service.ListContacts()); // View(_entities.Contacts.ToList());
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
            //Insert Logic
            if(_service.CreateContact(contactToCreate))
                return RedirectToAction("Index");
            return View();
        }
        
        //
        // GET: /Home/Edit/5
 
        public ActionResult Edit(int id)
        {
           return View(_service.GetContact(id)); 
        }

        //
        // POST: /Home/Edit/5

        //[HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Contact contactToEdit)
        {
            if(_service.EditContact(contactToEdit))
                return RedirectToAction("Index");
            return View();
        }

        //
        // GET: /Home/Delete/5
 
        public ActionResult Delete(int id)
        {           
            return View(_service.GetContact(id)); 
        }

        //
        // POST: /Home/Delete/5

        //[HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(Contact contactToDelete)
        {
            if(_service.DeleteContact(contactToDelete))  
                return RedirectToAction("Index");
            return View();
        }
    }
}
