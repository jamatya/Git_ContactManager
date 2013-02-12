using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ContactManager.Models;
using ContactManager.Models.Validation;
using System.Web.Mvc;
using System.Data.Entity;

namespace ContactManager.Tests.Models
{
    /// <summary>
    /// Summary description for ContactManagerServiceTest
    /// </summary>
    [TestClass] 
    public class ContactManagerServiceTest
    {
        private Mock<IContactManagerRepository> _mockRepository;
        private ModelStateDictionary _modelState;
        private IContactManagerService _service;

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<IContactManagerRepository>();
            _modelState = new ModelStateDictionary();
            _service = new ContactManagerService(new ModelStateWrapper(_modelState));

        }
        
        [TestMethod]
        public void CreateContact()
        {
            //Arrange
            var contact = Contact.CreateContact(-1, "Test", "User", "44-444", "add@domain.com");

            //Act
            var result = _service.CreateContact(contact);

            //Asser
            Assert.IsTrue(result);
        }

        //[TestMethod]
        //public void CreateContactRequiredFirstName()
        //{ 
        //    //Arrange
        //    var contact = Contact.CreateContact(-1, string.Empty, "User", "222-5555", "add@domain.com");
        
        //    //Act
        //    var result = _service.CreateContact(contact);

        //    //Assert
        //    Assert.IsTrue(result);
        //    var error = _modelState["FirstName"].Errors[0];
        //    Assert.AreEqual("First name is required.", error.ErrorMessage);
        //}

        //[TestMethod]
        //public void CreateContactRequiredLastName()
        //{ 
        //    //Arrange
        //    var contact = Contact.CreateContact(-1, "Test", string.Empty, "444-4444", "add@domain.com"); 

        //    //Act
        //    var result = _service.CreateContact(contact);
            
        //    //Assert
        //    Assert.IsTrue(result);
        //    var error = _modelState["LastName"].Errors[0];
        //    Assert.AreEqual("Last name is required.", error.ErrorMessage);
        //}

        //[TestMethod]
        //public void CreateContactInvalidPhone()
        //{
        //    // Arrange
        //    var contact = Contact.CreateContact(-1, "Stephen", "Walther", "apple", "steve@somewhere.com");

        //    // Act
        //    var result = _service.CreateContact(contact);

        //    // Assert
        //    Assert.IsFalse(result);
        //    var error = _modelState["Phone"].Errors[0];
        //    Assert.AreEqual("Invalid phone number.", error.ErrorMessage);
        //}

        //[TestMethod]
        //public void CreateContactInvalidEmail()
        //{
        //    // Arrange
        //    var contact = Contact.CreateContact(-1, "Stephen", "Walther", "555-5555", "apple");

        //    // Act
        //    var result = _service.CreateContact(contact);

        //    // Assert
        //    Assert.IsFalse(result);
        //    var error = _modelState["Email"].Errors[0];
        //    Assert.AreEqual("Invalid email address.", error.ErrorMessage);
        //}




    }
}
