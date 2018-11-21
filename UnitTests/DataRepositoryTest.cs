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

            data.customers.Add(customer);
            int newListSize = data.customers.Count;

            //check if size of new and old list is different
            Assert.AreNotEqual(oldListSize, newListSize);

            //check if list contains object
            Assert.IsTrue(data.customers.Contains(customer));
        }

    }
}
