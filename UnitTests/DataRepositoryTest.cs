using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zadanie1.Data;
using Zadanie1.MainLogic;

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
        public void AddEventTestt() {
            int oldListsize = data.events.Count;


        }

    }
}
