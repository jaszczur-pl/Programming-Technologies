using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zadanie1.Data;
using Zadanie1.MainLogic;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class DataRepositoryTest
    {
        DataContext data;
        DataFiller filler;
        DataRepository dataRepository;

        [TestInitialize]
        public void InitializeTests() {
            data = new DataContext();
            filler = new ConstantFiller();
            dataRepository = new DataRepository(filler, data);
        }

        //'Add' methods tests

        [TestMethod]
        public void AddCustomerTest() {
            int oldListSize = data.customers.Count;

            Customer customer = new Customer() {
                name = "Kamil",
                surname = "Filipczak",
                emailAddress = "kf@gmail.com",
                age = 51
            };

            dataRepository.AddCustomer(customer);
            int newListSize = data.customers.Count;

            //check if size of new and old list is different
            Assert.AreNotEqual(oldListSize, newListSize);

            //check if list contains object
            Assert.IsTrue(data.customers.Contains(customer));
        }

        [TestMethod]
        public void AddCDTest() {
            int oldDictSize = data.cds.Count;

            CD cd = new CD() {
                id = 1234,
                title = "Nevermind",
                group = "Nirvana"
            };

            dataRepository.AddCD(cd);
            int newDictSize = data.cds.Count;

            //check if size of new and old dictionary is different
            Assert.AreNotEqual(oldDictSize, newDictSize);

            //check if dictionary contains object
            Assert.IsTrue(data.cds.ContainsKey(1234));
            Assert.IsTrue(data.cds.ContainsValue(cd));
        }

        [TestMethod]
        public void AddEventTest() {
            int oldListSize = data.events.Count;

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

            dataRepository.AddEvent(evt);
            int newListSize = data.events.Count;

            //check if size of new and old collection is different
            Assert.AreNotEqual(oldListSize, newListSize);

            //check if collection contains added event
            Assert.IsTrue(data.events.Contains(evt));
        }

        [TestMethod]
        public void AddCDStateTest() {
            int oldListSize = data.cdStates.Count;

            CD cd = new CD() {
                id = 1234,
                title = "Nevermind",
                group = "Nirvana"
            };

            CDState cdState = new CDState() {
                cd = cd,
                dateOfPurchase = new DateTimeOffset(new DateTime(2013, 11, 03))
            };

            dataRepository.AddCDState(cdState);
            int newListSize = data.cdStates.Count;

            //check if size of new and old collection is different
            Assert.AreNotEqual(oldListSize, newListSize);

            //check if collection contains added event
            Assert.IsTrue(data.cdStates.Contains(cdState));
        }

        //'Get' methods tests

        [TestMethod]
        public void GetCustomerTest() {
            int randomIndex = new Random().Next(0, data.customers.Count - 1);
            int outOfRangeIndex = data.customers.Count;

            //check if method returns matched object for proper index
            Assert.AreEqual(data.customers[randomIndex], dataRepository.GetCustomer(randomIndex));

            //check if method returns null 
            Assert.AreEqual(null, dataRepository.GetCustomer(outOfRangeIndex));
        }

        [TestMethod]
        public void GetCDTest() {

            Dictionary<int,CD>.KeyCollection keysList = data.cds.Keys;

            //check if method returns matched object for proper index
            foreach (int key in keysList) {
                Assert.AreEqual(data.cds[key], dataRepository.GetCD(key));
            }

            //check if method returns null 
            Assert.AreEqual(null, dataRepository.GetCD(-1));
        }

        [TestMethod]
        public void GetEventTest() {
            int randomIndex = new Random().Next(0, data.events.Count - 1);
            int outOfRangeIndex = data.events.Count;

            //check if method returns matched object for proper index
            Assert.AreEqual(data.events[randomIndex], dataRepository.GetEvent(randomIndex));

            //check if method returns null 
            Assert.AreEqual(null, dataRepository.GetEvent(outOfRangeIndex));
        }

        [TestMethod]
        public void GetCDStateTest() {
            int randomIndex = new Random().Next(0, data.cdStates.Count - 1);
            int outOfRangeIndex = data.cdStates.Count;

            //check if method returns matched object for proper index
            Assert.AreEqual(data.cdStates[randomIndex], dataRepository.GetCDState(randomIndex));

            //check if method returns null 
            Assert.AreEqual(null, dataRepository.GetCDState(outOfRangeIndex));
        }

        //'GetAll' methods tests

        [TestMethod]
        public void GetAllCustomersTest() {

            //check if collections contents are matched
            Assert.AreEqual(data.customers, dataRepository.GetAllCustomers());
        }

        [TestMethod]
        public void GetAllCDsTest() {
            IEnumerable<CD> listOfCDs = dataRepository.GetAllCDs();

            //check if collections contents are matched
            foreach (CD cd in listOfCDs) {
                Assert.IsTrue(data.cds.Values.Contains(cd));
            }
        }

        [TestMethod]
        public void GetAllEventsTest() {

            //check if collections contents are matched
            Assert.AreEqual(data.events, dataRepository.GetAllEvents());
        }

        [TestMethod]
        public void GetAllCDStatesTest() {

            //check if collections contents are matched
            Assert.AreEqual(data.cdStates, dataRepository.GetAllCDStates());
        }

        //'Update' methods tests

        [TestMethod]
        public void UpdateCustomerTest() {

            Customer updatedCustomer = new Customer() {
                name = "Kamil",
                surname = "Filipczak",
                emailAddress = "kf@gmail.com",
                age = 51
            };

            int customersCounter = data.customers.Count;
            int randomIndex = new Random().Next(0, customersCounter - 1);

            dataRepository.UpdateCustomer(randomIndex, updatedCustomer);

            //check if number of customers is not changed
            Assert.AreEqual(customersCounter, data.customers.Count);

            //check if properties are the same
            Assert.AreEqual(updatedCustomer.name, data.customers[randomIndex].name);
            Assert.AreEqual(updatedCustomer.surname, data.customers[randomIndex].surname);
            Assert.AreEqual(updatedCustomer.emailAddress, data.customers[randomIndex].emailAddress);
            Assert.AreEqual(updatedCustomer.age, data.customers[randomIndex].age);
        }

        [TestMethod]
        public void UpdateCDTest() {

            CD cd = new CD() {
                id = 1526,
                title = "Nevermind",
                group = "Nirvana"
            };

            int oldDictSize = data.cds.Count;

            dataRepository.UpdateCD(cd.id, cd);

            //check if number of cds is not changed
            Assert.AreEqual(oldDictSize, data.cds.Count);

            //check if properties are the same
            Assert.AreEqual(cd.id, data.cds[cd.id].id);
            Assert.AreEqual(cd.title, data.cds[cd.id].title);
            Assert.AreEqual(cd.group, data.cds[cd.id].group);
        }

        [TestMethod]
        public void UpdateEventTest() {

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

            dataRepository.UpdateEvent(randomIndex, evt);
            int newCollectionSize = data.events.Count;

            //check if number of events is not changed
            Assert.AreEqual(oldCollectionSize, newCollectionSize);

            //check if properties are the same
            Assert.AreEqual(evt.cdState, data.events[randomIndex].cdState);
            Assert.AreEqual(evt.customer, data.events[randomIndex].customer);
        }

        [TestMethod]
        public void UpdateCDStateTest() {

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

            dataRepository.UpdateCDState(randomIndex, cdState);
            int newListSize = data.cdStates.Count;

            //check if size of new and old list is the same
            Assert.AreEqual(oldListSize, newListSize);

            //compare properties
            Assert.AreEqual(cdState.cd, data.cdStates[randomIndex].cd);
            Assert.AreEqual(cdState.dateOfPurchase, data.cdStates[randomIndex].dateOfPurchase);
        }


        [TestMethod]
        public void DeleteCustomerTest() {

            int oldCollectionSize = data.customers.Count;
            int randomIndex = new Random().Next(0, oldCollectionSize - 1);

            Customer removedCustomer = data.customers[randomIndex];

            dataRepository.DeleteCustomer(randomIndex);

            //check if list size is decreased
            Assert.AreNotEqual(oldCollectionSize, data.customers.Count);

            //check if object is removed from collection
            Assert.IsFalse(data.customers.Contains(removedCustomer));
        }

        [TestMethod]
        public void DeleteCDTest() {

            int oldCollectionSize = data.cds.Count;
            int constKey = 1526;

            CD removedCD = data.cds[constKey];

            dataRepository.DeleteCD(constKey);

            //check if list size is decreased
            Assert.AreNotEqual(oldCollectionSize, data.cds.Count);

            //check if object is removed from collection
            Assert.IsFalse(data.cds.ContainsValue(removedCD));
            Assert.IsFalse(data.cds.ContainsKey(removedCD.id));
        }

        [TestMethod]
        public void DeleteEventTest() {

            int oldCollectionSize = data.events.Count;
            int randomIndex = new Random().Next(0, oldCollectionSize - 1);

            Event removedEvent = data.events[randomIndex];

            dataRepository.DeleteEvent(randomIndex);

            //check if list size is decreased
            Assert.AreNotEqual(oldCollectionSize, data.events.Count);

            //check if object is removed from collection
            Assert.IsFalse(data.events.Contains(removedEvent));
        }

        [TestMethod]
        public void DeleteCDStateTest() {

            int oldCollectionSize = data.cdStates.Count;
            int randomIndex = new Random().Next(0, oldCollectionSize - 1);

            CDState removedCDState = data.cdStates[randomIndex];

            dataRepository.DeleteCDState(randomIndex);

            //check if list size is decreased
            Assert.AreNotEqual(oldCollectionSize, data.cdStates.Count);

            //check if object is removed from collection
            Assert.IsFalse(data.cdStates.Contains(removedCDState));
        }


    }
}
