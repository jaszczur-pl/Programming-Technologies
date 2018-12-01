using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zadanie1.Data;
using Zadanie1.MainLogic;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class DataServiceTest {
        DataService service;
        DataRepository dataRepository;
        DataContext data;
        DataFiller filler;

        [TestInitialize]
        public void InitializeTests() {
            data = new DataContext();
            filler = new FillingFromFile();
            dataRepository = new DataRepository(filler, data);

            service = new DataService(dataRepository);
        }

        [TestMethod]
        public void AddCustomerPositiveTest() {
            int oldListSize = data.customers.Count;

            Customer customer = new Customer() {
                name = "Kamil",
                surname = "Filipczak",
                emailAddress = "kf@gmail.com",
                age = 51
            };

            service.AddCustomer(customer);
            int newListSize = data.customers.Count;

            //check if size of new and old list is different
            Assert.AreNotEqual(oldListSize, newListSize);

            //check if list contains object
            Assert.IsTrue(data.customers.Contains(customer));
        }

        [TestMethod]
        public void AddCustomerNegativeTest() {
            int oldListSize = data.customers.Count;

            Customer customer = dataRepository.GetAllCustomers().ElementAt(0);

            service.AddCustomer(customer);
            int newListSize = data.customers.Count;

            //check if size of new and old list is different
            Assert.AreEqual(oldListSize, newListSize);
        }

        [TestMethod]
        public void AddCDPositiveTest() {
            int oldDictSize = data.cds.Count;

            CD cd = new CD() {
                id = 1234,
                title = "Nevermind",
                group = "Nirvana"
            };

            service.AddCD(cd);
            int newDictSize = data.cds.Count;

            //check if size of new and old dictionary is different
            Assert.AreNotEqual(oldDictSize, newDictSize);

            //check if dictionary contains object
            Assert.IsTrue(data.cds.ContainsKey(1234));
            Assert.IsTrue(data.cds.ContainsValue(cd));
        }

        [TestMethod]
        public void AddCDNegativeTest() {
            int oldDictSize = data.cds.Count;

            CD cd = new CD() {
                id = 1526,
                title = "Nevermind",
                group = "Nirvana"
            };

            service.AddCD(cd);
            int newDictSize = data.cds.Count;

            //check if size of new and old dictionary is the same
            Assert.AreEqual(oldDictSize, newDictSize);

            //check if dictionary data is not changed
            Assert.AreNotEqual(data.cds[1526].group, cd.group);
            Assert.AreNotEqual(data.cds[1526].title, cd.title);
        }

        [TestMethod]
        public void AddEventPositiveTest() {
            int oldListSize = data.events.Count;

            CDState cdState = dataRepository.GetAllCDStates().ElementAt(0);
            Customer customer = dataRepository.GetAllCustomers().ElementAt(1);

            service.AddEvent(customer, cdState);
            int newListSize = data.events.Count;

            //check if size of new and old collection is different
            Assert.AreNotEqual(oldListSize, newListSize);

            //check if collection contains added event
            Assert.IsTrue(data.events.Any( x => x.customer.Equals(customer) && x.cdState.Equals(cdState)));
        }

        [TestMethod]
        public void AddEventNegativeTest() {
            int oldListSize = data.events.Count;

            CDState cdState = dataRepository.GetAllCDStates().ElementAt(0);
            Customer customer = dataRepository.GetAllCustomers().ElementAt(0);

            service.AddEvent(customer, cdState);
            int newListSize = data.events.Count;

            //check if size of new and old collection is different
            Assert.AreEqual(oldListSize, newListSize);
        }

        [TestMethod]
        public void AddCDStatePositiveTest() {
            int oldListSize = data.cdStates.Count;

            CD cd = new CD() {
                id = 5463,
                title = "Nevermind",
                group = "Nirvana"
            };

            service.AddCDState(cd);
            int newListSize = data.cdStates.Count;

            //check if size of new and old collection is different
            Assert.AreNotEqual(oldListSize, newListSize);

            //check if collection contains added event
            Assert.IsTrue(data.cdStates.Any(x => x.cd.Equals(cd)));
        }

        [TestMethod]
        public void AddCDStateNegativeTest() {
            int oldListSize = data.cdStates.Count;

            CD cd = new CD() {
                id = 1234,
                title = "Nevermind",
                group = "Nirvana"
            };

            service.AddCDState(cd);
            int newListSize = data.cdStates.Count;

            //check if size of new and old collection is different
            Assert.AreEqual(oldListSize, newListSize);

            //check if collection contains added event
            Assert.IsFalse(data.cdStates.Any(x => x.cd.Equals(cd)));
        }

        [TestMethod]
        public void UpdateCustomerPositiveTest() {

            Customer updatedCustomer = new Customer() {
                name = "Kamil",
                surname = "Filipczak",
                emailAddress = "kf@gmail.com",
                age = 51
            };

            int customersCounter = data.customers.Count;
            int randomIndex = new Random().Next(0, customersCounter - 1);

            service.UpdateCustomer(randomIndex, updatedCustomer);

            //check if number of customers is not changed
            Assert.AreEqual(customersCounter, data.customers.Count);

            //check if properties are the same
            Assert.AreEqual(updatedCustomer.name, data.customers[randomIndex].name);
            Assert.AreEqual(updatedCustomer.surname, data.customers[randomIndex].surname);
            Assert.AreEqual(updatedCustomer.emailAddress, data.customers[randomIndex].emailAddress);
            Assert.AreEqual(updatedCustomer.age, data.customers[randomIndex].age);
        }

        [TestMethod]
        public void UpdateCDNegativeTest() {

            //clear dictionary
            data.cds.Clear();

            CD cd = new CD() {
                id = 1526,
                title = "Nevermind",
                group = "Nirvana"
            };

            //try to update empty dictionary
            service.UpdateCD(cd.id, cd);

            //check if dictionary is still empty
            Assert.AreEqual(0, data.cds.Count);
        }

        [TestMethod]
        public void UpdateEventPositiveTest() {

            int oldCollectionSize = data.events.Count;
            int randomIndex = new Random().Next(0, oldCollectionSize - 1);

            CD cd = new CD() {
                id = 1234,
                title = "Nevermind",
                group = "Nirvana"
            };

            CDState cdState = new CDState() {
                cd = cd,
                dateOfPurchase = new DateTimeOffset(new DateTime(2013, 11, 03))
            };

            Customer customer = new Customer() {
                name = "Kamil",
                surname = "Filipczak",
                emailAddress = "kf@gmail.com",
                age = 51
            };

            Event evt = new Event() {
                cdState = cdState,
                customer = customer
            };

            service.UpdateEvent(randomIndex, evt);
            int newCollectionSize = data.events.Count;

            //check if number of events is not changed
            Assert.AreEqual(oldCollectionSize, newCollectionSize);

            //check if properties are the same
            Assert.AreEqual(evt.cdState, data.events[randomIndex].cdState);
            Assert.AreEqual(evt.customer, data.events[randomIndex].customer);
        }

        [TestMethod]
        public void UpdateEventNegativeTest() {

            //clear collection
            data.events.Clear();

            CD cd = new CD() {
                id = 1234,
                title = "Nevermind",
                group = "Nirvana"
            };

            CDState cdState = new CDState() {
                cd = cd,
                dateOfPurchase = new DateTimeOffset(new DateTime(2013, 11, 03))
            };

            Customer customer = new Customer() {
                name = "Kamil",
                surname = "Filipczak",
                emailAddress = "kf@gmail.com",
                age = 51
            };

            Event evt = new Event() {
                cdState = cdState,
                customer = customer
            };

            //try to update empty collection
            service.UpdateEvent(0, evt);

            //check if collection is still empty
            Assert.AreEqual(0, data.events.Count);
        }

        [TestMethod]
        public void UpdateCDStatePositiveTest() {

            int oldListSize = data.cdStates.Count;
            int randomIndex = new Random().Next(0, oldListSize - 1);

            CD cd = new CD() {
                id = 1234,
                title = "Nevermind",
                group = "Nirvana"
            };

            CDState cdState = new CDState() {
                cd = cd,
                dateOfPurchase = new DateTimeOffset(new DateTime(2013, 11, 03))
            };

            service.UpdateCDState(randomIndex, cdState);
            int newListSize = data.cdStates.Count;

            //check if size of new and old list is the same
            Assert.AreEqual(oldListSize, newListSize);

            //compare properties
            Assert.AreEqual(cdState.cd, data.cdStates[randomIndex].cd);
            Assert.AreEqual(cdState.dateOfPurchase, data.cdStates[randomIndex].dateOfPurchase);
        }

        [TestMethod]
        public void UpdateCDStateNegativeTest() {

            //clear list
            data.cdStates.Clear();

            CD cd = new CD() {
                id = 1234,
                title = "Nevermind",
                group = "Nirvana"
            };

            CDState cdState = new CDState() {
                cd = cd,
                dateOfPurchase = new DateTimeOffset(new DateTime(2013, 11, 03))
            };

            //try to update empty list
            service.UpdateCDState(0, cdState);

            //check if size of new and old list is the same
            Assert.AreEqual(0, data.cdStates.Count);
        }

        [TestMethod]
        public void DeleteCustomerPositiveTest() {

            int oldCollectionSize = data.customers.Count;
            int randomIndex = new Random().Next(0, oldCollectionSize - 1);

            Customer removedCustomer = data.customers[randomIndex];

            service.DeleteCustomer(randomIndex);

            //check if list size is decreased
            Assert.AreNotEqual(oldCollectionSize, data.customers.Count);

            //check if object is removed from collection
            Assert.IsFalse(data.customers.Contains(removedCustomer));
        }

        [TestMethod]
        public void DeleteCustomerNegativeTest() {

            int oldCollectionSize = data.customers.Count;

            //try to delete customer (index is out of range)
            service.DeleteCustomer(oldCollectionSize);

            //check if list size is not changed
            Assert.AreEqual(oldCollectionSize, data.customers.Count);
        }

        [TestMethod]
        public void DeleteCDPositiveTest() {

            int oldCollectionSize = data.cds.Count;
            int constKey = 1526;

            CD removedCD = data.cds[constKey];

            service.DeleteCD(constKey);

            //check if list size is decreased
            Assert.AreNotEqual(oldCollectionSize, data.cds.Count);

            //check if object is removed from collection
            Assert.IsFalse(data.cds.ContainsValue(removedCD));
            Assert.IsFalse(data.cds.ContainsKey(removedCD.id));
        }

        [TestMethod]
        public void DeleteCDNegativeTest() {

            int oldCollectionSize = data.cds.Count;
            int constKey = -1;

            //try to delete object with incorrect key
            service.DeleteCD(constKey);

            //check if list size is decreased
            Assert.AreEqual(oldCollectionSize, data.cds.Count);
        }

        [TestMethod]
        public void DeleteEventPositiveTest() {

            int oldCollectionSize = data.events.Count;
            int randomIndex = new Random().Next(0, oldCollectionSize - 1);

            Event removedEvent = data.events[randomIndex];

            service.DeleteEvent(randomIndex);

            //check if list size is decreased
            Assert.AreNotEqual(oldCollectionSize, data.events.Count);

            //check if object is removed from collection
            Assert.IsFalse(data.events.Contains(removedEvent));
        }

        [TestMethod]
        public void DeleteEventNegativeTest() {

            int oldCollectionSize = data.events.Count;

            //try to delete customer (index is out of range)
            service.DeleteEvent(oldCollectionSize);

            //check if list size is not changed
            Assert.AreEqual(oldCollectionSize, data.events.Count);
        }

        [TestMethod]
        public void DeleteCDStatePositiveTest() {

            int oldCollectionSize = data.cdStates.Count;
            int randomIndex = new Random().Next(0, oldCollectionSize - 1);

            CDState removedCDState = data.cdStates[randomIndex];

            service.DeleteCDState(randomIndex);

            //check if list size is decreased
            Assert.AreNotEqual(oldCollectionSize, data.cdStates.Count);

            //check if object is removed from collection
            Assert.IsFalse(data.cdStates.Contains(removedCDState));
        }

        [TestMethod]
        public void DeleteCDStateNegativeTest() {

            int oldCollectionSize = data.cdStates.Count;

            //try to delete customer (index is out of range)
            service.DeleteCDState(oldCollectionSize);

            //check if list size is not changed
            Assert.AreEqual(oldCollectionSize, data.cdStates.Count);
        }
    }
}
