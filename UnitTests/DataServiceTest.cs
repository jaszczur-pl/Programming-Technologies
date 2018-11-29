using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zadanie1.Data;
using Zadanie1.MainLogic;

namespace UnitTests
{
    [TestClass]
    public class DataServiceTest
    {
        DataService service;
        DataRepository repository;
        DataContext context;
        DataFiller filler;

        [TestInitialize]
        public void InitializeTests() {
            filler = new FillingFromFile();
            context = new DataContext();
            repository = new DataRepository(filler, context);

            service = new DataService(repository);
        }
    }
}
