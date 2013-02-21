using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ContactManager.Models;
using System.Web.Mvc;
using ContactManager.Models.Validation;

namespace ContactManager.Tests
{
    /// <summary>
    /// Summary description for ContactServiceTest
    /// </summary>
    [TestClass]
    public class ContactManagerServiceTest
    {
        private Mock<IContactManagerRepository> _mockRepository;
        private ModelStateDictionary _modelState;
        private IContactManagerService _service;

        public ContactManagerServiceTest()
        {
            _mockRepository = new Mock<IContactManagerRepository>();
            _modelState = new ModelStateDictionary();
            _service = new ContactManagerService(new ModelStateWrapper(_modelState), _mockRepository.Object);
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

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

        [TestMethod]
        public void CreateContact()
        {
            // Arrange
            var contact = Contact.CreateContact(-1, "Stephen", "Walther", "555-5555", "steve@somewhere.com");

            // Act
            var result = _service.CreateContact(contact);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CreateContactRequiredFirstName()
        {
            // Arrange
            var contact = Contact.CreateContact(-1, string.Empty, "Walther", "555-5555", "steve@somewhere.com");

            // Act
            var result = _service.CreateContact(contact);

            // Assert
            Assert.IsFalse(result);
            var error = _modelState["FirstName"].Errors[0];
            Assert.AreEqual("First name is required.", error.ErrorMessage);
        }


        [TestMethod]
        public void CreateContactRequiredLastName()
        {
            // Arrange
            var contact = Contact.CreateContact(-1, "Stephen", string.Empty, "555-5555", "steve@somewhere.com");

            // Act
            var result = _service.CreateContact(contact);

            // Assert
            Assert.IsFalse(result);
            var error = _modelState["LastName"].Errors[0];
            Assert.AreEqual("Last name is required.", error.ErrorMessage);
        }

        [TestMethod]
        public void CreateContactInvalidPhone()
        {
            // Arrange
            var contact = Contact.CreateContact(-1, "Stephen", "Walther", "apple", "steve@somewhere.com");

            // Act
            var result = _service.CreateContact(contact);

            // Assert
            Assert.IsFalse(result);
            var error = _modelState["Phone"].Errors[0];
            Assert.AreEqual("Invalid phone number.", error.ErrorMessage);
        }


        [TestMethod]
        public void CreateContactInvalidEmail()
        {
            // Arrange
            var contact = Contact.CreateContact(-1, "Stephen", "Walther", "555-5555", "apple");

            // Act
            var result = _service.CreateContact(contact);

            // Assert
            Assert.IsFalse(result);
            var error = _modelState["Email"].Errors[0];
            Assert.AreEqual("Invalid email address.", error.ErrorMessage);
        }

    }
}
