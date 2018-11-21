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
        public void AddCDPositiveTest() {
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
        public void AddCDNegativeTest() {
            int oldDictSize = data.cds.Count;

            CD cd = new CD() {
                id = 1526,
                title = "Nevermind",
                group = "Nirvana"
            };

            dataRepository.AddCD(cd);
            int newDictSize = data.cds.Count;

            //check if size of new and old dictionary is the same
            Assert.AreEqual(oldDictSize, newDictSize);

            //check if dictionary data is not changed
            Assert.AreNotEqual(data.cds[1526].group, cd.group);
            Assert.AreNotEqual(data.cds[1526].title, cd.title);
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
    }
}
